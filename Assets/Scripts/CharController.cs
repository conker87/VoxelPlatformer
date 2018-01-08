using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 4f, jumpHeight = 8f;
	[SerializeField]
	bool bCanMove = true, bIsOnGround;

	[SerializeField]
	float baseButtStompForce = 10f;
	bool bHasButtStomped = false;

	float fPreJumpYLevel;

	[SerializeField]
	LayerMask lmGeometryLayerMask;

	Rigidbody rb;
	Player p;

	Vector3 v3Forward, v3Right;

	float fRaycastSkin = 0.1f;
	float fCapsuleColliderYBounds;

	void Start() {
		
		ResetForwardDirection ();

		rb = GetComponent<Rigidbody> ();
		p = GetComponent<Player> ();

		fCapsuleColliderYBounds = GetComponent<CapsuleCollider>().bounds.extents.y;

	}

	void Update() {

		IsGrounded ();

		if (Input.GetButtonDown ("Jump")) {

			Jump ();

		}

		if (Input.GetButtonDown ("ButtStomp") && !bHasButtStomped) {

			if (!bIsOnGround) {
				ButtStomp ();
			}

		}

	}

	void FixedUpdate() {

		rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);

		if (p.IsDead) {

			return;

		}

		if (!bCanMove) {

			return;

		}

		Move ();

	}

	void Move() {
		
		Vector3 rightMovement = v3Right * moveSpeed * Time.deltaTime * Input.GetAxis ("HorizontalKey"); 
		Vector3 upMovement = v3Forward * moveSpeed * Time.deltaTime * Input.GetAxis ("VerticalKey");
		Vector3 heading = Vector3.Normalize (rightMovement + upMovement);

		if (heading.magnitude > 0.1f) {
			
			transform.forward = heading;

			transform.position += rightMovement;
			transform.position += upMovement;

		}
	}

	void Jump() {

		if (!bIsOnGround) {



			return;

		} else {

			fPreJumpYLevel = transform.position.y;

		}

		rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);
		rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);

	}

	void ButtStomp() {

		float fCurrentYLevel = transform.position.y;
		float fButtStompForce = baseButtStompForce + (1.5f * (fCurrentYLevel - fPreJumpYLevel));

		Debug.Log (string.Format("buttonStompForce: {0}, original/currentY: {1}/{2}", fButtStompForce, fPreJumpYLevel, fCurrentYLevel));

		rb.velocity = Vector3.zero;
		bHasButtStomped = true;
		rb.AddForce (-Vector3.up * fButtStompForce, ForceMode.VelocityChange);

	}

	public void ResetForwardDirection() {

		v3Forward = Camera.main.transform.forward; 
		v3Forward.y = 0; 
		v3Forward = Vector3.Normalize(v3Forward); 
		v3Right = Quaternion.Euler(new Vector3(0, 90, 0)) * v3Forward; 

	}

	void IsGrounded() {
		
		if (Physics.Raycast (transform.position, -Vector3.up, fCapsuleColliderYBounds + fRaycastSkin, lmGeometryLayerMask)) {

			bHasButtStomped = false;
			bIsOnGround = true;

		} else {

			bIsOnGround = false; 

		}
			
	}

	void OnDrawGizmos() {

		Debug.DrawLine (transform.position, new Vector3 (transform.position.x, transform.position.y - (fCapsuleColliderYBounds + fRaycastSkin), transform.position.z), Color.green);
		// Gizmos.DrawWireSphere (new Vector3(transform.position.x, transform.position.y - fSphereColliderOffset, transform.position.z), 1f);

	}

	/* void OnCollisionEnter(Collision other) {

		foreach (ContactPoint cp in other.contacts) {
			
			// Debug.Log (cp.point);
			//cp.otherCollider.

		}


		Debug.Log ("Entered Collision");
		bIsOnGround = true;

	}

	//consider when character is jumping .. it will exit collision.
	void OnCollisionExit(Collision other) {

		bIsOnGround = false;

	} */

}
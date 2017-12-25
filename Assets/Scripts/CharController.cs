using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 4f;
	public Transform playerGFX, ground;

	[SerializeField]
	float jumpHeight = 8f;
	[SerializeField]
	int jumpIndex = 0, maxDoubleJumps = 1;
	Rigidbody rb;

	[SerializeField]
	PlayerForceMode playerForceMode;

	Vector3 forward, right;

	void Start() {
		
		ResetForwardDirection ();

		rb = GetComponent<Rigidbody> ();

	}

	void FixedUpdate() {

		if (IsOnGround () && jumpIndex != 0) {

			jumpIndex = 0;

		}

		if (Input.GetButtonDown ("Jump")) {

			Jump ();

		} else {
			
			Move ();

		}

		rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);

	}

	void Move() {
		
		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis ("HorizontalKey"); 
		Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis ("VerticalKey");
		Vector3 heading = Vector3.Normalize (rightMovement + upMovement);

		if (heading.magnitude > 0.1f) {
			
			transform.forward = heading;

			transform.position += rightMovement;
			transform.position += upMovement;

		}
	}

	void Jump() {

		if (jumpIndex >= maxDoubleJumps) {

			return;

		}

		rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);
		rb.AddForce (Vector3.up * jumpHeight, SetPlayerForceMode(playerForceMode));//ForceMode.Impulse);
		jumpIndex++;

	}

	bool IsOnGround() {

		return ground.GetComponent<OnGround> ().IsOnGround;

	}

	public void ResetForwardDirection() {

		forward = Camera.main.transform.forward; 
		forward.y = 0; 
		forward = Vector3.Normalize(forward); 
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; 

	}

	ForceMode SetPlayerForceMode(PlayerForceMode _playerForceMode) {

		if (_playerForceMode == PlayerForceMode.Acceleration) {

			return ForceMode.Acceleration;

		}

		if (_playerForceMode == PlayerForceMode.Force) {

			return ForceMode.Force;

		}

		if (_playerForceMode == PlayerForceMode.Impulse) {

			return ForceMode.Impulse;

		}

		if (_playerForceMode == PlayerForceMode.VelocityChange) {

			return ForceMode.VelocityChange;

		}

		return ForceMode.Impulse;

	}

} enum PlayerForceMode { Acceleration, Force, Impulse, VelocityChange }
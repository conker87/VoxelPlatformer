using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 4f, jumpHeight = 8f, preJumpYLevel, maxFallDistance = 5f;

	[SerializeField]
	bool canMove = true, isOnGround = true, hasJumped;

	[SerializeField]
	float interactableDistance = 5f;

	[SerializeField]
	float baseButtStompForce = 10f;
	bool hasButtStomped = false;

	[SerializeField]
	LayerMask geometryLayerMask, interactableLayerMask;

    public LayerMask GeometryLayerMask {
        get {
            return geometryLayerMask;
        }

        set {
            geometryLayerMask = value;
        }
    }
    public LayerMask InteractableLayerMask {
        get {
            return interactableLayerMask;
        }

        set {
            interactableLayerMask = value;
        }
    }

    Rigidbody rb;
	Player p;

	Vector3 v3Forward, v3Right;

    [SerializeField]
	float raycastSkin = 0.01f, isGroundedCheckTime, isGroundedCheckCooldown = 0.5f;
	float capsuleColliderYBounds;

    void Start() {
		
		ResetForwardDirection ();

		rb = GetComponent<Rigidbody> ();
		p = GetComponent<Player> ();

		capsuleColliderYBounds = GetComponent<CapsuleCollider>().bounds.extents.y;

	}

	void Update() {

		if (Input.GetButtonDown ("Jump")) {

			Jump ();

		}

		if (Input.GetButtonDown ("ButtStomp") && hasButtStomped == false && isOnGround == false) {

				ButtStomp ();

		}

        if (isOnGround == true) {

            if (preJumpYLevel - transform.position.y > maxFallDistance) {

                Player.current.DamagePlayer(1);

            }
            
            preJumpYLevel = transform.position.y;

        }

		// Set this to "Interact"
		if (Input.GetKeyDown (KeyCode.E)) {

			Collider[] overlappedSphere = Physics.OverlapSphere (transform.position, interactableDistance, InteractableLayerMask);

			if (overlappedSphere != null && overlappedSphere.Length > 0) {

				foreach (Collider coll in overlappedSphere) {

					Interactable interactable;

					if ((interactable = coll.GetComponent<Interactable>()) != null) {
					
						interactable.Interact (true, InteractableTriggerCauses.OnTriggerInteract, InteractableTriggerEffect.Toggle);

					}

				}

			}

		}

        IsGrounded();

    }

	void FixedUpdate() {

		rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);

		if (p.IsDead) {

			return;

		}

		if (!canMove) {

			return;

		}

		Move ();

    }

	void Move() {
		
		Vector3 rightMovement = v3Right * moveSpeed * Time.deltaTime * Input.GetAxis ("HorizontalKey"); 
		Vector3 upMovement = v3Forward * moveSpeed * Time.deltaTime * Input.GetAxis ("VerticalKey");
		Vector3 heading = Vector3.Normalize (rightMovement + upMovement);

		if (heading.magnitude > 0.1f) {

            if (!isOnGround)
            {
               // rightMovement *= .2f;
               // upMovement *= .2f;
            }

            transform.forward = heading;

            transform.position += rightMovement;
            transform.position += upMovement;

        }
	}

    void Jump() {

        if (!isOnGround) {

            return;

        } else {

            preJumpYLevel = transform.position.y;

        }

        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        //rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);

        isOnGround = false;
        hasJumped = true;

        isGroundedCheckTime += 0.5f;
    }

    void ButtStomp() {

		float fCurrentYLevel = transform.position.y;
		float fButtStompForce = baseButtStompForce + (1.5f * (fCurrentYLevel - preJumpYLevel));

		Debug.Log (string.Format("buttonStompForce: {0}, original/currentY: {1}/{2}", fButtStompForce, preJumpYLevel, fCurrentYLevel));

		rb.velocity = Vector3.zero;
		hasButtStomped = true;
		rb.AddForce (-Vector3.up * fButtStompForce, ForceMode.Force);

	}

	public void ResetForwardDirection() {

		v3Forward = Camera.main.transform.forward; 
		v3Forward.y = 0; 
		v3Forward = Vector3.Normalize(v3Forward); 
		v3Right = Quaternion.Euler(new Vector3(0, 90, 0)) * v3Forward; 

	}

    void IsGrounded() {

        if (Time.time > isGroundedCheckTime) {

            isGroundedCheckTime = Time.time + isGroundedCheckCooldown;

            // Debug.Log("Will Ground Check.");

            if (Physics.Raycast(transform.position, -Vector3.up, capsuleColliderYBounds + raycastSkin, GeometryLayerMask)) {

                isOnGround = true;
                hasButtStomped = hasJumped = false;
                

            } else {

                isOnGround = false;

            }

        }

    }

	void OnDrawGizmos() {

		Debug.DrawLine (transform.position, new Vector3 (transform.position.x, transform.position.y - (capsuleColliderYBounds + raycastSkin), transform.position.z), Color.green);
		Gizmos.DrawWireSphere (transform.position, interactableDistance);

	}

}
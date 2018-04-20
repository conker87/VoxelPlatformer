﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 4f, jumpHeight = 8f, preJumpYLevel, maxFallDistance = 5f;

	[SerializeField]
	bool canMove = true, isOnGround = true, hasJumped = false, hasJumpedDouble = false;

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
	float raycastSkin = 0.01f, isGroundedCheckTime, isGroundedCheckCooldown = 0.25f;
	float capsuleColliderYBounds;

    [SerializeField]
    CapsuleCollider capsuleCollider;

    void Start() {
		
		ResetForwardDirection ();

		rb = GetComponent<Rigidbody> ();
		p = GetComponent<Player> ();

		capsuleColliderYBounds = capsuleCollider.bounds.extents.y;

	}

	void Update() {

        if (p.IsDead == true || canMove == false) {
            return;
        }

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

			Collider[] overlappedSphere = Physics.OverlapSphere (transform.position, interactableDistance);

			if (overlappedSphere != null && overlappedSphere.Length > 0) {

                Interactable interactable, previousInteractable = null;

                foreach (Collider coll in overlappedSphere) {

                    interactable = coll.GetComponentInParent<Interactable>();

                    if (interactable == null) {
                        continue;
                    }

                    if (previousInteractable != null && interactable == previousInteractable) {
                        continue;
                    }

                    Debug.Log(string.Format("Found Interactable: {0}", interactable));

                    InteractableTrigger interactingTrigger = new
                        InteractableTrigger(interactable, InteractableTriggerCauses.OnTriggerInteract, InteractableTriggerEffect.Toggle, InteractableTriggerAction.Interact, 0f, false, false);

					interactable.Interact (interactingTrigger, true);

                    previousInteractable = interactable;

                }
			}
		}

        IsGrounded();

    }

	void FixedUpdate() {

		rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);

		if (p.IsDead == true || canMove == false) {
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

        if ((!isOnGround && hasJumped == true && GameController.current.HasAcquiredAbility("DOUBLE_JUMP") == true && hasJumpedDouble == true)
            || (!isOnGround && hasJumped == true && GameController.current.HasAcquiredAbility("DOUBLE_JUMP") == false)) {

            return;

        } else {

            preJumpYLevel = transform.position.y;

        }

        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        //rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);

        // Set the main grounded bool to false.
        isOnGround = false;

        // Chec
        if (hasJumped == false) {
            hasJumped = true;
        } else {
            hasJumpedDouble = true;
        }

        // Prevent issues with IsGrounded() by stopping it checking for the ground for another .5 seconds.
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
                hasButtStomped = hasJumped = hasJumpedDouble = false;
                

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
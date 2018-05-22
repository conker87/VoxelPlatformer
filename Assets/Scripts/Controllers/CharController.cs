using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Controller that controls movement of the Player.
/// </summary>
public class CharController : MonoBehaviour {

	[SerializeField]
    // moveSpeed:           The Movement Speed of the Player.
    // jumpHeight:          The Maximum jump height of the Player.
    // preJumpYLevel:       The y position of where the Player jumped off.
    // maxFallDistance:     The maximum distance the Player can fall before incuring damage.
    // baseButtStompForce:  The base force of the Butt Stomp ability.
    float moveSpeed = 4f, jumpHeight = 8f, preJumpYLevel, maxFallDistance = 5f, baseButtStompForce = 10f, currentJumpTimer;

	[SerializeField]
    // canMove:         Can the Player move?
    // isOnGround:      Is the Player currenty on the Ground?
    // hasJumped:       Has the Player jumped?
    // hasJumpedDouble: Has the Player Jumped again, if they have that ability?
    // hasButtStomped:  Has the Player Butt Stomped?
    bool canMove = true, isGrounded = true, hasJumped = false, hasJumpedDouble = false, hasButtStomped = false;

    [SerializeField]
    // The Transform of where the Player can Interact from.
    Transform interactableTransform;
    [SerializeField]
    // The maximum distance the Player can Interact from.
	float interactableDistance = 5f;

	[SerializeField]
    // geometryLayerMask:       The Layer of what the Player can walk on and what it collides with.
    LayerMask geometryLayerMask;
    public LayerMask GeometryLayerMask {
        get {
            return geometryLayerMask;
        }

        set {
            geometryLayerMask = value;
        }
    }

    // The rigidbody connected to the Player.
    Rigidbody rb;
    // The actual Player component.
	Player p;

    // Forward and Right based on the Camera, used for Isometric calculations.
	Vector3 v3Forward, v3Right;

    [SerializeField]
    // raycastSkin:             The amount purtuding from the Player collider to check for Ground.
    // isGroundedCheckTime:     The next tim    e when the ground should be checked for.
    // isGroundedCheckCooldown: The cooldown on when the ground should be checked for.
    // capsuleColliderYBounds:  The vertical bounds of the Player Capsule Collider.
    float raycastSkin = 0.01f, isGroundedCheckTime, isGroundedCheckCooldown = 0.25f, capsuleColliderYBounds;
    public bool IsGrounded {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    [SerializeField]
    CapsuleCollider capsuleCollider;

    void Start() {
		
		ResetForwardDirection ();

        // If these vars are null then find their respective components on the objec.
		rb = rb ?? GetComponent<Rigidbody>();
		p = p ?? GetComponent<Player>();
        capsuleCollider = capsuleCollider ?? GetComponent<CapsuleCollider>();

        // Sets the Collider bounds to a var.
        capsuleColliderYBounds = capsuleCollider.bounds.extents.y;

	}

	void Update() {

        // If the Player is Dead or it cannot move then just return this entire function.
        if (p.IsDead == true || canMove == false) {

            return;

        }

        // If the button for Jump was pressed.
        if (Input.GetButtonDown ("Jump")) {

			Jump ();

		}

        // If the Button for the Butt Stomp
		if (Input.GetButtonDown ("ButtStomp")) {

			ButtStomp ();

		}

        // If the Player is Grounded.
        if (isGrounded == true) {

            // If the previous jump lead to a drop of more than the Max Fall Distance.
            if (preJumpYLevel - transform.position.y > maxFallDistance) {

                Player.current.DamagePlayer(1);

            }
            
            // Always set the Pre-Jump float to the Y position if Grounded.
            // preJumpYLevel = transform.position.y;

        }

		// If the button for Interact was pressed.
        // TODO: Set this up as an actual button.
		if (Input.GetButtonDown("Interact")) {

            // Populate an array with all of the Geometry Colliders within the Interact sphere.
			Collider[] overlappedSphere = Physics.OverlapSphere (interactableTransform.position, interactableDistance);

            // If this array is not null and has a length.
            if (overlappedSphere != null && overlappedSphere.Length > 0) {

                Interactable interactable;
                
                // Itterate through the array of Colliders. 
                foreach (Collider coll in overlappedSphere) {

                    interactable = coll.GetComponent<Interactable>();

                    // If the Current collider does not have the Interactable component.
                    if (interactable == null) {

                        continue;

                    }

                    // Create a new basic InteractableTrigger so it can be Interacted with.
                    InteractableTrigger interactingTrigger = new
                        InteractableTrigger(
                            interactable,
                            InteractableTriggerCauses.OnTriggerInteract,
                            InteractableTriggerEffect.Toggle,
                            InteractableTriggerAction.Interact,
                            0f,
                            false,
                            false
                        );

                    // Interact with the Interactable.
					interactable.Interact (interactingTrigger, true);

                }
			}
		}

        if (isGrounded == false) {

            currentJumpTimer += Time.unscaledDeltaTime;

        }

        // Check to see if the Player is Grounded.
        IsCurrentlyGrounded();

    }

	void FixedUpdate() {

        // Sets the velocity of the Rigidbody to 0 on the horizontal plane while keeping the vertical speed.
        //      Had issues with the Player moving about while stood still, this fixed it.
		rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);

        // If the Player is Dead or it cannot move then just return this entire function.
        if (p.IsDead == true || canMove == false) {

            return;

        }

        // Actually Move the Player.
        Move ();

    }

    /// <summary>
    /// Moves the Player in accordance to the Current Camera's Forward and Right vector.
    /// </summary>
	void Move() {

        // rightMovement:   Sets the West/East movement to the speed.
        // upMovement:      Sets the North/South movement to the speed.
        // heading:         Sets the current forward vector of the Player.

        // TODO: Do I want to force the player into snapping to the directions?
        float horizontalAxis = Mathf.Round(Input.GetAxisRaw("HorizontalKey"));
        float verticalAxis = Mathf.Round(Input.GetAxisRaw("VerticalKey"));

        // Vector3 rightMovement = v3Right * moveSpeed * Time.deltaTime * Input.GetAxisRaw("HorizontalKey"); 
        // Vector3 upMovement = v3Forward * moveSpeed * Time.deltaTime * Input.GetAxisRaw("VerticalKey");
        Vector3 rightMovement = v3Right * moveSpeed * Time.deltaTime * horizontalAxis; 
        Vector3 upMovement = v3Forward * moveSpeed * Time.deltaTime * verticalAxis;

        Vector3 heading = Vector3.Normalize (rightMovement + upMovement);

        // Makes sure the Player facing direction is only updated if the Player is moving.
		if (heading.magnitude > 0.1f) {

            transform.forward = heading;

            transform.position += rightMovement;
            transform.position += upMovement;

        }

	}

    /// <summary>
    /// Makes the Player Jump.
    /// </summary>
    void Jump() {

        // We're not Grounded, we have Jumped, we have the DOUBLE_JUMP ability and we have Double Jumped
        if ((isGrounded == false
            && hasJumped == true
            && MainGameController.current.HasPlayerCollectedCollectable("DOUBLE_JUMP") == true
            && hasJumpedDouble == true)) {

            // Do not continue the function.
            return;

        }

        // We're not Grounded, we have Jumped, we don't have the DOUBLE_JUMP ability.
        if (isGrounded == false
            && hasJumped == true
            && MainGameController.current.HasPlayerCollectedCollectable("DOUBLE_JUMP") == false) {

            // Do not continue the function.
            return;

        }

        if (isGrounded == false && hasJumped == false) {

            // Do not continue the function.
            return;

        }

        // Set the Pre-Jump float to the Y position.
        preJumpYLevel = transform.position.y;

        // Set the vertical velocity of the Player while keeping the horizontal velocities.
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);

        // Set the main grounded bool to false.
        isGrounded = false;

        // hasJumpedDouble = (hasJumped == true) ? true : hasJumpedDouble;
        // hasJumped = (hasJumped == false) ? true : hasJumped;

        currentJumpTimer = 0f;

        // If the Player hasn't jumped before.
        if (hasJumped == false) {

            hasJumped = true;

        } else {

            hasJumpedDouble = true;

        }

        // Prevent issues with IsGrounded() by stopping it checking for the ground for another .5 seconds.
        isGroundedCheckTime += 0.5f;

    }

    /// <summary>
    /// Makes the Player Butt Stomp.
    /// </summary>
    void ButtStomp() {

        if (isGrounded == true) {

            return;

        }

        if (hasButtStomped == true) {

            return;

        }

        // Sets the Current Player's Y position.
		float currentYLevel = transform.position.y;
        // Sets the Butt Stomp Force to a value depending on how far away from the original Y pos.
        float buttStompForce = baseButtStompForce + (3f * currentJumpTimer);
            
            // Mathf.Abs((1.5f * (currentYLevel - preJumpYLevel)));

		Debug.LogFormat ("buttonStompForce: {0}, original/currentY: {1}/{2}", buttStompForce, preJumpYLevel, currentYLevel);

        // We set the Player's velocity to 0f.
		rb.velocity = Vector3.zero;

        // We reset the Butt Stomped bool to true.
		hasButtStomped = true;

        // We actually do the butt stomp.
        rb.velocity = new Vector3(rb.velocity.x, -buttStompForce, rb.velocity.z);

    }

    /// <summary>
    /// Resets the Forward direction depending on the Current Foward vector of the Camera.
    /// </summary>
	public void ResetForwardDirection() {

		v3Forward = Camera.main.transform.forward; 
		v3Forward.y = 0; 
		v3Forward = Vector3.Normalize(v3Forward); 
		v3Right = Quaternion.Euler(new Vector3(0, 90, 0)) * v3Forward; 

	}

    void IsCurrentlyGrounded() {

        if (Time.time > isGroundedCheckTime) {

            isGroundedCheckTime = Time.time + isGroundedCheckCooldown;

            if (Physics.Raycast(transform.position, -Vector3.up, capsuleColliderYBounds + raycastSkin, GeometryLayerMask)) {

                isGrounded = true;
                hasButtStomped = hasJumped = hasJumpedDouble = false;
                

            } else {

                isGrounded = false;

            }

        }

    }

    void OnDrawGizmos() {

        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - (capsuleColliderYBounds + raycastSkin), transform.position.z), Color.green);

        if (interactableTransform != null) {

            Gizmos.DrawWireSphere(interactableTransform.position, interactableDistance);

        }
	}
}
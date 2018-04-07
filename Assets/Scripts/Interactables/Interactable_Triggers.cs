using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Triggers : Interactable {

    #region Serialized Private Fields

    private bool hasTriggerOverlapSphere, hasTriggerTimeSinceLevelStart, hasTriggerRepeatTime;
    
    [SerializeField]
    private float onTriggerStayCooldown = 1f;
    protected float onTriggerStayTime;
    protected float onTriggerUpdateTime;

    [SerializeField]
    private List<InteractableTrigger> interactableTriggers = new List<InteractableTrigger>();
    #endregion

    #region Encapsulated Public Fields

    public float OnTriggerStayCooldown {
        get {
            return onTriggerStayCooldown;
        }

        set {
            onTriggerStayCooldown = value;
        }
    }

    public List<InteractableTrigger> InteractableTriggers {
        get {
            return interactableTriggers;
        }

        set {
            interactableTriggers = value;
        }
    }

    #endregion

    private void Start() {

        // Check to see if the list of triggers has a OnTriggerOverlapSphere, OnTriggerTimeSinceLevelStart or OnTriggerRepeatTime
        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            if (interactable.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerOverlapSphere)
                hasTriggerOverlapSphere = true;

            if (interactable.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerTimeSinceLevelStart)
                hasTriggerTimeSinceLevelStart = true;

            if (interactable.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerRepeatTime)
                hasTriggerRepeatTime = true;

        }

    }

    private void Update() {

        if ((hasTriggerOverlapSphere == true || hasTriggerTimeSinceLevelStart == true || hasTriggerRepeatTime == true)
            && Time.time > onTriggerUpdateTime) {

            Debug.Log(string.Format("This should only execute if one+ of these three values are true: {0},{1}, {2}",
                hasTriggerOverlapSphere,
                hasTriggerTimeSinceLevelStart,
                hasTriggerRepeatTime));

            foreach (InteractableTrigger interactableTrigger in InteractableTriggers) {

                if (interactableTrigger.InteractableToTrigger == null)
                    continue;

                if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerOverlapSphere) {

                    Collider[] overlappedSphere = Physics.OverlapSphere(transform.position, interactableTrigger.InteractableTriggerValue);

                    if (overlappedSphere != null && overlappedSphere.Length > 0) {

                        foreach (Collider coll in overlappedSphere) {

                            Player player;

                            if ((player = coll.GetComponent<Player>()) != null
                                && GameController.current.currentTime > interactableTrigger.InteractableTriggerValue) {

                                interactableTrigger.InteractableToTrigger.Interact(true,
                                    interactableTrigger.InteractableTriggerCause,
                                    interactableTrigger.InteractableTriggerEffect);

                            }

                        }

                    }

                }
                else if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerTimeSinceLevelStart) {

                    continue;

                    interactableTrigger.InteractableToTrigger.Interact(true,
                        interactableTrigger.InteractableTriggerCause,
                        interactableTrigger.InteractableTriggerEffect);

                }
                else if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerRepeatTime) {

                    continue;

                    interactableTrigger.InteractableToTrigger.Interact(true,
                        interactableTrigger.InteractableTriggerCause,
                        interactableTrigger.InteractableTriggerEffect);

                }

            }

            onTriggerUpdateTime = Time.time + 1f;

        }

    }

    public override void Interact(bool playerInteracting = false,
        InteractableTriggerCauses interactableTriggerCauses = InteractableTriggerCauses.OnTriggerInteract,
        InteractableTriggerEffect interactableTriggerEffect = InteractableTriggerEffect.Toggle) {

        if (OneTimeUse == true && HasBeenUsedOnce == true)
            return;

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true)
            return;

        switch (interactableTriggerEffect) {
            case InteractableTriggerEffect.TurnOff:
                IsOn = false;
                break;
            case InteractableTriggerEffect.TurnOn:
                IsOn = true;
                break;
            default:
            case InteractableTriggerEffect.Toggle:
                IsOn = !IsOn;
                break;
        }

        // if (Anim != null)
        //    Anim.SetBool("IsOn", IsOn);

        if (OneTimeUse == true)
            HasBeenUsedOnce = true;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            if (interactable.InteractableTriggerCause != interactableTriggerCauses)
                continue;

            interactable.InteractableToTrigger.Interact(false, interactableTriggerCauses, interactable.InteractableTriggerEffect);

        }

    }

    protected virtual void OnTriggerEnter(Collider other) {

        if (other.GetComponent<Player>() == null)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            if (interactable.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerEnter)
                continue;

            interactable.InteractableToTrigger.Interact(false, InteractableTriggerCauses.OnTriggerEnter, interactable.InteractableTriggerEffect);

        }

    }

    protected virtual void OnTriggerStay(Collider other) {

        if (other.GetComponent<Player>() == null)
            return;

        if (Time.time - onTriggerStayTime < OnTriggerStayCooldown)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            if (interactable.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerOverlapSphere)
                continue;

            interactable.InteractableToTrigger.Interact(false, InteractableTriggerCauses.OnTriggerOverlapSphere, interactable.InteractableTriggerEffect);

        }

        onTriggerStayTime = Time.time;

    }

    protected virtual void OnTriggerExit(Collider other) {

        if (other.GetComponent<Player>() == null)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            if (interactable.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerExit)
                continue;

            interactable.InteractableToTrigger.Interact(false, InteractableTriggerCauses.OnTriggerExit, interactable.InteractableTriggerEffect);

        }

    }

}

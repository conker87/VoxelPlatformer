using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Triggers : Interactable {
    
    #region Serialized Private Fields
    [SerializeField]
    private float onTriggerExecuteCooldown = 1f;
    protected float onTriggerExecuteTime;

    [SerializeField]
    private float onTriggerTimeSinceLevelStart = 60f;

    [SerializeField]
    private float onTriggerRepeatTime = 10f;

    [SerializeField]
    private List<InteractableTrigger> interactableTriggers = new List<InteractableTrigger>();
    #endregion

    #region Encapsulated Public Fields
    public float OnTriggerExecuteCooldown {
        get {
            return onTriggerExecuteCooldown;
        }

        set {
            onTriggerExecuteCooldown = value;
        }
    }
    public float OnTriggerTimeSinceLevelStart {
        get {
            return onTriggerTimeSinceLevelStart;
        }

        set {
            onTriggerTimeSinceLevelStart = value;
        }
    }
    public float OnTriggerRepeatTime {
        get {
            return onTriggerRepeatTime;
        }

        set {
            onTriggerRepeatTime = value;
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

        if (Time.time - onTriggerExecuteTime < OnTriggerExecuteCooldown)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            if (interactable.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerStay)
                continue;

            interactable.InteractableToTrigger.Interact(false, InteractableTriggerCauses.OnTriggerStay, interactable.InteractableTriggerEffect);

        }

        onTriggerExecuteTime = Time.time;

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

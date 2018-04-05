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

    public override void Interact(bool playerInteracting = true) {

        if (OneTimeUse == true && HasBeenUsedOnce == true)
            return;

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true)
            return;

        IsOn = !IsOn;

        if (OneTimeUse == true)
            HasBeenUsedOnce = true;

        if (IsOn == true) {

            foreach (InteractableTrigger interactable in InteractableTriggers) {

                if (interactable.InteractableToTrigger == null)
                    continue;

                if (interactable.InteractableTriggerCauses.Length == 0)
                    continue;

                interactable.InteractableToTrigger.Interact(false);

            }

        }

    }

    protected virtual void OnTriggerEnter(Collider other) {

        if (other.GetComponent<Player>() == null)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            if (interactable.InteractableTriggerCauses.Length == 0)
                continue;

            foreach (InteractableTriggerCauses cause in interactable.InteractableTriggerCauses) {

                if (cause != InteractableTriggerCauses.OnTriggerEnter)
                    continue;

                interactable.InteractableToTrigger.Interact(false);

            }

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

            if (interactable.InteractableTriggerCauses.Length == 0)
                continue;

            foreach (InteractableTriggerCauses cause in interactable.InteractableTriggerCauses) {

                if (cause != InteractableTriggerCauses.OnTriggerStay)
                    continue;

                interactable.InteractableToTrigger.Interact(false);

            }

        }

        onTriggerExecuteTime = Time.time;

    }

    protected virtual void OnTriggerExit(Collider other) {

        if (other.GetComponent<Player>() == null)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            if (interactable.InteractableTriggerCauses.Length == 0)
                continue;

            foreach (InteractableTriggerCauses cause in interactable.InteractableTriggerCauses) {

                if (cause != InteractableTriggerCauses.OnTriggerExit)
                    continue;

                interactable.InteractableToTrigger.Interact(false);

            }

        }

    }
}

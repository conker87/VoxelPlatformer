using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    #region Serialized Private Fields
    [SerializeField]
    private string interactableID = "";
    [SerializeField]
    private bool oneTimeUse;
    private bool hasBeenUsedOnce;
    [SerializeField]
    private bool canOnlyInteractFromOtherInteractables = false;

    [SerializeField]
    private bool isOn = false;

    [SerializeField]
    protected bool canContinue = true;

    [SerializeField]
    private List<InteractableTrigger> interactableTriggers = new List<InteractableTrigger>();
    #endregion

    public string InteractableID {
        get {
            return interactableID;
        }

        set {
            interactableID = value;
        }
    }
    public bool OneTimeUse {
        get {
            return oneTimeUse;
        }

        set {
            oneTimeUse = value;
        }
    }
    public bool HasBeenUsedOnce {
        get {
            return hasBeenUsedOnce;
        }

        set {
            hasBeenUsedOnce = value;
        }
    }
    public bool CanOnlyInteractFromOtherInteractables {
        get {
            return canOnlyInteractFromOtherInteractables;
        }

        set {
            canOnlyInteractFromOtherInteractables = value;
        }
    }
    public bool IsOn {
        get {
            return isOn;
        }

        set {
            isOn = value;
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

    public void Interact(bool playerInteracting = true) {

        // If the Interactable is a one time use only and it has already been used then we ignore this Interact.
        if (OneTimeUse == true && HasBeenUsedOnce == true) {

            return;

        }

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true) {

            return;

        }

        if (canContinue == false) {

            return;

        }

        IsOn = !IsOn;

        if (OneTimeUse == true) {

            HasBeenUsedOnce = true;

        }

        if (IsOn == true) {

            foreach (InteractableTrigger interactable in InteractableTriggers) {

                interactable.InteractableToTrigger.Interact(false);

            }

        }

    }

    private void OnTriggerEnter(Collider other) {

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.OnTriggerEnter == true) {
                interactable.InteractableToTrigger.Interact(false);
            }
        }

    }

    private void OnTriggerStay(Collider other) {

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.OnTriggerStay == true) {
                interactable.InteractableToTrigger.Interact(false);
            }
        }

    }

    private void OnTriggerExit(Collider other) {

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.OnTriggerExit == true) {
                interactable.InteractableToTrigger.Interact(false);
            }
        }

    }

}

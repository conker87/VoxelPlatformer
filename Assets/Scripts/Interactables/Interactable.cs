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
    private float onTriggerExecuteCooldown = 1f;
    private float onTriggerExecuteTime;

    [SerializeField]
    private float onTriggerTimeSinceLevelStart = 60f;

    [SerializeField]
    private float onTriggerRepeatTime = 10f;

    [SerializeField]
    private List<InteractableTrigger> interactableTriggers = new List<InteractableTrigger>();
    #endregion

    #region Encapsulated Public Fields
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

    /// TODO: Create an inherited class from this and the Triggers to that to seperate basic Interactables
    /// and Advanced Interactables that have triggers, and call it
    /// Interactable_Triggers

    public void Interact(bool playerInteracting = true) {

        if (OneTimeUse == true && HasBeenUsedOnce == true)
            return;

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true)
            return;

        IsOn = !IsOn;

        if (OneTimeUse == true)
            HasBeenUsedOnce = true;

        if (IsOn == true) {

            foreach (InteractableTrigger interactable in InteractableTriggers) {

                interactable.InteractableToTrigger.Interact(false);

            }

        }

    }

    private void OnTriggerEnter(Collider other) {

        if (other.GetComponent<Player>() == null)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            foreach (InteractableTriggerCauses cause in interactable.InteractableTriggerCauses) {

                if (cause != InteractableTriggerCauses.OnTriggerEnter)
                    continue;

                interactable.InteractableToTrigger.Interact(false);

            }
                
        }

    }

    private void OnTriggerStay(Collider other) {

        if (other.GetComponent<Player>() == null)
            return;

        if (Time.time - onTriggerExecuteTime < onTriggerExecuteCooldown)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            foreach (InteractableTriggerCauses cause in interactable.InteractableTriggerCauses) {

                if (cause != InteractableTriggerCauses.OnTriggerStay)
                    continue;

                interactable.InteractableToTrigger.Interact(false);

            }

        }

        onTriggerExecuteTime = Time.time;

    }

    private void OnTriggerExit(Collider other) {

        if (other.GetComponent<Player>() == null)
            return;

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null)
                continue;

            foreach (InteractableTriggerCauses cause in interactable.InteractableTriggerCauses) {

                if (cause != InteractableTriggerCauses.OnTriggerExit)
                    continue;

                interactable.InteractableToTrigger.Interact(false);

            }

        }

    }

}

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
    #endregion

    public virtual void Interact(bool playerInteracting = true) {

        if (OneTimeUse == true && HasBeenUsedOnce == true)
            return;

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true)
            return;

        IsOn = !IsOn;

        if (OneTimeUse == true)
            HasBeenUsedOnce = true;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class must be given to Interactables that will be interacted by other Interactables.
/// </summary>
public class Interactable : MonoBehaviour {

    #region Serialized Private Fields

    [SerializeField]
    private string interactableID = "";
    [SerializeField]
    private bool isDisabled, isLocked, isActivated = false, oneTimeUse,
        canOnlyInteractFromOtherInteractables = true, canContinue = true;
    [SerializeField]
    Animator anim;
    [SerializeField]
    private List<Interactable_Triggers> triggersConnectedToMe;

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
    public bool IsDisabled {
        get {
            return isDisabled;
        }

        set {
            isDisabled = value;
        }
    }
    public bool IsLocked {
        get {
            return isLocked;
        }

        set {
            isLocked = value;
        }
    }
    public bool IsActivated {
        get {
            return isActivated;
        }

        set {
            isActivated = value;
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
    public bool CanOnlyInteractFromOtherInteractables {
        get {
            return canOnlyInteractFromOtherInteractables;
        }

        set {
            canOnlyInteractFromOtherInteractables = value;
        }
    }
    public bool CanContinue {
        get {
            return canContinue;
        }

        set {
            canContinue = value;
        }
    }
    public Animator Anim {
        get {
            return anim;
        }

        set {
            anim = value;
        }
    }
    public List<Interactable_Triggers> TriggersConnectedToMe {
        get {
            return triggersConnectedToMe;
        }

        set {
            triggersConnectedToMe = value;
        }
    }

    #endregion

    protected virtual void Start() {

        anim = GetComponent<Animator>();

    }

    public virtual void Interact(InteractableTrigger interactableTrigger, bool playerInteracting = false) {

        CanContinue = true;

        interactableTrigger = (interactableTrigger == null) ?
            new InteractableTrigger(this, InteractableTriggerCauses.OnTriggerInteract, InteractableTriggerEffect.Toggle, InteractableTriggerAction.Interact, 0, false, false)
            : interactableTrigger;

        if (IsDisabled == true) {
            CanContinue = false;
        }

        if (IsLocked == true && interactableTrigger.InteractableTriggerAction == InteractableTriggerAction.Interact) {
            CanContinue = false;
        }

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true) {
            CanContinue = false;
        }

        if ((interactableTrigger.InteractableTriggerEffect == InteractableTriggerEffect.Deactivate
                    && IsActivated == false)
            || (interactableTrigger.InteractableTriggerEffect == InteractableTriggerEffect.Activate
                    && IsActivated == true)) {
            CanContinue = false;
        }

        if (interactableTrigger.InteractableTriggerAction == InteractableTriggerAction.Lock) {

            if (interactableTrigger.InteractableTriggerEffect == InteractableTriggerEffect.Deactivate) {
                IsLocked = false;
            }

            if (interactableTrigger.InteractableTriggerEffect == InteractableTriggerEffect.Activate) {
                IsLocked = true;
            }

            if (interactableTrigger.InteractableTriggerEffect == InteractableTriggerEffect.Toggle) {
                IsLocked = !IsLocked;
            }

            CanContinue = false;

        }

        if (interactableTrigger.InteractableTriggerAction == InteractableTriggerAction.Interact) {

            if (CanContinue == true) {

                if (Anim != null) {
                    Anim.SetBool("IsOn", IsActivated);
                }

                if (OneTimeUse == true) {
                    IsDisabled = true;
                }

                if (interactableTrigger.InteractableTriggerEffect == InteractableTriggerEffect.Deactivate) {
                    IsActivated = false;
                }

                if (interactableTrigger.InteractableTriggerEffect == InteractableTriggerEffect.Activate) {
                    IsActivated = true;
                }

                if (interactableTrigger.InteractableTriggerEffect == InteractableTriggerEffect.Toggle) {
                    IsActivated = !IsActivated;
                }

            }
        }
    }
}

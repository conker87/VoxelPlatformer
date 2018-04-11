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
    private bool oneTimeUse;
    private bool hasBeenUsedOnce;
    [SerializeField]
    private bool canOnlyInteractFromOtherInteractables = true;
    [SerializeField]
    Animator anim;

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
    public Animator Anim {
        get {
            return anim;
        }

        set {
            anim = value;
        }
    }
    #endregion

    protected virtual void Start() {

        anim = GetComponent<Animator>();

    }

    public virtual void Interact(bool playerInteracting = false,
        InteractableTriggerCauses interactableTriggerCauses = InteractableTriggerCauses.OnTriggerInteract,
        InteractableTriggerEffect interactableTriggerEffect = InteractableTriggerEffect.Toggle,
        bool dontCauseTriggerEffect = false) {

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true)
            return;

        if (OneTimeUse == true && HasBeenUsedOnce == true)
            return;

        if ((interactableTriggerEffect == InteractableTriggerEffect.TurnOff && IsOn == false)
            || (interactableTriggerEffect == InteractableTriggerEffect.TurnOn && IsOn == true))
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

        if (Anim != null)
            Anim.SetBool("IsOn", IsOn);

        if (OneTimeUse == true)
            HasBeenUsedOnce = true;

    }

}

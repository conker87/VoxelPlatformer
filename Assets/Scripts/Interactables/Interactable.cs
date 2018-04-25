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
    private bool defaultIsDisabled, defaultIsLocked, defaultIsActivated;
    [SerializeField]
    private bool isDisabled, isLocked, isActivated;
    [SerializeField]
    private bool oneTimeUse, canOnlyInteractFromOtherInteractables = true, canContinue = true;

    [SerializeField]
    private float resetInTime = -1f;
    [SerializeField]
    private bool hasToReset = false;
    [SerializeField]
    protected float resetTime;
    [SerializeField]
    Animator anim;
    [SerializeField]
    private List<Interactable_Triggers> triggersConnectedToMe;

    [SerializeField]
    private string turnOnText, turnOffText;

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

    public bool DefaultIsDisabled {
        get {
            return defaultIsDisabled;
        }

        set {
            defaultIsDisabled = value;
        }
    }
    public bool DefaultIsLocked {
        get {
            return defaultIsLocked;
        }

        set {
            defaultIsLocked = value;
        }
    }
    public bool DefaultIsActivated {
        get {
            return defaultIsActivated;
        }

        set {
            defaultIsActivated = value;
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
    public float ResetInTime {
        get {
            return resetInTime;
        }

        set {
            resetInTime = value;
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

    public bool HasToReset {
        get {
            return hasToReset;
        }

        set {
            hasToReset = value;
        }
    }

    public string TurnOnText {
        get {
            return turnOnText;
        }

        set {
            turnOnText = value;
        }
    }
    public string TurnOffText {
        get {
            return turnOffText;
        }

        set {
            turnOffText = value;
        }
    }

    #endregion

    protected virtual void Start() {

        if (InteractableID == "") {
            Debug.LogWarningFormat("{0} at {1} has no InteractableID and is being given a temporary one. Please copy the new ID and use this as the official one.", this, transform.position.ToString());

            InteractableID = gameObject.name + string.Format("({0},{1},{2})", transform.position.x, transform.position.y, transform.position.z);

        }

        if (anim == null) {
            anim = GetComponent<Animator>();
        }
    }

    private void OnEnable() {

        if (GameController.current.CurrentSaveState == SaveState.NewGame) {

            Debug.Log("GameController.current.CurrentSaveState == SaveState.NewGame");

            IsActivated = DefaultIsActivated;
            IsDisabled = DefaultIsDisabled;
            IsLocked = DefaultIsLocked;

        }

        if (anim != null) {

            anim.SetBool("IsOn", IsActivated);

        }
    }

    protected virtual void Update() {

        if (anim != null) {

            // anim.SetBool("IsOn", IsActivated);

        }

        if (IsActivated == true && HasToReset == true && ResetInTime > 0f) {

            if (Time.time > resetTime) {

                Interact();
                HasToReset = false;

            }
        }
    }

    public virtual void Interact(InteractableTrigger interactableTrigger = null, bool playerInteracting = false) {

        CanContinue = true;

        if (interactableTrigger == null) {

            interactableTrigger = new InteractableTrigger(this, InteractableTriggerCauses.OnTriggerInteract, InteractableTriggerEffect.Toggle, InteractableTriggerAction.Interact, 0, false, false);

        }

        if (IsDisabled == true) {
            CanContinue = false;
        }

        if (IsLocked == true && playerInteracting == true && interactableTrigger.InteractableTriggerAction == InteractableTriggerAction.Interact) {
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

                if (Anim != null) {
                    Anim.SetBool("IsOn", IsActivated);
                }
            }
        }

        HasToReset = false;

        if (isActivated == true && TurnOnText != "") {

            Debug.Log(TurnOnText);

        }

        if (IsActivated == false && TurnOffText != "") {

            Debug.Log(TurnOffText);

        }

        if (HasToReset == false && ResetInTime > 0f) {
            Debug.Log("ResetTimer > 0f");

            HasToReset = true;
            resetTime = Time.time + ResetInTime;
        }
    }
}

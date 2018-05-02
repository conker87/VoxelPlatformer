using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class must be given to Interactables that will be interacted by other Interactables.
/// </summary>
public class Interactable : MonoBehaviour {

    #region Serialized Private Fields

    [Header("Interactable Settings")]
    [SerializeField] private string interactableID = "";
    [SerializeField] private bool oneTimeUse;
    [SerializeField] private bool canContinue = true;
    [SerializeField] private Animator anim;

    [Header("Interacting Settings")]
    [SerializeField] private bool canOnlyInteractFromOtherInteractables = true;
    [SerializeField] private string onlyInteractFromOtherInteracblesText = "";

    [Header("Disabled Settings")]
    [SerializeField] private bool isDisabled;
    [SerializeField] private string disabledText = "";

    [Header("Locked Settings")]
    [SerializeField] private bool isLocked;
    [SerializeField] private string lockedText = "";

    [Header("Activated Settings")]
    [SerializeField] private bool isActivated;
    [SerializeField] private string isActivatedText = "", isDeactivatedText = "";

    [Header("Reset Fields")]
    [SerializeField] private float resetInTime = -1f;
    [SerializeField] private bool hasToReset = false;
    [SerializeField] protected float resetTime;

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

    public bool HasToReset {
        get {
            return hasToReset;
        }

        set {
            hasToReset = value;
        }
    }

    public string DisabledText {
        get {
            return disabledText;
        }

        set {
            disabledText = value;
        }
    }
    public string LockedText {
        get {
            return lockedText;
        }

        set {
            lockedText = value;
        }
    }
    public string IsActivatedText {
        get {
            return isActivatedText;
        }

        set {
            isActivatedText = value;
        }
    }
    public string IsDeactivatedText {
        get {
            return isDeactivatedText;
        }

        set {
            isDeactivatedText = value;
        }
    }

    #endregion

    bool firstDisable = true;

    protected virtual void Start() {

        if (InteractableID == "") {
            Debug.LogWarningFormat("{0} at {1} has no InteractableID and is being given a temporary one. Please copy the new ID and use this as the official one.", this, transform.position.ToString());

            InteractableID = gameObject.name + string.Format("({0},{1},{2})", transform.position.x, transform.position.y, transform.position.z);

        }

        if (anim == null) {

            anim = GetComponent<Animator>();

        }

        if (GetComponent<Collider>() != null) {

            GetComponent<Collider>().isTrigger = true;

        }

    }

    protected void OnEnable() {

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

            if (DisabledText != "") {

                UIElements.current.ShowRPGFluffText(DisabledText);

            }

            CanContinue = false;

        }

        if (IsLocked == true && playerInteracting == true && interactableTrigger.InteractableTriggerAction == InteractableTriggerAction.Interact) {

            if (LockedText != "") {

                UIElements.current.ShowRPGFluffText(LockedText);

            }

            CanContinue = false;
        }

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true) {

            if (onlyInteractFromOtherInteracblesText != "") {

                UIElements.current.ShowRPGFluffText(onlyInteractFromOtherInteracblesText);

            }

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

        if (canContinue == true) {

            if (isActivated == true && IsActivatedText != "") {


                UIElements.current.ShowRPGFluffText(IsActivatedText);

            }

            if (IsActivated == false && IsDeactivatedText != "") {


                UIElements.current.ShowRPGFluffText(IsDeactivatedText);

            }
        }

        if (HasToReset == false && ResetInTime > 0f) {
            Debug.Log("ResetTimer > 0f");

            HasToReset = true;
            resetTime = Time.time + ResetInTime;
        }
    }
}

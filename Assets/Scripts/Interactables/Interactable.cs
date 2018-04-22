﻿using System.Collections;
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
    private float resetInTime = -1f;
    [SerializeField]
    private bool hasToReset = false;
    [SerializeField]
    protected float resetTime;
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

    #endregion

    protected virtual void Start() {

        if (anim == null) {
            anim = GetComponent<Animator>();
        }

    }

    protected virtual void Update() {

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
        // 
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

                IsLocked = true;

                if (Anim != null) {
                    Anim.SetBool("IsOn", IsActivated);
                }
            }
        }

        HasToReset = false;

        if (HasToReset == false && ResetInTime > 0f) {
            Debug.Log("ResetTimer > 0f");

            HasToReset = true;
            resetTime = Time.time + ResetInTime;
        }
    }
}

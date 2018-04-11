using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Trap : Interactable {

    #region Serialized Private Fields

    [SerializeField]
    protected Ammo ammo;
    [SerializeField]
    protected Transform fireLocation;
    [SerializeField, Range(0.5f, 20f)]
    protected float ammoSpeed = 5f, ammoDamage = 1f, ammoDestroyAfter = 5f, attackSpeed = 1f;
    private float attackSpeedTime;
    [SerializeField]
    protected bool fireOnce = true;

    private bool hasFiredOnce = false;

    #endregion

    #region Encapsulated Public Fields

    public Ammo Ammo {
        get {
            return ammo;
        }

        set {
            ammo = value;
        }
    }
    public Transform FireLocation {
        get {
            return fireLocation;
        }

        set {
            fireLocation = value;
        }
    }
    public float AmmoSpeed {
        get {
            return ammoSpeed;
        }

        set {
            ammoSpeed = value;
        }
    }
    public float AmmoDamage {
        get {
            return ammoDamage;
        }

        set {
            ammoDamage = value;
        }
    }
    public float AmmoDestroyAfter {
        get {
            return ammoDestroyAfter;
        }

        set {
            ammoDestroyAfter = value;
        }
    }
    public float AttackSpeed {
        get {
            return attackSpeed;
        }

        set {
            attackSpeed = value;
        }
    }
    public bool FireOnce {
        get {
            return fireOnce;
        }

        set {
            fireOnce = value;
        }
    }

    #endregion

    private void Update() {
        
        if (FireOnce == true && hasFiredOnce == true) {
            return;
        }

        if (IsOn == false) {
            return;
        }

        if (Time.time > attackSpeedTime) {

            Fire();
            attackSpeedTime = Time.time + attackSpeed;

            if (FireOnce == true) {

                hasFiredOnce = true;

            }

        }

    }

    public override void Interact(bool playerInteracting = false,
    InteractableTriggerCauses interactableTriggerCauses = InteractableTriggerCauses.OnTriggerInteract,
    InteractableTriggerEffect interactableTriggerEffect = InteractableTriggerEffect.Toggle,
    bool dontCauseTriggerEffect = false) {

        if (OneTimeUse == true && HasBeenUsedOnce == true) {
            return;
        }

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true) {
            return;
        }

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

        if (Anim != null) {
            Anim.SetBool("IsOn", IsOn);
        }

        if (OneTimeUse == true) {
            HasBeenUsedOnce = true;
        }

        if (dontCauseTriggerEffect == true) {
            return;
        }

        if (fireOnce == true && hasFiredOnce == true) {
            return;
        }

    }

    void Fire() {

        Ammo ammoFired = null;

        ammoFired = Instantiate(ammo, FireLocation.position, Quaternion.LookRotation(transform.forward), transform) as Ammo;

        ammoFired.Speed = AmmoSpeed;
        ammoFired.Damage = AmmoDamage;
        ammoFired.DestroyAfter = AmmoDestroyAfter;

    }

}

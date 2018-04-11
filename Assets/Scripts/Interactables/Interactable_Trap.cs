using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Trap : Interactable {

    #region Serialized Private Fields

    [SerializeField]
    float fireCooldown = 0.5f;
    float fireTime;

    [SerializeField]
    List<TrapAmmo> trapFire = new List<TrapAmmo>();

    [SerializeField]
    protected bool fireOnce = true;
    private bool hasFiredOnce = false;

    #endregion

    #region Encapsulated Public Fields

    public bool FireOnce {
        get {
            return fireOnce;
        }

        set {
            fireOnce = value;
        }
    }

    public List<TrapAmmo> TrapFire {
        get {
            return trapFire;
        }

        set {
            trapFire = value;
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

        if (fireTime > Time.time) {
            return;
        }

        foreach (TrapAmmo ammo in TrapFire) {

            if (ammo.AttackSpeedTime > Time.time) {
                continue;
            }

            Ammo ammoFired = null;

            ammoFired = Instantiate(ammo.Ammo, ammo.FireLocation.position, Quaternion.LookRotation(transform.forward), transform) as Ammo;

            ammoFired.Speed = ammo.AmmoSpeed;
            ammoFired.Damage = ammo.AmmoDamage;
            ammoFired.DestroyAfter = ammo.AmmoDestroyAfter;

            ammo.AttackSpeedTime = Time.time + ammo.AttackSpeed;

            if (FireOnce == true) {

                hasFiredOnce = true;

            }

        }

        fireTime = Time.time + fireCooldown;

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

}

[System.Serializable]
public class TrapAmmo {

    public Ammo Ammo;
    public Transform FireLocation;
    public float AmmoSpeed = 5f;
    public float AmmoDamage = 1f;
    public float AmmoDestroyAfter = 5f;
    public float AttackSpeed = 1f;
    public float AttackSpeedTime;

    public TrapAmmo() {

    }

    public TrapAmmo(TrapAmmo value) {

        Ammo = value.Ammo;
        FireLocation = value.FireLocation;
        AmmoSpeed = value.AmmoSpeed;
        AmmoDamage = value.AmmoDamage;
        AmmoDestroyAfter = value.AmmoDestroyAfter;
        AttackSpeed = value.AttackSpeed;
        AttackSpeedTime = value.AttackSpeedTime;

    }

}

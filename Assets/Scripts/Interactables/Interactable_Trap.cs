using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Trap : Interactable {

    #region Serialized Private Fields

    [SerializeField]
    float fireCheckCooldown = 0.1f;
    float fireCheckTime;

    [SerializeField]
    List<TrapAmmo> trapFire = new List<TrapAmmo>();

    [SerializeField]
    protected bool fireOnce = true;

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

    protected override void Update() {

        base.Update();

        if (IsDisabled == true) {
            return;
        }

        if (IsActivated == false) {
            return;
        }

        if (fireCheckTime > Time.time) {
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

                IsDisabled = true;

            }

        }

        fireCheckTime = Time.time + fireCheckCooldown;

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

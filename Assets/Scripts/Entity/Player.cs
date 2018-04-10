using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region Singleton
    public static Player current;

	void Awake() {

		if (current == null) {

			current = this;

		}

	}

    #endregion

    #region Serialized Private Fields

    [SerializeField]
    private int currentHealth = 3, maximumHealth = 3;

    [SerializeField]
    private bool isInvulnerable = false;

    [SerializeField]
    private bool isCurrentlyInInvincibilityFrames = false;
    [SerializeField]
    private float invincibilityFramesLength, invincibilityFramesTime;

    [SerializeField]
    private bool isDead = false;

    #endregion

    #region Encapsulated Public Fields

    public int CurrentHealth {
        get {
            return currentHealth;
        }

        set {
            currentHealth = value;
        }
    }
    public int MaximumHealth {
        get {
            return maximumHealth;
        }

        set {
            maximumHealth = value;
        }
    }

    public bool IsInvulnerable {
        get {
            return isInvulnerable;
        }

        set {
            isInvulnerable = value;
        }
    }

    public bool IsCurrentlyInInvincibilityFrames {
        get {
            return isCurrentlyInInvincibilityFrames;
        }

        set {
            isCurrentlyInInvincibilityFrames = value;
        }
    }

    public float InvincibilityFramesLength {
        get {
            return invincibilityFramesLength;
        }

        set {
            invincibilityFramesLength = value;
        }
    }

    public bool IsDead {
        get {
            return isDead;
        }

        set {
            isDead = value;
        }
    }

    #endregion

    void Start () {
		
	}

	void Update () {

		if (IsDead) {

			return;

		}

		IsDead = (CurrentHealth == 0) ? true : false;

		// Disable invincibility frames when needed.
		if (IsCurrentlyInInvincibilityFrames && Time.time > invincibilityFramesTime) {

			IsCurrentlyInInvincibilityFrames = false;

		}

	}

	public void HealPlayer(int health = 1) {

		if (CurrentHealth + health > MaximumHealth) {

			CurrentHealth = MaximumHealth;
			return;

		}

		CurrentHealth += health;

	}

	public void DamagePlayer(int health = 1) {

		if (IsInvulnerable == true || IsCurrentlyInInvincibilityFrames == true) {
			return;
		}

		IsCurrentlyInInvincibilityFrames = true;
        invincibilityFramesTime = Time.time + InvincibilityFramesLength;

		if (CurrentHealth - health < 1) {

			CurrentHealth = 0;
			return;

		}

		CurrentHealth -= health;

	}


}

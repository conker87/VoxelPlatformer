using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player current;

	void Awake() {

		if (current == null) {

			current = this;

		}

	}

	public int 		CurrentHealth, MaximumHealth;

	public bool		IsCurrentlyInInvincibilityFrames = false;
	public float 	InvincibilityFramesLength = 0.5f, CurrentInvincibilityFramesTime;

	public bool 	IsDead = false;



	void Start () {
		
	}

	void Update () {

		if (IsDead) {

			return;

		}

		IsDead = (CurrentHealth == 0) ? true : false;

		// Disable invincibility frames when needed.
		if (IsCurrentlyInInvincibilityFrames && Time.time > CurrentInvincibilityFramesTime) {

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

		if (IsCurrentlyInInvincibilityFrames) {
			return;
		}

		IsCurrentlyInInvincibilityFrames = true;
		CurrentInvincibilityFramesTime = Time.time + InvincibilityFramesLength;

		if (CurrentHealth - health < 1) {

			CurrentHealth = 0;
			return;

		}

		CurrentHealth -= health;

	}


}

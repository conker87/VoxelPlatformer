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

	[SerializeField]
	public List<string> Abilities = new List<string>();
	[SerializeField]
	public List<string> Coins = new List<string>();
	[SerializeField]
	public List<string> Stars = new List<string>();

	void Start () {
		
	}

	void Update () {

		if (IsDead) {

			return;

		}

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

	public void AddToAbilities(string abilityID) {

		if (Abilities.Contains (abilityID)) {

			return;

		}

		Abilities.Add (abilityID);

	}

	public bool HasAcquiredAbility(string abilityID) {

		return Abilities.Contains (abilityID);

	}

	public void AddToCoins(string coinID) {

		if (Coins.Contains (coinID)) {

			return;

		}

		Coins.Add (coinID);

	}

	public int NumberOfCoins() {

		return Coins.Count;

	}

	public void AddToStars(string starID) {

		if (Stars.Contains (starID)) {

			return;

		}

		Stars.Add (starID);

	}

	public int NumberOfStars() {

		return Stars.Count;

	}

}

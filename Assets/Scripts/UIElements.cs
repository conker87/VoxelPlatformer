using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElements : MonoBehaviour {

	public TextMeshProUGUI NumberOfCoins, NumberOfStars, Health;

	public Player player;

	void Start() {



	}

	void OnGUI() {

		if (!FindPlayer ()) {

			return;

		}

		if (NumberOfCoins != null) {

			NumberOfCoins.text = "Coins: " + player.NumberOfCoins ().ToString();

		}

		if (NumberOfStars != null) {

			NumberOfStars.text = "Stars: " + player.NumberOfStars ().ToString();

		}

		if (Health != null) {

			Health.text = player.CurrentHealth + "/" + player.MaximumHealth;

		}


	}

	bool FindPlayer() {

		if (player != null) {

			return true;

		} else {

			player = FindObjectOfType<Player> ();

			if (player != null) {

				return true;

			}

		}

		return false;

	}

}

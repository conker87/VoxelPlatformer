using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElements : MonoBehaviour {

	public TextMeshProUGUI NumberOfCoins, NumberOfStars, Health;

	public Player player;
	public GameController gameController;

	void Start() {

		gameController = FindObjectOfType<GameController> ();

	}

	void OnGUI() {

		if (!FindPlayer ()) {

			return;

		}

		if (NumberOfCoins != null) {

			NumberOfCoins.text = "Coins: " + gameController.NumberOfCoins ().ToString();

		}

		if (NumberOfStars != null) {

			NumberOfStars.text = "Stars: " + gameController.NumberOfStars ().ToString();

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

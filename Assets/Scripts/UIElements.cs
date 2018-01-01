﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElements : MonoBehaviour {

	public TextMeshProUGUI NumberOfCoins, NumberOfStars;

	public Player player;

	void Start() {



	}

	void OnGUI() {

		FindPlayer ();

		if (NumberOfCoins != null) {

			NumberOfCoins.text = "Coins: " + player.NumberOfCoins ().ToString();

		}

		if (NumberOfStars != null) {

			NumberOfStars.text = "Stars: " + player.NumberOfStars ().ToString();

		}


	}

	void FindPlayer() {

		if (player != null) {

			return;

		}

		player = FindObjectOfType<Player> ();


	}

}
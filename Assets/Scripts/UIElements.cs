using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

public class UIElements : MonoBehaviour {

	public TextMeshProUGUI NumberOfCoins, NumberOfStars, Health, TimeT;
	public Transform LevelLoadedCanvas, LevelSelectCanvas, LevelLoadedPausedCanvas;

	public Player player;

	void OnGUI() {

		if (GameController.current.justChangedState) {
			LevelSelectCanvas.gameObject.SetActive (false);
			LevelLoadedCanvas.gameObject.SetActive (false);
			LevelLoadedPausedCanvas.gameObject.SetActive (false);
		}

		if (GameController.current.CurrentState == "") {

			GameController.current.justChangedState = false;
			Debug.LogWarning (string.Format("GameController.current.CurrentState is empty, this shouldn't ever happen!!"));

		}

		if (GameController.current.CurrentState == "MAIN_MENU") {

			GameController.current.justChangedState = false;
			Debug.LogWarning (string.Format("GameController.current.CurrentState is empty, this shouldn't ever happen!!"));

		}

		if (GameController.current.CurrentState == "LEVEL_SELECT") {

			GameController.current.justChangedState = false;
			LevelSelectCanvas.gameObject.SetActive (true);

		}

		if (GameController.current.CurrentState == "LEVEL_LOADED") {

			GameController.current.justChangedState = false;
			LevelLoadedCanvas.gameObject.SetActive (true);

			if (NumberOfCoins != null) {

				NumberOfCoins.text = "Coins: " + GameController.current.NumberOfCoins ().ToString();

			}

			if (NumberOfStars != null) {

				NumberOfStars.text = "Stars: " + GameController.current.NumberOfStars ().ToString();

			}

			if (Health != null && FindPlayer()) {

				Health.text = player.CurrentHealth + "/" + player.MaximumHealth;

			}

			if (TimeT != null && FindPlayer()) {

				GameController.current.currentTime += Time.deltaTime;

				TimeSpan t = TimeSpan.FromSeconds (GameController.current.currentTime);

				string format = "";

				if (t.Hours > 0)
					format += "{3:D2}:";

				if (t.Minutes > 0)
					format += "{2:D2}:";

				format += "{1:D2}.{0:D1}";

				TimeT.text = string.Format(format, t.Milliseconds, t.Seconds, t.Minutes, t.Hours);

			}

		}

		if (GameController.current.CurrentState == "LEVEL_LOADED_PAUSED") {

			GameController.current.justChangedState = false;
			LevelLoadedCanvas.gameObject.SetActive (true);
			LevelLoadedPausedCanvas.gameObject.SetActive (true);

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

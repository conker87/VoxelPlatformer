using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

public class UIElements : MonoBehaviour {

	public TextMeshProUGUI NumberOfCoins, NumberOfStars, Health, TimeT;
	public Transform LevelLoadedCanvas, LevelSelectCanvas, LevelLoadedPausedCanvas;

	void OnGUI() {

		if (GameController.current.justChangedState) {
			LevelSelectCanvas.gameObject.SetActive (false);
			LevelLoadedCanvas.gameObject.SetActive (false);
			LevelLoadedPausedCanvas.gameObject.SetActive (false);

			GameController.current.justChangedState = false;
		}

		if (GameController.current.CurrentState == "") {
			Debug.LogWarning (string.Format("GameController.current.CurrentState is empty, this shouldn't ever happen!!"));

		}

		if (GameController.current.CurrentState == "MAIN_MENU") {

			Debug.LogWarning (string.Format("GameController.current.CurrentState is empty, this shouldn't ever happen!!"));

		}

		if (GameController.current.CurrentState == "LEVEL_SELECT") {

			LevelSelectCanvas.gameObject.SetActive (true);

		}

		if (GameController.current.CurrentState == "LEVEL_LOADED") {

			LevelLoadedCanvas.gameObject.SetActive (true);

			if (NumberOfCoins != null) {

				NumberOfCoins.text = "Coins: " + GameController.current.NumberOfCoins ().ToString();

			}

			if (NumberOfStars != null) {

				NumberOfStars.text = "Stars: " + GameController.current.NumberOfStars ().ToString();

			}

			if (Health != null && GameController.current.Player != null) {

				Health.text = GameController.current.Player.CurrentHealth + "/" + GameController.current.Player.MaximumHealth;

			}

			if (TimeT != null && GameController.current.Player != null) {

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

			LevelLoadedCanvas.gameObject.SetActive (true);
			LevelLoadedPausedCanvas.gameObject.SetActive (true);

		}

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElements : MonoBehaviour {

	public TextMeshProUGUI NumberOfCoins, NumberOfStars, Health;
	public Transform GameCanvas, LevelSelectCanvas, GameOptionsCanvas;

	public Player player;
	public GameController gameController;

	void Start() {

		gameController = FindObjectOfType<GameController> ();

	}

	void OnGUI() {

		if (GameController.current.CurrentState == "") {

			Debug.LogWarning (string.Format("GameController.current.CurrentState is empty, this shouldn't ever happen!!"));

		}

		if (GameController.current.CurrentState == "LEVEL_SELECT") {

			GameCanvas.gameObject.SetActive (false);
			LevelSelectCanvas.gameObject.SetActive (true);

		}

		if (GameController.current.CurrentState == "LEVEL_LOADED") {

			GameCanvas.gameObject.SetActive (true);
			LevelSelectCanvas.gameObject.SetActive (false);

		}

		if (NumberOfCoins != null) {

			NumberOfCoins.text = "Coins: " + gameController.NumberOfCoins ().ToString();

		}

		if (NumberOfStars != null) {

			NumberOfStars.text = "Stars: " + gameController.NumberOfStars ().ToString();

		}

		if (Health != null && FindPlayer()) {

			Health.text = player.CurrentHealth + "/" + player.MaximumHealth;

		}

		// Debug.Log (string.Format("Number of currentCoins: {0}", GameController.current.LevelPrefabs[2].levelScore.CurrentCoins));

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

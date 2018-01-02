using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DEBUG_LevelList : MonoBehaviour {

	GameController gameController;

	public LevelSelectButton ButtonLevelSelect;


	void Start () {

		gameController = FindObjectOfType<GameController> ();

		PopulateLevelSelect ();

	}

	void PopulateLevelSelect() {

		if (gameController == null)
			return;

		if (gameController.LevelPrefabs.Count == 0)
			return;

		int i = 0;

		foreach (Level level in gameController.LevelPrefabs) {

			LevelSelectButton levelSelectButton = Instantiate (ButtonLevelSelect, transform) as LevelSelectButton;

			levelSelectButton.level = i;
			levelSelectButton.LevelName = level.LevelName;

			levelSelectButton.GetComponent<Button> ().onClick.AddListener (delegate {
				gameController.LoadLevel (levelSelectButton.level);
			} );

			levelSelectButton.GetComponentInChildren<Text> ().text = levelSelectButton.LevelName;

			i++;
		}

	}

}

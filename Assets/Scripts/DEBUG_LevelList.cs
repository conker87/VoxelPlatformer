using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class DEBUG_LevelList : MonoBehaviour {

	public LevelSelectButton ButtonLevelSelect;


	void Start () {

		PopulateLevelSelect ();

	}

	void OnEnable() {

		PopulateLevelSelect ();

	}

	void PopulateLevelSelect() {

		foreach (Button levelSelectButton in GetComponentsInChildren<Button>()) {

			Destroy (levelSelectButton.gameObject);

		}

		if (GameController.current == null)
			return;

		if (GameController.current.LevelPrefabs.Count == 0)
			return;

		int i = 0;

		foreach (Level level in GameController.current.LevelPrefabs) {

			if (level == null)
				continue;

			LevelScore foundLS = GameController.current.LevelScores.FirstOrDefault (s => s.LevelID == level.LevelID);

			if (foundLS != null && !foundLS.HasUnlockedLevel)
				continue;

			LevelSelectButton levelSelectButton = Instantiate (ButtonLevelSelect, transform) as LevelSelectButton;

			levelSelectButton.level = i;
			levelSelectButton.LevelName = level.LevelName;

			levelSelectButton.GetComponent<Button> ().onClick.AddListener (delegate {
				GameController.current.LoadLevel (levelSelectButton.level);
			} );

			levelSelectButton.GetComponentInChildren<Text> ().text = levelSelectButton.LevelName;

			i++;
		}

	}

}

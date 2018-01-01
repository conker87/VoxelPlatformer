using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// public static GameController current;

	public List<float> BestLevelTimes = new List<float> ();
	public List<bool> OpenedLevels = new List<bool>();

	public List<Level> LevelPrefabs = new List<Level> ();

	public Player PlayerPrefab;

	bool HasLoaded = false;

	void Start() {

		if (LevelPrefabs.Count > 0) {

			// DEBUG: Spawn the test level.

			foreach (Level level in FindObjectsOfType<Level>()) {

				if (level.LevelName == LevelPrefabs [0].LevelName) {

					HasLoaded = true;

				}

			}

			if (!HasLoaded) {
				Instantiate (LevelPrefabs [0]);
			}

		}

	}

}

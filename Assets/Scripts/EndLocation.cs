using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class EndLocation : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		if (other.GetComponent<Player> () != null) {

			Debug.LogWarning (string.Format("The player has his the end portal and should show the end screen and go to the level select screen."));

			LevelScore currentLevelScore = GameController.current.LevelScores.FirstOrDefault (s => s.LevelID == GameController.current.currentlyLoadedLevel.LevelID);
			int indexOfCurrent = GameController.current.LevelScores.IndexOf(currentLevelScore);

			if (indexOfCurrent < GameController.current.LevelScores.Count - 1) {

				GameController.current.LevelScores [indexOfCurrent + 1].HasUnlockedLevel = true;

			}

			GameController.current.UnloadLevel(GameController.current.currentlyLoadedLevel);

		}

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelScore {

	public string LevelID;

	public float BestTime;
	public int CurrentCoins, MaxCoins;

	public LevelScore (string levelID, float bestTime, int currentCoins, int maxCoins) {

		LevelID = levelID;
		BestTime = bestTime;
		CurrentCoins = currentCoins;
		MaxCoins = maxCoins;

	}

	public LevelScore (LevelScore levelDetails) {

		LevelID = levelDetails.LevelID;
		BestTime = levelDetails.BestTime;
		CurrentCoins = levelDetails.CurrentCoins;
		MaxCoins = levelDetails.MaxCoins;

	}

}

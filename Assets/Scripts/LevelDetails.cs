using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDetails {

	public string LevelID;

	public float BestTime;
	public int CurrentCoins, MaxCoins;

	public LevelDetails (string levelID, float bestTime, int currentCoins, int maxCoins) {

		LevelID = levelID;
		BestTime = bestTime;
		CurrentCoins = currentCoins;
		MaxCoins = maxCoins;

	}

	public LevelDetails (LevelDetails levelDetails) {

		LevelID = levelDetails.LevelID;
		BestTime = levelDetails.BestTime;
		CurrentCoins = levelDetails.CurrentCoins;
		MaxCoins = levelDetails.MaxCoins;

	}

}

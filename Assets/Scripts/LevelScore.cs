[System.Serializable]
public class LevelScore {

	public string LevelID;

	public float BestTime;
	public int CurrentCoins, MaxCoins;
	public int CurrentStars, MaxStars;

	public bool HasUnlockedLevel;

	public LevelScore (string levelID, float bestTime, int currentCoins, int maxCoins, int currentStars, int maxStars, bool hasUnlockedLevel) {

		LevelID = levelID;

		BestTime = bestTime;

		CurrentCoins = currentCoins;
		MaxCoins = maxCoins;

		CurrentStars = currentStars;
		MaxStars = maxStars;

		HasUnlockedLevel = hasUnlockedLevel;

	}

	public LevelScore (LevelScore levelDetails) {

		LevelID = levelDetails.LevelID;

		BestTime = levelDetails.BestTime;

		CurrentCoins = levelDetails.CurrentCoins;
		MaxCoins = levelDetails.MaxCoins;

		CurrentStars = levelDetails.CurrentStars;
		MaxStars = levelDetails.MaxStars;

		HasUnlockedLevel = levelDetails.HasUnlockedLevel;

	}

	public LevelScore() {

	}

}

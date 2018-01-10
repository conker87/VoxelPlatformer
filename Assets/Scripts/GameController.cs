using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController current;

	void Awake() {

		if (current == null) {

			current = this;

		}

	}

	public List<float> BestLevelTimes = new List<float> ();
	public List<string> OpenedLevels = new List<string>();

	public List<Level> LevelPrefabs = new List<Level> ();

	public List<string> Abilities = new List<string>();
	public List<string> Coins = new List<string>();
	public List<string> Stars = new List<string>();

	Level currentlyLoadedLevel;

	public Player PlayerPrefab;

	public string CurrentState = "";

	bool HasLoaded = false;

	void Start() {

		CurrentState = "LEVEL_SELECT";

	}

	public void LoadLevel(Level level) {

		UnloadLevel (currentlyLoadedLevel);

		currentlyLoadedLevel = Instantiate (level) as Level;
		currentlyLoadedLevel.IsCurrentLevel = true;

		CurrentState = "LEVEL_LOADED";

	}

	public void UnloadLevel(Level level) {

		if (level != null) {
			Destroy (level.gameObject);
			currentlyLoadedLevel = null;
		}

	}

	public void LoadLevel(int level) {

		LoadLevel (LevelPrefabs [level]);

	}

	public void AddToAbilities(string abilityID) {

		if (Abilities.Contains (abilityID)) {

			return;

		}

		Abilities.Add (abilityID);

	}

	public bool HasAcquiredAbility(string abilityID) {

		return Abilities.Contains (abilityID);

	}

	public void AddToCoins(string coinID) {

		if (Coins.Contains (coinID)) {

			return;

		}

		Coins.Add (coinID);

	}

	public int NumberOfCoins() {

		return Coins.Count;

	}

	public void AddToStars(string starID) {

		if (Stars.Contains (starID)) {

			return;

		}

		Stars.Add (starID);

	}

	public int NumberOfStars() {

		return Stars.Count;

	}

}

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

	// This is used ONLY as a place to store the prefabs of levels, (as I hate using Resources.*), this will not save currently loaded level scores!
	public List<Level> LevelPrefabs = new List<Level> ();
	public List<LevelScore> LevelScores = new List<LevelScore> ();

	public List<string> Abilities = new List<string>();
	public List<string> Coins = new List<string>();
	public List<string> Stars = new List<string>();

	public float currentTime;

	public Level currentlyLoadedLevel;

	// PlayerSpawnablePrefab: The Prefab that should be spawned into the game.
	// Player: 
	public Player PlayerSpawnablePrefab, Player;

	public string CurrentState = "";
	public bool justChangedState = false;

	public float OriginalCameraFOV = 17.5f;

	void Start() {

		ChangeState ("LEVEL_SELECT");

		bool first = true;

		foreach (Level level in LevelPrefabs) {

			// Create a new LevelScore, otherwise it saves details to the Prefab itself.
			LevelScore newLS = new LevelScore (level.LevelID, 0f, 0, 0, 0, 0, first);

			first = false;

			LevelScores.Add (newLS);

		}

	}

	void Update() {

		if (CurrentState != "" || CurrentState != "LEVEL_SELECT" || CurrentState != "MAIN_MENU") {

			// FindPlayer ();

		}

	}

//	bool FindPlayer() {
//
//		if (Player != null) {
//
//			return true;
//
//		} else {
//
//			Player = FindObjectOfType<Player> ();
//
//			if (Player != null) {
//
//				return true;
//
//			}
//
//		}
//
//		return false;
//
//	}

	public void LoadLevel(Level level) {

		UnloadLevel (currentlyLoadedLevel);

		currentlyLoadedLevel = Instantiate (level) as Level;
		currentlyLoadedLevel.IsCurrentLevel = true;

		currentTime = 0f;
		Camera.main.orthographicSize = (currentlyLoadedLevel.OverwriteCameraFOV > 0) ? currentlyLoadedLevel.OverwriteCameraFOV : OriginalCameraFOV;

		ChangeState ("LEVEL_LOADED");

	}

	public void UnloadLevel(Level level) {

		if (level != null) {

			currentlyLoadedLevel.SaveLevelScores ();

			Destroy (level.gameObject);
			currentlyLoadedLevel = null;

			Camera.main.orthographicSize = OriginalCameraFOV;

			ChangeState ("LEVEL_SELECT");

		}

	}

	public void ChangeState(string newState) {

		justChangedState = true;
		CurrentState = newState;

		Debug.Log (string.Format("Changing States to -- {0}", newState));

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

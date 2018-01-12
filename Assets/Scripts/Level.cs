using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class Level : MonoBehaviour {

	[Header("Level Details")]
	public string LevelName = "";
	public string LevelID = "";
	public int LevelNumber = -1;

	public bool IsCurrentLevel = false;

	[Header("Level Transforms")]
	public Transform StartLocation;
	public Transform FinishLocation;
	public Transform GeometryParent;

	[Header("Enemies")]
	public List<Enemy> EnemiesInLevel = new List<Enemy>();

	public LevelScore LevelsLevelScore;

	// [Header("Player Details")]

	void Start() {

		LevelsLevelScore = new LevelScore ();

		if (LevelID == "") {

			Debug.LogError (string.Format("{0} has no LevelID, this is really bad.", this.LevelName));

		}

		if (!IsCurrentLevel) {

			// GeometryObject.SetActive (false);

		}

		int MaximumCoins = 0, MaximumStars = 0;

		foreach (Coin coin in GetComponentsInChildren<Coin>()) {

			coin.CurrentLevel = this;
			MaximumCoins++;

		}

		foreach (Star star in GetComponentsInChildren<Star>()) {

			star.CurrentLevel = this;
			MaximumStars++;

		}

		// LevelScore foundLS = GameController.current.LevelScores.FirstOrDefault (s => s.LevelID == LevelID);

		LevelsLevelScore.LevelID = LevelID;
		LevelsLevelScore.HasUnlockedLevel = true;

		if (LevelsLevelScore != null && MaximumCoins >= LevelsLevelScore.MaxCoins) {

			LevelsLevelScore.MaxCoins = MaximumCoins;

		}

		if (LevelsLevelScore != null && MaximumStars >= LevelsLevelScore.MaxStars) {

			LevelsLevelScore.MaxStars = MaximumStars;

		}

		LevelsLevelScore.BestTime = 0f;

	}

	public void Update() {

		LevelsLevelScore.BestTime = GameController.current.currentTime;

	}

	public void SaveLevelScores() {

		LevelScore foundLS = GameController.current.LevelScores.FirstOrDefault (s => s.LevelID == LevelID);

		if (foundLS.BestTime != 0f && LevelsLevelScore.BestTime > foundLS.BestTime) {

			LevelsLevelScore.BestTime = foundLS.BestTime;

		}

		int index = GameController.current.LevelScores.IndexOf (foundLS);

		GameController.current.LevelScores.Remove (foundLS);
		GameController.current.LevelScores.Insert (index, LevelsLevelScore);


	}

	void OnEnable() {

		GameController.current.Player = Instantiate (GameController.current.PlayerSpawnablePrefab, (StartLocation == null) ? Vector3.zero : StartLocation.transform.position, Quaternion.identity, transform);

		if (StartLocation == null || FinishLocation == null) {

			Debug.LogError (string.Format("{0} has no StartingLocation/FinishLocation and is fucked.", LevelID));

		}

	}

	void OnDrawGizmos() {

		if (EnemiesInLevel != null)
			return;

		if (!IsCurrentLevel)
			return;

		Vector3 trans = transform.position;

		foreach (Enemy enemy in EnemiesInLevel) {

			Gizmos.color = Color.blue;

			Gizmos.DrawLine (trans + enemy.SpawningPosition,
				new Vector3 (trans.x + enemy.SpawningPosition.x + 1f, 	trans.y + enemy.SpawningPosition.y,			trans.z + enemy.SpawningPosition.z));
			Gizmos.DrawLine (trans + enemy.SpawningPosition,
				new Vector3 (trans.x + enemy.SpawningPosition.x - 1f, 	trans.y + enemy.SpawningPosition.y, 		trans.z + enemy.SpawningPosition.z));
			Gizmos.DrawLine (trans + enemy.SpawningPosition,
				new Vector3 (trans.x + enemy.SpawningPosition.x, 		trans.y + enemy.SpawningPosition.y + 1f, 	trans.z + enemy.SpawningPosition.z));
			Gizmos.DrawLine (trans + enemy.SpawningPosition,
				new Vector3 (trans.x + enemy.SpawningPosition.x, 		trans.y + enemy.SpawningPosition.y - 1f, 	trans.z + enemy.SpawningPosition.z));
			Gizmos.DrawLine (trans + enemy.SpawningPosition,
				new Vector3 (trans.x + enemy.SpawningPosition.x, 		trans.y + enemy.SpawningPosition.y, 		trans.z + enemy.SpawningPosition.z + 1f));
			Gizmos.DrawLine (trans + enemy.SpawningPosition,
				new Vector3 (trans.x + enemy.SpawningPosition.x, 		trans.y + enemy.SpawningPosition.y, 		trans.z + enemy.SpawningPosition.z - 1f));

		}

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public string LevelName = "", LevelID = "";
	public int LevelNumber = -1;
	public Transform StartLocation, FinishLocation;

	public bool IsCurrentLevel = false;

	public GameObject GeometryObject;

	public List<Enemy> EnemiesInLevel = new List<Enemy>();

	public LevelDetails LevelDetails;

	public Player player;

	void Start() {

		if (LevelID == "") {

			Debug.LogError (string.Format("{0} has no LevelID, this is really bad.", this.LevelName));

		}

		if (!IsCurrentLevel) {

			GeometryObject.SetActive (false);

		}

		foreach (Transform go in GetComponentsInChildren<Transform>()) {

			go.gameObject.tag = "Geometry";

		}

	}

	void OnEnable() {

		Instantiate (player, (StartLocation == null) ? Vector3.zero : StartLocation.transform.position, Quaternion.identity, transform);

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

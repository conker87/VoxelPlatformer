using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public string LevelName = "";
	public int LevelNumber = -1;
	public Transform StartLocation, FinishLocation;

	public bool IsCurrentLevel = false;

	public List<Enemy> EnemiesInLevel = new List<Enemy>();

	// Should this be OnEnable?
	void Start() {



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

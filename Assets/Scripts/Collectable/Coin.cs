using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable {

	public Level CurrentLevel;

	protected override void Start () {

		base.Start ();

		foreach (LevelScore levelScore in GameController.current.LevelScores) {

			if (levelScore.LevelID == CurrentLevel.LevelID) {

				//levelScore.MaxCoins++;

			}

		}

		HasCollectedCollectable ();

	}

	protected override void OnTriggerEnter (Collider other) {
		
		if (other.GetComponent<Player>() != null) {

			Debug.Log(string.Format("Collectable '{0}' at position {1} has hit the player and will be collected.", CollectableID, transform.position));

			gameController.AddToCoins (CollectableID);

			SFXManager.instance.PlayRandomCoinClip ();

			foreach (LevelScore levelScore in GameController.current.LevelScores) {

				if (levelScore.LevelID == CurrentLevel.LevelID) {

					levelScore.CurrentCoins++;

				}

			}

			Destroy (gameObject);

		}

	}

	protected override void HasCollectedCollectable () {

		foreach (string SCollectableID in GameController.current.Coins) {

			if (SCollectableID == CollectableID) {

				gameObject.SetActive (false);

			}

		}

	}

}

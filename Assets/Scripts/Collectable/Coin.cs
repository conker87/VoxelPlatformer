using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable {

	protected override void Start () {

		base.Start ();

		HasCollectedCollectable ();

	}

	protected override void OnTriggerEnter (Collider other) {

        if (CollectableCollected == true) {
            return;
        }

		if (other.GetComponentInParent<Player>() != null) {

            Debug.Log(string.Format("Collectable '{0}' at position {1} has hit the player and will be collected. {2}", CollectableID, transform.position, other.ToString()), this);

            GameController.current.AddToCoins (CollectableID);
            SFXController.instance.PlayRandomCoinClip ();

			CurrentLevel.LevelsLevelScore.CurrentCoins++;

            CollectableCollected = true;

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

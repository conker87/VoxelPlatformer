﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Collectable {

	protected override void Start () {

		base.Start ();

		HasCollectedCollectable ();

	}

	protected override void OnTriggerEnter (Collider other) {

		if (other.GetComponent<Player>() != null) {

			Debug.Log(string.Format("Collectable '{0}' at position {1} has hit the player and will be collected.", CollectableID, transform.position));

			GameController.current.AddToStars (CollectableID);

			// TODO: Add some star collection clips.
			SFXManager.instance.PlayRandomCoinClip ();

			Destroy (gameObject);

		}

	}

	protected override void HasCollectedCollectable () {

		foreach (string SCollectableID in GameController.current.Stars) {

			if (SCollectableID == CollectableID) {

				gameObject.SetActive (false);


			}

		}

	}

}

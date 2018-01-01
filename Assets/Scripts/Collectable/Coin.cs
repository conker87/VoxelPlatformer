﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable {

	protected override void OnTriggerEnter (Collider other) {
		
		Player player;

		if ((player = other.GetComponent<Player>()) != null) {

			Debug.Log(string.Format("Collectable '{0}' at position {1} has hit the player and will be collected.", CollectableID, transform.position));

			player.AddToCoins (CollectableID);

			SFXManager.instance.PlayRandomCoinClip ();

			Destroy (gameObject);

		}

	}

}
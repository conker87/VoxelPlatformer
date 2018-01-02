using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : Collectable {

	[SerializeField]
	string AbilityID;

	protected override void OnTriggerEnter (Collider other) {

		if (other.GetComponent<Player>() != null) {

			Debug.Log(string.Format("Collectable '{0}' at position {1} has hit the player and will be collected.", CollectableID, transform.position));

			gameController.AddToAbilities (AbilityID);

			// TODO: Add some star collection clips.
			SFXManager.instance.PlayRandomCoinClip ();

			Destroy (gameObject);

		}

	}

}

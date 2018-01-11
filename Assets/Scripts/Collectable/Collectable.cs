using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	// TODO: This needs to be split into different inherited Collectables: Coin, Star, etc.

	public string CollectableID = "";
	[SerializeField]
	protected string CollectableType = "";

	public bool CollectableCollected = false;

	public Level CurrentLevel;

	protected virtual void Start () {

		if (string.IsNullOrEmpty (CollectableType) || CollectableType.Equals ("") || CollectableType == "" || CollectableType == null) {

			Debug.LogError (string.Format("Collectable at position: {0} has no CollectableID. This WILL break the game. Please fix.", transform.position));

		}

		CollectableID = CollectableType + "_" + transform.position + "_" + CurrentLevel.LevelID;

	}

	protected virtual void OnTriggerEnter(Collider other) {

		Debug.LogError (string.Format("{0}, of type {1}, requires OnTriggerEnter to be overriden in its inheritence.", gameObject.name, CollectableType));

		return;

		/* Player player;

		if ((player = other.GetComponent<Player>()) != null) {

			Debug.Log(string.Format("Collectable '{0}' at position {1} has hit the player and will be collected.", CollectableID, transform.position));

			player.AddToCoins (CollectableID);

			SFXManager.instance.PlayRandomCoinClip ();

			Destroy (gameObject);

		} */


	}

	protected virtual void Update() {
		
		if (CollectableCollected) {

			gameObject.SetActive (false);

		}

	}

	protected virtual void HasCollectedCollectable() {

		Debug.LogError (string.Format("{0}, of type {1}, requires HasCollectedCollected to be overriden in its inheritence.", gameObject.name, CollectableType));

	}

}

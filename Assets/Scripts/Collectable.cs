using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	[SerializeField]
	string CollectableID = "";

	[SerializeField]
	LayerMask PlayerLayerMask;

	// Use this for initialization
	void Start () {

		if (string.IsNullOrEmpty (CollectableID) || CollectableID.Equals ("") || CollectableID == "" || CollectableID == null) {

			Debug.LogError (string.Format("Collectable at position: {0} has no CollectableID. This WILL break the game. Please fix.", transform.position));

		}

	}

	void OnTriggerEnter(Collider other) {

		Debug.Log("Hit something:");

		if (other.gameObject.layer == PlayerLayerMask) {

			Debug.Log(string.Format("Collectable '{0}' at position {1} has hit the player and will be collected.", CollectableID, transform.position));

		}


	}


}

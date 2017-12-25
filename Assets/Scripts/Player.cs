using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	List<string> Collectables = new List<string>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddToCollectables(string collectableID) {

		if (Collectables.Contains (collectableID)) {

			return;

		}

		Collectables.Add (collectableID);

	}

}

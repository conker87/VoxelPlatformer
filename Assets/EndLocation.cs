using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLocation : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		if (other.GetComponent<Player> () != null) {

			Debug.LogWarning (string.Format("The player has his the end portal and should show the end screen and go to the level select screen."));

		}

	}

}

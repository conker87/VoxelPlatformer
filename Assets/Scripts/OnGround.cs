using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour {
	
	public bool IsOnGround = false;

	// Use this for initialization

	void OnTriggerEnter(Collider other) {

		IsOnGround = true;

	}

	void OnTriggerStay(Collider other) {

		// OnTriggerEnter (other);

	}

	void OnTriggerExit(Collider other) {

		IsOnGround = false;

	}

}

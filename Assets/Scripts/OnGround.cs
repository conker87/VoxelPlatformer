using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour {
	
	public bool IsOnGround = false;

	[SerializeField]
	LayerMask GeometryLayerMask;

	// Use this for initialization

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.layer == Mathf.Log(GeometryLayerMask.value, 2)) {
			IsOnGround = true;
		}

	}

	void OnTriggerStay(Collider other) {

		// OnTriggerEnter (other);

	}

	void OnTriggerExit(Collider other) {

		if (other.gameObject.layer == Mathf.Log(GeometryLayerMask.value, 2)) {
			IsOnGround = false;
		}

	}

}

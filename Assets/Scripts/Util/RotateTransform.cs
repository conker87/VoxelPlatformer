using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransform : MonoBehaviour {

	[SerializeField]
	float rotationSpeed;

	void Update () {
	
		transform.Rotate (0f, rotationSpeed, 0f, Space.Self);

	}
}

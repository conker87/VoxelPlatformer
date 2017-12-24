using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChild : MonoBehaviour {

	[SerializeField]
	float rotationSpeed;

	void Update () {
	
		transform.Rotate (0f, rotationSpeed, 0f, Space.Self);//Around (transform.position, Vector3.right, 20f * Time.deltaTime);

	}

}

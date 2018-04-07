using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {
	
	Vector3 forward, right;

	void Start()
	{
		forward = Camera.main.transform.forward; 

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += forward * 4f * Time.deltaTime;
	}
}

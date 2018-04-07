using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObjectOnLinecast : MonoBehaviour {

	MeshRenderer meshRenderer;
	Coroutine enableMeshRenderer;

	float enableMeshRendererAfterSeconds = 0f;

	void Start() {

		meshRenderer = GetComponent<MeshRenderer> ();

	}

	public void DisableMeshRenderer() {

		StopAllCoroutines ();

		meshRenderer.enabled = false;

		StartCoroutine ("EnableMeshRenderer");

	}

	IEnumerator EnableMeshRenderer() {

		yield return new WaitForSeconds (enableMeshRendererAfterSeconds);

		meshRenderer.enabled = true;

	}

}

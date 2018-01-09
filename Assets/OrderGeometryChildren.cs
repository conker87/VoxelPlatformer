using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrderGeometryChildren : MonoBehaviour {

	[SerializeField]
	List<MeshCollider> children = new List<MeshCollider>();

	float fPositionModifier = 125f;
	float fScaleModifier = 4f;

	[SerializeField]
	int iTransformsInRow, iTransformsColumn = 0, iTransformRow = 0;


	// Use this for initialization
	void OnEnable () {

		iTransformsColumn = iTransformRow = 0;

		// Clear List just in case.
		children.Clear ();

		// Itterate through the children then add them to the list.
		foreach (MeshCollider t in GetComponentsInChildren<MeshCollider>()) {

			children.Add (t);

		}

		iTransformsInRow = Mathf.FloorToInt (Mathf.Sqrt ((float)children.Count));

		for (int i = 0; i < children.Count; i++) {

			if (i % iTransformsInRow == 0) { 

				iTransformsColumn++;
				iTransformRow = 0;

			}

			children [i].transform.parent.position = new Vector3 ((float) iTransformsColumn * (fPositionModifier / fScaleModifier), 0f, (float) iTransformRow * (fPositionModifier / fScaleModifier));

			iTransformRow++;

		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the GameObject to inactive on start. Useful for disabling objects until they will be needed later.
/// </summary>
public class SetActiveOnStart : MonoBehaviour {

    public bool SetActive;
    public GameObject SetActiveGameObject;

	void Start () {

        SetActiveGameObject.SetActive(SetActive);

    }
}

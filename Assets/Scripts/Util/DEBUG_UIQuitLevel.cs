using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_UIQuitLevel : MonoBehaviour {

	void Start() {

		GetComponent<Button>().onClick.AddListener (SaveController.EndGame);

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton_SaveGame : MonoBehaviour {

	void Start() {

		GetComponent<Button>().onClick.AddListener (GameSaveController.SaveGame);

	}

}
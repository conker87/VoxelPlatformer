using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton_LoadGame : MonoBehaviour {

    string tempSaveGameLocation = "saveGameTest.xml";

	void Start() {

		GetComponent<Button>().onClick.AddListener (delegate { LoadSaveController.LoadMainGame(tempSaveGameLocation); });

	}

}

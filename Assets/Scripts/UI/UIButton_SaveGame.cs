using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton_SaveGame : MonoBehaviour {

    string tempSaveGameLocation = "saveGameTest.xml";

    void Start() {

        MainGameController.current.GatherSaveDetails();

	}

}
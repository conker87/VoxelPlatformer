using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICutsceneCanvas : MonoBehaviour {

    [SerializeField]
    Image imageTransform, speakingCharacter;
    [SerializeField]
    Text textTransform, currentLine;

    public Image SpeakingCharacter {
        get {
            return speakingCharacter;
        }

        set {
            speakingCharacter = value;
        }
    }
    public Text CurrentLine {
        get {
            return currentLine;
        }

        set {
            currentLine = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

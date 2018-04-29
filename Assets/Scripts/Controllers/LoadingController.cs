using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour {

    [SerializeField]
    GameObject levelParent;

    [SerializeField]
    List<Level> levelsToLoad = new List<Level>();

    public List<Level> LevelsToLoad {
        get {
            return levelsToLoad;
        }

        set {
            levelsToLoad = value;
        }
    }

    private void Start() {

        if (levelParent == null) {

            levelParent = GameObject.Find("LevelParent");

        }
    }

    private void OnTriggerEnter(Collider other) {
        
        if (levelsToLoad.Count == 0) {

            return;

        }

        if (other.GetComponentInParent<Player>() != null) {

            foreach (Level level in GameController.allLevels) {

                level.gameObject.SetActive(false);

                foreach (Level levelToLoad in LevelsToLoad) {

                    if (level == levelToLoad) {

                        levelToLoad.gameObject.SetActive(true);

                    }
                }
            }
        }
    }
}

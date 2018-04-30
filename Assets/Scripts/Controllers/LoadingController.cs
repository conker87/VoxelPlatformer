using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour {

    [SerializeField]
    GameObject levelParent;

    [SerializeField]
    List<Level> levelsToLoad = new List<Level>();
    bool loadedLevel = false;

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
        
        if (levelsToLoad.Count == 0 || GameController.current.allLevels.Count == 0 || GameController.current.allLevels == null) {

            return;

        }

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        foreach (Level level in GameController.current.allLevels) {

            loadedLevel = false;

            foreach (Level loadingLevel in levelsToLoad) {

                if (loadingLevel.LevelName == level.LevelName) {

                    level.LoadLevel();
                    loadedLevel = true;

                    break;

                }
            }

            if (loadedLevel == false && level.IsCurrentlyLoaded == true) {

                level.UnloadLevel();

            }

            //loadedLevel = false;

            //foreach (Level levelToLoad in LevelsToLoad) {

            //    if (level.LevelName == levelToLoad.LevelName) {

            //        loadedLevel = true;
            //        break;

            //    }
            //}

            //if (loadedLevel == false) {

            //    level.gameObject.SetActive(false);

            //} else {

            //    Debug.Log("gfgwe");
            //    level.gameObject.SetActive(true);

            //}
        }
    }
}

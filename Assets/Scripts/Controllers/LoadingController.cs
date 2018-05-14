using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProBuilder.Core;

public class LoadingController : MonoBehaviour {

    [SerializeField]
    GameObject levelParent;

    [SerializeField]
    List<Level> levelsToLoad = new List<Level>();
    bool loadedLevel = false;

    [SerializeField]
    Color backgroundColor;

    [SerializeField]
    pb_Object backgroundPlane;

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

        if (backgroundPlane == null) {

            backgroundPlane = GameObject.Find("BackgroundPlane").GetComponent<pb_Object>();

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

        ChangeBackgroundColor(backgroundColor);
    }

    void ChangeBackgroundColor(Color newColor) {

        Color color = newColor;

        // Cycle through each unique vertex in the cube (8 total), and assign a color to the index in the sharedIndices array.
        int si_len = backgroundPlane.sharedIndices.Length;
        Color[] vertexColors = new Color[si_len];

        for (int i = 0; i < si_len; i++) {

            vertexColors[i] = color;

        }

        // Now go through each face (vertex colors are stored the pb_Face class) and assign the pre-calculated index color to each index in the triangles array.
        Color[] colors = backgroundPlane.colors;

        for (int CurSharedIndex = 0; CurSharedIndex < backgroundPlane.sharedIndices.Length; CurSharedIndex++) {

            foreach (int CurIndex in backgroundPlane.sharedIndices[CurSharedIndex].array) {

                colors[CurIndex] = vertexColors[CurSharedIndex];

            }
        }

        backgroundPlane.SetColors(colors);

        // In order for these changes to take effect, you must refresh the mesh object.
        backgroundPlane.Refresh();

    }
}

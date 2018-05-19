using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using ProBuilder.Core;

public class Level : MonoBehaviour {

    #region Serialized Private Fields

    [Header("Level Details")]
    [SerializeField] string levelName = "";
    [SerializeField] string levelID = "";
    [SerializeField] List<Level> levelsToLoad = new List<Level>();
    [SerializeField] bool loadedLevel = false;
    [SerializeField] bool isCurrentlyLoaded = false;
    [SerializeField] Color backgroundColor;
    [SerializeField] pb_Object backgroundPlane;

    GameObject levelOffset;

    #endregion

    #region Encapsulated Public Fields

    public string LevelName {
        get {
            return levelName;
        }

        set {
            levelName = value;
        }
    }
    public string LevelID {
        get {
            return levelID;
        }

        set {
            levelID = value;
        }
    }
    public bool IsCurrentlyLoaded {
        get {
            return isCurrentlyLoaded;
        }

        set {
            isCurrentlyLoaded = value;
        }
    }
    public GameObject LevelOffset {
        get {
            return levelOffset;
        }

        set {
            levelOffset = value;
        }
    }

    #endregion

    // public List<Renderer> allLevelRenderers = new List<Renderer>();

    private void Start() {

        if (LevelOffset == null) {

            foreach (Transform go in GetComponentsInChildren<Transform>()) {

                if (go.name == "Offset") {

                    LevelOffset = go.gameObject;
                    break;

                }

            }

        }

        if (backgroundPlane == null) {

            backgroundPlane = GameObject.Find("BackgroundPlane").GetComponent<pb_Object>();

        }

    }

    private void OnTriggerEnter(Collider other) {

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        if (levelsToLoad.Count == 0 || MainGameController.current.levels.Count == 0 || MainGameController.current.levels == null) {

            return;

        }

        LoadAllLevels();

    }

    public void LoadLevel() {

        foreach (Renderer renderer in LevelOffset.GetComponentsInChildren<Renderer>(true)) {

            renderer.enabled = true;
        }

        // LevelOffset.SetActive(true);

        IsCurrentlyLoaded = true;

    }

    public void UnloadLevel() {

        foreach (Renderer renderer in LevelOffset.GetComponentsInChildren<Renderer>(true)) {

            renderer.enabled = false;
        }

        // LevelOffset.SetActive(false);

        IsCurrentlyLoaded = false;

    }

    void LoadAllLevels() {

        string debug_log = "";

        foreach (Level level in MainGameController.current.levels) {

            loadedLevel = false;

            foreach (Level loadingLevel in levelsToLoad) {

                if (loadingLevel.LevelName == level.LevelName) {

                    debug_log += level.LevelName + ", ";

                    level.gameObject.SetActive(true);
                    level.LoadLevel();
                    loadedLevel = true;

                    MainGameController.current.ShowLocationText(level.LevelName);

                    continue;

                }

            }

            if (loadedLevel == false) {

                level.UnloadLevel();

            }
        }

        Debug.LogFormat("You are in: {0}, plus, these should load: {1}", LevelName, debug_log);

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

using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class Level : MonoBehaviour {

    #region Serialized Private Fields

    [Header("Level Details")]
    [SerializeField]
    string levelName = "";
    string levelID = "";

    [SerializeField]
    bool isCurrentlyLoaded = false;

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

    #endregion

    public List<Renderer> allLevelRenderers = new List<Renderer>();

    private void Start() {

        AddRenderersToList();

    }

    void AddRenderersToList() {

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {

            allLevelRenderers.Add(renderer);

            if (LevelName != "Blockington Castle's Dungeon") {
                renderer.enabled = false;
            }
        }
    }

    public void LoadLevel() {

        foreach (Renderer renderer in allLevelRenderers) {

            renderer.enabled = true;
        }

        IsCurrentlyLoaded = true;

    }

    public void UnloadLevel() {

        foreach (Renderer renderer in allLevelRenderers) {

            renderer.enabled = false;
        }

        IsCurrentlyLoaded = false;

    }
}

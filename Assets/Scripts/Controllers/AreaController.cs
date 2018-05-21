using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class AreaController : MonoBehaviour {

    #region Serialized Private Fields

    [Header("Level Details")]
    [SerializeField] public string areaName = "";
    [SerializeField] string areaID = "";
    [SerializeField] bool isCurrentlyLoaded = false;
    [SerializeField] Transform levelOffset;

    #endregion

    #region Encapsulated Public Fields

    public string LevelName {
        get {
            return areaName;
        }

        set {
            areaName = value;
        }
    }
    public string LevelID {
        get {
            return areaID;
        }

        set {
            areaID = value;
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
    public Transform LevelOffset {
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

            LevelOffset = GetComponentsInChildren<Transform>().Single(a => a.gameObject.name == "Offset").transform;

        }
    }

    public void LoadArea() {

        if (LevelOffset == null) {

            return;

        }

        foreach (Renderer renderer in LevelOffset.GetComponentsInChildren<Renderer>(true)) {

            renderer.enabled = true;

        }

        IsCurrentlyLoaded = true;

    }

    public void UnloadArea() {

        if (LevelOffset == null) {

            return;

        }

        Debug.LogFormat("Area '{0}' is currently being unloaded, number of renderers being unloaded: {1}", areaName, LevelOffset.GetComponentsInChildren<Renderer>(true).Count());

        string rendererNames = "";

        foreach (Renderer renderer in LevelOffset.GetComponentsInChildren<Renderer>(true)) {

            renderer.enabled = false;
            rendererNames += renderer.gameObject.name;

        }

        Debug.Log(rendererNames);

        IsCurrentlyLoaded = false;

    }
}

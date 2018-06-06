using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using ProBuilder.Core;

public class AreaLoadController : MonoBehaviour {

    #region Serialized Private Fields

    [Header("Level Details")]
    [SerializeField] AreaController areaController;

    [SerializeField] List<AreaController> areasToLoad = new List<AreaController>();
    // [SerializeField] bool loadedLevel = false;
    [SerializeField] public string SubareaText = "";
    [SerializeField] bool isCurrentlyLoaded = false;
    [SerializeField] Color backgroundColor;
    #endregion

    #region Encapsulated Public Fields

    public bool IsCurrentlyLoaded {
        get {
            return isCurrentlyLoaded;
        }

        set {
            isCurrentlyLoaded = value;
        }
    }

    #endregion

    // public List<Renderer> allLevelRenderers = new List<Renderer>();

    private void Start() {

        areaController = GetComponentInParent<AreaController>();

        foreach (Collider col in GetComponents<Collider>()) {

            col.isTrigger = true;

        }
    }

    private void OnTriggerEnter(Collider other) {

        if (areasToLoad.Count == 0 || MainGameController.current.levels.Count == 0 || MainGameController.current.levels == null) {

            return;

        }

        if (MainGameController.current.SubAreaLocation == this) {

            return;

        }

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        //if (MainGameController.current.currentArea == areaLoaderIsConnectedTo) {

        // Debug.Log("MainGameController.current.currentArea == areaLoaderIsConnectedTo: " + other.transform.position + " called: " + other.gameObject.name, other.gameObject);

        //    MainGameController.current.ShowSubLocationText(SubareaText);
        //    return;

        //}

        // AreaCheck(areaLoaderIsConnectedTo, areasToLoad, backgroundColor, MainGameController.current.currentArea == areaLoaderIsConnectedTo, SubareaText);

        if (MainGameController.current.currentSubArea != this) {

            MainGameController.current.currentSubArea = this;
            MainGameController.current.ShowSubLocationText(SubareaText, 2f);

        }

        if (MainGameController.current.currentArea != areaController) {

            MainGameController.current.currentArea = areaController;
            MainGameController.current.ShowMainLocationText(areaController.AreaName, 2f);

        }

        AreaCheck(areaController, areasToLoad, backgroundColor);

    }

    void AreaCheck(AreaController _currentArea, List<AreaController> areasToLoad, Color backgroundColor) {

        string debug_log = "";
        bool loadedLevel = false;
        int i = 0;

        foreach (AreaController area in MainGameController.current.levels) {

            loadedLevel = false;
            i = 0;

            foreach (AreaController loadingArea in areasToLoad) {

                if (loadingArea.AreaName == area.AreaName) {

                    debug_log += area.AreaName + ", ";

                    area.LoadArea();
                    loadedLevel = true;

                }

                i++;
            }

            if (loadedLevel == true) {

                continue;

            }

            area.UnloadArea();

        }

        MainGameController.current.ChangeBackgroundColor(backgroundColor);

    }
}

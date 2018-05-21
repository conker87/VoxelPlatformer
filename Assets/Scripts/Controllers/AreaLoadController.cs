using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using ProBuilder.Core;

public class AreaLoadController : MonoBehaviour {

    #region Serialized Private Fields

    [Header("Level Details")]
    [SerializeField] AreaController areaLoaderIsConnectedTo;

    [SerializeField] List<AreaController> areasToLoad = new List<AreaController>();
    [SerializeField] bool loadedLevel = false;
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

        areaLoaderIsConnectedTo = GetComponentInParent<AreaController>();

        foreach (Collider col in GetComponents<Collider>()) {

            col.isTrigger = true;

        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        if (areasToLoad.Count == 0 || MainGameController.current.levels.Count == 0 || MainGameController.current.levels == null) {

            return;

        }

        Debug.Log("Player has been found & levels have been populated.");

        MainGameController.current.CheckIfInArea(areasToLoad, backgroundColor);

    }
}

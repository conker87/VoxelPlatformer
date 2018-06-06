using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class AreaController : MonoBehaviour {

    #region Serialized Private Fields

    [Header("Level Details")]
    [SerializeField] string areaName = "";
    [SerializeField] string areaID = "";
    [SerializeField] bool isCurrentlyLoaded = false;

    [SerializeField] Transform geometryPivot;

    #endregion

    #region Encapsulated Public Fields

    public string AreaName {
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

    #endregion

    public void LoadArea() {

        if (geometryPivot == null || geometryPivot.gameObject.activeInHierarchy == true) {

            return;

        }

        geometryPivot.gameObject.SetActive(true);

        IsCurrentlyLoaded = true;

    }

    public void UnloadArea() {

        if (geometryPivot == null || geometryPivot.gameObject.activeInHierarchy == false) {

            return;

        }

        geometryPivot.gameObject.SetActive(false);
        
        IsCurrentlyLoaded = false;

    }
}

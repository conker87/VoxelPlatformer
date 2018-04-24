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
    bool isCurrentLevel = false;

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
    public bool IsCurrentLevel {
        get {
            return isCurrentLevel;
        }

        set {
            isCurrentLevel = value;
        }
    }

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for any collectable that will be added to the GameController's Collectables list.
/// </summary>
public class Collectable : MonoBehaviour {

    #region Serialized Private Fields

    [SerializeField]
    string collectableID = "";
    [SerializeField]
    CollectableType collectableType;
    [SerializeField]
    bool collectableCollected = false;
    [SerializeField]
    GameObject gfxParent;

    #endregion

    #region Encapsulated Public Fields

    public string CollectableID {
        get {
            return collectableID;
        }

        set {
            collectableID = value;
        }
    }
    public CollectableType CollectableType {
        get {
            return collectableType;
        }

        set {
            collectableType = value;
        }
    }
    public bool CollectableCollected {
        get {
            return collectableCollected;
        }

        set {
            collectableCollected = value;
        }
    }
    public GameObject GFXParent {
        get {
            return gfxParent;
        }

        set {
            gfxParent = value;
        }
    }

    #endregion

    public Collectable(string _CollectableID, CollectableType _CollectableType, bool _CollectableCollected) {

        CollectableID = _CollectableID;
        CollectableType = _CollectableType;
        CollectableCollected = _CollectableCollected;
        GFXParent = null;

    }

    void Start() {

        if (string.IsNullOrEmpty(CollectableID) || CollectableID.Equals("") || CollectableID == "" || CollectableID == null) {

            CollectableID = CollectableType.ToString() + "_" + transform.position;

        }

        GetComponent<Collider>().enabled = true;

        CollectableCollected = MainGameController.current.HasPlayerCollectedCollectable(CollectableID);

        GetComponent<Collider>().enabled = !CollectableCollected;
        gfxParent.SetActive(!CollectableCollected);

    }

    void OnTriggerEnter(Collider other) {

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        CollectableCollected = true;

        LoadSaveController.LoadedCollectables.Add(new CollectableSave(this));

        if (CollectableType == CollectableType.Coin) {

            SFXController.current.PlayRandomCoinClip();

        }

        GetComponent<Collider>().enabled = false;
        gfxParent.SetActive(false);

    }
}

public enum CollectableType { Coin, Star, Ability };

[System.Serializable]
public class CollectableSave {

    public string CollectableID;
    public CollectableType CollectableType;
    public bool CollectableCollected;
    
    public CollectableSave(Collectable value) {

        CollectableID = value.CollectableID;
        CollectableType = value.CollectableType;
        CollectableCollected = value.CollectableCollected;

    }

    public CollectableSave(CollectableSave value) {

        CollectableID = value.CollectableID;
        CollectableType = value.CollectableType;
        CollectableCollected = value.CollectableCollected;

    }

    public CollectableSave(string _collectableID, CollectableType _collectableType, bool _collectableCollected) {

        CollectableID = _collectableID;
        CollectableType = _collectableType;
        CollectableCollected = _collectableCollected;

    }
}
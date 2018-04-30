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

    void Start() {

        if (string.IsNullOrEmpty(CollectableID) || CollectableID.Equals("") || CollectableID == "" || CollectableID == null) {

            // Debug.LogWarningFormat("Collectable at position: {0} has no CollectableID. A temporary ID has been given", transform.position);

            CollectableID = CollectableType.ToString() + "_" + transform.position;

        }
	}

    private void OnEnable() {

        GetComponent<Collider>().enabled = true;


        if (GameController.current.CurrentSaveState == SaveState.NewGame) {

            CollectableCollected = false;

        }

        GetComponent<Collider>().enabled = !CollectableCollected;
        gfxParent.SetActive(!CollectableCollected);

    }

    void OnTriggerEnter(Collider other) {

        if (other.GetComponentInParent<Player>() == null) {
            return;
        }

        CollectableListValue newCollectableListValue = new CollectableListValue(CollectableID, CollectableType);

        GameController.current.AddToCollectables(newCollectableListValue);

        CollectableCollected = true;

        if (CollectableType == CollectableType.Coin) {

            SFXController.current.PlayRandomCoinClip();

        }

        GetComponent<Collider>().enabled = false;
        gfxParent.SetActive(false);

    }

	bool HasCollectedCollectable() {

        foreach (CollectableListValue value in GameController.current.Collectables) {

            if (value.CollectableID == CollectableID) {

                return true;

            }
        }

        return false;

    }
}

[System.Serializable]
public class CollectableListValue {

    public string CollectableID;
    public CollectableType CollectableType;

    public CollectableListValue(CollectableListValue value) {

        CollectableID = value.CollectableID;
        CollectableType = value.CollectableType;

    }

    public CollectableListValue(string collectableID, CollectableType collectableType) {

        CollectableID = collectableID;
        CollectableType = collectableType;

    }
}

public enum CollectableType { Coin, Star, Ability };
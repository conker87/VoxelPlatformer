using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent_OnTriggerEnter : MonoBehaviour {

    [SerializeField]
    Transform levelsParent, pivotTransform;
    Transform player;

	// Use this for initialization
	void Start () {

        if (levelsParent == null) {

            levelsParent = GameObject.Find("LevelParent").transform;

        }

    }

    private void OnTriggerEnter(Collider other) {

        if (levelsParent == null || pivotTransform == null) {

            return;

        }

        if (other.GetComponentInParent<Player>() != null) {

            player = other.GetComponentInParent<Player>().transform;

            player.parent = pivotTransform;
            
        }
    }

    private void OnTriggerExit(Collider other) {

        if (levelsParent == null || pivotTransform == null) {

            return;

        }

        if (other.GetComponentInParent<Player>() != null) {

            player = other.GetComponentInParent<Player>().transform;

            player.parent = levelsParent;

        }
    }
}

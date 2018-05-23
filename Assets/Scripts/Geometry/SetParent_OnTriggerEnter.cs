using System.Collections;
using System.Collections.Generic;

using System.Linq;

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

        if (pivotTransform == null) {

            pivotTransform = GetComponentsInChildren<Transform>().First(a => a.gameObject.name == "Pivot");

        }

    }

    void OnTriggerEnter(Collider other) {

        if (MainGameController.current.player == null || levelsParent == null || pivotTransform == null) {

            return;

        }

        if (MainGameController.current.player.GetComponent<CharController>().IsGrounded == false) {

            return;

        }

        if (other.GetComponentInParent<Player>() != null) {

            player = other.GetComponentInParent<Player>().transform;

            player.parent = pivotTransform;
            
        }
    }

    void OnTriggerStay(Collider other) {

        if (MainGameController.current.player == null || levelsParent == null || pivotTransform == null) {

            return;

        }

        if (MainGameController.current.player.GetComponent<CharController>().IsGrounded == false) {

            if (other.GetComponentInParent<Player>() != null) {

                // Debug.Log("Setting Player to elevator.");

                player = other.GetComponentInParent<Player>().transform;

                player.parent = levelsParent;

            }
        } else {

            if (other.GetComponentInParent<Player>() != null) {

                player = other.GetComponentInParent<Player>().transform;

                player.parent = pivotTransform;

            }
        }

    }

    void OnTriggerExit(Collider other) {

        if (MainGameController.current.player == null || levelsParent == null || pivotTransform == null) {

            return;

        }

        if (other.GetComponentInParent<Player>() != null) {

            player = other.GetComponentInParent<Player>().transform;

            player.parent = levelsParent;

        }
    }
}

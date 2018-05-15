using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSavingGameInTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        GameController.current.CanCurrentSaveGame = false;

    }

    private void OnTriggerExit(Collider other) {

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        GameController.current.CanCurrentSaveGame = true;

    }

}

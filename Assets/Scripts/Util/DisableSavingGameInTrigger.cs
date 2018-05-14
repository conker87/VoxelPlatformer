using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSavingGameInTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {

        GameController.current.CanCurrentSaveGame = false;

    }

    private void OnTriggerExit(Collider other) {

        GameController.current.CanCurrentSaveGame = true;

    }

}

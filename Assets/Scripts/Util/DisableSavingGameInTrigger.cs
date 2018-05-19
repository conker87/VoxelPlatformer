using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSavingGameInTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        MainGameController.current.SavingGameAllowed = false;

    }

    private void OnTriggerExit(Collider other) {

        if (other.GetComponentInParent<Player>() == null) {

            return;

        }

        MainGameController.current.SavingGameAllowed = true;

    }

}

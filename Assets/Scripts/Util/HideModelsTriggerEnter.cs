using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideModelsTriggerEnter : MonoBehaviour {

    [SerializeField]
    string hideTag = "";

    [SerializeField]
    float zOffset = 2f, yOffset = 5f;

    Renderer otherRenderer;

    AreaController rendererLevel;

    private void OnTriggerEnter(Collider other) {

        otherRenderer = other.GetComponent<Renderer>();
        if (MainGameController.current.player == null || otherRenderer == null || other.gameObject.tag != hideTag) {

            return;

        }

        otherRenderer.enabled = false;
        return;

        /// I literally had to draw a diagram for this as it was pretty hard to imagine:
        ///     The camera is at x+,z-, we want all blocks (with the tag) to be made invisible if they are:
        ///         - Towards the camera (x+,z-),
        ///         - To the right of the camera (x+,z+),
        ///     And not if they are behind the player at any point:
        ///         - (x-,z-),
        ///         - (x-,z+).
        if ((otherRenderer.transform.position.x > MainGameController.current.player.transform.position.x
                 && otherRenderer.transform.position.z < MainGameController.current.player.transform.position.z + zOffset)
             //|| (otherRenderer.transform.position.x > GameController.current.Player.transform.position.x
             //    && otherRenderer.transform.position.z > GameController.current.Player.transform.position.z)
             || otherRenderer.transform.position.y > MainGameController.current.player.transform.position.y + yOffset) {

            otherRenderer.enabled = false;
            return;

        }
    }

    private void OnTriggerStay(Collider other) {

        // OnTriggerEnter(other);

    }

    private void OnTriggerExit(Collider other) {

        otherRenderer = other.GetComponent<Renderer>();
        if (MainGameController.current.player == null || otherRenderer == null || other.gameObject.tag != hideTag) {

            return;

        }

        otherRenderer.enabled = true;

    }
}

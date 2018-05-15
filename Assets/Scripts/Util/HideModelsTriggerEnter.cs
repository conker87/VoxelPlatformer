using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideModelsTriggerEnter : MonoBehaviour {

    [SerializeField]
    string hideTag = "";

    Renderer otherRenderer;

    private void OnTriggerEnter(Collider other) {
     
        if (other.gameObject.tag != hideTag) {

            return;

        }

        otherRenderer = other.GetComponent<Renderer>();

        if (otherRenderer == false) {

            return;

        }

        if (otherRenderer.enabled == false) {

            return;

        }

        otherRenderer.enabled = false;

    }

    private void OnTriggerStay(Collider other) {

        OnTriggerEnter(other);

    }

    private void OnTriggerExit(Collider other) {

        if (other.gameObject.tag != hideTag) {

            return;

        }

        otherRenderer = other.GetComponent<Renderer>();

        if (otherRenderer == false) {

            return;

        }

        otherRenderer.enabled = true;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PressurePlate_OnTriggers : MonoBehaviour {

    // TODO: Have anims, probably just Vector3.Lerp from current Position to one like 0.5 units down or some shit.

    string moveableBlockName = "";
    public string MoveableBlockName {
        get {
            return moveableBlockName;
        }

        set {
            moveableBlockName = value;
        }
    }

    [SerializeField]
    Interactable_Triggers_PressurePlate ITP;

    private void Start() {

        if (ITP == null) {
            ITP = GetComponentInParent<Interactable_Triggers_PressurePlate>();
        }
        moveableBlockName = ITP.MoveableBlockName;

    }

    protected void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag.Equals(MoveableBlockName)) {

            ITP.OnTriggerEnter(other);

        }

    }

    protected void OnTriggerStay(Collider other) {

        if (other.gameObject.tag.Equals(MoveableBlockName)) {

            ITP.OnTriggerStay(other);

        }

    }

    protected void OnTriggerExit(Collider other) {

        if (other.gameObject.tag.Equals(MoveableBlockName)) {

            ITP.OnTriggerExit(other);

        }

    }

}

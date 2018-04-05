using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Triggers_PressurePlate : Interactable_Triggers {

    [SerializeField]
    string moveableBlockName = "";
    public string MoveableBlockName {
        get {
            return moveableBlockName;
        }

        set {
            moveableBlockName = value;
        }
    }

    public override void Interact(bool playerInteracting = true) {



    }
}

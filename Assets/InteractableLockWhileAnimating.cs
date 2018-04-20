using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLockWhileAnimating : MonoBehaviour {

    Interactable interactable;
    public bool interactabledLocked;

    public void Start() {
        interactable = GetComponentInParent<Interactable>();
    }

    public void Update() {
        
        if (interactable == null) {
            return;
        }

        interactable.IsLocked = interactabledLocked;

    }

}

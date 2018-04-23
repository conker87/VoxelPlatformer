using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLockWhileAnimating : MonoBehaviour {

    [SerializeField]
    Interactable interactable;
    public bool interactabledLocked;

    public void Start() {

        if (interactable == null) {
            interactable = GetComponentInParent<Interactable>();
        }
    }

    public void Update() {
        
        if (interactable == null) {
            return;
        }

        Debug.Log(string.Format("{0}", interactable));
        interactable.IsLocked = interactabledLocked;

    }

}

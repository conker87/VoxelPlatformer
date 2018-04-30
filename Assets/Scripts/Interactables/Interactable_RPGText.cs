using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class must be given to Interactables that will be interacted by other Interactables.
/// </summary>
public class Interactable_RPGText : Interactable {

    public string FluffText = "";

    public override void Interact(InteractableTrigger interactableTrigger = null, bool playerInteracting = false) {

        base.Interact(interactableTrigger, playerInteracting);

        if (string.IsNullOrEmpty(FluffText) == false) {
            Debug.Log(FluffText);
        }

    }

}

using System;

[Serializable]
public class InteractableTrigger {

    public Interactable InteractableToTrigger;

    public InteractableTriggerCauses InteractableTriggerCause;
    public InteractableTriggerEffect InteractableTriggerEffect;

    public float InteractableTriggerValue = 0f;


    /// <summary>
    ///  Use this to invert the Trigger's clause. Will only work with the following:
    ///     OnTriggerStay: Only when the Player is not within the trigger boundaries.
    ///     OnTriggerDistance: Only when the Player is not within the Distance from GameObject.
    ///     OnTriggerTimeSinceLevelStart: Only when the time is under the given value.
    /// </summary>
    public bool Invert;

    /// <summary>
    /// Set to true to allow the InteractableToTrigger
    /// to activate its Interact();
    /// </summary>
    public bool CauseTriggerEffect;

    public InteractableTrigger() {

    }

    public InteractableTrigger(InteractableTrigger value) {

        InteractableToTrigger = value.InteractableToTrigger;
        InteractableTriggerCause = value.InteractableTriggerCause;
        InteractableTriggerEffect = value.InteractableTriggerEffect;
        InteractableTriggerValue = value.InteractableTriggerValue;
        Invert = value.Invert;
        CauseTriggerEffect = value.CauseTriggerEffect;

    }

    public InteractableTrigger(Interactable interactableToTrigger, InteractableTriggerCauses interactableTriggerCause, InteractableTriggerEffect interactableTriggerEffect, float interactableTriggerValue, bool invert, bool triggerEffect) {

        InteractableToTrigger = interactableToTrigger;
        InteractableTriggerCause = interactableTriggerCause;
        InteractableTriggerEffect = interactableTriggerEffect;
        InteractableTriggerValue = interactableTriggerValue;
        Invert = invert;
        CauseTriggerEffect = triggerEffect;

    }

}

public enum InteractableTriggerEffect { TurnOn, Toggle, TurnOff };
public enum InteractableTriggerCauses { OnTriggerEnter, OnTriggerStay, OnTriggerExit, OnTriggerInteract, OnTriggerOverlapSphere, OnTriggerTimeSinceLevelStart, OnTriggerRepeatTime };
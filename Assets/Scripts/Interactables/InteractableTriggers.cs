using System;

[Serializable]
public class InteractableTrigger {

    public Interactable InteractableToTrigger;

    public InteractableTriggerEffect InteractableTriggerEffect;
    public InteractableTriggerCauses[] InteractableTriggerCauses;

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
        InteractableTriggerEffect = value.InteractableTriggerEffect;
        InteractableTriggerCauses = value.InteractableTriggerCauses;
        Invert = value.Invert;
        CauseTriggerEffect = value.CauseTriggerEffect;

    }

    public InteractableTrigger(Interactable interactableToTrigger, InteractableTriggerEffect interactableTriggerEffect, InteractableTriggerCauses[] interactableTriggerCauses, bool invert, bool triggerEffect) {

        InteractableToTrigger = interactableToTrigger;
        InteractableTriggerEffect = interactableTriggerEffect;
        InteractableTriggerCauses = interactableTriggerCauses;
        Invert = invert;
        CauseTriggerEffect = triggerEffect;

    }

}

public enum InteractableTriggerEffect { TurnOn, Toggle, TurnOff };
public enum InteractableTriggerCauses { OnTriggerEnter, OnTriggerStay, OnTriggerExit, OnTriggerInteract, OnTriggerTimeSinceLevelStart, OnTriggerRepeatTime };
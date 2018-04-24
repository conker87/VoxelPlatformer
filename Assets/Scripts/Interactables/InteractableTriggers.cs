using System;

[Serializable]
public class InteractableTrigger {

    public Interactable InteractableToTrigger;

    public InteractableTriggerCauses InteractableTriggerCause;
    public InteractableTriggerEffect InteractableTriggerEffect;
    public InteractableTriggerAction InteractableTriggerAction;

    [UnityEngine.Tooltip(@"This is used differently depending on the InteractableTriggerCause chosen:
    • OnTriggerEnter, OnTriggerExit, OnTriggerInteract: No effect.
    • OnTriggerStay: The cooldown between checking for next Stay.
    • OnTriggerOverlapSphere: Radius of sphere to check.
    • OnTriggerTimeSinceLevelStart: Time from the level start.
    • OnTriggerRepeatTime: After every this time.")]
    public float InteractableTriggerValue;

    /// <summary>
    ///  Use this to invert the Trigger's clause. Will only work with the following:
    ///     OnTriggerOverlapSphere: Only when the Player is not within the trigger boundaries.
    ///     OnTriggerTimeSinceLevelStart: Only when the time is under the given value.
    /// </summary>
    public bool Invert;

    /// <summary>
    /// Set to true to allow the InteractableToTrigger
    /// to activate its Interact();
    /// </summary>
    public bool DontCauseTriggerEffect;

    public InteractableTrigger() {

    }

    public InteractableTrigger(InteractableTrigger value) {

        InteractableToTrigger = value.InteractableToTrigger;
        InteractableTriggerCause = value.InteractableTriggerCause;
        InteractableTriggerEffect = value.InteractableTriggerEffect;
        InteractableTriggerValue = value.InteractableTriggerValue;
        InteractableTriggerAction = value.InteractableTriggerAction;
        Invert = value.Invert;
        DontCauseTriggerEffect = value.DontCauseTriggerEffect;

    }

    public InteractableTrigger(Interactable interactableToTrigger, InteractableTriggerCauses interactableTriggerCause, 
        InteractableTriggerEffect interactableTriggerEffect, InteractableTriggerAction interactableTriggerAction,
        float interactableTriggerValue, bool invert, bool dontCauseTriggerEffect) {

        InteractableToTrigger = interactableToTrigger;
        InteractableTriggerCause = interactableTriggerCause;
        InteractableTriggerEffect = interactableTriggerEffect;
        InteractableTriggerValue = interactableTriggerValue;
        InteractableTriggerAction = interactableTriggerAction;
        Invert = invert;
        DontCauseTriggerEffect = dontCauseTriggerEffect;

    }

}

public enum InteractableTriggerEffect { Toggle, Activate, Deactivate };
public enum InteractableTriggerCauses { OnTriggerEnter, OnTriggerStay, OnTriggerExit, OnTriggerInteract, OnTriggerOverlapSphere, OnTriggerTimeSinceLevelStart, OnTriggerRepeatTime };
public enum InteractableTriggerAction { Interact, Lock };
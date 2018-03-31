using System;

[Serializable]
public class InteractableTrigger {

    public Interactable InteractableToTrigger;
    public bool OnTriggerEnter;
    public bool OnTriggerStay;
    public bool OnTriggerExit;
    public bool OnInteract;

/// <summary>
    /// Set to true to allow the InteractableToTrigger
    /// to activate its Interact();
    /// </summary>
    public bool CauseTriggerEffect;

    public InteractableTrigger() {

    }

    public InteractableTrigger(InteractableTrigger value) {
        InteractableToTrigger = value.InteractableToTrigger;
        OnTriggerEnter = value.OnTriggerEnter;
        OnTriggerStay = value.OnTriggerStay;
        OnTriggerExit = value.OnTriggerExit;
        OnInteract = value.OnInteract;
        CauseTriggerEffect = value.CauseTriggerEffect;
    }

    public InteractableTrigger(Interactable interactableToTrigger, bool triggerEnter, bool triggerStay, bool triggerExit, bool onInteract,
        bool triggerEffect) {
        InteractableToTrigger = interactableToTrigger;
        OnTriggerEnter = triggerEnter;
        OnTriggerStay = triggerStay;
        OnTriggerExit = triggerExit;
        OnInteract = onInteract;
        CauseTriggerEffect = triggerEffect;
    }

}

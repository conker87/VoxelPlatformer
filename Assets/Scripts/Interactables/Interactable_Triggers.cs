using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Triggers : Interactable {

    #region Serialized Private Fields

    [HideInInspector]
    public bool hasOnTriggerEnter, hasOnTriggerStay, hasOnTriggerExit, hasTriggerOverlapSphere, hasTriggerTimeSinceLevelStart, hasTriggerRepeatTime;
    
    [SerializeField]
    private float onTriggerStayCooldown = 1f;
    protected float onTriggerStayTime, onTriggerUpdateTime, onTriggerRepeatTime;

    [SerializeField]
    private List<InteractableTrigger> interactableTriggers = new List<InteractableTrigger>();
    #endregion

    #region Encapsulated Public Fields

    public float OnTriggerStayCooldown {
        get {
            return onTriggerStayCooldown;
        }

        set {
            onTriggerStayCooldown = value;
        }
    }

    public List<InteractableTrigger> InteractableTriggers {
        get {
            return interactableTriggers;
        }

        set {
            interactableTriggers = value;
        }
    }

    #endregion

    protected override void Start() {

        base.Start();

        // Check to see if the list of triggers has a OnTriggerOverlapSphere, OnTriggerTimeSinceLevelStart or OnTriggerRepeatTime
        foreach (InteractableTrigger interactableTrigger in InteractableTriggers) {

            if (interactableTrigger.InteractableToTrigger == null) {
                continue;
            }

            interactableTrigger.InteractableToTrigger.TriggersConnectedToMe.Add(this);

            if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerEnter) {

                hasOnTriggerEnter = true;
                continue;

            }

            if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerStay) {

                hasOnTriggerStay = true;
                continue;

            }

            if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerExit) {

                hasOnTriggerExit = true;
                continue;

            }

            if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerOverlapSphere) {

                hasTriggerOverlapSphere = true;
                continue;

            }


            if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerTimeSinceLevelStart) {

                hasTriggerTimeSinceLevelStart = true;
                continue;

            }

            if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerRepeatTime) {

                hasTriggerRepeatTime = true;
                continue;

            }

        }

        Debug.LogWarning(string.Format("{0} is an Interactable_Trigger and has not been sorted out in regards to IsActivated.", this.InteractableID));

    }

    protected override void Update() {

        base.Update();

        if ((hasTriggerOverlapSphere == true || hasTriggerTimeSinceLevelStart == true || hasTriggerRepeatTime == true)
            && Time.time > onTriggerUpdateTime) {

            Debug.Log(string.Format("This should only execute if one+ of these three values are true: {0}, {1}, {2}",
                hasTriggerOverlapSphere,
                hasTriggerTimeSinceLevelStart,
                hasTriggerRepeatTime));

            foreach (InteractableTrigger interactableTrigger in InteractableTriggers) {

                if (interactableTrigger.InteractableToTrigger == null) {
                    continue;
                }

                if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerOverlapSphere) {

                    Collider[] overlappedSphere = Physics.OverlapSphere(transform.position, interactableTrigger.InteractableTriggerValue);

                    bool foundPlayer = false;

                    if (overlappedSphere != null && overlappedSphere.Length > 0) {

                        foreach (Collider coll in overlappedSphere) {

                            if (coll.GetComponent<Player>() != null) {

                                foundPlayer = true;

                                interactableTrigger.InteractableToTrigger.Interact(interactableTrigger, false);

                            }

                        }

                        if (foundPlayer == false && interactableTrigger.Invert == true) {

                            interactableTrigger.InteractableToTrigger.Interact(interactableTrigger, false);

                        }

                    }

                }
                else if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerTimeSinceLevelStart) {

                    if (GameController.current.currentTime < interactableTrigger.InteractableTriggerValue && interactableTrigger.Invert == true) {

                        interactableTrigger.InteractableToTrigger.Interact(interactableTrigger, false);

                    }
                    else if (GameController.current.currentTime > interactableTrigger.InteractableTriggerValue) {

                        interactableTrigger.InteractableToTrigger.Interact(interactableTrigger, false);

                    }

                }
                else if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerRepeatTime) {

                    if (Time.time > onTriggerRepeatTime) {

                        interactableTrigger.InteractableToTrigger.Interact(interactableTrigger, false);

                        onTriggerRepeatTime = Time.time + interactableTrigger.InteractableTriggerValue;

                    }

                }

            }

            onTriggerUpdateTime = Time.time + 1f;

        }

    }

    public override void Interact(InteractableTrigger interactableTrigger, bool playerInteracting = false) {

        base.Interact(interactableTrigger, playerInteracting);

        if (interactableTrigger.DontCauseTriggerEffect == true) {
            CanContinue = false;
        }

        if (CanContinue == true) {

            foreach (InteractableTrigger interactableTriggerItem in InteractableTriggers) {

                if (interactableTriggerItem.InteractableToTrigger == null) {
                    continue;
                }

                if (interactableTriggerItem.InteractableTriggerCause != interactableTrigger.InteractableTriggerCause) {
                    continue;
                }

                interactableTriggerItem.InteractableToTrigger.Interact(interactableTriggerItem, false);

            }
        }
    }

    public void OnTriggerEnter(Collider other) {

        Debug.Log("Interactable_Triggers::OnTriggerEnter");

        IsActivated = false;

        foreach (InteractableTrigger interactableTrigger in InteractableTriggers) {

            if (interactableTrigger.InteractableToTrigger == null) {
                continue;
            }

            if (interactableTrigger.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerEnter) {
                continue;
            }

            IsActivated = true;

            interactableTrigger.InteractableToTrigger.Interact(interactableTrigger, false);

        }

    }

    public void OnTriggerStay(Collider other) {

        if (hasOnTriggerStay == false) {
            return;
        }

        if (onTriggerStayTime > Time.time) {

            return;

        }

        Debug.Log("Interactable_Triggers::OnTriggerStay");

        IsActivated = false;

        foreach (InteractableTrigger interactableTrigger in InteractableTriggers) {

            if (interactableTrigger.InteractableToTrigger == null) {
                continue;
            }

            if (interactableTrigger.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerStay) {
                continue;
            }

            IsActivated = true;

            interactableTrigger.InteractableToTrigger.Interact(interactableTrigger, false);

        }

        onTriggerStayTime = Time.time + onTriggerStayCooldown;

    }

    public void OnTriggerExit(Collider other) {

        Debug.Log("Interactable_Triggers::OnTriggerExit");

        IsActivated = false;

        foreach (InteractableTrigger interactableTrigger in InteractableTriggers) {

            if (interactableTrigger.InteractableToTrigger == null) {
                continue;
            }

            if (interactableTrigger.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerExit) {
                continue;
            }

            interactableTrigger.InteractableToTrigger.Interact(interactableTrigger, false);

        }

    }

}

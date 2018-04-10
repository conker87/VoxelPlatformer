using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Triggers : Interactable {

    #region Serialized Private Fields

    private bool hasTriggerOverlapSphere, hasTriggerTimeSinceLevelStart, hasTriggerRepeatTime;
    
    [SerializeField]
    private float onTriggerStayCooldown = 1f;
    protected float onTriggerStayTime;
    protected float onTriggerUpdateTime;
    protected float onTriggerRepeatTime;


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
        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null) {
                continue;
            }

            if (interactable.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerOverlapSphere) {

                hasTriggerOverlapSphere = true;
                continue;

            }


            if (interactable.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerTimeSinceLevelStart) {

                hasTriggerTimeSinceLevelStart = true;
                continue;

            }

            if (interactable.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerRepeatTime) {

                hasTriggerRepeatTime = true;
                continue;

            }

        }

    }

    private void Update() {

        if ((hasTriggerOverlapSphere == true || hasTriggerTimeSinceLevelStart == true || hasTriggerRepeatTime == true)
            && Time.time > onTriggerUpdateTime) {

            Debug.Log(string.Format("This should only execute if one+ of these three values are true: {0},{1}, {2}",
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

                                interactableTrigger.InteractableToTrigger.Interact(true,
                                    interactableTrigger.InteractableTriggerCause,
                                    interactableTrigger.InteractableTriggerEffect,
                                    interactableTrigger.DontCauseTriggerEffect);

                            }

                        }

                        if (foundPlayer == false && interactableTrigger.Invert == true) {

                            interactableTrigger.InteractableToTrigger.Interact(true,
                                interactableTrigger.InteractableTriggerCause,
                                interactableTrigger.InteractableTriggerEffect,
                                interactableTrigger.DontCauseTriggerEffect);

                        }

                    }

                }
                else if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerTimeSinceLevelStart) {

                    if (GameController.current.currentTime < interactableTrigger.InteractableTriggerValue && interactableTrigger.Invert == true) {

                        interactableTrigger.InteractableToTrigger.Interact(true,
                            interactableTrigger.InteractableTriggerCause,
                            interactableTrigger.InteractableTriggerEffect,
                            interactableTrigger.DontCauseTriggerEffect);

                    }
                    else if (GameController.current.currentTime > interactableTrigger.InteractableTriggerValue) {

                        interactableTrigger.InteractableToTrigger.Interact(true,
                            interactableTrigger.InteractableTriggerCause,
                            interactableTrigger.InteractableTriggerEffect,
                            interactableTrigger.DontCauseTriggerEffect);

                    }

                }
                else if (interactableTrigger.InteractableTriggerCause == InteractableTriggerCauses.OnTriggerRepeatTime) {

                    if (Time.time > onTriggerRepeatTime) {

                        interactableTrigger.InteractableToTrigger.Interact(true,
                            interactableTrigger.InteractableTriggerCause,
                            interactableTrigger.InteractableTriggerEffect,
                            interactableTrigger.DontCauseTriggerEffect);

                        onTriggerRepeatTime = Time.time + interactableTrigger.InteractableTriggerValue;

                    }

                }

            }

            onTriggerUpdateTime = Time.time + 1f;

        }

    }

    public override void Interact(bool playerInteracting = false,
        InteractableTriggerCauses interactableTriggerCauses = InteractableTriggerCauses.OnTriggerInteract,
        InteractableTriggerEffect interactableTriggerEffect = InteractableTriggerEffect.Toggle,
        bool dontCauseTriggerEffect = false) {

        if (OneTimeUse == true && HasBeenUsedOnce == true) {
            return;
        }

        if (CanOnlyInteractFromOtherInteractables == true && playerInteracting == true) {
            return;
        }

        switch (interactableTriggerEffect) {
            case InteractableTriggerEffect.TurnOff:
                IsOn = false;
                break;
            case InteractableTriggerEffect.TurnOn:
                IsOn = true;
                break;
            default:
            case InteractableTriggerEffect.Toggle:
                IsOn = !IsOn;
                break;
        }

         if (Anim != null) {
            Anim.SetBool("IsOn", IsOn);
        }

        if (OneTimeUse == true) {
            HasBeenUsedOnce = true;
        }

        if (dontCauseTriggerEffect == true) {
            return;
        }

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null) {
                continue;
            }

            if (interactable.InteractableTriggerCause != interactableTriggerCauses) {
                continue;
            }

            interactable.InteractableToTrigger.Interact(false, interactableTriggerCauses, interactable.InteractableTriggerEffect);

        }

    }

    public void OnTriggerEnter(Collider other) {

        Debug.Log("OnTriggerEnter");

        foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null) {
                continue;
            }

            if (interactable.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerEnter) {
                continue;
            }

            Debug.Log("OnTriggerEnter -- Able");

            interactable.InteractableToTrigger.Interact(false, InteractableTriggerCauses.OnTriggerEnter, interactable.InteractableTriggerEffect);

        }

    }

    public void OnTriggerStay(Collider other) {

        foreach (InteractableTrigger interactableTrigger in InteractableTriggers) {

            if (Time.time > onTriggerStayTime) {
                break;
            }

            if (interactableTrigger.InteractableToTrigger == null) {
                continue;
            }

            if (interactableTrigger.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerStay) {
                continue;
            }

            interactableTrigger.InteractableToTrigger.Interact(false, interactableTrigger.InteractableTriggerCause, interactableTrigger.InteractableTriggerEffect);

            onTriggerStayTime = Time.time + interactableTrigger.InteractableTriggerValue;

        }

    }

    public void OnTriggerExit(Collider other) {

         foreach (InteractableTrigger interactable in InteractableTriggers) {

            if (interactable.InteractableToTrigger == null) {
                continue;
            }

            if (interactable.InteractableTriggerCause != InteractableTriggerCauses.OnTriggerExit) {
                continue;
            }

            interactable.InteractableToTrigger.Interact(false, InteractableTriggerCauses.OnTriggerExit, interactable.InteractableTriggerEffect);

        }

    }

}

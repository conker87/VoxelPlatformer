using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public string InteractableID = "";
	public bool OneTimeUse = false, HasBeenUsedOnce = false, canOnlyInteractFromOtherInteractables = false;

	public bool isOn = false;

	protected bool canContinue = true;

	public virtual void Interact (bool directlyInteracting = false) {

		if (canOnlyInteractFromOtherInteractables == true && directlyInteracting == false) {

			canContinue = false;

		}

	}

}

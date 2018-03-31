using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Switch : Interactable {

	/* public Interactable[] Connections;

	public void Interact (bool directlyInteracting = false) {

		base.Interact (directlyInteracting);

		if (OneTimeUse == true && HasBeenUsedOnce == true) {

			canContinue = false;

		}

		if (canContinue == false) {

			return;

		}

		isOn = !isOn;

		if (OneTimeUse == true) {

			HasBeenUsedOnce = true;

		}

		if (isOn == true) {
			
			foreach (InteractableTriggers interactable in TriggerList) {

				interactable.InteractableToTrigger.Interact ();

			}

		}

	} */

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Switch : Interactable {

	public Interactable[] Connections;

	void Update() {

	}

	public override void Interact (bool directlyInteracting = false) {

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
			
			foreach (Interactable interactable in Connections) {

				interactable.Interact ();

			}

		}

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Switch : Interactable {

	public Interactable[] Connections;

	[SerializeField]
	bool isOn = false, doInteracts = false;

	void Update() {

		if (isOn == true) {

			if (doInteracts == true) {

				foreach (Interactable interactable in Connections) {

					interactable.Interact ();

				}

				doInteracts = false;

			}

		}

	}

	public override void Interact () {

		if (OneTimeUse == true && HasBeenUsedOnce == true) {

			return;

		}

		isOn = !isOn;

		if (OneTimeUse == true) {

			HasBeenUsedOnce = true;

		}

		if (isOn == true) {
			
			doInteracts = true;

		}

	}

}

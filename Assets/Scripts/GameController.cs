using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController current;

	[SerializeField]
	public List<float> BestLevelTimes = new List<float> ();
	[SerializeField]
	public List<bool> OpenedLevels = new List<bool>();

	void Awake() {

		if (current == null) {

			current = this;

		}

	}

}

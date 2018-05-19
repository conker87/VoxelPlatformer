using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIElements : MonoBehaviour {

 //   // Create the Singleton for this class.
 //   public static UIElements current = null;
 //   void Awake() {

 //       if (current == null) {

 //           current = this;

 //       }
 //       else if (current != this) {

 //           Destroy(gameObject);

 //       }
 //   }

 //   [Header("Canvases")]
	//public Transform IngameLevelLoadedCanvas;
	//public Transform IngameLevelLoadedPausedCanvas;
 //   public Transform CutsceneCanvas;

	//[Header("Level Loaded Canvas")]
	//public TextMeshProUGUI NumberOfCoins;
	//public TextMeshProUGUI NumberOfStars, Health, TimeT, RPGFluff, Location;
 //   Coroutine RPGFluffFadeOut, LocationTextFadeout;

	//[Header("Menu Options Canvas")]
	//public Dropdown QualityPresetsDropdown;
	//public Dropdown GameResolutionDropdown;
	//public Toggle FullscreenToggle;

	//// Not usable currently?
	//public float resolutionScale = 1f;

	//void Start() {


	//	// QualityPresets
	//	PopulateQualityPresetsDropdown ();
	//	PopulateGameResolutionDropdown ();

	//	FullscreenToggle.isOn = Screen.fullScreen;

 //       // Add listeners to all the allocated Buttons.


 //   }

	//void Update() {

 //       if (GameController.current == null) {

 //           return;

 //       }

 //       if (GameController.current.justChangedState) {

	//		IngameLevelLoadedCanvas.gameObject.SetActive (false);
	//		// IngameLevelLoadedPausedCanvas.gameObject.SetActive (false);

	//	}

	//	if (GameController.current.CurrentState == GameState.Ingame) {

	//		// IngameLevelLoadedCanvas.gameObject.SetActive (true);



	//		if (NumberOfCoins != null) {

 //               NumberOfCoins.text = " " + GameController.current.Player.CurrentHealth + "/" + GameController.current.Player.MaximumHealth +
 //                   " | Coins: " + GameController.current.NumberOfCollectables(CollectableType.Coin) +
 //                   " | Stars: " + GameController.current.NumberOfCollectables(CollectableType.Star);

 //           }

	//		if (TimeT != null) {

 //   			TimeSpan t = TimeSpan.FromSeconds (GameController.current.currentTime);

	//			string format = "";

	//			if (t.Hours > 0)
	//				format += "{3:D2}:";

	//			if (t.Minutes > 0)
	//				format += "{2:D2}:";

	//			format += "{1:D2}.{0:D3}";

	//			TimeT.text = " " + string.Format(format, t.Milliseconds, t.Seconds, t.Minutes, t.Hours);

	//		}

	//	}

	//	if (GameController.current.CurrentState == GameState.IngameOptions) {

	//		IngameLevelLoadedCanvas.gameObject.SetActive (true);
	//		IngameLevelLoadedPausedCanvas.gameObject.SetActive (true);

	//	}

	//}

 //   void AddListener(Button button, UnityEngine.Events.UnityAction action) {

 //       if (button == null) {

 //           Debug.LogErrorFormat("{0} was null while trying to add a listener.", button.gameObject.name);
 //           return;

 //       }

 //       button.onClick.AddListener(action);

 //   }
 //   #region Populate Dropdowns

 //   void PopulateQualityPresetsDropdown() {

 //       if (QualityPresetsDropdown == null) {

 //           return;

 //       }


 //       QualityPresetsDropdown.ClearOptions ();

	//	List<string> qualityPresets = QualitySettings.names.ToList<string>();

	//	QualityPresetsDropdown.AddOptions (qualityPresets);

	//}

	//void PopulateGameResolutionDropdown() {

 //       if (GameResolutionDropdown == null) {

 //           return;

 //       }

	//	GameResolutionDropdown.ClearOptions ();

	//	Resolution[] resolutions = Screen.resolutions;
	//	List<string> resolutionsList = new List<string> ();

	//	int currentResolution = 0;
	//	for (int i = 0; i < resolutions.Count(); i++) {

	//		resolutionsList.Add (resolutions[i].width + " x " + resolutions[i].height + " x " + resolutions[i].refreshRate + "Hz");

	//		if (Screen.fullScreen) {

	//			if (Screen.currentResolution.width == resolutions [i].width && Screen.currentResolution.height == resolutions [i].height && Screen.currentResolution.refreshRate == resolutions [i].refreshRate) {

	//				currentResolution = i;

	//			}

	//		} else {

	//			if (Screen.width == resolutions [i].width && Screen.height == resolutions [i].height) {

	//				currentResolution = i;

	//			}

	//		}

	//	}

	//	GameResolutionDropdown.AddOptions (resolutionsList);
	//	GameResolutionDropdown.value = currentResolution;

	//}

 //   #endregion

 //   #region OnClicks

 //   // OnClick methods
 //   public void QualityPresetsOnClick(int index) {

	//	QualitySettings.SetQualityLevel(index, true);

	//}

	//// Major Button OnClicks
	//public void SaveButtonOnClick() {

	//	// Set the resolution of the game.
	//	Screen.SetResolution (Screen.resolutions [GameResolutionDropdown.value].width, Screen.resolutions [GameResolutionDropdown.value].height, FullscreenToggle.isOn);

	//	//Screen.

	//}

 //   #endregion
}

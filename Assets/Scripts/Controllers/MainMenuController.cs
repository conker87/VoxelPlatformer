using System;

using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controller that manages the MainMenu scene, including states and UI elements.
/// </summary>
public class MainMenuController : MonoBehaviour {

    // Create the Singleton for this class.
    public static MainMenuController current = null;
    void Awake() {

        if (current == null) {

            current = this;

        }
        else if (current != this) {

            Destroy(gameObject);

        }
    }

    [Header("Canvases")]
    public Transform MainMenu;
    public Transform OptionsMenu;

    [Header("Main Menu Canvas")]
    public Button MainMenuContinue;
    public Button MainMenuNewGame, MainMenuOptions, MainMenuQuit;

    [Header("Options Menu Canvas")]
    public Button OptionsMenuSave;
    public Button OptionsMenuRevert, OptionsMenuClose;
    public Dropdown OptionsMenuQualityPresets;
    public Dropdown OptionsMenuGameResolution;
    public Toggle OptionsMenuFullscreen;

    [Header("Main Menu State")][SerializeField]
    MainMenuState MainMenuState;
    bool ChangedState;

    void Start() {

        // QualityPresets
        PopulateQualityPresetsDropdown();
        PopulateGameResolutionDropdown();

        OptionsMenuFullscreen.isOn = Screen.fullScreen;

        // Add listeners to all the allocated Buttons.
        AddListener(MainMenuContinue, delegate { LoadSaveController.LoadMainGame(LoadSaveController.SaveGameLocation); });
        AddListener(MainMenuNewGame, delegate { LoadSaveController.LoadMainGame(""); });
        AddListener(MainMenuOptions, delegate { ChangeMainMenuState(MainMenuState.OptionsMenu); });
        AddListener(MainMenuQuit, Application.Quit);

        AddListener(OptionsMenuSave, SaveButton_OnClick);
        AddListener(OptionsMenuRevert, null);
        AddListener(OptionsMenuSave, delegate { ChangeMainMenuState(MainMenuState.MainMenu); });

    }

    void Update() {

        if (ChangedState) {

            MainMenu.gameObject.SetActive(false);
            OptionsMenu.gameObject.SetActive(false);

        }

        if (MainMenuState == MainMenuState.MainMenu) {

            MainMenu.gameObject.SetActive(true);

        }

        if (MainMenuState == MainMenuState.OptionsMenu) {

            OptionsMenu.gameObject.SetActive(true);

        }

        ChangedState = false;
    }

    void AddListener(Button button, UnityEngine.Events.UnityAction action) {

        if (button == null) {

            Debug.LogErrorFormat("{0} was null while trying to add a listener.", button.gameObject.name);
            return;

        }

        button.onClick.AddListener(action);

    }

    void PopulateQualityPresetsDropdown() {

        OptionsMenuQualityPresets.ClearOptions();

        List<string> qualityPresets = QualitySettings.names.ToList<string>();

        OptionsMenuQualityPresets.AddOptions(qualityPresets);

    }
    void PopulateGameResolutionDropdown() {

        OptionsMenuGameResolution.ClearOptions();

        Resolution[] resolutions = Screen.resolutions;
        List<string> resolutionsList = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Count(); i++) {

            resolutionsList.Add(resolutions[i].width + " x " + resolutions[i].height + " x " + resolutions[i].refreshRate + "Hz");

            if (Screen.fullScreen) {

                if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height && Screen.currentResolution.refreshRate == resolutions[i].refreshRate) {

                    currentResolution = i;

                }

            }
            else {

                if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height) {

                    currentResolution = i;

                }

            }

        }

        OptionsMenuGameResolution.AddOptions(resolutionsList);
        OptionsMenuGameResolution.value = currentResolution;

    }

    public void QualityPresetsOnClick(int index) {

        QualitySettings.SetQualityLevel(index, true);

    }

    // Button OnClick Events.
    public void SaveButton_OnClick() {

        Screen.SetResolution(
            Screen.resolutions[OptionsMenuGameResolution.value].width,
            Screen.resolutions[OptionsMenuGameResolution.value].height,
            OptionsMenuFullscreen.isOn
        );

    }

    // Menu State Changer
    void ChangeMainMenuState(MainMenuState state) {

        ChangedState = true;
        MainMenuState = state;

    }
}

enum MainMenuState { MainMenu, OptionsMenu, LegalMenu, CreditsMenu };
using System;

using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

/// <summary>
/// Controller that manages the MainGame scene, including states and UI elements.
/// </summary>
public class MainGameController : MonoBehaviour {

    // Create the Singleton for this class.
    public static MainGameController current = null;
    void Awake() {

        if (current == null) {

            current = this;

        }
        else if (current != this) {

            Destroy(gameObject);

        }
    }

    public static Player player;
    SaveDetails SaveDetails;
    public List<Level> levels = new List<Level>();

    bool changedState;

    float totalTimePlayed = 0f;
    bool savingGameAllowed = true;

    MainGameState MainGameState;
    bool ChangedState;

    // Public Fields
    public float TotalTimePlayed { get { return totalTimePlayed; } set { totalTimePlayed = value; } }
    public bool SavingGameAllowed { get { return savingGameAllowed; } set { savingGameAllowed = value; } }

    // Public UI Field
    [Header("Canvases")]
    public Transform IngameLevelLoadedCanvas;
    public Transform IngameLevelLoadedPausedCanvas;
    public Transform CutsceneCanvas;

    [Header("Level Loaded Canvas")]
    public TextMeshProUGUI NumberOfCoins;
    public TextMeshProUGUI NumberOfStars, Health, TimeT, RPGFluff, Location;
    Coroutine RPGFluffFadeOut, LocationTextFadeout;

    // Use this for initialization
    void Start () {

        player = null;
        levels.Clear();

        // Technically we're still loading...
        LocateAndUpdateCollectablesAndInteractables();

        // Populate levels
        foreach (Level level in ExtendList.FindObjectsOfTypeInactive<Level>()) {
            //levelsParent.GetComponentsInChildren<Level>(true)) {

            levels.Add(level);

        }

        FindPlayer();

    }
	
	// Update is called once per frame
	void Update () {

        if (FindPlayer() == false) {

            return;

        }

        if (MainGameState == MainGameState.Playing) {

            TotalTimePlayed += Time.unscaledDeltaTime;

            if (Input.GetKeyDown(KeyCode.Escape)) {

                // This is supposed to enable the PlayingMenu.
                ChangeGameState(MainGameState.PausedMenu);
                SceneManager.LoadScene("_LoadMainMenu");

            }
        }
    }

    void LocateAndUpdateCollectablesAndInteractables() {

        if (LoadSaveController.SaveGameLocation == "") {

            return;

        }

        foreach (Collectable collectable in ExtendList.FindObjectsOfTypeInactive<Collectable>()) {

            collectable.CollectableCollected = true;

        }

        foreach (Interactable interactable in ExtendList.FindObjectsOfTypeInactive<Interactable>()) {

            foreach (Interactable savedInteractable in LoadSaveController.LoadedInteractables) {

                if (savedInteractable.InteractableID == interactable.InteractableID) {

                    Debug.LogFormat("Found an interactable in the world: {0} and also in the Saved Interactables.", interactable);

                    interactable.IsActivated = savedInteractable.IsActivated;
                    interactable.IsDisabled = savedInteractable.IsDisabled;
                    interactable.IsLocked = savedInteractable.IsLocked;
                    break;

                }
            }
        }
    }

    public bool HasPlayerCollectedCollectable(string _CollectableID) {

        foreach (Collectable collectable in LoadSaveController.LoadedCollectables) {

            if (collectable.CollectableID == _CollectableID) {

                return true;

            }
        }

        return false;

    }

    // Gather together all details needed to save the game and initiate the save.
    public void GatherSaveDetails() {

        if (SavingGameAllowed == false) {

            Debug.Log("Saving the game is disabled.");
            return;

        }

        List<Collectable> Collectables = FindObjectsOfType<Collectable>().Where(a => a.CollectableCollected = true).ToList<Collectable>();
        List<Interactable> Interactables = FindObjectsOfType<Interactable>().ToList<Interactable>();

        SaveDetails gatheredSaveDetails = new SaveDetails(
            LoadSaveController.SaveGameLocation,
            player.transform.position,
            TotalTimePlayed,
            DateTime.Now,
            Collectables,
            Interactables);

        LoadSaveController.SaveMainGame(gatheredSaveDetails);
    }

    public void ShowRPGFluffText(string text, float fadeOutInTime = 3f, float fadeOutTime = 1f) {

        if (RPGFluff == null) {

            return;

        }

        if (RPGFluffFadeOut != null) {

            StopCoroutine(RPGFluffFadeOut);

        }

        RPGFluff.color = new Color(RPGFluff.color.r, RPGFluff.color.g, RPGFluff.color.b, 1);

        RPGFluff.text = "[PH] " + text;

        RPGFluffFadeOut = null;
        RPGFluffFadeOut = StartCoroutine(FadeOutRPGFluffText(fadeOutInTime, fadeOutTime));
    }
    IEnumerator FadeOutRPGFluffText(float fadeOutInTime, float fadeOutTime) {

        yield return new WaitForSeconds(fadeOutInTime);

        RPGFluff.color = new Color(RPGFluff.color.r, RPGFluff.color.g, RPGFluff.color.b, 1);

        while (RPGFluff.color.a > 0.0f) {

            RPGFluff.color = new Color(RPGFluff.color.r,
                RPGFluff.color.g,
                RPGFluff.color.b,
                RPGFluff.color.a - (Time.deltaTime / fadeOutTime)
            );

            yield return null;
        }

        RPGFluff.text = "";

    }

    public void ShowLocationText(string text, float fadeOutInTime = 3f, float fadeOutTime = 1f) {

        if (Location == null) {

            return;

        }

        if (LocationTextFadeout != null) {

            StopCoroutine(LocationTextFadeout);

        }

        Location.color = new Color(Location.color.r, Location.color.g, Location.color.b, 1);

        Location.text = text;

        LocationTextFadeout = null;
        LocationTextFadeout = StartCoroutine(FadeOutLocationText(fadeOutInTime, fadeOutTime));
    }
    IEnumerator FadeOutLocationText(float fadeOutInTime, float fadeOutTime) {

        yield return new WaitForSeconds(fadeOutInTime);

        Location.color = new Color(Location.color.r, Location.color.g, Location.color.b, 1);

        while (Location.color.a > 0.0f) {

            Location.color = new Color(Location.color.r,
                Location.color.g,
                Location.color.b,
                Location.color.a - (Time.deltaTime / fadeOutTime)
            );

            yield return null;
        }

        Location.text = "";

    }

    // Menu State Changer
    void ChangeGameState(MainGameState state) {

        changedState = true;
        MainGameState = state;

    }

    public static bool FindPlayer() {

        if (player == null) {

            player = FindObjectOfType<Player>();

            if (player == null) {

                Debug.LogError("MainGameController::FindPlayer -- Something is seriously wrong, we could not find the player!");

                return false;

            }

            return true;

        }

        return false;
    }
}

enum MainGameState { Playing, PausedMenu, PausedOptions };
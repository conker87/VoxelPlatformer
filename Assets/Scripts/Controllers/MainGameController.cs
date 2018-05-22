using System;

using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

using ProBuilder.Core;
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

    public Player player;
    SaveDetails SaveDetails;
    public List<AreaController> levels = new List<AreaController>();

    [SerializeField] pb_Object backgroundPlane;

    // bool hasChangedState;
    bool hasFullyLoaded = false;

    string saveGameName = "";
    float totalTimePlayed = 0f;
    bool savingGameAllowed = true;

    MainGameState MainGameState;

    // Public Fields
    public string SaveGameName { get { return saveGameName; } set { saveGameName = value; } }
    public float TotalTimePlayed { get { return totalTimePlayed; } set { totalTimePlayed = value; } }
    public bool SavingGameAllowed { get { return savingGameAllowed; } set { savingGameAllowed = value; } }

    // Public UI Field
    [Header("Canvases")]
    public Transform IngameLevelLoadedCanvas;
    public Transform IngameLevelLoadedPausedCanvas;
    public Transform CutsceneCanvas;

    [Header("Level Loaded Canvas")]
    public TextMeshProUGUI NumberOfCoins;
    public TextMeshProUGUI NumberOfStars, Health, TimeT, RPGFluff;
    public Text MainAreaLocation, SubAreaLocation;

    Coroutine RPGFluffFadeOut, MainLocationTextFadeout, SubLocationTextFadeout;

    public List<CollectableSave> Collectables = new List<CollectableSave>();
    public List<InteractableSave> Interactables = new List<InteractableSave>();

    public AreaController currentArea;

    void OnEnable() {

        FindPlayer();
        SceneManager.sceneLoaded += ApplySaveData;

    }

    void OnDisable() {

        SceneManager.sceneLoaded -= ApplySaveData;

    }

    void Start () {

        player = null;
        levels.Clear();

        // Technically we're still loading...
        // LocateAndUpdateCollectablesAndInteractables();
        
        // Populate levels
        foreach (AreaController level in ExtendList.FindObjectsOfTypeInactive<AreaController>()) {
            //levelsParent.GetComponentsInChildren<Level>(true)) {

            levels.Add(level);

        }

        #if UNITY_EDITOR
            FindPlayer();
        #endif


        if (backgroundPlane == null) {

            backgroundPlane = GameObject.Find("BackgroundPlane").GetComponent<pb_Object>();

        }

    }

    void Update () {

        if (hasFullyLoaded == false) {

            Debug.Log("hasFullyLoaded is false.");
            return;

        }

        if (FindPlayer() == false) {

            Debug.Log("FindPlayer() is false, will try next frame.");
            return;

        }

        if (MainGameState == MainGameState.Playing) {

            TotalTimePlayed += Time.unscaledDeltaTime;

            // Update UI
            if (NumberOfCoins != null) {

                NumberOfCoins.text = string.Format("{0}/{1} | Coins: {2} | Stars: {3}",
                    player.CurrentHealth, player.MaximumHealth, NumberOfCollectables(CollectableType.Coin), NumberOfCollectables(CollectableType.Star));

            }

            if (TimeT != null) {

                TimeSpan t = TimeSpan.FromSeconds(TotalTimePlayed);

                string format = "";

                if (t.Hours > 0)
                    format += "{3:D2}:";

                if (t.Minutes > 0)
                    format += "{2:D2}:";

                format += "{1:D2}.{0:D3}";

                TimeT.text = string.Format(format, t.Milliseconds, t.Seconds, t.Minutes, t.Hours);

            }

                if (Input.GetKeyDown(KeyCode.Escape)) {

                // This is supposed to enable the PlayingMenu.
                ChangeGameState(MainGameState.PausedMenu);
                SceneManager.LoadScene("_LoadMainMenu");

            }
        }

        // hasChangedState = false;
    }

    void ApplySaveData(Scene scene, LoadSceneMode mode) {

        // Editor function as loadedFromMainMenu will never be false in production.
        if (LoadSaveController.loadedFromMainMenu == false) {

            LoadSaveController.SaveGameLocation = "saveGameTest.xml";

        }

        if (LoadSaveController.SaveGameLocation == "") {

            Debug.Log("LoadSaveController.SaveGameLocation");

            FindPlayer();

            hasFullyLoaded = true;
            return;

        }

        SaveGameName = LoadSaveController.LoadedSaveName;
        player.transform.position = LoadSaveController.LoadedPosition;
        TotalTimePlayed = LoadSaveController.LoadedTotalTime;

        // Populate Collectable list.
        foreach (Collectable collectable in ExtendList.FindObjectsOfTypeInactive<Collectable>()) {

            foreach (CollectableSave savedCollectable in LoadSaveController.LoadedCollectables) {

                if (collectable.CollectableID == savedCollectable.CollectableID) {

                    collectable.CollectableCollected = savedCollectable.CollectableCollected;
                    break;

                }
            }
        }

        // Populate Interactable list.
        foreach (Interactable interactable in ExtendList.FindObjectsOfTypeInactive<Interactable>()) {

            foreach (InteractableSave savedInteractable in LoadSaveController.LoadedInteractables) {

                if (savedInteractable.InteractableID == interactable.InteractableID) {

                    interactable.IsActivated = savedInteractable.IsActivated;
                    interactable.IsDisabled = savedInteractable.IsDisabled;
                    interactable.IsLocked = savedInteractable.IsLocked;
                    break;

                }
            }
        }

        FindPlayer();

        hasFullyLoaded = true;
    }

    public bool HasPlayerCollectedCollectable(string _CollectableID) {

        foreach (CollectableSave collectable in LoadSaveController.LoadedCollectables) {

            if (collectable.CollectableID == _CollectableID) {

                return true;

            }
        }

        return false;

    }

    public int NumberOfCollectables(CollectableType _CollectableType, bool _countAllCollectables = false) {

        if (_countAllCollectables == true) {

            return LoadSaveController.LoadedCollectables.Count;

        }

        return LoadSaveController.LoadedCollectables.Where(a => a.CollectableType == _CollectableType).Count();

    }

    // Gather together all details needed to save the game and initiate the save.
    public void GatherSaveDetails() {

        if (SavingGameAllowed == false) {

            Debug.Log("Saving the game is disabled.");
            return;

        }

        Collectables.Clear();
        Interactables.Clear();

        // FindObjectsOfType<Collectable>().Where(a => a.CollectableCollected == true).ToList<Collectable>().ForEach(x => Collectables.Add(new CollectableSave(x.CollectableID, x.CollectableType, x.CollectableCollected)));

         FindObjectsOfType<Interactable>().ToList<Interactable>().ForEach(x => Interactables.Add(new InteractableSave(x.InteractableID, x.IsDisabled, x.IsLocked, x.IsActivated)));

        // Hardcoding for the time being...
        SaveDetails gatheredSaveDetails = new SaveDetails(
            LoadSaveController.SaveGameLocation,
            MainGameController.current.player.transform.position,
            TotalTimePlayed,
            DateTime.Now,
            LoadSaveController.LoadedCollectables,
            //Collectables,
            Interactables);

        LoadSaveController.SaveMainGame(gatheredSaveDetails);

    }

    public void ShowRPGFluffText(string text, float fadeOutInTime = 3f, float fadeOutTime = 1f) {

        if (RPGFluff == null) {

            return;

        }

        if (text == "") {

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
    }

    public void ShowMainLocationText(string text, float fadeOutInTime = 3f, float fadeOutTime = 1f) {

        if (MainAreaLocation == null || text == "" || MainAreaLocation.text == text) {

            return;

        }

        if (SubLocationTextFadeout != null) {

            StopCoroutine(SubLocationTextFadeout);

        }

        if (MainLocationTextFadeout != null) {

            StopCoroutine(MainLocationTextFadeout);

        }

        MainAreaLocation.color = new Color(MainAreaLocation.color.r, MainAreaLocation.color.g, MainAreaLocation.color.b, 1);

        MainAreaLocation.text = text;

        MainLocationTextFadeout = null;
        MainLocationTextFadeout = StartCoroutine(FadeOutMainLocationText(fadeOutInTime, fadeOutTime));
    }
    IEnumerator FadeOutMainLocationText(float fadeOutInTime, float fadeOutTime) {

        yield return new WaitForSeconds(fadeOutInTime);

        MainAreaLocation.color = new Color(MainAreaLocation.color.r, MainAreaLocation.color.g, MainAreaLocation.color.b, 1);

        while (MainAreaLocation.color.a > 0.0f) {

            MainAreaLocation.color = new Color(MainAreaLocation.color.r,
                MainAreaLocation.color.g,
                MainAreaLocation.color.b,
                MainAreaLocation.color.a - (Time.deltaTime / fadeOutTime)
            );

            yield return null;
        }
    }

    public void ShowSubLocationText(string text, float fadeOutInTime = 3f, float fadeOutTime = 1f) {

        if (SubAreaLocation == null || text == ""  || SubAreaLocation.text == text) {

            return;

        }

        if (SubLocationTextFadeout != null) {

            StopCoroutine(SubLocationTextFadeout);

        }

        SubAreaLocation.color = new Color(SubAreaLocation.color.r, SubAreaLocation.color.g, SubAreaLocation.color.b, 1);

        SubAreaLocation.text = text;

        SubLocationTextFadeout = null;
        SubLocationTextFadeout = StartCoroutine(FadeOutSubLocationText(fadeOutInTime, fadeOutTime));
    }
    IEnumerator FadeOutSubLocationText(float fadeOutInTime, float fadeOutTime) {

        yield return new WaitForSeconds(fadeOutInTime);

        SubAreaLocation.color = new Color(SubAreaLocation.color.r, SubAreaLocation.color.g, SubAreaLocation.color.b, 1);

        while (SubAreaLocation.color.a > 0.0f) {

            SubAreaLocation.color = new Color(SubAreaLocation.color.r,
                SubAreaLocation.color.g,
                SubAreaLocation.color.b,
                SubAreaLocation.color.a - (Time.deltaTime / fadeOutTime)
            );

            yield return null;
        }
    }

    // Menu State Changer
    void ChangeGameState(MainGameState state) {

        // hasChangedState = true;
        MainGameState = state;

    }

    public void CheckIfInArea(List<AreaController> areasToLoad, Color backgroundColor, bool onlyShowNewSubareaText = false, string subAreaText = "") {

        if (onlyShowNewSubareaText == true) {

            MainGameController.current.ShowSubLocationText(subAreaText, 2f);

            return;
        }

        string debug_log = "";
        bool loadedLevel = false;
        int i = 0;

        #region
        /* foreach (AreaController loadingArea in areasToLoad) {

        //    foreach (AreaController area in MainGameController.current.levels) {

        //        if (loadingArea.LevelName == area.LevelName) {

        //            debug_log += area.LevelName + ", ";

        //            area.gameObject.SetActive(true);
        //            area.LoadArea();
        //            loadedLevel = true;

        //            if (i == 0) {

        //                if (currentArea != loadingArea) {

        //                    MainGameController.current.ShowLocationText(area.LevelName);

        //                    currentArea = loadingArea;
        //                }
        //            }
        //        }
        //    }

        //    if (loadedLevel == true) {

        //        continue;

        //    }

        //    i++;
        //    area.UnloadArea();
        } */

        // --

        #endregion

        foreach (AreaController area in MainGameController.current.levels) {

            loadedLevel = false;
            i = 0;

            foreach (AreaController loadingArea in areasToLoad) {
                
                if (loadingArea.AreaName == area.AreaName) {

                    debug_log += area.AreaName + ", ";

                    area.gameObject.SetActive(true);
                    area.LoadArea();
                    loadedLevel = true;

                    if (i == 0) {

                        MainGameController.current.ShowMainLocationText(area.AreaName, 2f);
                        MainGameController.current.ShowSubLocationText(subAreaText, 2f);

                        currentArea = loadingArea;
                    }
                }

                i++;
            }

            if (loadedLevel == true) {

                continue;

            }

            area.UnloadArea();

        }

        ChangeBackgroundColor(backgroundColor);

    }

    void ChangeBackgroundColor(Color newColor) {

        Color color = newColor;

        // Cycle through each unique vertex in the cube (8 total), and assign a color to the index in the sharedIndices array.
        int si_len = backgroundPlane.sharedIndices.Length;
        Color[] vertexColors = new Color[si_len];

        for (int i = 0; i < si_len; i++) {

            vertexColors[i] = color;

        }

        // Now go through each face (vertex colors are stored the pb_Face class) and assign the pre-calculated index color to each index in the triangles array.
        Color[] colors = backgroundPlane.colors;

        for (int CurSharedIndex = 0; CurSharedIndex < backgroundPlane.sharedIndices.Length; CurSharedIndex++) {

            foreach (int CurIndex in backgroundPlane.sharedIndices[CurSharedIndex].array) {

                colors[CurIndex] = vertexColors[CurSharedIndex];

            }
        }

        backgroundPlane.SetColors(colors);

        // In order for these changes to take effect, you must refresh the mesh object.
        backgroundPlane.Refresh();

    }

    public static bool FindPlayer() {

        if (current.player == null) {

            current.player = FindObjectOfType<Player>();

            if (current.player == null) {

                Debug.LogError("MainGameController::FindPlayer -- Something is seriously wrong, we could not find the player!");

                return false;

            }
        }

        return true;

    }
}

enum MainGameState { Playing, PausedMenu, PausedOptions };
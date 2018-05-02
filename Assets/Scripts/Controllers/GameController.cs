using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The controller of the entire game. Controls Collectable collection lists, the lists of all Levels, the Player spawn prefab and location and timer.
/// </summary>
public class GameController : MonoBehaviour {

    // Create the Singleton for this class.
    public static GameController current = null;
    void Awake() {

        if (current == null) {

            current = this;

        } else if (current != this) {

            Destroy(gameObject);

        }
    }

    [Header("Saving Lists")]
    // List of all the Collectables that the Player has collected in this save file.
	public List<CollectableListValue> Collectables = new List<CollectableListValue>();

    [Header("Game Objects")]
    // The Parent objects to all of the Levels.
    public GameObject levelsParent;
    // The Start location of the Player for a New Game.
    public GameObject definedGameStartPosition;
    // The position of the Player for a Loaded Game.
    public Vector3 playerLoadedPosition = Vector3.zero;
    // Player specific varaibles.
    public Player PlayerSpawnablePrefab, Player;

    // List of all Levels in the game.
    public List<Level> allLevels = new List<Level>();

    [Header("Game Fields")]
    // Can the Player Save the Game right now?
    public bool isAllowedToSaveGame;
    // The Current time played this Save Data.
    public float currentTime;

    [Header("Game States")]
    // The Current "Scene" of the Game, used to determine which menu you are in.
    public GameState CurrentState = GameState.MainMenu;
    // The Current Save State of the game, used to determine if it's a New Game or Loaded Game.
    public SaveState CurrentSaveState;
    
    public bool justChangedState = false;

	void Start() {

        // We should probably have the game in its own scene once we've completed the game so all rigidbodies reset.
        // Also, we should set up areas where the player is not allowed to save the game because of said rigidbodies.
        // LoadingController::OnTriggerEnter->Needs sorting as this messes up frame rate entirely.It's Object.Awake()

        // The target frame rate of the Game, set this to something not peasentry.
        Application.targetFrameRate = 60;

        // Populates the list of all Levels.
        foreach (Level level in levelsParent.GetComponentsInChildren<Level>(true)) {

            GameController.current.allLevels.Add(level);

        }

        // Sets the Current Game State to the Main Menu.
		ChangeGameState (GameState.MainMenu);

        // Sets the start position of the Player to null, or near enough.
        playerLoadedPosition = Vector3.zero;

    }

	void Update() {

		if (CurrentState != GameState.TitleScreen || CurrentState != GameState.MainMenu) {

        }

        if (CurrentState == GameState.TitleScreen || CurrentState == GameState.MainMenu) {

            // Sets the Levels Parent to false when not actually in game.
            levelsParent.SetActive(false);

        }

        if (CurrentState == GameState.Ingame) {

            // Constantly increases the Current Time of the Save Data while playing.
            // TODO: We should pause this when the game is Paused?
            currentTime += Time.unscaledDeltaTime;

        }
    }

    private void LateUpdate() {

        // Resets the bool that determines whether or not the Current Game State has changed back to false.
        if (justChangedState == true) {

            justChangedState = false;

        }

    }

    /// <summary>
    /// Main function to start the game.
    /// </summary>
    /// <param name="saveState">The Save State, either it's a New Game or it's a Loaded Game.</param>
    public void StartGame(SaveState saveState = SaveState.NewGame) {

        // Change the Current Game State to being in game.
        ChangeGameState(GameState.Ingame);
        CurrentSaveState = saveState;

        // Sets the Levels Parent to true to show the game.
        levelsParent.transform.position = Vector3.zero;
        levelsParent.SetActive(true);
        
        // Find the already instansiated Player object that's in Levels Parent.
        Player = levelsParent.GetComponentInChildren<Player>();

        // Sets the current Player location to definedGameStartPosition if it's a New Game, else it sets it to the positon defined by the Save Data.
        Player.transform.position = (saveState == SaveState.NewGame) ? definedGameStartPosition.transform.position : playerLoadedPosition;

    }

    /// <summary>
    /// Main function to quit from in game.
    /// </summary>
    public void QuitGame() {

        SceneManager.LoadScene("prototype");

    }
    
    /// <summary>
    /// Sets the Game State to the defined param.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeGameState(GameState newState) {

		justChangedState = true;
		CurrentState = newState;

		Debug.Log (string.Format("GameController::ChangeGameState: New State: {0}", newState));

	}

    /// <summary>
    /// Adds the CollectableListValue to the Current list of the Collectables for this Save Data.
    /// </summary>
    /// <param name="collectableList">The basic details of the Currently adding Collectable.</param>
	public void AddToCollectables(CollectableListValue collectableList) {

		if (Collectables.Contains (collectableList)) {

			return;

		}

        Collectables.Add (collectableList);

	}

    /// <summary>
    /// Check to see if a Collectable has already been collected.
    /// </summary>
    /// <param name="collectableID">The ID of the collectable you wish to search for.</param>
    /// <returns>Returns true if the Collectable was found in the List.</returns>
	public bool HasAcquiredCollectable(string collectableID) {

        foreach (CollectableListValue value in Collectables) {

            if (value.CollectableID == collectableID) {

                return true;

            }
        }

        return false;

	}

    /// <summary>
    /// The number of the given type of Collectables in the Current Collectable List.
    /// </summary>
    /// <param name="collectableType">The Collectable Type to search.</param>
    /// <returns>Returns an int of the amount of the given Type.</returns>
	public int NumberOfCollectables(CollectableType collectableType) {

        int total = 0;

        foreach (CollectableListValue value in Collectables) {

            if (value.CollectableType == collectableType) {

                total++;

            }
        }

        return total;

	}
}

public enum GameState { TitleScreen, MainMenu, Ingame, IngameOptions, MainMenuOptions };
public enum SaveState { NewGame, LoadedGame };
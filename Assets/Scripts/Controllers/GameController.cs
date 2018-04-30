using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    #region Singleton

    public static GameController current = null;

    void Awake() {

        if (current == null) {
            current = this;
        }
        else if (current != this) {
            Destroy(gameObject);
        }

       //  DontDestroyOnLoad(gameObject);

    }

    #endregion

    [Header("Saving Lists")]
	public List<CollectableListValue> Collectables = new List<CollectableListValue>();

    [Header("Game Objects")]
    public GameObject levelsParent;
    public GameObject definedGameStartPosition;
    public Vector3 playerLoadedPosition = Vector3.zero;
    public Player PlayerSpawnablePrefab, Player;

    public List<Level> allLevels = new List<Level>();

    [Header("Game Fields")]
    public bool isAllowedToSaveGame;
    public float currentTime;

    [Header("Game States")]
    public GameState CurrentState = GameState.MainMenu;
    public SaveState CurrentSaveState;
    
    public bool justChangedState = false;

	void Start() {

        Debug.LogWarning(@"We should probably have the game in its own scene once we've completed the game so all rigidbodies reset.
Also, we should set up areas where the player is not allowed to save the game because of said rigidbodies.
LoadingController::OnTriggerEnter -> Needs sorting as this messes up frame rate entirely. It's Object.Awake()");

        Application.targetFrameRate = 60;

        foreach (Level level in levelsParent.GetComponentsInChildren<Level>(true)) {

            GameController.current.allLevels.Add(level);

        }

		ChangeState (GameState.MainMenu);
        Collectables.Clear();

	}

	void Update() {

		if (CurrentState != GameState.TitleScreen || CurrentState != GameState.MainMenu) {

        }

        if (CurrentState == GameState.TitleScreen || CurrentState == GameState.MainMenu) {

            levelsParent.SetActive(false);

        }

        if (CurrentState == GameState.Ingame) {

            currentTime += Time.unscaledDeltaTime;

        }
    }

    private void LateUpdate() {

        justChangedState = false;

    }

    public void StartGame(SaveState saveState = SaveState.NewGame) {

        ChangeState(GameState.Ingame);
        CurrentSaveState = saveState;

        levelsParent.SetActive(true);
        Player = levelsParent.GetComponentInChildren<Player>();
        // SceneManager.LoadScene("game");

        Player.transform.position = (saveState == SaveState.NewGame) ? definedGameStartPosition.transform.position : playerLoadedPosition;

    }

    public void QuitGame() {

      
        Collectables.Clear();
        playerLoadedPosition = Vector3.zero;

        SceneManager.LoadScene("prototype");

        //ChangeState(GameState.MainMenu);

    }





    public void ChangeState(GameState newState) {

		justChangedState = true;
		CurrentState = newState;

		Debug.Log (string.Format("Changing States to -- {0}", newState));

	}

	public void AddToCollectables(CollectableListValue collectableList) {

		if (Collectables.Contains (collectableList)) {

			return;

		}

        Collectables.Add (collectableList);

	}

	public bool HasAcquiredCollectable(string collectableID) {

        foreach (CollectableListValue value in Collectables) {

            if (value.CollectableID == collectableID) {

                return true;

            }
        }

        return false;

	}

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
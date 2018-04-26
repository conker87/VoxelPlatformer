using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region Singleton

    public static GameController current;

	void Awake() {

		if (current == null) {

			current = this;

		}

	}

    #endregion

	public List<CollectableListValue> Collectables = new List<CollectableListValue>();

	public float currentTime;

    public GameObject levelsParent;
    public GameObject definedGameStartPosition;
    public Vector3 playerLoadedPosition = Vector3.zero;

    public static List<Level> allLevels = new List<Level>();

    public Player PlayerSpawnablePrefab, Player;

	public GameState CurrentState = GameState.MainMenu;
    public SaveState CurrentSaveState;
    
    public bool justChangedState = false;

	void Start() {

        foreach (Level level in levelsParent.GetComponentsInChildren<Level>(true)) {

            Debug.Log(level);
            GameController.allLevels.Add(level);

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

        Player.transform.position = (saveState == SaveState.NewGame) ? definedGameStartPosition.transform.position : playerLoadedPosition;

    }

    public void QuitGame() {

        Collectables.Clear();
        playerLoadedPosition = Vector3.zero;

        ChangeState(GameState.MainMenu);

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
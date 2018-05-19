using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLoader : MonoBehaviour {

    public Text progressionText;

    void OnEnable() {

        StopAllCoroutines();
        StartCoroutine(LoadScene());

    }

    IEnumerator LoadScene() {

        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainMenu");

        while (!asyncOperation.isDone) {

            progressionText.text = "Loading progress: " + (asyncOperation.progress * 100) * (10f/9f) + "%";

            yield return null;

        }
    }
}
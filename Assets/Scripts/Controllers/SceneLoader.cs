using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    public Text progressionText;
    public bool requiresInputToContinue = false;
    public string sceneToLoad = "MainMenu";

    void OnEnable() {

        StopAllCoroutines();
        StartCoroutine(LoadScene());

    }

    IEnumerator LoadScene() {

        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        asyncOperation.allowSceneActivation = !requiresInputToContinue;

        while (!asyncOperation.isDone) {

            float currentProgressionPercentage = (asyncOperation.progress * 100f) * (10f / 9f);

            progressionText.text = "Loading progress: " + currentProgressionPercentage + "%";

            if (requiresInputToContinue == true) {

                

                if (currentProgressionPercentage >= 99f) {

                    progressionText.text = "Press any key to continue";

                    if (Input.anyKeyDown == true || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {

                        asyncOperation.allowSceneActivation = true;

                    }
                }
            }

            yield return null;

        }
    }
}
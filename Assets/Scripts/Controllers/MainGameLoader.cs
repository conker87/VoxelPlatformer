using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameLoader : MonoBehaviour {

    //public Text progressionText;

    //void OnEnable() {

    //    StopAllCoroutines();
    //    StartCoroutine(LoadScene());

    //}

    //IEnumerator LoadScene() {

    //    yield return null;

    //    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainGame");

    //    asyncOperation.allowSceneActivation = false;

    //    while (!asyncOperation.isDone) {

    //        progessionText.text = "Loading progress: " + (asyncOperation.progress * 100f) * (10f / 9f) + "%";

    //        if (asyncOperation.progress >= 1f) {

    //            progessionText.text = "Press the space bar to continue";

    //            if (Input.GetKeyDown(KeyCode.Space)) {

    //                asyncOperation.allowSceneActivation = true;
    //            }
    //        }

    //        yield return null;

    //    }
    //}
}

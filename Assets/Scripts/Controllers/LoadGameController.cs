using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameController : MonoBehaviour {

    public Text m_Text;

    void OnEnable() {
        StopAllCoroutines();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene() {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("prototype");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone) {
            //Output the current progress
            m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) * (10f/9f) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f) {
                //Change the Text to show the Scene is ready
                m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
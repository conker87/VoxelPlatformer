﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtendList : MonoBehaviour {

    /// Use this method to get all loaded objects of some type, including inactive objects. 
    /// This is an alternative to Resources.FindObjectsOfTypeAll (returns project assets, including prefabs), and GameObject.FindObjectsOfTypeAll (deprecated).
    public static List<T> FindObjectsOfTypeInactive<T>() {

        List<T> results = new List<T>();

        for (int i = 0; i < SceneManager.sceneCount; i++) {

            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded) {

                var allGameObjects = s.GetRootGameObjects();

                for (int j = 0; j < allGameObjects.Length; j++) {

                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));

                }
            }
        }

        return results;

    }
}

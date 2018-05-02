using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable), true), CanEditMultipleObjects]
public class InteractableEditor : Editor {

    Interactable[] components;

    private void OnEnable() {

        Object[] objects = targets;
        components = new Interactable[objects.Length];

        for (int i = 0; i < objects.Length; i++) {

            components[i] = objects[i] as Interactable;

        }
    }

    public override void OnInspectorGUI() {

        if (GUILayout.Button("Set InteractableID")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].InteractableID = components[i].gameObject.name + string.Format("({0},{1},{2})", components[i].transform.position.x, components[i].transform.position.y, components[i].transform.position.z);

            }

        }

        DrawDefaultInspector();

    }

}

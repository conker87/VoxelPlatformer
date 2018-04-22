using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomlyPickPBObjectColor)), CanEditMultipleObjects]
public class RandomlyPickPBObjectColorEditor : Editor {

    RandomlyPickPBObjectColor[] components;

    private void OnEnable() {

        Object[] objects = targets;
        components = new RandomlyPickPBObjectColor[objects.Length];

        for (int i = 0; i < objects.Length; i++) {

            components[i] = objects[i] as RandomlyPickPBObjectColor;

        }
    }

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        if (GUILayout.Button("Colorize Block")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].ChangeBlockColorsToRandom();

            }
        }
    }
}

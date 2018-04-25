using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GeometryBlock)), CanEditMultipleObjects]
public class RandomlyPickPBObjectColorEditor : Editor {

    GeometryBlock[] components;

    private void OnEnable() {

        Object[] objects = targets;
        components = new GeometryBlock[objects.Length];

        for (int i = 0; i < objects.Length; i++) {

            components[i] = objects[i] as GeometryBlock;

        }
    }

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        if (GUILayout.Button("Colorize Blocks")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].ChangeBlockColorsToRandom();

            }
        }

        if (GUILayout.Button("Randomize Position of Blocks")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].ChangeBlockPostionsToRandom();

            }
        }

        if (GUILayout.Button("Flip Block Layer Pattern")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].FlipBlockPattern();

            }
        }

        if (GUILayout.Button("Mirror Blocks")) {

            //for (int i = 0; i < components.Length; i++) {

                //components[i].MirrorBlocks();

            //}
        }
    }
}

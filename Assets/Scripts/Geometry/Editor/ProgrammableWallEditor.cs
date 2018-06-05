using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(ProgrammableWall)), CanEditMultipleObjects]
public class ProgrammableWallEditor : Editor {

    ProgrammableWall[] components;

    private void OnEnable() {

        Object[] objects = targets;
        components = new ProgrammableWall[objects.Length];

        for (int i = 0; i < objects.Length; i++) {

            components[i] = objects[i] as ProgrammableWall;

        }
    }

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        if (GUILayout.Button("Create Wall")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].CreateWall();

            }
        }

        if (GUILayout.Button("Reset Wall")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].ResetWall();

            }
        }

        if (GUILayout.Button("Colorize Wall")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].ColorizeAllObjects();

            }
        }

        if (GUILayout.Button("Finalize Wall")) {

            for (int i = 0; i < components.Length; i++) {

                DestroyImmediate(components[i]);

            }
        }
    }
}

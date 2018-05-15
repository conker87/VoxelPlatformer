using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProgrammableWall))]
public class ProgrammableWallEditor : Editor {

    ProgrammableWall component;

    private void OnEnable() {

        component = (ProgrammableWall) target;

    }

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        if (GUILayout.Button("Create Wall")) {

            component.CreateWall();

        }

        if (GUILayout.Button("Reset Wall")) {

            component.ResetWall();

        }

        if (GUILayout.Button("Colorize Wall")) {

            component.ColorizeAllObjects();

        }

        /* if (GUILayout.Button("Merge Wall")) {

            component.MergeWall();

        } */

    }
}

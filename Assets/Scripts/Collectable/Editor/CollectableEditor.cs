using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Collectable), true), CanEditMultipleObjects]
public class CollectableEditor : Editor {

    Collectable[] components;

    private void OnEnable() {

        Object[] objects = targets;
        components = new Collectable[objects.Length];

        for (int i = 0; i < objects.Length; i++) {

            components[i] = objects[i] as Collectable;

        }
    }

    public override void OnInspectorGUI() {

        if (GUILayout.Button("Set CollectableID")) {

            for (int i = 0; i < components.Length; i++) {

                if (components[i].CollectableType == CollectableType.Ability) {

                    continue;

                }


                components[i].CollectableID = components[i].CollectableType + string.Format("({0},{1},{2})",
                    components[i].transform.position.x.ToString("F1"),
                    components[i].transform.position.y.ToString("F1"),
                    components[i].transform.position.z.ToString("F1")
                );

            }

        }

        DrawDefaultInspector();

    }
}

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
                components[i].finalizeWall = false;

            }
        }

        if (GUILayout.Button("Reset Wall")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].ResetWall();
                components[i].finalizeWall = false;

            }
        }

        if (GUILayout.Button("Colorize Wall")) {

            for (int i = 0; i < components.Length; i++) {

                components[i].ColorizeAllObjects();
                components[i].finalizeWall = false;

            }
        }

        if (GUILayout.Button("Finalize Wall")) {

            for (int i = 0; i < components.Length; i++) {

                if (components[i].finalizeWall == true) { continue; }

                components[i].finalizeWall = true;

            }
        }

        for (int i = 0; i < components.Length; i++) {

            if (components[i].finalizeWall == true) {

                if (GUILayout.Button("Are You Sure? Yes.")) {

                    components[i].finalizeWall = false;
                    DestroyImmediate(components[i]);

                }

                if (GUILayout.Button("Are You Sure? No.")) {

                    components[i].finalizeWall = false;

                }
            }
        }
    }

    void MergeObjects(Transform pivot, int currentIndex) {

        /* MeshFilter[] meshFilters = pivot.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;

        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        GameObject mergedObject = new GameObject();

        mergedObject.transform.parent = pivot;
        mergedObject.AddComponent<MeshFilter>();

        mergedObject.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        mergedObject.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
        mergedObject.gameObject.SetActive(true); */

    }
}

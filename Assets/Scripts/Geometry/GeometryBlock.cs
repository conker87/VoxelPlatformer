﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProBuilder.Core;

public class GeometryBlock : MonoBehaviour {

    public Color[] randomColors = new Color[] { new Color(0.44f, 0.44f, 0.44f), new Color(0.45f, 0.45f, 0.45f),
        new Color(0.46f, 0.46f, 0.46f), new Color(0.47f, 0.47f, 0.47f), new Color(0.48f, 0.48f, 0.48f),
        new Color(0.49f, 0.49f, 0.49f), new Color(0.5f, 0.5f, 0.5f), new Color(0.51f, 0.51f, 0.51f),
        new Color(0.52f, 0.52f, 0.52f), new Color(0.53f, 0.53f, 0.53f), new Color(0.54f, 0.54f, 0.54f),
        new Color(0.56f, 0.56f, 0.56f), new Color(0.57f, 0.57f, 0.57f), new Color(0.58f, 0.58f, 0.58f),
        new Color(0.59f, 0.59f, 0.59f), new Color(0.6f, 0.6f, 0.6f), new Color(0.61f, 0.61f, 0.61f),
        new Color(0.62f, 0.62f, 0.62f), new Color(0.63f, 0.63f, 0.63f) };

    [SerializeField]
    List<pb_Object> childPBObjects = new List<pb_Object>();
    [SerializeField]
    List<Vector3> childPositions = new List<Vector3>();

    private System.Random rng = new System.Random();

    void PopulatePositions(bool ignoreNoneNamedBlocks = true) {

        childPositions.Clear();

        foreach (pb_Object pb in childPBObjects) {

            if (ignoreNoneNamedBlocks == true && pb.gameObject.name.Contains("Block") == false) {
                continue;
            }

            childPositions.Add(pb.transform.localPosition);
            
        }
    }

    void PopulateChildObjects(bool ignoreNoneNamedBlocks = true) {

        childPBObjects.Clear();

        foreach (pb_Object pb in GetComponentsInChildren<pb_Object>()) {

            if (ignoreNoneNamedBlocks == true && pb.gameObject.name.Contains("Block") == false) {
                continue;
            }

            childPBObjects.Add(pb);

        }

        childPBObjects = childPBObjects.OrderBy(a => rng.Next()).ToList();

    }

    public void ChangeBlockColorsToRandom(float minimum = 0.44f, float maximum = 0.62f) {

        PopulateChildObjects(false);

        int randomColorsLength = randomColors.Length;

        foreach (pb_Object currentBlock in childPBObjects) {

            int randomInt = Random.Range(0, randomColorsLength);

            // Random.seed = GameController.current.randomGeneratorSeed;
            Color color = randomColors[randomInt];

            // Cycle through each unique vertex in the cube (8 total), and assign a color to the index in the sharedIndices array.
            int si_len = currentBlock.sharedIndices.Length;
            Color[] vertexColors = new Color[si_len];

            for (int i = 0; i < si_len; i++) {

                vertexColors[i] = color;

            }

            // Now go through each face (vertex colors are stored the pb_Face class) and assign the pre-calculated index color to each index in the triangles array.
            Color[] colors = currentBlock.colors;

            for (int CurSharedIndex = 0; CurSharedIndex < currentBlock.sharedIndices.Length; CurSharedIndex++) {

                foreach (int CurIndex in currentBlock.sharedIndices[CurSharedIndex].array) {

                    colors[CurIndex] = vertexColors[CurSharedIndex];

                }
            }

            currentBlock.SetColors(colors);

            // In order for these changes to take effect, you must refresh the mesh object.
            currentBlock.Refresh();

        }

        childPBObjects.Clear();
        childPositions.Clear();

    }

    public void ChangeBlockPostionsToRandom() {

        PopulateChildObjects();
        PopulatePositions();

        PopulateChildObjects();

        for (int i = 0; i < childPBObjects.Count; i++) {

            childPBObjects[i].transform.localPosition = childPositions[i];

        }
    }

    public void FlipBlockPattern() {

        PopulateChildObjects(false);

        float maxYLevel = 0f;

        for (int i = 0; i < childPBObjects.Count; i++) {

            if (childPBObjects[i].transform.localPosition.y > maxYLevel) {

                maxYLevel = childPBObjects[i].transform.localPosition.y;

            }
        }

        for (int i = 0; i < childPBObjects.Count; i++) {

            float newYPosition = (childPBObjects[i].transform.localPosition.y - 1f < 0) ? maxYLevel : childPBObjects[i].transform.localPosition.y - 1f;

            childPBObjects[i].transform.localPosition =
            new Vector3(
                childPBObjects[i].transform.localPosition.x,
                newYPosition,
                childPBObjects[i].transform.localPosition.z
            );

        }


        childPBObjects.Clear();
        childPositions.Clear();

    }

    public void MirrorBlocks() {

        PopulateChildObjects();

    }
}

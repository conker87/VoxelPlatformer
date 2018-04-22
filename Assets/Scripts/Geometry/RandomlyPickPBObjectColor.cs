using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProBuilder.Core;

public class RandomlyPickPBObjectColor : MonoBehaviour {

    float randomMin = 0.44f, randomMax = 0.62f;

    List<pb_Object> childPBObjects = new List<pb_Object>();

    public float RandomMin {
        get {
            return randomMin;
        }

        set {
            randomMin = value;
        }
    }
    public float RandomMax {
        get {
            return randomMax;
        }

        set {
            randomMax = value;
        }
    }

    pb_Object[] PopulateChildObjects() {

        return GetComponentsInChildren<pb_Object>();

    }

    public void ChangeBlockColorsToRandom(float minimum = 0.44f, float maximum = 0.62f) {

        childPBObjects = PopulateChildObjects().ToList<pb_Object>();

        foreach (pb_Object currentBlock in childPBObjects) {

            if (currentBlock.name != "Block") {
                continue;
            }

            float random = Random.Range(minimum, maximum);

            // Random.seed = GameController.current.randomGeneratorSeed;
            Color color = new Color(random, random, random);

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

        childPBObjects = null;

    }
}

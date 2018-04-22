using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProBuilder.Core;

public class RandomlyPickPBObjectColor : MonoBehaviour {

    public float randomMin = 0.44f, randomMax = 0.62f;
    float random;
    pb_Object pb_Object;

    void Start () {

        pb_Object = GetComponent<pb_Object>();

        // Random.seed = GameController.current.randomGeneratorSeed;

        random = Random.Range(randomMin, randomMax);
        Color color = new Color(random, random, random);

        // Cycle through each unique vertex in the cube (8 total), and assign a color to the index in the sharedIndices array.
        int si_len = pb_Object.sharedIndices.Length;
        Color[] vertexColors = new Color[si_len];

        for (int i = 0; i < si_len; i++) {

            vertexColors[i] = color;

        }

        // Now go through each face (vertex colors are stored the pb_Face class) and assign the pre-calculated index color to each index in the triangles array.
        Color[] colors = pb_Object.colors;

        for (int CurSharedIndex = 0; CurSharedIndex < pb_Object.sharedIndices.Length; CurSharedIndex++) {

            foreach (int CurIndex in pb_Object.sharedIndices[CurSharedIndex].array) {

                colors[CurIndex] = vertexColors[CurSharedIndex];

            }
        }

        pb_Object.SetColors(colors);

        // In order for these changes to take effect, you must refresh the mesh object.
        pb_Object.Refresh();

    }
}

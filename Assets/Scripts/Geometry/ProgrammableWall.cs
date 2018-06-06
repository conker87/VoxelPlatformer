using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ProBuilder.Core;

using UnityEngine;

[ExecuteInEditMode]
public class ProgrammableWall : MonoBehaviour {

    [SerializeField] public Transform pivot;

    [Header("Block Pools & Colors")]
    [SerializeField] List<pb_Object> blockPool = new List<pb_Object>();
    [SerializeField] List<pb_Object> endBlockPool = new List<pb_Object>();  // blockSizeX / 2, blockSizeY.
    [SerializeField] Color[] randomColors = new Color[] { new Color(0.44f, 0.44f, 0.44f), new Color(0.45f, 0.45f, 0.45f),
        new Color(0.46f, 0.46f, 0.46f), new Color(0.47f, 0.47f, 0.47f), new Color(0.48f, 0.48f, 0.48f),
        new Color(0.49f, 0.49f, 0.49f), new Color(0.5f, 0.5f, 0.5f), new Color(0.51f, 0.51f, 0.51f),
        new Color(0.52f, 0.52f, 0.52f), new Color(0.53f, 0.53f, 0.53f), new Color(0.54f, 0.54f, 0.54f),
        new Color(0.56f, 0.56f, 0.56f), new Color(0.57f, 0.57f, 0.57f), new Color(0.58f, 0.58f, 0.58f),
        new Color(0.59f, 0.59f, 0.59f), new Color(0.6f, 0.6f, 0.6f), new Color(0.61f, 0.61f, 0.61f),
        new Color(0.62f, 0.62f, 0.62f), new Color(0.63f, 0.63f, 0.63f) };
    [SerializeField] string forceTag = "Untagged";

    [Header("Block Size")]
    [SerializeField] float blockSizeY = 1f;
    [SerializeField] float blockSizeX = 2f;
    [SerializeField] Vector3 blockGlobalScale = Vector3.one;

    float previousBlockSizeY, previousBlockSizeX;
    Vector3 previousBlockGlobalScale;

    [Header("Wall Size")]
    [Range(1, 50)] [SerializeField] int layerSize = 6;
    [Range(1, 50)] [SerializeField] int columnSize = 8;

    float previousLayerSize, previousColumnSize;

    // bool doNotInstantiate = false;

    [Header("Wall Settings")]
    [SerializeField] WallDirection wallDirection = WallDirection.North;
    [SerializeField] bool stagger = true;
    [SerializeField] bool flipStagger = false, addEndingBlock = false, isAcshullyFloor = false, evenSize = true, flipGrowDirection = false;

    WallDirection previousWallDirection;
    bool previousStagger, previousFlipStagger, previousAddEndingBlock, previousIsAcshullyFloor, previousEvenSize, previousFlipGrowDirection;

    bool doEndBlock = false;

    public void CreateWall() {

        if (CheckForMergedObjects()) {

            // Debug.LogError("This prefab contains a merged object, if you want to create a new wall then add another ProgrammableWall prefab into the scene.");
            return;

        }

        if (blockPool.Count == 0 || endBlockPool.Count == 0) {

            Debug.LogError("This prefab does not have any blocks assigned to the Pool Lists.");
            return;

        }
        
        ResetWall();

        pivot.localEulerAngles = (isAcshullyFloor == true) ? new Vector3(-90f, 0f, 0f) : new Vector3(0f, 0f, 0f);

        if (wallDirection == WallDirection.North) {
            pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, -90f, 0f);
        }
        if (wallDirection == WallDirection.East) {
            pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, 0f, 0f);
        }
        if (wallDirection == WallDirection.South) {
            pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, 90f, 0f);
        }
        if (wallDirection == WallDirection.West) {
            pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, 180f, 0f);
        }

        for (int j = 0; j < layerSize; j++) {

            GameObject currentLayer = new GameObject {
                name = j.ToString(),

            };
            currentLayer.transform.parent = pivot;

            for (int i = 0; i < columnSize; i++) {

                // doNotInstantiate = false;
                float currentLocalX = i * (blockSizeX);
                float currentLocalY = (flipGrowDirection == true) ? j * (-blockSizeY) : j * (blockSizeY);
                Vector3 currentLocalPostion = new Vector3(currentLocalX, currentLocalY, 0f);

                if (addEndingBlock == true && stagger == true) {

                    doEndBlock = false;

                    Vector3 endPosition = currentLocalPostion;

                    if (evenSize == false) {

                        if ((flipStagger == false && j % 2 == 0 && i == 0)
                            || (flipStagger == true && j % 2 != 0 && i == 0)) {

                            endPosition = new Vector3(currentLocalPostion.x, currentLocalPostion.y, 0f);
                            doEndBlock = true;

                        }

                        if ((flipStagger == false && j % 2 != 0 && i == columnSize - 1)
                            || (flipStagger == true && j % 2 == 0 && i == columnSize - 1)) {

                            endPosition = new Vector3(currentLocalPostion.x + blockSizeX, currentLocalPostion.y, 0f);
                            doEndBlock = true;

                        }
                    }

                    if (evenSize == true) {

                        if ((flipStagger == false && j % 2 == 0 && i == 0)
                            || (flipStagger == true && j % 2 != 0 && i == 0)) {

                            endPosition = new Vector3(currentLocalPostion.x, currentLocalPostion.y, 0f);
                            doEndBlock = true;

                        }

                        if ((flipStagger == true && j % 2 != 0 && i == columnSize - 1)
                            || (flipStagger == false && j % 2 == 0 && i == columnSize - 1)) {

                            endPosition = new Vector3(currentLocalPostion.x + (blockSizeX / 2f), currentLocalPostion.y, 0f);
                            doEndBlock = true;

                        }
                    }

                    if (doEndBlock == true) {

                        pb_Object endObj = Instantiate(endBlockPool[Random.Range(0, endBlockPool.Count)]) as pb_Object;

                        endObj.transform.parent = pivot;
                        endObj.transform.localPosition = endPosition;

                        endObj.gameObject.name = string.Format("EndBlock({0},{1})", j, i);
                        endObj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

                        endObj.transform.parent = currentLayer.transform;
                        endObj.tag = forceTag;

                        ColorizeObject(endObj);
                    }
                }

                if (stagger == true) {

                    if (flipStagger == false && j % 2 == 0) {

                        currentLocalPostion = new Vector3(currentLocalPostion.x + (blockSizeX / 2f), currentLocalPostion.y, 0f);

                    }

                    if (flipStagger == true & j % 2 != 0) {

                        currentLocalPostion = new Vector3(currentLocalPostion.x + (blockSizeX / 2f), currentLocalPostion.y, 0f);

                    }
                }

                if (stagger == true && evenSize == true) {

                    if ((flipStagger == true && j % 2 != 0 && i == columnSize - 1)
                        || (flipStagger == false && j % 2 == 0 && i == columnSize - 1)) {

                        continue;

                    }
                }

                pb_Object obj = Instantiate(blockPool[Random.Range(0, blockPool.Count)]) as pb_Object;

                obj.transform.parent = pivot;
                obj.transform.localPosition = currentLocalPostion;

                obj.gameObject.name = string.Format("Block({0},{1})", j, i);
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

                obj.transform.parent = currentLayer.transform;
                obj.tag = forceTag;

                ColorizeObject(obj);

            }
        }
    }

    public void ResetWall() {

        do {

            for (int i = pivot.GetComponentsInChildren<Transform>().Length - 1; i >= 0; i--) {

                if (pivot.GetComponentsInChildren<Transform>()[i].gameObject.name.Contains("MergedObject")
                    || pivot.GetComponentsInChildren<Transform>()[i].gameObject.name.Contains("Pivot")
                    || pivot.GetComponentsInChildren<Transform>()[i].gameObject.name.Contains("Prevent")) {
                    
                    continue;

                }

                DestroyImmediate(pivot.GetComponentsInChildren<Transform>()[i].gameObject);

            }

        } while (pivot.GetComponentsInChildren<Transform>().Count(a => a.gameObject.name.Contains("Block")) > 0);
    }

    public void MergeWall() {

        Debug.LogWarningFormat("ProgrammableWall::MergeWall -- Not currently implimented.");

    }

    public void ScaleWall() {

        pivot.transform.localScale = new Vector3(1f * blockGlobalScale.x, 1f * blockGlobalScale.y, 1f * blockGlobalScale.z);

    }

    void ColorizeObject(pb_Object obj) {

        int randomColorsLength = randomColors.Length;
        int randomInt = Random.Range(0, randomColorsLength);

        // Random.seed = GameController.current.randomGeneratorSeed;
        Color color = randomColors[randomInt];

        // Cycle through each unique vertex in the cube (8 total), and assign a color to the index in the sharedIndices array.
        int si_len = obj.sharedIndices.Length;
        Color[] vertexColors = new Color[si_len];

        for (int i = 0; i < si_len; i++) {

            vertexColors[i] = color;

        }

        // Now go through each face (vertex colors are stored the pb_Face class) and assign the pre-calculated index color to each index in the triangles array.
        Color[] colors = obj.colors;

        for (int CurSharedIndex = 0; CurSharedIndex < obj.sharedIndices.Length; CurSharedIndex++) {

            foreach (int CurIndex in obj.sharedIndices[CurSharedIndex].array) {

                colors[CurIndex] = vertexColors[CurSharedIndex];

            }
        }

        obj.SetColors(colors);

        // In order for these changes to take effect, you must refresh the mesh object.
        obj.Refresh();

    }

    public void ColorizeAllObjects() {

        foreach (pb_Object obj in pivot.GetComponentsInChildren<pb_Object>()) {

            ColorizeObject(obj);
            
        }
    }

    bool CheckForMergedObjects() {

        return pivot.GetComponentsInChildren<pb_Object>().FirstOrDefault(a => a.gameObject.name.Contains("MergedObject"));

    }

    void Start() {

        foreach (Transform transform in GetComponentsInChildren<Transform>()) {

            if (transform.gameObject.name.Contains("Pivot")) {

                pivot = transform;
                break;

            }
        }
    }

    void Update() {
        
        if (previousColumnSize != columnSize
            || previousLayerSize != layerSize
            || previousBlockSizeX != blockSizeX
            || previousBlockSizeY != blockSizeY
            || previousWallDirection != wallDirection
            || previousStagger != stagger
            || previousFlipStagger != flipStagger
            || previousAddEndingBlock != addEndingBlock
            || previousIsAcshullyFloor != isAcshullyFloor
            || previousEvenSize != evenSize
            || previousFlipGrowDirection != flipGrowDirection) {

            pivot.transform.localScale = Vector3.one;

            CreateWall();

        }

        if (previousBlockGlobalScale != blockGlobalScale
            || blockGlobalScale != pivot.localScale) {

            ScaleWall();

        }
    }

    void LateUpdate() {

        previousColumnSize = columnSize;
        previousLayerSize = layerSize;

        previousBlockSizeX = blockSizeX;
        previousBlockSizeY = blockSizeY;
        previousBlockGlobalScale = blockGlobalScale;

        previousWallDirection = wallDirection;

        previousStagger = stagger;
        previousFlipStagger = flipStagger;
        previousAddEndingBlock = addEndingBlock;
        previousIsAcshullyFloor = isAcshullyFloor;
        previousEvenSize = evenSize;
        previousFlipGrowDirection = flipGrowDirection;

    }
}

enum WallDirection { North, East, South, West }
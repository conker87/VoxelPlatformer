using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene {

    public string CutsceneID = "_CHANGE_ME_";
    public int CutsceneLevelID = -1;

    public List<CutsceneAction> CutsceneActions = new List<CutsceneAction>();

}

public class CutsceneAction {

    public Image CutsceneSpeakingCharacter;
    public string CutsceneSpeakingLine;
    public CutsceneMovement CutsceneMovement;

    public CutsceneMovementFrom CutsceneMovementFrom;
    public Vector3 CutsceneMovementFromPosition;
    public CutsceneMovementTime CutsceneMovementTime;
    public Vector3 CutsceneMovementToPosition;

    public float CutsceneMovementSpeed;

}

public enum CutsceneMovement { NoMovement, Movement }
public enum CutsceneMovementFrom { GivenPosition, CurrentLocation }
public enum CutsceneMovementTime { StartOfLine, DuringLine, EndOfLine }
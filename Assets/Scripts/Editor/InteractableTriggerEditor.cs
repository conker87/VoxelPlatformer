using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractableTrigger))]
public class InteractableTriggerEditor : Editor {

    public override void OnInspectorGUI() {

        // InteractableTrigger interactableTrigger = (InteractableTrigger) target;

        // EditorGUILayout.LabelField("Boobs", interactableTrigger.OnTriggerExit.ToString());

    }

}

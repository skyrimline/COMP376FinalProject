using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (ToePart2D))]

public class ToePart2DEditor : SCGEditor {

    ToePart2D toe;

    void Prepare () {
        toe = (ToePart2D) target;

        if (toe.footPart && toe.footPart.body)
            SCGAnimationToolEditor.SetJointDisplayTarget (toe.footPart.body);
    }

    public override void OnInspectorGUI () {
        Prepare ();
        base.OnInspectorGUI ();
    }

    private void OnSceneGUI () {
        SCGAnimationToolEditor.ShowJoint ();
    }
}
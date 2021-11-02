using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[CustomEditor (typeof (SCGHandle), true)]
public class SCGHandleEditor : SCGEditor {
    public SCGHandle handle;

    public void Prepare () {
        handle = (SCGHandle) target;

        if (handle && handle.body)
            SCGAnimationToolEditor.SetJointDisplayTarget (handle.body);

    }

    public override void OnInspectorGUI () {
        Prepare ();
        base.OnInspectorGUI ();
    }

    protected virtual void OnSceneGUI () {
        SCGAnimationToolEditor.ShowJoint ();
        Tools.current = Tool.Move;
    }
}
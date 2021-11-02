using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[CustomEditor (typeof (PerspectiveHandle2D))]
public class PerspectiveHandle2DEditor : SCGHandleEditor {

    float value;

    public override void OnInspectorGUI () {

        base.OnInspectorGUI ();

        if (handle && handle.body) {

            if (handle.body.simulatePerspective) {
                EditorGUI.indentLevel++;

                value = EditorGUILayout.Slider ("Angle", handle.GetValue (), 0f, 180f);

                EditorGUILayout.BeginHorizontal ();

                EditorGUILayout.LabelField ("", GUILayout.Width (150));

                float smallButtonSize = 65;

                if (Button ("0", smallButtonSize / 2f)) {
                    SetValue (0);
                }
                if (Button ("45", smallButtonSize / 2f)) {

                    SetValue (45);

                }
                if (Button ("90", smallButtonSize / 2f)) {

                    SetValue (90);

                }
                if (Button ("135", smallButtonSize / 2f)) {

                    SetValue (135);

                }
                if (Button ("180", smallButtonSize / 2f)) {
                    SetValue (180);

                }

                EditorGUILayout.EndHorizontal ();

                EditorGUI.indentLevel--;

            } else {
                handle.body.SetPerspective (0);
            }

            if (GUI.changed) {
                Validate ();
            }
        }

    }

    void Validate () {

        if (handle && handle.body) {
            EditorUtility.SetDirty (this);
            handle.GetComponent<PerspectiveHandle2D> ().SetValue (value, true);

            handle.body.ApplyPerspective ();
        }
    }

    protected override void OnSceneGUI () {
        base.OnSceneGUI ();

        if (handle && handle.body) {

            Handles.DrawLine (handle.body.transform.position - new Vector3 (handle.GetOffset (), 0, 0), handle.body.transform.position + new Vector3 (handle.GetOffset (), 0, 0));

        }

    }

    void SetValue (int v) {

        if (handle) {
            value = (v);
            EditorUtility.SetDirty (this);
            EditorUtility.SetDirty (handle);
            Validate ();
        }

    }
}
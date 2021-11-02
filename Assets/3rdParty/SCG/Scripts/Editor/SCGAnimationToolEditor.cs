using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]

[CustomEditor (typeof (SCGAnimationTool))]

public class SCGAnimationToolEditor : SCGEditor {

    SCGAnimationTool tool;

    static CharacterBody2D body;

    HandPart2D rightHand;
    HandPart2D leftHand;

    int tab;

    GameObject currentActive;

    GUIStyle jointButton;

    bool relaxRightHand, relaxLeftHand;

    public static float jointSize = 0.175f;

    public static float jointFactor = 1f;

    static CharacterBody2D targetBody;
    static BodyPart2D targetPart;

    void Prepare () {

        jointButton = new GUIStyle (GUI.skin.button);
        jointButton.normal.textColor = Color.white;

        tool = (SCGAnimationTool) target;

        if (tool) body = tool.body;

        if (body) {

            SetJointDisplayTarget (body);
            rightHand = body.hand.GetComponent<HandPart2D> ();
            leftHand = body.hand.buddy.GetComponent<HandPart2D> ();
        }

    }

    public override void OnInspectorGUI () {

        Prepare ();

        base.OnInspectorGUI ();

        if (body) {

            Line ();

            Animate ();

            Pose ();

            if (GUI.changed) {
                Validate ();
            }

        }

    }

    private void OnSelectionChange () {
        Repaint ();
    }

    void Animate () {
        Title ("Settings");

        EditorGUI.indentLevel++;

        body.simulateGround = EditorGUILayout.Toggle ("Simulate Ground", body.simulateGround);
        SCGAnimationTool.simulateSkirt = EditorGUILayout.Toggle ("Simulate Skirt & Shirttail", SCGAnimationTool.simulateSkirt);

        if (jointFactor == 0) {
            if (Button ("Show Joint Handles")) {

                jointFactor = 1;
            }
        } else {
            if (Button ("Hide Joint Handles")) {
                jointFactor = 0;

            }
            jointSize = EditorGUILayout.Slider ("Joint Handle Size", jointSize, 0.05f, 1f);

        }

        EditorGUI.indentLevel--;

        Line ();

    }

    void Pose () {
        EditorGUILayout.BeginHorizontal ();
        if (Button ("Relax")) {
            body.Relax ();
        }

        if (Button ("Straight")) {
            body.StraightenBody ();

        }

        if (Button ("A Pose")) {
            body.APose ();

        }

        if (Button ("T Pose")) {
            body.TPose ();

        }

        EditorGUILayout.EndHorizontal ();

        EditorGUI.BeginChangeCheck ();

        bool sitting = EditorGUILayout.Toggle ("Sitting", body.Sitting);

        if (EditorGUI.EndChangeCheck ()) {
            body.Sitting = sitting;
        }

        Line ();

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.BeginVertical ();

        EditorGUILayout.BeginHorizontal ();

        Title ("Right Hand", 80);

        if (relaxRightHand) {
            if (Button ("Hold")) {
                rightHand.Hold ();
                relaxRightHand = false;

            }
        } else {
            if (Button ("Relax")) {
                rightHand.Straight ();
                relaxRightHand = true;
            }
        }

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.EndVertical ();

        EditorGUILayout.BeginVertical ();

        EditorGUILayout.BeginHorizontal ();
        Title ("Left Hand", 80f);

        if (relaxLeftHand) {
            if (Button ("Hold")) {
                leftHand.Hold ();
                relaxLeftHand = false;

            }
        } else {
            if (Button ("Relax")) {
                leftHand.Straight ();
                relaxLeftHand = true;
            }
        }

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.EndVertical ();

        EditorGUILayout.EndHorizontal ();

    }

    public void Validate () {

        if (this) EditorUtility.SetDirty (this);
        EditorUtility.SetDirty (body);
        body.ApplyPerspective ();

    }

    static void DrawHandle (BodyPart2D part, float size, Color c) {
        if (part) {
            DrawHandle (part.transform, part, size, c);
        }
    }

    static void DrawBodyHandle (BodyPart2D upper, float size, Color c) {
        if (targetBody) {
            Handles.color = c;

            if (Handles.Button (upper.renderer.transform.position, Quaternion.identity, size * 0.8f * jointFactor, size * 0.8f * jointFactor, Handles.CubeHandleCap)) {

                Undo.RecordObject (targetBody.floor, "Change Side");

                Vector3 oldPerspective = targetBody.perspectiveHandle.transform.position;

                targetBody.SwitchTurn ();
                targetBody.perspectiveHandle.transform.position = oldPerspective;

            }

        }
    }

    static void DrawGroundHandle () {
        if (targetBody) {

            if (!targetBody.simulateGround) {
                Handles.color = Color.red - new Color (0, 0, 0f, 0.5f);

            } else {
                Handles.color = Color.cyan;

            }

            if (Handles.Button (targetBody.floor.transform.position + new Vector3 (0, (jointSize * 6f * jointFactor) + 0.5f, 0), Quaternion.Euler (90, -180f, 0), jointSize * 6f * jointFactor, jointSize * 6f * jointFactor, Handles.ArrowHandleCap)) {
                targetBody.simulateGround = !targetBody.simulateGround;
                targetBody.hip.SimulateHip ();

            }

        }

    }

    static void DrawPerspectiveHandle () {
        if (targetBody && targetBody.perspectiveHandle) {

            if (Selection.activeGameObject != targetBody.perspectiveHandle.gameObject) {
                Handles.color = Color.cyan - new Color (0, 0, 0f, 0.8f);

            } else {
                Handles.color = Color.cyan;

            }

            if (Handles.Button (targetBody.perspectiveHandle.transform.position, Quaternion.identity, jointSize * 1.2f * jointFactor, jointSize * 1.2f * jointFactor, Handles.SphereHandleCap)) {
                if (Selection.activeGameObject != targetBody.perspectiveHandle.gameObject) Selection.activeGameObject = targetBody.perspectiveHandle.gameObject;
                Tools.current = Tool.Move;
            }

        }

    }

    static void DrawIKHandle (BodyPart2D foot) {
        if (foot && foot.handle) {

            Color handleColor = Color.red;
            if (foot.handle.type == BodyPartHandle2D.Type.Arm) {
                handleColor = Color.green;
            }

            if (Selection.activeGameObject != foot.handle.gameObject) {
                Handles.color = handleColor - new Color (0, 0, 0f, 0.5f);

            } else {
                Handles.color = handleColor;

            }

            if (Handles.Button (foot.handle.transform.position, Quaternion.identity, jointSize * 1f * jointFactor, jointSize * 1f * jointFactor, Handles.SphereHandleCap)) {

                Tools.current = Tool.Move;

                Selection.activeGameObject = foot.handle.gameObject;

            }

        }

    }

    static void DrawHandle (Transform part, BodyPart2D bodyPart, float size, Color c) {

        if (part) {
            EditorGUI.BeginChangeCheck ();

            Handles.color = c;

            Quaternion rot = Handles.Disc (part.rotation, part.position, new Vector3 (0, 0, 1), size * jointFactor, true, 0);
            if (EditorGUI.EndChangeCheck () && jointFactor > 0) {

                Undo.RecordObject (part, "Rotation");
                if (bodyPart.type == BodyPart2D.Type.Leg) targetBody.simulateGround = false;

                part.transform.rotation = rot;

                if (Selection.activeGameObject != part.gameObject) {
                    Selection.activeGameObject = part.gameObject;
                    Tools.current = Tool.None;
                }

            }

            Handles.color = Color.white - new Color (0, 0, 0, 0.5f);

            if (Handles.Button (part.position, Quaternion.identity, size * 0.7f * jointFactor, size * 0.7f * jointFactor, Handles.SphereHandleCap)) {

                if (Selection.activeGameObject != part.gameObject) {
                    Selection.activeGameObject = part.gameObject;
                    Tools.current = Tool.None;
                }

                if (bodyPart.gameObject == part.gameObject) {
                    Undo.RecordObject (part, "Straighten");

                    bodyPart.StraightenSelf ();
                } else {
                    Undo.RecordObject (part, "Straighten");

                    part.GetComponent<ToePart2D> ().StraightenSelf ();
                }

            }

            Handles.color = c;

            if (bodyPart.type == BodyPart2D.Type.Face || bodyPart.isHand ()) {
                if (Handles.Button (part.position - new Vector3 (0, size, 0), Quaternion.identity, size * 0.5f * jointFactor, size * 0.5f * jointFactor, Handles.CubeHandleCap)) {
                    if (bodyPart.gameObject == part.gameObject) {

                        Undo.RecordObject (part, "Change Side");
                        Tools.current = Tool.None;

                        bodyPart.SwitchSide ();
                        bodyPart.SetThisDirty ();
                    }

                }
            }
        }

    }
    static void DrawHandle (FootPart2D part, float size, Color c) {
        if (part) {
            DrawHandle (part.transform, part, size, c);
            DrawHandle (part.toeTransform, part, size / 2f, c);
        }

    }

    public static void SetJointDisplayTarget (CharacterBody2D body) {
        targetBody = body;
    }

    public static void ShowJoint () {
        if (targetBody) {

            Color drop = new Color (0.45f, 0.45f, 0.45f, 0);
            DrawBodyHandle (targetBody.flank, jointSize, Color.blue);
            DrawHandle (targetBody.head, jointSize, Color.blue);
            DrawHandle (targetBody.shoulder, jointSize, Color.blue);
            DrawHandle (targetBody.armUpper, jointSize, Color.green);
            DrawHandle (targetBody.armUpper.buddy, jointSize, Color.green - drop);
            DrawHandle (targetBody.armLower, jointSize, Color.green);
            DrawHandle (targetBody.armLower.buddy, jointSize, Color.green - drop);
            DrawHandle (targetBody.hand, jointSize, Color.green);
            DrawHandle (targetBody.hand.buddy, jointSize, Color.green - drop);
            DrawHandle (targetBody.flank, jointSize, Color.blue);
            DrawIKHandle (targetBody.foot);
            DrawIKHandle (targetBody.foot.buddy);
            DrawIKHandle (targetBody.hand);
            DrawIKHandle (targetBody.hand.buddy);
            DrawHandle (targetBody.legUpper, jointSize, Color.red);
            DrawHandle (targetBody.legUpper.buddy, jointSize, Color.red - drop);
            DrawHandle (targetBody.legLower, jointSize, Color.red);
            DrawHandle (targetBody.legLower.buddy, jointSize, Color.red - drop);
            DrawHandle (targetBody.foot.GetComponent<FootPart2D> (), jointSize, Color.red);
            DrawHandle (targetBody.foot.buddy.GetComponent<FootPart2D> (), jointSize, Color.red - drop);
            DrawPerspectiveHandle ();
            DrawGroundHandle ();

        }
    }

    private void OnSceneGUI () {
        ShowJoint ();
    }

}
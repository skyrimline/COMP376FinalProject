using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (BodyPart2D), true)]

[System.Serializable]
public class BodyPart2DEditor : SCGEditor {

    bool toggleProportion = true;
    bool toggleRotation = true;
    bool toggleColorOverride = true;

    bool toggleMain;

    SerializedProperty propRely;
    SerializedProperty side;
    SerializedProperty ballScale;
    SerializedProperty rendererScale;

    float minProp = 0, maxProp = 2f;

    [SerializeField] BodyPart2D bodyPart;
    GUIStyle title;
    GUIStyle boldFoldout;

    void OnEnable () {

        propRely = serializedObject.FindProperty (nameof (BodyPart2D.relyOnParent));
        side = serializedObject.FindProperty (nameof (BodyPart2D.side));
        rendererScale = serializedObject.FindProperty (nameof (BodyPart2D.scaleBy));

        toggleMain = true;
        toggleProportion = false;
        toggleRotation = false;
        toggleColorOverride = false;
    }

    void ShowStatus () {

        EditorStyles.label.fontStyle = FontStyle.Bold;

        bodyPart.type = (BodyPart2D.Type) EditorGUILayout.EnumPopup ("Type", bodyPart.type, boldEnum);

        EditorStyles.label.fontStyle = FontStyle.Normal;
        GUIStyle warning = new GUIStyle (GUI.skin.label);

        warning.fontStyle = FontStyle.Bold;

        if ((bodyPart.type == BodyPart2D.Type.Arm || bodyPart.type == BodyPart2D.Type.Leg) && bodyPart.isDirectlyConnected ()) {

            if (!bodyPart.buddy) {

                warning.normal.textColor = Color.red;

                EditorGUILayout.LabelField ("Warning : This Arm/Leg Part doesn't have a syncing buddy", warning);

            } else if (bodyPart.buddy == bodyPart) {

                bodyPart.buddy = null;

            } else {

                warning.normal.textColor = Color.blue;
                EditorGUILayout.LabelField ("Syncing with : " + bodyPart.buddy, warning);

            }

        }
        if (bodyPart.type == BodyPart2D.Type.Fat) {
            EditorGUI.indentLevel++;
            bodyPart.isPerspectivePart = EditorGUILayout.Toggle ("Is Perspective Part", bodyPart.isPerspectivePart);
            EditorGUI.indentLevel--;
            warning.normal.textColor = Color.blue;
            EditorGUILayout.LabelField ("Syncing with : " + bodyPart.buddy, warning);
        }

        EditorGUI.indentLevel++;

        EditorStyles.label.fontStyle = FontStyle.Bold;

        if (bodyPart.parentPart) {

            StringBuilder sb = new StringBuilder ();
            sb.Append ("Parent : ");
            sb.Append (bodyPart.parentPart.name.Trim ());

            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (sb.ToString (), GUILayout.ExpandWidth (true));

            EditorGUILayout.EndHorizontal ();

        }

        if (bodyPart.childPart) {
            StringBuilder sb = new StringBuilder ();
            sb.Append ("Child : ");
            sb.Append (bodyPart.childPart.name.Trim ());

            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (sb.ToString (), GUILayout.ExpandWidth (true));

            EditorGUILayout.EndHorizontal ();

        }

        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUI.indentLevel--;
        EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

    }

    void Prepare () {

        bodyPart = (BodyPart2D) target;
        SCGAnimationToolEditor.SetJointDisplayTarget (bodyPart.body);

        title = new GUIStyle (GUI.skin.label);
        title.fontStyle = FontStyle.Bold;
        boldFoldout = new GUIStyle (EditorStyles.foldout);
        boldFoldout.fontStyle = FontStyle.Bold;
        boldEnum = new GUIStyle (EditorStyles.popup);
        boldEnum.fontStyle = FontStyle.Bold;
    }

    void ShowSize () {
        EditorGUILayout.LabelField ("Size", title);

        EditorGUI.BeginChangeCheck ();

        float width = EditorGUILayout.Slider ("Width", bodyPart.Width, bodyPart.GetMinWidth (), bodyPart.GetMaxWidth ());

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObjects (bodyPart.GetRenderers (), "Modify Body Part Size");
            bodyPart.SetWidth (width);

        }

        EditorGUI.BeginChangeCheck ();

        float height = EditorGUILayout.Slider ("Height", bodyPart.Height, bodyPart.GetMinHeight (), bodyPart.GetMaxHeight ());

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObjects (bodyPart.GetRenderers (), "Modify Body Part Size");
            bodyPart.SetHeight (height);
        }

    }

    void ShowRenderers () {
        EditorGUILayout.LabelField ("Renderers", title);
        bodyPart.renderer = (SpriteRenderer) EditorGUILayout.ObjectField ("Renderer", bodyPart.renderer, typeof (SpriteRenderer), true);

        if (bodyPart.renderer) {

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.LabelField ("", GUILayout.MaxWidth (EditorGUIUtility.currentViewWidth * 0.425f));
            EditorGUILayout.LabelField ("Pivot", GUILayout.Width (35));

            bodyPart.SetPivot (EditorGUILayout.Vector2Field ("", bodyPart.Pivot, GUILayout.MaxWidth (100)));
            EditorGUILayout.EndHorizontal ();

        }

        bodyPart.ball = (SpriteRenderer) EditorGUILayout.ObjectField ("Ball", bodyPart.ball, typeof (SpriteRenderer), true);

        if (bodyPart.ball) {

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.LabelField ("", GUILayout.MaxWidth (EditorGUIUtility.currentViewWidth * 0.425f));
            EditorGUILayout.LabelField ("Pivot", GUILayout.Width (35));

            bodyPart.SetBallPivot (EditorGUILayout.Vector2Field ("", bodyPart.BallPivot, GUILayout.MaxWidth (100)));

            EditorGUILayout.EndHorizontal ();
        }

        if (bodyPart.type == BodyPart2D.Type.Leg || bodyPart.type == BodyPart2D.Type.Arm)

            bodyPart.wrapper = (SpriteRenderer) EditorGUILayout.ObjectField ("Wrapper", bodyPart.wrapper, typeof (SpriteRenderer), true);

    }

    public override void OnInspectorGUI () {

        Prepare ();

        ShowStatus ();

        EditorGUILayout.Space ();

        ShowSize ();

        EditorGUILayout.Space ();

        ShowRenderers ();

        EditorGUILayout.Space ();

        base.OnInspectorGUI ();

        EditorGUILayout.Space ();

        toggleMain = EditorGUILayout.Foldout (toggleMain, "Settings", boldFoldout);

        Settings ();
        Validate ();

    }

    void Validate () {
        if (GUI.changed && bodyPart) {
            EditorUtility.SetDirty (bodyPart);
            serializedObject.ApplyModifiedProperties ();
            bodyPart.Validate ();
            CharacterEditor2D.quickEdit = false;
        }
    }

    void Settings () {
        if (toggleMain) {

            EditorGUI.indentLevel++;

            Section[] ProportionSec = { Renderer_Size };
            NewSegment ("Proportion", ProportionSec, ref toggleProportion);

            Section[] RotationSec = { Rotation };
            NewSegment ("Rotation", RotationSec, ref toggleRotation);

            Section[] SyncingSec = { Mirroring, Color_Override };
            NewSegment ("Syncing", SyncingSec, ref toggleColorOverride);
        }
    }

    void Renderer_Size () {

        EditorGUILayout.PropertyField (rendererScale);
        EditorGUILayout.PropertyField (propRely);

        DrawMinMax ("Width Min/Max", ref bodyPart.minWidthScale, ref bodyPart.maxWidthScale, minProp, maxProp);
        DrawMinMax ("Height Min/Max", ref bodyPart.minHeightScale, ref bodyPart.maxHeightScale, minProp, maxProp);
        bodyPart.circleProportion = EditorGUILayout.Slider ("Ball Scale", bodyPart.circleProportion, 0, 1f);

    }

    void Rotation () {
        bodyPart.IK = EditorGUILayout.Toggle ("IK (Experimental)", bodyPart.IK);
        DrawMinMax ("Angle Min/Max", ref bodyPart.minRotation, ref bodyPart.maxRotation, -360f, 360f);
    }

    void Mirroring () {

        EditorGUILayout.PropertyField (side);

        if (bodyPart.side != BodyPart2D.PartSide.Center || bodyPart.type == BodyPart2D.Type.Fat) {
            bodyPart.buddy = (BodyPart2D) EditorGUILayout.ObjectField ("Buddy Part", bodyPart.buddy, typeof (BodyPart2D), true);

        }

    }
    void Color_Override () {

        bodyPart.colorRef = (BodyPart2D) EditorGUILayout.ObjectField ("Renderer Color Override", bodyPart.colorRef, typeof (BodyPart2D), true);
        bodyPart.circleColorRef = (BodyPart2D) EditorGUILayout.ObjectField ("Ball Color Override", bodyPart.circleColorRef, typeof (BodyPart2D), true);
    }

    private void OnSceneGUI () {

        SCGAnimationToolEditor.ShowJoint ();

    }

}
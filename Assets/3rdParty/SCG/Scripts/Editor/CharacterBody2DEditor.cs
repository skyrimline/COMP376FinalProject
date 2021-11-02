using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (CharacterBody2D))]
[System.Serializable]

public class CharacterBody2DEditor : SCGEditor {

    CharacterBody2D body;

    int tab;

    public void Prepare () {
        body = (CharacterBody2D) target;
        SCGAnimationToolEditor.SetJointDisplayTarget (body);

    }

    public override void OnInspectorGUI () {

        Prepare ();

        EditorGUILayout.Space ();

        tab = GUILayout.Toolbar (tab, new string[] { "Parts", "Sorting" });

        EditorGUILayout.Space ();

        switch (tab) {
            case 0:
                Setups ();
                break;

            case 1:
                Sorting ();

                break;

        }

        Validate ();

    }

    void Sorting () {

        body.sortingLayerName = EditorGUILayout.TextField ("Sorting Layer Name", body.sortingLayerName);

        ShowSortableList ("Sorting Orders", ref body.sortingOrders, body);

    }

    void Setups () {

        Title ("Body Parts");

        EditorGUI.indentLevel++;

        body.shoulder = (BodyPartSocket2D) EditorGUILayout.ObjectField ("Shoulder", body.shoulder, typeof (BodyPartSocket2D), true);
        body.breasts = (BodyPart2D) EditorGUILayout.ObjectField ("Breasts", body.breasts, typeof (BodyPart2D), true);
        body.belly = (BodyPart2D) EditorGUILayout.ObjectField ("Belly", body.belly, typeof (BodyPart2D), true);
        body.hip = (BodyPartSocket2D) EditorGUILayout.ObjectField ("Hip", body.hip, typeof (BodyPartSocket2D), true);

        EditorGUI.indentLevel--;
        Title ("Renderers");
        EditorGUI.indentLevel++;

        body.hatRenderer = (SpriteRenderer) EditorGUILayout.ObjectField ("Hat", body.hatRenderer, typeof (SpriteRenderer), true);
        body.hairRenderer = (SpriteRenderer) EditorGUILayout.ObjectField ("Hair", body.hairRenderer, typeof (SpriteRenderer), true);
        body.eyewearRenderer = (SpriteRenderer) EditorGUILayout.ObjectField ("Eyewear", body.eyewearRenderer, typeof (SpriteRenderer), true);
        body.mustageRenderer = (SpriteRenderer) EditorGUILayout.ObjectField ("Mustage", body.mustageRenderer, typeof (SpriteRenderer), true);
        body.innerPart = (SpriteRenderer) EditorGUILayout.ObjectField ("Inner Sprite", body.innerPart, typeof (SpriteRenderer), true);

        EditorGUI.indentLevel--;

        Title ("Others");
        EditorGUI.indentLevel++;
        body.floor = (Transform) EditorGUILayout.ObjectField ("Floor", body.floor, typeof (Transform), true);
        body.sittingLevelTransform = (Transform) EditorGUILayout.ObjectField ("Sitting Level Transform", body.sittingLevelTransform, typeof (Transform), true);
        body.perspectiveHandle = (PerspectiveHandle2D) EditorGUILayout.ObjectField ("Perspective Handle", body.perspectiveHandle, typeof (PerspectiveHandle2D), true);

        EditorGUI.indentLevel--;

    }

    public void Validate () {
        if (GUI.changed) {
            EditorUtility.SetDirty (body);
            body.Validate ();
            body.Repaint ();
        }
    }

    private void OnSceneGUI () {

        SCGAnimationToolEditor.ShowJoint ();

    }

}
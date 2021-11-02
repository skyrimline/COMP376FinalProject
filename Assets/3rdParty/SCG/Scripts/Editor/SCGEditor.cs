using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class SCGEditor : Editor {

    protected delegate void Section ();

    protected GUIStyle bold;
    protected GUIStyle boldEnum;

    public override void OnInspectorGUI () {
        base.OnInspectorGUI ();

    }

    public virtual void OnEnable () {

    }

    protected void DrawMinMax (string name, ref float minValue, ref float maxValue, float minLimit, float maxLimit) {
        EditorGUILayout.BeginHorizontal ();

        minValue = (float) Math.Round ((double) Mathf.Clamp (EditorGUILayout.FloatField (name, minValue), minLimit, maxValue), 2);
        EditorGUILayout.MinMaxSlider (ref minValue, ref maxValue, minLimit, maxLimit, GUILayout.ExpandWidth (true));
        maxValue = (float) Math.Round ((double) Mathf.Clamp (EditorGUILayout.FloatField (maxValue, GUILayout.MaxWidth (100)), minValue, maxLimit), 2);

        EditorGUILayout.EndHorizontal ();
    }

    protected void NewSegment (string name, Section[] functions, ref bool toggle) {

        GUIStyle myFoldoutStyle = new GUIStyle (EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;

        toggle = EditorGUILayout.Foldout (toggle, name, myFoldoutStyle);
        if (toggle) {
            EditorGUI.indentLevel++;

            for (int i = 0; i <= functions.Length - 1; i++) {
                if (functions.Length > 1) {
                    EditorGUILayout.Space ();
                    EditorGUILayout.LabelField (functions[i].Method.Name.Replace ('_', ' '), EditorStyles.boldLabel);

                }
                functions[i] ();

            }

            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

        }
    }

    public void Title (string name) {
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField (name);
        EditorStyles.label.fontStyle = FontStyle.Normal;
    }

    public bool Button (string name) {
        return GUILayout.Button (name);
    }

    public bool Button (string name, float width) {
        return GUILayout.Button (name, GUILayout.Width (width));
    }

    protected void ShowColorsPicker (List<Color> colors, ref Color targetVar, CharacterBody2D body) {

        if (colors != null && colors.Count > 0) {

            int width = (int) (EditorGUIUtility.currentViewWidth * 0.4f) / 25;
            int height = Mathf.CeilToInt (colors.Count / (float) width);

            int index = 0;

            GUIStyle boxStyle = new GUIStyle (GUI.skin.button);
            boxStyle.fixedWidth = 25f;
            boxStyle.fixedHeight = 25f;

            boxStyle.border = new RectOffset (1, 1, 1, 1);

            for (int i = 1; i <= height; i++) {

                EditorGUILayout.BeginHorizontal ("color");
                EditorGUILayout.LabelField ("", GUILayout.Width ((int) EditorGUIUtility.currentViewWidth * 0.45f));

                for (int j = 1; j <= width; j++) {
                    if (index <= colors.Count - 1) {

                        GUI.backgroundColor = colors[index];
                        GUI.color = Color.white;
                        GUI.contentColor = Color.white;

                        if (GUILayout.Button ("", boxStyle)) {
                            Undo.RecordObject (body, "Modify Color");

                            targetVar = colors[index];
                        }
                        index++;
                    }
                }

                EditorGUILayout.EndHorizontal ();

            }

            GUI.backgroundColor = Color.white;

        }

    }

    protected void ShowColorsPicker (string name, string palttteName, ref List<Color> colors, ref Color targetVar, ref bool toggle, CharacterEditor2DEditor editor) {
        EditorGUILayout.BeginHorizontal ();

        EditorGUI.BeginChangeCheck ();

        Color value = EditorGUILayout.ColorField (name, targetVar);

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (editor.editor.target, "Modify Color");
            targetVar = value;

        }
        if (GUILayout.Button ("Save", GUILayout.MaxWidth (40)) && !colors.Contains (targetVar)) {
            colors.Add (targetVar);
            editor.SaveColors ();

        }

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("", GUILayout.Width ((int) EditorGUIUtility.currentViewWidth * 0.4f));
        toggle = EditorGUILayout.Foldout (toggle, palttteName + " Color Palette");
        EditorGUILayout.EndHorizontal ();

        if (toggle) {
            editor.OnEnable ();
            ShowColorsPicker (colors, ref targetVar, editor.editor.target);
        }

    }

    public void ShowSortableList (string name, ref List<GameObject> lists, CharacterBody2D body) {

        StringBuilder sb = new StringBuilder ();
        sb.Append (name);
        sb.Append (" (");
        sb.Append (lists.Count);
        sb.Append (")");

        Title (sb.ToString ());
        EditorGUI.indentLevel++;

        for (int i = 0; i <= lists.Count - 1; i++) {
            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (i.ToString (), GUILayout.Width (40));
            lists[i] = (GameObject) EditorGUILayout.ObjectField (lists[i], typeof (GameObject), true);

            EditorGUI.BeginDisabledGroup (i == 0);
            if (Button ("Up", 25)) {
                Undo.RecordObject (body, "Modify Sorting Order");

                GameObject me = lists[i];
                lists[i] = lists[i - 1];
                lists[i - 1] = me;
                break;
            }
            EditorGUI.EndDisabledGroup ();

            EditorGUI.BeginDisabledGroup (i == lists.Count - 1);

            if (Button ("Down", 45)) {
                Undo.RecordObject (body, "Modify Sorting Order");

                GameObject me = lists[i];
                lists[i] = lists[i + 1];
                lists[i + 1] = me;

                break;
            }
            EditorGUI.EndDisabledGroup ();

            if (Button ("x", 20)) {
                Undo.RecordObject (body, "Modify Sorting Order");

                lists.Remove (lists[i]);
                break;
            }

            EditorGUILayout.EndHorizontal ();
        }

        if (Button ("Add")) {

            Undo.RecordObject (body, "Modify Sorting Order");

            lists.Add (null);
        }

        EditorGUI.indentLevel--;
    }

    public void ShowCharacterList (string name, ref List<CharacterBody2D> lists, CharacterGenerator2D generator) {

        StringBuilder sb = new StringBuilder ();
        sb.Append (name);
        sb.Append (" (");
        sb.Append (lists.Count);
        sb.Append (")");

        Title (sb.ToString ());
        EditorGUI.indentLevel++;

        for (int i = 0; i <= lists.Count - 1; i++) {
            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (i.ToString (), GUILayout.Width (40));
            lists[i] = (CharacterBody2D) EditorGUILayout.ObjectField (lists[i], typeof (CharacterBody2D), true);

            if (Button ("x", 20)) {

                if (lists[i]) DestroyImmediate (lists[i].gameObject);
                lists.RemoveAt (i);
                break;
            }

            EditorGUILayout.EndHorizontal ();
        }

        if (Button ("Add")) {

            generator.AddToPool ();

        }

        EditorGUI.indentLevel--;
    }

    public void ShowSpriteRenderersList (string name, ref List<SpriteRenderer> lists) {

        StringBuilder sb = new StringBuilder ();
        sb.Append (name);
        sb.Append (" (");
        sb.Append (lists.Count);
        sb.Append (")");

        Title (sb.ToString ());
        EditorGUI.indentLevel++;

        for (int i = 0; i <= lists.Count - 1; i++) {
            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (i.ToString (), GUILayout.Width (40));
            lists[i] = (SpriteRenderer) EditorGUILayout.ObjectField (lists[i], typeof (SpriteRenderer), true);

            if (Button ("x", 20)) {
                lists.Remove (lists[i]);
                break;
            }

            EditorGUILayout.EndHorizontal ();
        }

        if (Button ("Add")) {
            lists.Add (null);
        }

        EditorGUI.indentLevel--;
    }

    public void Title (string name, float size) {
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField (name, GUILayout.Width (size));
        EditorStyles.label.fontStyle = FontStyle.Normal;
    }

    public void Line () {
        EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

    }

    public void ShowSpritesList (string name, ref List<Sprite> lists) {

        StringBuilder sb = new StringBuilder ();
        sb.Append (name);
        sb.Append (" (");
        sb.Append (lists.Count);
        sb.Append (")");

        EditorGUILayout.LabelField (sb.ToString ());
        EditorGUI.indentLevel++;

        for (int i = 0; i <= lists.Count - 1; i++) {
            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (i.ToString (), GUILayout.Width (40));
            lists[i] = (Sprite) EditorGUILayout.ObjectField (lists[i], typeof (Sprite), true);

            if (Button ("x", 20)) {
                lists.Remove (lists[i]);
                break;
            }

            EditorGUILayout.EndHorizontal ();
        }

        if (Button ("Add")) {
            lists.Add (null);
        }

        EditorGUI.indentLevel--;
    }

    public void ShowSpritesList (string name, ref List<Sprite> lists, ref List<Sprite> lists2) {

        StringBuilder sb = new StringBuilder ();
        sb.Append (name);
        sb.Append (" (");
        sb.Append (lists.Count);
        sb.Append (")");

        EditorGUILayout.LabelField (sb.ToString ());
        EditorGUI.indentLevel++;

        for (int i = 0; i <= lists.Count - 1; i++) {
            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (i.ToString (), GUILayout.Width (40));

            lists[i] = (Sprite) EditorGUILayout.ObjectField (lists[i], typeof (Sprite), true);
            lists2[i] = (Sprite) EditorGUILayout.ObjectField (lists2[i], typeof (Sprite), true);

            if (Button ("x", 20)) {
                lists.Remove (lists[i]);
                lists2.Remove (lists2[i]);
                break;
            }

            EditorGUILayout.EndHorizontal ();
        }

        if (Button ("Add")) {
            lists.Add (null);
            lists2.Add (null);
        }

        EditorGUI.indentLevel--;
    }

    public bool ShowColorsList (string name, ref List<Color> lists, ref bool includePalette, string paletteName) {

        StringBuilder sb = new StringBuilder ();
        sb.Append (name);
        sb.Append (" (");
        sb.Append (lists.Count);
        sb.Append (")");

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.LabelField (sb.ToString (), GUILayout.Width (100));

        EditorGUILayout.EndHorizontal ();
        EditorGUI.indentLevel++;

        for (int i = 0; i <= lists.Count - 1; i++) {
            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (i.ToString (), GUILayout.Width (40));
            lists[i] = EditorGUILayout.ColorField (lists[i], GUILayout.Width (80));

            if (Button ("x", 20)) {
                lists.Remove (lists[i]);
                break;
            }

            EditorGUILayout.EndHorizontal ();
        }

        if (Button ("Add", 150)) {
            lists.Add (new Color (1, 1, 1, 1));
        }

        StringBuilder palette = new StringBuilder ();
        palette.Append ("Include ");
        palette.Append (paletteName);
        palette.Append (" Color Palette");

        EditorGUILayout.BeginHorizontal ();

        includePalette = EditorGUILayout.Toggle (includePalette, GUILayout.Width (60));
        EditorGUILayout.LabelField (palette.ToString ());

        EditorGUILayout.EndHorizontal ();

        EditorGUI.indentLevel--;

        return includePalette;
    }

    public void ShowColorsList (string name, ref List<Color> lists) {

        StringBuilder sb = new StringBuilder ();
        sb.Append (name);
        sb.Append (" (");
        sb.Append (lists.Count);
        sb.Append (")");

        EditorGUILayout.LabelField (sb.ToString ());

        EditorGUI.indentLevel++;

        for (int i = 0; i <= lists.Count - 1; i++) {
            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (i.ToString (), GUILayout.Width (40));
            lists[i] = EditorGUILayout.ColorField (lists[i], GUILayout.Width (80));

            if (Button ("x", 20)) {
                lists.Remove (lists[i]);
                break;
            }

            EditorGUILayout.EndHorizontal ();
        }

        if (Button ("Add", 150)) {
            lists.Add (new Color (1, 1, 1, 1));
        }

        EditorGUI.indentLevel--;
    }

}
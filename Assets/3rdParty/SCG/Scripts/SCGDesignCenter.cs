using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public class SCGDesignCenter : EditorWindow {

    bool skinFoldout = true, hairFoldout = true, dyeFoldout = true, LeatherFoldout = true;
    public static bool dirty;
    Vector2 scrollPos = Vector2.zero;
    public static List<Color> skinColors = new List<Color> ();

    public static List<Color> hairColors = new List<Color> ();

    public static List<Color> dyeColors = new List<Color> ();

    public static List<Color> leatherColors = new List<Color> ();

    [MenuItem ("SCG/Color Palette")]
    public static void ShowWindow () {
        GetWindow<SCGDesignCenter> ("Color Palette");
    }

    void OnEnable () {
        LoadColors ();
    }

    public static void LoadColors () {
        SCGColorDatabase design = SCGSaveSystem.LoadColorDatabase ();
        skinColors = design.LoadColors (design.skinColors);
        hairColors = design.LoadColors (design.hairColors);
        dyeColors = design.LoadColors (design.dyeColors);
        leatherColors = design.LoadColors (design.leatherColors);
    }

    public void OnInspectorUpdate () {

        Repaint ();
    }

    void OnGUI () {

        if (dirty) {
            LoadColors ();
            dirty = false;

        }

        scrollPos = EditorGUILayout.BeginScrollView (scrollPos);
        Color_Palettes ();
        if (!dirty) SCGSaveSystem.SaveColorPalette (skinColors.ToArray (), hairColors.ToArray (), dyeColors.ToArray (), leatherColors.ToArray (), false);
        EditorGUILayout.EndScrollView ();

    }

    void Color_Palettes () {

        ShowPalette ("Skin Colors", skinColors, ref skinFoldout, 0);
        ShowPalette ("Hair Colors", hairColors, ref hairFoldout, 0);
        ShowPalette ("Dye Colors", dyeColors, ref dyeFoldout, 0);
        ShowPalette ("Leather Colors", leatherColors, ref LeatherFoldout, 1.8f);

    }

    protected void DrawMinMax (string name, ref float minValue, ref float maxValue, float minLimit, float maxLimit) {
        EditorGUILayout.BeginHorizontal (name);

        minValue = (float) Math.Round ((double) Mathf.Clamp (EditorGUILayout.FloatField (name, minValue), minLimit, maxValue), 2);
        EditorGUILayout.MinMaxSlider (ref minValue, ref maxValue, minLimit, maxLimit, GUILayout.ExpandWidth (true));
        maxValue = (float) Math.Round ((double) Mathf.Clamp (EditorGUILayout.FloatField (maxValue, GUILayout.MaxWidth (100)), minValue, maxLimit), 2);

        EditorGUILayout.EndHorizontal ();
    }

    protected void ShowColorsPicker (Color[] colors, ref Color targetVar) {

        int width = (int) (EditorGUIUtility.currentViewWidth * 0.4f) / 25;
        int height = Mathf.CeilToInt (colors.Length / (float) width);

        int index = 0;

        GUIStyle boxStyle = new GUIStyle (GUI.skin.button);
        boxStyle.fixedWidth = 25f;
        boxStyle.fixedHeight = 25f;

        boxStyle.border = new RectOffset (1, 1, 1, 1);

        for (int i = 1; i <= height; i++) {

            EditorGUILayout.BeginHorizontal ("color");
            EditorGUILayout.LabelField ("", GUILayout.Width ((int) EditorGUIUtility.currentViewWidth * 0.45f));

            for (int j = 1; j <= width; j++) {
                if (index <= colors.Length - 1) {
                    GUI.backgroundColor = colors[index];
                    GUI.color = Color.white;
                    GUI.contentColor = Color.white;

                    if (GUILayout.Button ("", boxStyle)) {
                        targetVar = colors[index];
                    }
                    index++;
                }
            }

            EditorGUILayout.EndHorizontal ();

        }

        GUI.backgroundColor = Color.grey;

    }

    protected void ShowColorsPicker (string name, Color[] colors, ref Color targetVar, ref bool toggle) {

        targetVar = EditorGUILayout.ColorField (name, targetVar);
        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("", GUILayout.Width ((int) EditorGUIUtility.currentViewWidth * 0.4f));
        toggle = EditorGUILayout.Foldout (toggle, name + " Color Palette");
        EditorGUILayout.EndHorizontal ();

        if (toggle) {
            ShowColorsPicker (colors, ref targetVar);

        }

    }

    void ShowPalette (string name, List<Color> colors, ref bool toggle, float plusPadding) {

        GUIStyle bold = new GUIStyle (EditorStyles.foldout);
        bold.fontStyle = FontStyle.Bold;
        bold.fixedWidth = 500f;

        EditorGUILayout.BeginHorizontal ();

        StringBuilder sb = new StringBuilder (name);

        sb.Append (" (");
        sb.Append (colors.Count);
        sb.Append (")");

        toggle = EditorGUILayout.Foldout (toggle, sb.ToString (), bold);

        EditorGUILayout.LabelField ("", GUILayout.Width ((4.5f * (sb.Length + plusPadding))));
        if (toggle) {
            if (GUILayout.Button ("+", GUILayout.MaxWidth (20))) {
                colors.Add (Color.white);

            }
        }

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.Space ();

        int width = (int) EditorGUIUtility.currentViewWidth / 90;
        int height = Mathf.CeilToInt (colors.Count / (float) width);

        if (toggle) {
            EditorGUI.indentLevel++;

            int index = 0;

            GUI.backgroundColor = Color.gray - new Color (0, 0, 0, 0.5f);

            for (int i = 0; i <= height - 1; i++) {
                EditorGUILayout.BeginHorizontal ();
                for (int j = 0; j <= width - 1; j++) {

                    if (index <= colors.Count - 1) {

                        colors[index] = EditorGUILayout.ColorField (colors[index], GUILayout.Width (60));
                        if (GUILayout.Button ("X", GUILayout.MaxWidth (20))) {
                            colors.RemoveAt (index);
                            break;

                        } else {
                            index++;
                        }

                    } else {
                        break;
                    }

                }
                EditorGUILayout.EndHorizontal ();

            }

            GUI.backgroundColor = Color.white;

            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

        }

    }

}
#endif
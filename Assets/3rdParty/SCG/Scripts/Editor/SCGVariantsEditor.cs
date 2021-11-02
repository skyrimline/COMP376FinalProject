using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (SCGVariants))]
[System.Serializable]
public class SCGVariantsEditor : CharacterEditor2DEditor {

    SCGVariants variants;

    bool toggleFaces, toggleHeadwears, toggleHairs, toggleEyewears, toggleMustage, toggleBodies;
    bool toggleSkinColors, toggleHairColors, toggleUpperColors, togglePantColors, toggleShoesColors, toggleGlovesColors, toggleSleeveColors, toggleInnerSleeveColors;
    bool toggleGrayHairColors;
    bool toggleInnerPart;
    bool toggleShoes;

    CharacterBody2D targetLoad;

    public override void OnEnable () {

    }

    protected override void Prepare () {

        variants = (SCGVariants) target;
        body = variants;

        boldToggle = new GUIStyle (EditorStyles.toggle);
        boldToggle.fontStyle = FontStyle.Bold;

        bold = new GUIStyle ();
        bold.fontStyle = FontStyle.Bold;

        boldEnum = new GUIStyle (EditorStyles.popup);
        boldEnum.fontStyle = FontStyle.Bold;
    }

    public override void OnInspectorGUI () {
        Prepare ();
        SaveLoad ();

        if (variants) {

            MainSettings ();

            if (GUI.changed) {

                Vaildate ();

            }

        }

    }

    protected override void Vaildate () {

        EditorUtility.SetDirty (variants);

    }

    protected override void AgeGender () {

        EditorGUILayout.BeginHorizontal ();

        float ageSize = 40;

        EditorGUILayout.LabelField ("Age : ", GUILayout.Width (ageSize * 2));
        EditorGUILayout.LabelField ("Teen", GUILayout.Width (ageSize));
        variants.teen = EditorGUILayout.Toggle (variants.teen, GUILayout.Width (25));
        EditorGUILayout.LabelField ("Adult", GUILayout.Width (ageSize));
        variants.adult = EditorGUILayout.Toggle (variants.adult, GUILayout.Width (25));
        EditorGUILayout.LabelField ("Middle Age", GUILayout.Width (ageSize * 2f));
        variants.middleAge = EditorGUILayout.Toggle (variants.middleAge, GUILayout.Width (25));
        EditorGUILayout.LabelField ("Old", GUILayout.Width (ageSize / 1.5f));
        variants.old = EditorGUILayout.Toggle (variants.old, GUILayout.Width (25));

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.LabelField ("Sex : ", GUILayout.Width (ageSize * 2));
        EditorGUILayout.LabelField ("Male", GUILayout.Width (ageSize));
        variants.male = EditorGUILayout.Toggle (variants.male, GUILayout.Width (25));
        EditorGUILayout.LabelField ("Female", GUILayout.Width (ageSize + 10));
        variants.female = EditorGUILayout.Toggle (variants.female, GUILayout.Width (25));

        EditorGUILayout.EndHorizontal ();

    }

    protected override void SizeScale () {

        DrawMinMax ("Scale", ref variants.minScale, ref variants.maxScale, 0, 1);
        DrawMinMax ("Size", ref variants.minActualSize, ref variants.maxActualSize, body.minSize, body.maxSize);

        EditorStyles.label.fontStyle = FontStyle.Normal;
    }

    protected override void QuickEditWrapper () {
        QuickEdit ();
    }

    protected override void QuickEdit () {

        EditorGUI.indentLevel++;

        DrawMinMax ("Body Width", ref variants.minBodyWidth, ref variants.maxBodyWidth, 0, 1);
        DrawMinMax ("Body Height", ref variants.minBodyHeight, ref variants.maxBodyHeight, 0, 1);
        DrawMinMax ("Face Width", ref variants.minFaceWidth, ref variants.maxFaceWidth, 0, 1);
        DrawMinMax ("Face Height", ref variants.minFaceHeight, ref variants.maxFaceHeight, 0, 1);

        EditorGUI.indentLevel--;
        Line ();

        EditorStyles.label.fontStyle = FontStyle.Bold;

        DrawMinMax ("Fatness", ref variants.minFatness, ref variants.maxFatness, 0, 1);

        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUI.indentLevel++;

        DrawMinMax ("Shoulder", ref variants.minShoulder, ref variants.maxShoulder, 0, 1);

        if (variants.female) {
            DrawMinMax ("Breasts", ref variants.minBreasts, ref variants.maxBreasts, 0, 1);

        }

        DrawMinMax ("Belly", ref variants.minBelly, ref variants.maxBelly, 0, 1);

        DrawMinMax ("Hip", ref variants.minHip, ref variants.maxHip, 0, 1);

        EditorGUI.indentLevel--;

        Line ();

    }

    void ShapeMultiply () {

        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("x", GUILayout.Width (50));
        EditorGUILayout.EndHorizontal ();

    }

    protected override void Shape_Sprite () {

        GUIStyle myFoldoutStyle = new GUIStyle (EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;

        toggleFaces = EditorGUILayout.Foldout (toggleFaces, "Faces", myFoldoutStyle);

        if (toggleFaces) {
            if (variants.male) {

                ShowSpritesList ("Male Faces", ref variants.maleFaces);
            }
            if (variants.female) {

                ShowSpritesList ("Female Faces", ref variants.femaleFaces);
            }
        }

        Line ();

        toggleHeadwears = EditorGUILayout.Foldout (toggleHeadwears, "Headwears", myFoldoutStyle);

        if (toggleHeadwears) {

            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.BeginVertical ();
            ShowSpritesList ("Sprites", ref variants.headwears);
            EditorGUILayout.EndVertical ();
            EditorGUILayout.BeginVertical ();
            ShowColorsList ("Colors", ref variants.headwearColors);
            EditorGUILayout.EndVertical ();

            EditorGUILayout.EndHorizontal ();
        }

        Line ();

        toggleHairs = EditorGUILayout.Foldout (toggleHairs, "Hairs", myFoldoutStyle);

        if (toggleHairs) {

            ShowSpritesList ("Sprites", ref variants.hairs);
        }

        Line ();

        toggleEyewears = EditorGUILayout.Foldout (toggleEyewears, "Eyewears", myFoldoutStyle);

        if (toggleEyewears) {

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.BeginVertical ();

            ShowSpritesList ("Sprites", ref variants.eyewears);
            EditorGUILayout.EndVertical ();
            EditorGUILayout.BeginVertical ();

            ShowColorsList ("Colors", ref variants.eyewearColors);
            EditorGUILayout.EndVertical ();
            EditorGUILayout.EndHorizontal ();
        }
        Line ();

        if (variants.male && (variants.middleAge || variants.old)) {

            toggleMustage = EditorGUILayout.Foldout (toggleMustage, "Mustages", myFoldoutStyle);

            if (toggleMustage) {

                ShowSpritesList ("Sprites", ref variants.mustages);
            }
            Line ();

        }

        toggleBodies = EditorGUILayout.Foldout (toggleBodies, "Bodies", myFoldoutStyle);

        if (toggleBodies) {

            if (variants.male) {
                ShowSpritesList ("Male Bodies", ref variants.maleBodies);

            }
            if (variants.female) {
                ShowSpritesList ("Female Bodies", ref variants.femaleBodies);

            }

        }
        Line ();

    }

    protected override void ShowSaveAndLoad () {

        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Body : ", GUILayout.Width (70));

        targetLoad = (CharacterBody2D) EditorGUILayout.ObjectField (targetLoad, typeof (CharacterBody2D), true);

        EditorGUILayout.EndHorizontal ();
        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button ("Cancel")) {
            showSaveAs = showLoad = false;
        }

        EditorGUI.BeginDisabledGroup (!targetLoad);

        if (GUILayout.Button ("Load")) {

            variants.Load (targetLoad);
            showSaveAs = showLoad = false;
        }

        EditorGUI.EndDisabledGroup ();

        EditorGUILayout.EndHorizontal ();

    }

    protected override void HeaderButton () {
        if (Button ("Load Body", 100)) {
            showLoad = true;

        }
    }

    protected override void Coloring () {

        GUIStyle myFoldoutStyle = new GUIStyle (EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;

        toggleSkinColors = EditorGUILayout.Foldout (toggleSkinColors, "Skins", myFoldoutStyle);

        if (toggleSkinColors) {
            ShowColorsList ("Colors", ref variants.skinColors, ref variants.includeSkinPalette, "Skin");
        }
        Line ();

        toggleHairColors = EditorGUILayout.Foldout (toggleHairColors, "Hairs", myFoldoutStyle);

        if (toggleHairColors) {
            ShowColorsList ("Colors", ref variants.hairColors, ref variants.includeHairPalette, "Hair");
        }
        Line ();

        if (variants.old) {

            toggleGrayHairColors = EditorGUILayout.Foldout (toggleGrayHairColors, "Gray Hairs", myFoldoutStyle);

            if (toggleGrayHairColors) {
                ShowColorsList ("Colors", ref variants.grayHairColors, ref variants.includeGrayHairPalette, "Hair");
            }
            Line ();
        }

        toggleUpperColors = EditorGUILayout.Foldout (toggleUpperColors, "Upper Cloth", myFoldoutStyle);

        if (toggleUpperColors) {
            ShowColorsList ("Colors", ref variants.upperClothColors, ref variants.includeShirtPalette, "Dye");
        }
        Line ();

        if (variants.vest != SCGVariants.Options.No) {
            toggleSleeveColors = EditorGUILayout.Foldout (toggleSleeveColors, "Sleeve", myFoldoutStyle);

            if (toggleSleeveColors) {
                ShowColorsList ("Colors", ref variants.sleeveColors, ref variants.includeSleevePalette, "Dye");
            }
            Line ();
        }

        if (variants.innerSleeveOption != SCGVariants.Options.No) {

            toggleInnerSleeveColors = EditorGUILayout.Foldout (toggleInnerSleeveColors, "Inner Sleeve", myFoldoutStyle);

            if (toggleInnerSleeveColors) {
                ShowColorsList ("Colors", ref variants.innerSleeveColors, ref variants.includeInnerSleevePalette, "Dye");
            }
            Line ();
        }

        if (variants.fullGloves || variants.halfGloves) {

            toggleGlovesColors = EditorGUILayout.Foldout (toggleGlovesColors, "Gloves", myFoldoutStyle);

            if (toggleGlovesColors) {
                ShowColorsList ("Colors", ref variants.glovesColors, ref variants.includeGlovesPalette, "Leather");
            }
            Line ();
        }

        togglePantColors = EditorGUILayout.Foldout (togglePantColors, "Pants", myFoldoutStyle);

        if (togglePantColors) {
            ShowColorsList ("Colors", ref variants.pantColors, ref variants.includePantPalette, "Dye");
        }
        Line ();

        toggleShoesColors = EditorGUILayout.Foldout (toggleShoesColors, "Shoes", myFoldoutStyle);

        if (toggleShoesColors) {
            ShowColorsList ("Colors", ref variants.shoesColors, ref variants.includeShoesPalette, "Leather");
        }
        Line ();

    }

    protected override void ShowInnerLayers () {
        EditorGUI.indentLevel++;

        for (int i = 0; i <= variants.innerShirtLayers.Count - 1; i++) {

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.LabelField (string.Empty, GUILayout.Width (50));
            if (Button ("X", 20)) {
                variants.innerShirtLayers.RemoveAt (i);
                return;
            }
            EditorGUILayout.LabelField ("Layer " + (i + 1), GUILayout.Width (80));

            EditorGUILayout.EndHorizontal ();

            for (int j = 0; j <= variants.innerShirtLayers[i].innerShirts.Count - 1; j++) {

                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.BeginVertical ();
                EditorGUILayout.LabelField (string.Empty, GUILayout.Width (100));
                EditorGUI.BeginChangeCheck ();

                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.LabelField (string.Empty, GUILayout.Width (100));
                if (Button ("X", 20)) {
                    variants.innerShirtLayers[i].innerShirts.RemoveAt (j);
                    return;
                }
                Sprite innerBodyStyle = ((Sprite) EditorGUILayout.ObjectField (variants.innerShirtLayers[i].innerShirts[j].sprite, typeof (Sprite), true));
                if (EditorGUI.EndChangeCheck ()) {
                    Undo.RecordObject (variants, "Change Sprite");
                    variants.innerShirtLayers[i].innerShirts[j].sprite = innerBodyStyle;
                }
                EditorGUILayout.EndHorizontal ();

                EditorGUILayout.EndVertical ();

                EditorGUILayout.BeginVertical ();

                variants.innerShirtLayers[i].innerShirts[j].includeDyeColors = ShowColorsList ("Colors", ref variants.innerShirtLayers[i].innerShirts[j].colors, ref variants.innerShirtLayers[i].innerShirts[j].includeDyeColors, "Dye");

                EditorGUILayout.EndVertical ();

                EditorGUILayout.EndHorizontal ();
            }
            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField (string.Empty, GUILayout.Width (100));
            if (Button ("Add Entry", (80))) {
                variants.innerShirtLayers[i].innerShirts.Add (new InnerShirtEntry ());
            }
            EditorGUILayout.EndHorizontal ();
            Line ();

        }

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.LabelField (string.Empty, GUILayout.Width (50));
        if (Button ("Add Layer", (80))) {
            variants.innerShirtLayers.Add (new InnerShirtLayer ());
        }
        EditorGUILayout.EndHorizontal ();

        EditorGUI.indentLevel--;

    }

    protected override void Clothing () {

        EditorStyles.label.fontStyle = FontStyle.Bold;
        variants.shirtOn = (SCGVariants.Options) EditorGUILayout.EnumPopup ("Shirt", variants.shirtOn);
        EditorStyles.label.fontStyle = FontStyle.Normal;

        GUIStyle myFoldoutStyle = new GUIStyle (EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;

        if (variants.shirtOn != SCGVariants.Options.No) {

            EditorGUI.indentLevel++;

            EditorStyles.label.fontStyle = FontStyle.Bold;

            toggleInnerPart = EditorGUILayout.Foldout (toggleInnerPart, "Body Decoration", myFoldoutStyle);

            EditorStyles.label.fontStyle = FontStyle.Normal;

            if (toggleInnerPart) {

                EditorGUILayout.BeginHorizontal ();

                EditorGUILayout.BeginVertical ();
                ShowSpritesList ("Sprites", ref variants.innerParts);
                EditorGUILayout.EndVertical ();
                EditorGUILayout.BeginVertical ();

                ShowColorsList ("Colors", ref variants.innerPartColors, ref variants.includeInnerPartPalette, "Dye");
                EditorGUILayout.EndVertical ();

                EditorGUILayout.EndHorizontal ();

                Title ("Inner Layers");
                ShowInnerLayers ();
            }

            Line ();

            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.LabelField ("Arm Cover", bold, GUILayout.Width (100));

            EditorGUILayout.LabelField ("No", GUILayout.Width (35));
            variants.noArms = EditorGUILayout.Toggle (variants.noArms, GUILayout.Width (25));
            EditorGUILayout.LabelField ("Half", GUILayout.Width (40));
            variants.halfArms = EditorGUILayout.Toggle (variants.halfArms, GUILayout.Width (25));
            EditorGUILayout.LabelField ("Full", GUILayout.Width (40));
            variants.fullArms = EditorGUILayout.Toggle (variants.fullArms, GUILayout.Width (25));

            EditorGUILayout.EndHorizontal ();

            if (variants.halfArms || variants.fullArms) {

                EditorGUI.indentLevel++;

                variants.vest = (SCGVariants.Options) EditorGUILayout.EnumPopup ("Vest", variants.vest);

                variants.innerSleeveOption = (SCGVariants.Options) EditorGUILayout.EnumPopup ("Inner Sleeve", variants.innerSleeveOption);

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

            EditorStyles.label.fontStyle = FontStyle.Bold;

            variants.longCoat = (SCGVariants.Options) EditorGUILayout.EnumPopup ("Long Coat", variants.longCoat, boldEnum);

            EditorStyles.label.fontStyle = FontStyle.Normal;

            if (variants.longCoat != SCGVariants.Options.No) {
                EditorGUI.indentLevel++;

                DrawMinMax ("Flow Angle", ref variants.minSkirtRadius, ref variants.maxSkirtRadius, 0f, 360f);
                DrawMinMax ("Extra Height", ref variants.minSkirtHeight, ref variants.maxSkirtHeight, 0, 1f);

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

            EditorGUI.indentLevel--;

        }

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.LabelField ("Gloves Cover", bold, GUILayout.Width (115));

        EditorGUILayout.LabelField ("No", GUILayout.Width (40));
        variants.noGloves = EditorGUILayout.Toggle (variants.noGloves, GUILayout.Width (20));
        EditorGUILayout.LabelField ("Half", GUILayout.Width (40));
        variants.halfGloves = EditorGUILayout.Toggle (variants.halfGloves, GUILayout.Width (20));
        EditorGUILayout.LabelField ("Full", GUILayout.Width (40));
        variants.fullGloves = EditorGUILayout.Toggle (variants.fullGloves, GUILayout.Width (20));

        EditorGUILayout.EndHorizontal ();

        EditorGUI.indentLevel++;

        if (variants.halfGloves || variants.fullGloves) {

            DrawMinMax ("Gloves Sleeve", ref variants.minGlovesWrap, ref variants.maxGlovesWrap, 0f, 1f);

        }

        EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);
        EditorGUI.indentLevel--;

        EditorStyles.label.fontStyle = FontStyle.Bold;

        variants.skirtOn = (SCGVariants.Options) EditorGUILayout.EnumPopup ("Skirt", variants.skirtOn, boldEnum);

        EditorStyles.label.fontStyle = FontStyle.Normal;

        if (variants.skirtOn != SCGVariants.Options.No) {

            EditorGUI.indentLevel++;

            DrawMinMax ("Flow Angle", ref variants.minCoatRadius, ref variants.maxCoatRadius, 0f, 360f);
            DrawMinMax ("Extra Height", ref variants.minCoatHeight, ref variants.maxCoatHeight, 0, 1f);

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.LabelField ("Leg Cover", bold, GUILayout.Width (115));

        EditorGUILayout.LabelField ("Half", GUILayout.Width (40));
        variants.halfLeg = EditorGUILayout.Toggle (variants.halfLeg, GUILayout.Width (20));
        EditorGUILayout.LabelField ("Full", GUILayout.Width (40));
        variants.fullLeg = EditorGUILayout.Toggle (variants.fullLeg, GUILayout.Width (20));

        EditorGUILayout.EndHorizontal ();

        EditorStyles.label.fontStyle = FontStyle.Bold;

        toggleShoes = EditorGUILayout.Foldout (toggleShoes, "Shoes Style", myFoldoutStyle);

        EditorStyles.label.fontStyle = FontStyle.Normal;

        if (toggleShoes) {

            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.BeginVertical ();
            ShowSpritesList ("Shoes", ref variants.shoes, ref variants.toes);
            EditorGUILayout.EndVertical ();

            EditorGUILayout.EndHorizontal ();
        }

        Line ();

        DrawMinMax ("Shoes Sleeve", ref variants.minShoesWrap, ref variants.maxShoesWrap, 0, 1f);

        EditorStyles.label.fontStyle = FontStyle.Normal;

    }

}
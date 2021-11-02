using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (CharacterEditor2D))]
[System.Serializable]
public class CharacterEditor2DEditor : SCGEditor {

    public CharacterEditor2D editor;
    public CharacterBody2D body;

    bool toggleSkinColor;
    bool toggleHairColor;
    bool toggleShirtColor;
    bool toggleInnerSleeveColor;
    bool toggleSleeveColor;
    bool toggleGlovesColor;
    bool togglePantsColor;
    bool toggleShoesColor;
    bool isFemale;

    protected bool showSaveAs, showLoad;

    [SerializeField] string location = "SCG/Profiles/", filename = "";

    List<Color> skinColors = new List<Color> ();
    List<Color> dyeColors = new List<Color> ();
    List<Color> hairColors = new List<Color> ();
    List<Color> leatherColors = new List<Color> ();

    int justLoaded = 0;

    CharacterBodyData.Type loadType = CharacterBodyData.Type.All;

    protected GUIStyle boldToggle;
    int tab;

    float GetHalfWindowSize () {
        return (EditorGUIUtility.currentViewWidth / 2f) - 20f;
    }

    public override void OnEnable () {
        editor = (CharacterEditor2D) target;
        body = editor.target;
        Undo.undoRedoPerformed += Vaildate;
        LoadDesign ();
    }

    void LoadDesign () {
        SCGColorDatabase design = SCGSaveSystem.LoadColorDatabase ();

        if (design != null && design.skinColors != null) {
            if (design.skinColors.Length >= 0)
                skinColors = design.LoadColors (design.skinColors);

            if (design.hairColors.Length >= 0)

                hairColors = design.LoadColors (design.hairColors);

            if (design.dyeColors.Length >= 0)

                dyeColors = design.LoadColors (design.dyeColors);

            if (design.leatherColors.Length >= 0)

                leatherColors = design.LoadColors (design.leatherColors);
        }

    }

    protected void SaveLoad () {

        StringBuilder stringBuilder = new StringBuilder ("Profile : ");
        if (editor) stringBuilder.Append (editor.profileName);

        if (justLoaded == 0) {
            stringBuilder.Append ("*");
        }

        bold = new GUIStyle ();
        bold.fontStyle = FontStyle.Bold;

        if (!showLoad && !showSaveAs) {

            EditorGUILayout.BeginHorizontal ();

            if (editor) {
                EditorGUILayout.LabelField (stringBuilder.ToString (), bold);
                if (Button ("Load", 50)) {
                    showLoad = true;

                }

                if (justLoaded == 0) {

                    if (Button ("Save", 50)) {
                        showSaveAs = true;
                    }

                }
            }
            EditorGUILayout.EndHorizontal ();

        } else {

            ShowSaveAndLoad ();
        }

        if (editor) Line ();

    }

    protected virtual void Prepare () {

        editor = (CharacterEditor2D) target;

        if (editor && body == null) body = editor.GetComponent<CharacterBody2D> ();
        if (body) {

            isFemale = body.GetSex == CharacterBody2D.Sex.Female;
        }

        boldToggle = new GUIStyle (EditorStyles.toggle);
        boldToggle.fontStyle = FontStyle.Bold;

    }

    protected virtual void HeaderButton () {
        if (Button ("Make Varaints", 100)) {
            editor.target.CreateVaraints ();

        }
    }

    protected void MainSettings () {
        if (!showSaveAs && !showLoad) {

            EditorStyles.label.fontStyle = FontStyle.Bold;

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.LabelField ("Settings");

            HeaderButton ();

            EditorGUILayout.EndHorizontal ();
            EditorGUILayout.Space ();
            EditorStyles.label.fontStyle = FontStyle.Normal;

            tab = GUILayout.Toolbar (tab, new string[] { "Body", "Sprites", "Clothes", "Colors" });

            EditorGUILayout.Space ();

            switch (tab) {
                case 0:

                    OverallShape ();
                    break;

                case 1:
                    Shape_Sprite ();
                    break;
                case 2:
                    Clothing ();
                    break;
                case 3:
                    Coloring ();
                    break;
            }

        }
    }

    public override void OnInspectorGUI () {

        Prepare ();
        SaveLoad ();

        if (body) {

            EditorGUI.BeginChangeCheck ();

            MainSettings ();

            if (EditorGUI.EndChangeCheck ()) {
                if (justLoaded > 0) {
                    justLoaded--;
                }
            }

            if (GUI.changed) {

                Vaildate ();

            }

        }

    }

    protected virtual void Vaildate () {

        if (!body) Prepare ();
        if (editor) {
            EditorUtility.SetDirty (editor);
            if (this) EditorUtility.SetDirty (this);

            EditorUtility.SetDirty (body);
            EditorUtility.SetDirty (body.skirt);
            EditorUtility.SetDirty (body.coat);

            serializedObject.ApplyModifiedProperties ();
            body.Repaint ();
        }

    }

    protected virtual void ShowInnerLayers () {
        EditorGUI.indentLevel++;

        if (body.innerPartContainer) {
            for (int i = 0; i <= body.innerPartContainer.childs.Count - 1; i++) {
                if (body.innerPartContainer.childs[i] != null) {

                    EditorGUILayout.BeginHorizontal ();

                    EditorGUILayout.LabelField ("Layer " + (i + 1), GUILayout.Width (160));
                    EditorGUI.BeginChangeCheck ();
                    Sprite innerBodyStyle = ((Sprite) EditorGUILayout.ObjectField (body.innerPartContainer.childs[i].sprite, typeof (Sprite), true));
                    if (EditorGUI.EndChangeCheck ()) {
                        Undo.RecordObject (body.innerPartContainer.childs[i], "Change Sprite");
                        body.innerPartContainer.childs[i].sprite = innerBodyStyle;
                    }

                    EditorGUI.BeginChangeCheck ();
                    Color innerBodyColor = (EditorGUILayout.ColorField (body.innerPartContainer.childs[i].color));
                    if (EditorGUI.EndChangeCheck ()) {
                        Undo.RecordObject (body.innerPartContainer.childs[i], "Change Color");
                        body.innerPartContainer.childs[i].color = innerBodyColor;
                    }

                    if (Button ("X")) {
                        body.innerPartContainer.RemoveLayer (i);
                    }
                    EditorGUILayout.EndHorizontal ();
                }
            }
        }
        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.LabelField (string.Empty, GUILayout.Width (50));
        if (Button ("Add Layer", (80))) {
            body.innerPartContainer.AddLayer ();
        }
        EditorGUILayout.EndHorizontal ();

        EditorGUI.indentLevel--;

    }

    protected virtual void Clothing () {

        if (isFemale) {
            body.isWearShirt = (true);

            if (body.isWearSkirt) body.LegCover = (2);

        }

        EditorGUI.BeginDisabledGroup (isFemale);

        EditorStyles.label.fontStyle = FontStyle.Bold;

        EditorGUI.BeginChangeCheck ();
        bool isWearShirt = (EditorGUILayout.Toggle ("Shirt", body.isWearShirt));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Clothing");
            body.isWearShirt = isWearShirt;

        }

        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUI.EndDisabledGroup ();

        if (body.isWearShirt) {

            EditorGUI.indentLevel++;

            EditorStyles.label.fontStyle = FontStyle.Bold;

            EditorGUILayout.BeginHorizontal ();

            EditorGUILayout.PrefixLabel ("Body Decoration");

            EditorGUI.BeginChangeCheck ();
            Sprite innerBodyStyle = ((Sprite) EditorGUILayout.ObjectField (body.InnerBodyStyle, typeof (Sprite), true));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (body, "Change Sprite");

                body.InnerBodyStyle = innerBodyStyle;
            }

            EditorGUI.BeginChangeCheck ();
            Color innerBodyColor = (EditorGUILayout.ColorField (body.InnerBodyColor));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (body, "Change Color");

                body.InnerBodyColor = innerBodyColor;
            }

            EditorGUILayout.EndHorizontal ();

            if (innerBodyStyle) {
                ShowInnerLayers ();
            }

            EditorGUI.BeginChangeCheck ();
            int armCover = (EditorGUILayout.IntSlider ("Arm Cover", body.ArmCover, 0, 2));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (body, "Change Clothing");

                body.ArmCover = armCover;

            }

            EditorStyles.label.fontStyle = FontStyle.Normal;

            EditorGUI.indentLevel++;

            if (body.ArmCover > 0) {

                EditorGUI.BeginChangeCheck ();
                bool isDifferentSleeve = (EditorGUILayout.Toggle ("Vest", body.isDifferentSleeve));
                if (EditorGUI.EndChangeCheck ()) {
                    Undo.RecordObject (body, "Change Clothing");

                    body.isDifferentSleeve = isDifferentSleeve;

                }

            }

            if (body.ArmCover == 2) {

                EditorGUI.BeginChangeCheck ();
                bool isInnerSleeve = (EditorGUILayout.Toggle ("Inner Sleeve", body.isInnerSleeve));
                if (EditorGUI.EndChangeCheck ()) {
                    Undo.RecordObject (body, "Change Clothing");

                    body.isInnerSleeve = isInnerSleeve;

                }

            }

            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

            EditorStyles.label.fontStyle = FontStyle.Bold;

            EditorGUI.BeginChangeCheck ();
            bool isWearCoat = (EditorGUILayout.Toggle ("Long Coat", body.isWearCoat));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (body, "Change Clothing");
                body.isWearCoat = isWearCoat;

            }

            EditorStyles.label.fontStyle = FontStyle.Normal;

            if (body.isWearCoat) {
                EditorGUI.indentLevel++;

                DrawMinMax ("Flow Angle", ref body.coat.minRadius, ref body.coat.maxRadius, 0f, 360f);

                EditorGUI.BeginChangeCheck ();
                float extraHeight = (EditorGUILayout.Slider ("Extra Height", body.coat.extraHeight, 0, 1f));
                if (EditorGUI.EndChangeCheck ()) {
                    Undo.RecordObject (body.coat, "Change Clothing");
                    body.coat.extraHeight = extraHeight;

                }

                EditorGUI.indentLevel--;

            }
            EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

            EditorGUI.indentLevel--;

        } else {
            body.isWearCoat = (false);
        }

        EditorStyles.label.fontStyle = FontStyle.Bold;

        EditorGUI.BeginChangeCheck ();
        int gloves = (EditorGUILayout.IntSlider ("Gloves Cover", body.Gloves, 0, 2));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Clothing");
            body.Gloves = gloves;

        }

        EditorStyles.label.fontStyle = FontStyle.Normal;

        if (body.Gloves > 0) {

            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck ();
            float glovesWrap = (EditorGUILayout.Slider ("Gloves Sleeve", body.GlovesWrap, 0, 1f));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (body, "Change Clothing");
                body.GlovesWrap = glovesWrap;

            }

            EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);
            EditorGUI.indentLevel--;

        }

        if (body.GetSex == CharacterBody2D.Sex.Female) {
            EditorStyles.label.fontStyle = FontStyle.Bold;

            EditorGUI.BeginChangeCheck ();
            bool isWearSkirt = (EditorGUILayout.Toggle ("Skirt", body.isWearSkirt));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (body, "Change Clothing");
                body.isWearSkirt = isWearSkirt;

            }

            EditorStyles.label.fontStyle = FontStyle.Normal;
        } else {
            body.isWearSkirt = (false);
        }

        if (body.isWearSkirt) {
            EditorGUI.indentLevel++;

            DrawMinMax ("Flow Angle", ref body.skirt.minRadius, ref body.skirt.maxRadius, 0f, 360f);

            EditorGUI.BeginChangeCheck ();
            float extraHeight = (EditorGUILayout.Slider ("Extra Height", body.skirt.extraHeight, 0, 1f));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (body.skirt, "Change Clothing");
                body.skirt.extraHeight = extraHeight;

            }

            EditorGUI.indentLevel--;

        }

        if (body.isWearSkirt == false || body.GetSex == CharacterBody2D.Sex.Male) {
            EditorStyles.label.fontStyle = FontStyle.Bold;

            EditorGUI.BeginChangeCheck ();
            int legCover = (EditorGUILayout.IntSlider ("Leg Cover", body.LegCover, 1, 2));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (body, "Change Clothing");
                body.LegCover = legCover;

            }

            EditorStyles.label.fontStyle = FontStyle.Normal;
        }

        EditorGUILayout.BeginHorizontal ();

        EditorStyles.label.fontStyle = FontStyle.Bold;

        EditorGUILayout.PrefixLabel ("Shoes Style");

        EditorGUI.BeginChangeCheck ();
        Sprite shoesStyle = ((Sprite) EditorGUILayout.ObjectField (body.ShoesStyle, typeof (Sprite), true));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Clothing");
            body.ShoesStyle = shoesStyle;

        }

        EditorGUI.BeginChangeCheck ();
        Sprite toeStyle = ((Sprite) EditorGUILayout.ObjectField (body.ToeStyle, typeof (Sprite), true));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Clothing");
            body.ToeStyle = toeStyle;
        }

        EditorGUILayout.EndHorizontal ();

        EditorGUI.BeginChangeCheck ();
        float shoesWrap = (EditorGUILayout.Slider ("Shoes Sleeve", body.ShoesWrap, 0, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Clothing");
            body.ShoesWrap = shoesWrap;

        }

        EditorStyles.label.fontStyle = FontStyle.Normal;

    }

    protected virtual void Shape_Sprite () {

        EditorStyles.label.fontStyle = FontStyle.Bold;

        if (isFemale) {

            GUILayout.BeginHorizontal ();
            EditorGUILayout.PrefixLabel ("Face (Female)");

            EditorGUI.BeginChangeCheck ();
            Sprite femaleFace = ((Sprite) EditorGUILayout.ObjectField (editor.target.FemaleFace, typeof (Sprite), true));

            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (editor.target, "Change Sprite");
                editor.target.FemaleFace = femaleFace;

            }

            GUILayout.EndHorizontal ();

        } else {

            GUILayout.BeginHorizontal ();
            EditorGUILayout.PrefixLabel ("Face (Male)");

            EditorGUI.BeginChangeCheck ();
            Sprite maleFace = ((Sprite) EditorGUILayout.ObjectField (editor.target.MaleFace, typeof (Sprite), true));
            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (editor, "Change Sprite");

                editor.target.MaleFace = maleFace;

            }
            GUILayout.EndHorizontal ();

        }

        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUI.indentLevel++;

        GUILayout.BeginHorizontal ();
        EditorGUILayout.PrefixLabel ("Headwear");

        EditorGUI.BeginChangeCheck ();
        Sprite hatStyle = ((Sprite) EditorGUILayout.ObjectField (body.HatStyle, typeof (Sprite), true));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Sprite");
            body.HatStyle = hatStyle;
        }

        EditorGUI.BeginChangeCheck ();
        Color hatColor = (EditorGUILayout.ColorField (body.HatColor));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Color");
            body.HatColor = hatColor;
        }

        GUILayout.EndHorizontal ();

        GUILayout.BeginHorizontal ();
        EditorGUILayout.PrefixLabel ("Hair");

        EditorGUI.BeginChangeCheck ();
        Sprite hairStyle = (((Sprite) EditorGUILayout.ObjectField (body.HairStyle, typeof (Sprite), true)));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Sprite");
            body.HairStyle = hairStyle;
        }

        GUILayout.EndHorizontal ();

        GUILayout.BeginHorizontal ();
        EditorGUILayout.PrefixLabel ("Eyewear");

        EditorGUI.BeginChangeCheck ();
        Sprite eyewearStyle = ((Sprite) EditorGUILayout.ObjectField (body.EyewearStyle, typeof (Sprite), true));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Sprite");
            body.EyewearStyle = eyewearStyle;

        }

        EditorGUI.BeginChangeCheck ();
        Color eyewearColor = (EditorGUILayout.ColorField (body.EyewearColor));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Color");
            body.EyewearColor = eyewearColor;

        }
        GUILayout.EndHorizontal ();

        EditorGUI.BeginDisabledGroup ((body.GetAge != CharacterBody2D.Age.Old && body.GetAge != CharacterBody2D.Age.MiddleAge) || body.GetSex == CharacterBody2D.Sex.Female);

        GUILayout.BeginHorizontal ();
        EditorGUILayout.PrefixLabel ("Mustage");

        EditorGUI.BeginChangeCheck ();
        Sprite mustageStyle = ((Sprite) EditorGUILayout.ObjectField (body.MustageStyle, typeof (Sprite), true));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Sprite");
            body.MustageStyle = mustageStyle;

        }

        GUILayout.EndHorizontal ();

        EditorGUI.EndDisabledGroup ();

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

        EditorStyles.label.fontStyle = FontStyle.Bold;

        if (isFemale) {

            GUILayout.BeginHorizontal ();
            EditorGUILayout.PrefixLabel ("Body (Female)");

            EditorGUI.BeginChangeCheck ();
            Sprite femaleBody = ((Sprite) EditorGUILayout.ObjectField (editor.target.FemaleBody, typeof (Sprite), true));

            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (editor, "Change Sprite");

                editor.target.FemaleBody = femaleBody;

            }

            GUILayout.EndHorizontal ();

        } else {

            GUILayout.BeginHorizontal ();
            EditorGUILayout.PrefixLabel ("Body (Male)");

            EditorGUI.BeginChangeCheck ();
            Sprite maleBody = ((Sprite) EditorGUILayout.ObjectField (editor.target.MaleBody, typeof (Sprite), true));

            if (EditorGUI.EndChangeCheck ()) {
                Undo.RecordObject (editor, "Change Sprite");

                editor.target.MaleBody = maleBody;

            }

            GUILayout.EndHorizontal ();

        }

        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUI.indentLevel++;

        GUILayout.BeginHorizontal ();
        EditorGUILayout.PrefixLabel ("Right Hand");

        EditorGUI.BeginChangeCheck ();
        Sprite rightInner = ((Sprite) EditorGUILayout.ObjectField (body.RightInner, typeof (Sprite), true));

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Sprite");

            body.RightInner = rightInner;

        }

        EditorGUI.BeginChangeCheck ();
        Color rightInnerColor = (EditorGUILayout.ColorField (body.RightInnerColor));

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Color");

            body.RightInnerColor = rightInnerColor;

        }

        GUILayout.EndHorizontal ();

        GUILayout.BeginHorizontal ();
        EditorGUILayout.PrefixLabel ("Left Hand");

        EditorGUI.BeginChangeCheck ();
        Sprite leftInner = ((Sprite) EditorGUILayout.ObjectField (body.LeftInner, typeof (Sprite), true));

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Sprite");

            body.LeftInner = leftInner;

        }

        EditorGUI.BeginChangeCheck ();
        Color leftInnerColor = (EditorGUILayout.ColorField (body.LeftInnerColor));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Change Color");

            body.LeftInnerColor = leftInnerColor;

        }

        GUILayout.EndHorizontal ();

        EditorGUI.indentLevel--;
        EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

    }

    protected virtual void AgeGender () {

        EditorGUI.BeginChangeCheck ();

        CharacterBody2D.Age age = ((CharacterBody2D.Age) EditorGUILayout.EnumPopup ("Age", body.GetAge));

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Modify Age");
            body.GetAge = age;
            body.CorrectAge (age);
        }

        EditorGUI.BeginChangeCheck ();

        EditorGUI.indentLevel++;
        EditorGUI.BeginChangeCheck ();
        int ageNumber = EditorGUILayout.IntSlider ("Age Number", body.AgeNumber, 10, 80);
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Modify Age");
            body.AgeNumber = ageNumber;
            body.CorrectAge (ageNumber);
        }
        EditorGUI.indentLevel--;

        CharacterBody2D.Sex sex = ((CharacterBody2D.Sex) EditorGUILayout.EnumPopup ("Sex", body.GetSex));

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Modify Sex");
            body.GetSex = sex;

        }

    }

    protected virtual void SizeScale () {
        EditorGUI.BeginChangeCheck ();

        float scale = (EditorGUILayout.FloatField ("Scale", body.Scale));

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Modify Scale");
            body.Scale = scale;

        }

        EditorGUI.BeginChangeCheck ();

        float size = (EditorGUILayout.Slider ("Size", body.Size, body.minSize, body.maxSize));

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Modify Size");
            body.Size = size;

        }

        EditorStyles.label.fontStyle = FontStyle.Normal;
    }

    void OverallShape () {
        if (body) {

            EditorStyles.label.fontStyle = FontStyle.Bold;

            AgeGender ();

            Line ();

            SizeScale ();

            QuickEditWrapper ();

            Line ();

        }
    }

    protected virtual void QuickEditWrapper () {
        if (!CharacterEditor2D.quickEdit) {

            if (Button ("Enable Quick Edit")) {
                CharacterEditor2D.quickEdit = true;
            }

        } else {
            QuickEdit ();
        }
    }

    protected virtual void QuickEdit () {
        EditorGUI.indentLevel++;

        EditorGUI.BeginChangeCheck ();
        float width = (EditorGUILayout.Slider ("Body Width", body.Width, 0f, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");
            body.Width = width;
        }

        EditorGUI.BeginChangeCheck ();
        float height = (EditorGUILayout.Slider ("Body Height", body.Height, 0f, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");

            body.Height = (height);
        }

        EditorGUI.BeginChangeCheck ();
        float faceWidth = (EditorGUILayout.Slider ("Face Width", body.FaceWidth, 0f, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");
            body.FaceWidth = (faceWidth);

        }

        EditorGUI.BeginChangeCheck ();
        float faceHeight = (EditorGUILayout.Slider ("Face Height", body.FaceHeight, 0f, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");
            body.FaceHeight = (faceHeight);

        }

        EditorGUI.indentLevel--;
        Line ();

        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUI.BeginChangeCheck ();
        float fatness = (EditorGUILayout.Slider ("Fatness", body.Fatness, 0f, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");
            body.Fatness = (fatness);

        }
        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUI.indentLevel++;

        EditorGUI.BeginChangeCheck ();
        float shoulderHeight = (EditorGUILayout.Slider ("Shoulder", body.ShoulderHeight, 0f, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");
            body.ShoulderHeight = (shoulderHeight);
        }

        EditorGUI.BeginChangeCheck ();
        float breastsHeight = 0;

        if (body.GetSex == CharacterBody2D.Sex.Female) {
            breastsHeight = (EditorGUILayout.Slider ("Breasts", body.BreastsHeight, 0f, 1f));
        }

        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");
            body.BreastsHeight = (breastsHeight);

        }

        EditorGUI.BeginChangeCheck ();
        float bellyHeight = (EditorGUILayout.Slider ("Belly", body.BellyHeight, 0f, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");
            body.BellyHeight = (bellyHeight);

        }

        EditorGUI.BeginChangeCheck ();
        float hipWidth = (EditorGUILayout.Slider ("Hip", body.HipWidth, 0f, 1f));
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (body, "Quick Edit");
            body.HipWidth = (hipWidth);

        }

        EditorGUI.indentLevel--;

    }

    protected virtual void Coloring () {

        ShowColorsPicker ("Skin", "Skin", ref skinColors, ref body.skinColor, ref toggleSkinColor, this);

        ShowColorsPicker ("Hair", "Hair", ref hairColors, ref body.hairColor, ref toggleHairColor, this);

        if (body.GetAge == CharacterBody2D.Age.Old) {
            body.grayHairColor = EditorGUILayout.ColorField ("Grey Hair", body.grayHairColor);

        }

        ShowColorsPicker ("Upper Cloth", "Dye", ref dyeColors, ref body.shirtColor, ref toggleShirtColor, this);

        EditorGUI.indentLevel++;

        if (body.isDifferentSleeve)
            ShowColorsPicker ("Sleeve", "Dye", ref dyeColors, ref body.sleeveColor, ref toggleSleeveColor, this);

        if (body.isInnerSleeve)
            ShowColorsPicker ("Inner Sleeve", "Dye", ref dyeColors, ref body.innerSleeveColor, ref toggleInnerSleeveColor, this);

        EditorGUI.indentLevel--;

        if (body.Gloves > 0)
            ShowColorsPicker ("Gloves", "Leather", ref leatherColors, ref body.gloveColor, ref toggleGlovesColor, this);

        ShowColorsPicker ("Pant", "Dye", ref dyeColors, ref body.pantColor, ref togglePantsColor, this);

        ShowColorsPicker ("Shoes", "Leather", ref leatherColors, ref body.shoesColor, ref toggleShoesColor, this);

    }

    public void SaveColors () {

        SCGSaveSystem.SaveColorPalette (skinColors.ToArray (), hairColors.ToArray (), dyeColors.ToArray (), leatherColors.ToArray (), true);
    }

    string GetPath () {
        if (location[location.Length - 1] != '/') location += '/';
        return location + filename;
    }

    protected virtual void ShowSaveAndLoad () {

        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Filename : ", GUILayout.Width (70));
        filename = EditorGUILayout.TextField (filename, GUILayout.MinWidth (100));
        EditorGUILayout.LabelField (".bytes");
        EditorGUILayout.EndHorizontal ();
        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.LabelField ("Location : Resources/", GUILayout.Width (130));
        location = EditorGUILayout.TextField (location);

        EditorGUILayout.EndHorizontal ();
        if (showLoad) loadType = (CharacterBodyData.Type) EditorGUILayout.EnumPopup ("Load Type", loadType);

        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button ("Cancel")) {
            showSaveAs = showLoad = false;
        }

        EditorGUI.BeginDisabledGroup (string.IsNullOrEmpty (filename));
        if (showSaveAs) {

            if (GUILayout.Button ("Save")) {
                Selection.activeGameObject.GetComponent<CharacterBody2D> ().Save (GetPath ());

                editor.profileName = (filename);
                justLoaded = 2;
                showSaveAs = showLoad = false;
            }
        } else if (showLoad) {

            if (GUILayout.Button ("Load")) {

                Load ();
                showSaveAs = showLoad = false;
            }
        }

        EditorGUI.EndDisabledGroup ();

        EditorGUILayout.EndHorizontal ();

    }

    void Load () {

        if (Selection.activeGameObject.GetComponent<CharacterBody2D> ().Load (GetPath (), loadType)) {
            editor.profileName = (filename);
            justLoaded = 2;
        }

        Vaildate ();

    }

}
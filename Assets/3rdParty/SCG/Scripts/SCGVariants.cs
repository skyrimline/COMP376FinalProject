using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SCGVariants : CharacterBody2D {

    public enum Options {
        Yes,
        No,
        Random
    }

    public bool male, female;
    public bool teen, adult, middleAge, old;

    public float minScale, maxScale;
    public float minActualSize, maxActualSize;
    public float minBodyWidth, maxBodyWidth;
    public float minBodyHeight, maxBodyHeight;
    public float minFaceWidth, maxFaceWidth;
    public float minFaceHeight, maxFaceHeight;
    public float minShoulder, maxShoulder;
    public float minBelly, maxBelly;
    public float minHip, maxHip;

    public float minFatness, maxFatness;
    public float minBreasts, maxBreasts;

    public List<Sprite> maleFaces = new List<Sprite> ();
    public List<Sprite> maleBodies = new List<Sprite> ();

    public List<Sprite> femaleFaces = new List<Sprite> ();
    public List<Sprite> femaleBodies = new List<Sprite> ();
    public List<Sprite> headwears = new List<Sprite> ();
    public List<Color> headwearColors = new List<Color> ();
    public List<Sprite> eyewears = new List<Sprite> ();
    public List<Color> eyewearColors = new List<Color> ();
    public List<Sprite> mustages = new List<Sprite> ();
    public List<Sprite> hairs = new List<Sprite> ();

    public List<Color> skinColors = new List<Color> ();
    public List<Color> hairColors = new List<Color> ();
    public List<Color> upperClothColors = new List<Color> ();
    public List<Color> innerSleeveColors = new List<Color> ();
    public List<Color> sleeveColors = new List<Color> ();
    public List<Color> glovesColors = new List<Color> ();
    public List<Color> pantColors = new List<Color> ();
    public List<Color> shoesColors = new List<Color> ();

    public bool includeSkinPalette;
    public bool includeShirtPalette;
    public bool includePantPalette;
    public bool includeShoesPalette;
    public bool includeGlovesPalette;
    public bool includeHairPalette;
    public bool includeGrayHairPalette;
    public bool includeSleevePalette;
    public bool includeInnerSleevePalette;
    public bool includeInnerPartPalette;

    public List<Sprite> innerParts = new List<Sprite> ();
    public List<Color> innerPartColors = new List<Color> ();
    public List<Color> grayHairColors = new List<Color> ();

    public bool fullArms, halfArms, noArms;
    public bool fullGloves, halfGloves, noGloves;
    public bool fullLeg, halfLeg;

    public Options shirtOn;
    public Options vest;
    public Options innerSleeveOption;
    public Options longCoat;
    public Options skirtOn;

    public float minSkirtHeight, maxSkirtHeight;
    public float minSkirtRadius, maxSkirtRadius;
    public float minCoatHeight, maxCoatHeight;

    public float minCoatRadius, maxCoatRadius;

    public float minGlovesWrap, maxGlovesWrap;
    public float minShoesWrap, maxShoesWrap;

    public List<Sprite> shoes = new List<Sprite> ();
    public List<Sprite> toes = new List<Sprite> ();
    public List<InnerShirtLayer> innerShirtLayers = new List<InnerShirtLayer> ();

    protected override void Awake () {

    }

    public void Load (CharacterBody2D body) {
        if (body != null) {

            myProfile = body.MyProfile;

            size = body.Size;
            scale = body.Scale;

            age = (Age) body.GetAge;

            height = body.Height;
            width = body.Width;

            fatness = body.Fatness;
            shoulderHeight = body.ShoulderHeight;
            breastsHeight = body.BreastsHeight;
            hipWidth = body.HipWidth;
            faceHeight = body.FaceHeight;
            faceWidth = body.FaceWidth;
            bellyHeight = body.BellyHeight;

            sex = (Sex) body.GetSex;

            hairColor = body.hairColor;
            skinColor = body.skinColor;
            shirtColor = body.shirtColor;
            pantColor = body.pantColor;
            sleeveColor = body.sleeveColor;
            innerSleeveColor = body.innerSleeveColor;
            shoesColor = body.shoesColor;
            gloveColor = body.gloveColor;
            grayHairColor = body.grayHairColor;

            shoesStyle = body.ShoesStyle;
            toeStyle = body.ToeStyle;

            innerBodyColor = body.InnerBodyColor;

            hairStyle = body.HairStyle;
            hatStyle = body.HatStyle;
            hatColor = body.HatColor;

            mustageStyle = body.MustageStyle;

            eyewearStyle = body.EyewearStyle;
            eyewearColor = body.EyewearColor;

            leftInner = body.LeftInner;
            leftInnerColor = body.LeftInnerColor;
            rightInner = body.RightInner;
            rightInnerColor = body.RightInnerColor;

            innerBodyStyle = body.InnerBodyStyle;

            differentSleeve = body.isDifferentSleeve;

            innerSleeve = body.isInnerSleeve;
            gloves = body.Gloves;
            glovesWrap = body.GlovesWrap;
            shoesWrap = body.ShoesWrap;

            wearShirt = body.isWearShirt;
            wearCoat = body.isWearCoat;
            armCover = body.ArmCover;
            legCover = body.LegCover;

            headwears.Clear ();
            headwears.Add (body.HatStyle);

            headwearColors.Clear ();
            headwearColors.Add (body.HatColor);

            eyewears.Clear ();
            eyewears.Add (body.EyewearStyle);

            eyewearColors.Clear ();
            eyewearColors.Add (body.EyewearColor);

            hairs.Clear ();
            hairs.Add (body.HairStyle);

            mustages.Clear ();
            mustages.Add (body.MustageStyle);

            innerParts.Clear ();
            innerParts.Add (body.InnerBodyStyle);

            innerPartColors.Clear ();
            innerPartColors.Add (body.InnerBodyColor);

            minSkirtRadius = body.skirt.minRadius;
            maxSkirtRadius = body.skirt.maxRadius;
            SerializeMinMax (body.skirt.extraHeight, ref minSkirtHeight, ref maxSkirtHeight);

            minCoatRadius = body.coat.minRadius;
            maxCoatRadius = body.coat.maxRadius;
            SerializeMinMax (body.coat.extraHeight, ref minCoatHeight, ref maxCoatHeight);

            minSize = body.minSize;
            maxSize = body.maxSize;

            shoes.Clear ();
            shoes.Add (body.ShoesStyle);

            toes.Clear ();
            toes.Add (body.ToeStyle);

            skinColors.Clear ();
            skinColors.Add (body.skinColor);
            hairColors.Clear ();
            hairColors.Add (body.hairColor);
            grayHairColors.Clear ();
            grayHairColors.Add (body.grayHairColor);
            upperClothColors.Clear ();
            upperClothColors.Add (body.shirtColor);
            sleeveColors.Clear ();
            sleeveColors.Add (body.sleeveColor);
            innerSleeveColors.Clear ();
            innerSleeveColors.Add (body.innerSleeveColor);
            glovesColors.Clear ();
            glovesColors.Add (body.gloveColor);
            pantColors.Clear ();
            pantColors.Add (body.pantColor);
            shoesColors.Clear ();
            shoesColors.Add (body.shoesColor);

            teen = adult = middleAge = old = false;

            switch (age) {
                case Age.Teen:
                    teen = true;
                    break;
                case Age.Adult:
                    adult = true;

                    break;
                case Age.MiddleAge:
                    middleAge = true;
                    break;
                case Age.Old:
                    old = true;
                    break;

            }

            male = female = false;

            if (sex == Sex.Male) {
                male = true;
            } else {
                female = true;
            }

            maleFaces.Clear ();
            femaleFaces.Clear ();

            maleFaces.Add (body.MaleFace);

            femaleFaces.Add (body.FemaleFace);

            maleBodies.Clear ();
            femaleBodies.Clear ();

            maleBodies.Add (body.MaleBody);

            femaleBodies.Add (body.FemaleBody);

            SerializeMinMax (scale, ref minScale, ref maxScale);
            SerializeMinMax (size, ref minActualSize, ref maxActualSize);
            SerializeMinMax (width, ref minBodyWidth, ref maxBodyWidth);
            SerializeMinMax (height, ref minBodyHeight, ref maxBodyHeight);
            SerializeMinMax (faceWidth, ref minFaceWidth, ref maxFaceWidth);
            SerializeMinMax (faceHeight, ref minFaceHeight, ref maxFaceHeight);
            SerializeMinMax (fatness, ref minFatness, ref maxFatness);
            SerializeMinMax (shoulderHeight, ref minShoulder, ref maxShoulder);
            SerializeMinMax (breastsHeight, ref minBreasts, ref maxBreasts);
            SerializeMinMax (bellyHeight, ref minBelly, ref maxBelly);
            SerializeMinMax (hipWidth, ref minHip, ref maxHip);

            if (isWearShirt) {
                shirtOn = Options.Yes;
            } else {
                shirtOn = Options.No;
            }

            if (isDifferentSleeve) {
                vest = Options.Yes;
            } else {
                vest = Options.No;

            }

            if (isInnerSleeve) {
                innerSleeveOption = Options.Yes;
            } else {
                innerSleeveOption = Options.No;
            }

            if (isWearCoat) {
                longCoat = Options.Yes;
            } else {
                longCoat = Options.No;
            }

            if (isWearSkirt) {
                skirtOn = Options.Yes;
            } else {
                skirtOn = Options.No;
            }

            noArms = halfArms = fullArms = false;

            switch (armCover) {
                case 0:
                    noArms = true;
                    break;
                case 1:
                    halfArms = true;
                    break;
                case 2:
                    fullArms = true;
                    break;
            }

            noGloves = halfGloves = fullGloves = false;

            switch (gloves) {
                case 0:
                    noGloves = true;
                    break;
                case 1:
                    halfGloves = true;
                    break;
                case 2:
                    fullGloves = true;
                    break;
            }

            SerializeMinMax (glovesWrap, ref minGlovesWrap, ref maxGlovesWrap);

            halfLeg = fullLeg = false;

            switch (legCover) {

                case 1:
                    halfLeg = true;
                    break;
                case 2:
                    fullLeg = true;
                    break;
            }

            SerializeMinMax (shoesWrap, ref minShoesWrap, ref maxShoesWrap);

#if UNITY_EDITOR
            EditorUtility.SetDirty (this);
#endif

        }

    }

    float GetOffset () {
        return 0.125f;
    }

    void SerializeMinMax (float value, ref float min, ref float max) {
        min = value - GetOffset ();
        max = value + GetOffset ();
    }

}
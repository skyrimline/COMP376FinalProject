using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class InnerShirtLayerData {
    public string sprite;
    public float[] color;
    public InnerShirtLayerData (Sprite sprite, Color color) {
        this.sprite = SCGSaveSystem.GetSpriteName (sprite);
        CharacterBodyData.ParseColor (color, ref this.color);
    }
}

[System.Serializable]
public class InnerShirtEntry {
    public Sprite sprite;
    public List<Color> colors = new List<Color> ();

    public bool includeDyeColors;

}

[System.Serializable]
public class InnerShirtLayer {
    public List<InnerShirtEntry> innerShirts = new List<InnerShirtEntry> ();

}

[System.Serializable]
public class CharacterBodyData {

    public enum Type {
        All = 0,
        BodyOnly = 1,
        ClothesOnly = -1
    }

    public Type loadType;

    public float size;
    public float scale;

    public int ageNumber;
    public float[] skinColor;
    public float[] hairColor;
    public float[] shirtColor;
    public float[] pantColor;
    public float[] sleeveColor;
    public float[] innerSleeveColor;
    public float[] shoesColor;

    public string shoesStyle, toeStyle;
    public float[] gloveColor;
    public float[] grayHairColor;
    public float[] innerBodyColor;
    public float[] leftInnerColor;
    public float[] rightInnerColor;

    public string leftInner;
    public string rightInner;
    public int sex;
    public string hairStyle;
    public string hatStyle;
    public float[] hatColor;

    public string mustageStyle;
    public string eyewearStyle;
    public float[] eyewearColor;
    public string innerBodyStyle;

    public string maleFace;
    public string femaleFace;
    public string maleBody;
    public string femaleBody;
    public bool wearShirt;
    public bool wearSkirt;
    public bool wearCoat;
    public int armCover;
    public int legCover;

    public BodyPartData head;
    public BodyPartData neck;
    public BodyPartData flank;

    public BodyPartSocketData shoulder;

    public BodyPartData breasts;
    public BodyPartData belly;

    public BodyPartData armUpper, armLower;
    public BodyPartData hand;
    public BodyPartData hip;

    public float minSkirtRadius;
    public float maxSkirtRadius;
    public float extraSkirtHeight;

    public float minCoatRadius;
    public float maxCoatRadius;
    public float extraCoatHeight;

    public BodyPartData legUpper, legLower, foot;

    public int age;

    public float height, fatness, width;
    public float bellyHeight;
    public float breastsHeight;
    public float shoulderHeight;
    public float hipWidth;
    public float faceHeight;
    public float faceWidth;

    public bool differentSleeve;
    public bool innerSleeve;
    public int gloves;
    public float glovesWrap;
    public float shoesWrap;

    public List<InnerShirtLayerData> innerLayers;

    public static void ParseColor (Color color, ref float[] myVar) {
        myVar = new float[4];
        myVar[0] = color.r;
        myVar[1] = color.g;
        myVar[2] = color.b;
        myVar[3] = color.a;
    }

    public CharacterBodyData (CharacterBody2D body) {

        size = body.Size;

        scale = body.Scale;

        age = (int) body.GetAge;
        ageNumber = body.AgeNumber;

        gloves = body.Gloves;
        innerSleeve = body.isInnerSleeve;
        gloves = body.Gloves;

        height = body.Height;
        fatness = body.Fatness;
        width = body.Width;

        bellyHeight = body.BellyHeight;
        shoulderHeight = body.ShoulderHeight;
        hipWidth = body.HipWidth;
        faceWidth = body.FaceWidth;
        faceHeight = body.FaceHeight;
        breastsHeight = body.BreastsHeight;

        maleFace = SCGSaveSystem.GetSpriteName (body.MaleFace);
        femaleFace = SCGSaveSystem.GetSpriteName (body.FemaleFace);

        maleBody = SCGSaveSystem.GetSpriteName (body.MaleBody);
        femaleBody = SCGSaveSystem.GetSpriteName (body.FemaleBody);

        ParseColor (body.skinColor, ref skinColor);
        ParseColor (body.hairColor, ref hairColor);
        ParseColor (body.shirtColor, ref shirtColor);
        ParseColor (body.pantColor, ref pantColor);
        ParseColor (body.sleeveColor, ref sleeveColor);
        ParseColor (body.innerSleeveColor, ref innerSleeveColor);
        ParseColor (body.shoesColor, ref shoesColor);
        ParseColor (body.gloveColor, ref gloveColor);
        ParseColor (body.grayHairColor, ref grayHairColor);
        ParseColor (body.InnerBodyColor, ref innerBodyColor);
        ParseColor (body.HatColor, ref hatColor);
        ParseColor (body.EyewearColor, ref eyewearColor);
        ParseColor (body.LeftInnerColor, ref leftInnerColor);
        ParseColor (body.RightInnerColor, ref rightInnerColor);
        sex = (int) body.GetSex;

        hairStyle = SCGSaveSystem.GetSpriteName (body.HairStyle);
        hatStyle = SCGSaveSystem.GetSpriteName (body.HatStyle);

        shoesStyle = SCGSaveSystem.GetSpriteName (body.ShoesStyle);
        toeStyle = SCGSaveSystem.GetSpriteName (body.ToeStyle);

        mustageStyle = SCGSaveSystem.GetSpriteName (body.MustageStyle);
        eyewearStyle = SCGSaveSystem.GetSpriteName (body.EyewearStyle);

        innerBodyStyle = SCGSaveSystem.GetSpriteName (body.InnerBodyStyle);
        leftInner = SCGSaveSystem.GetSpriteName (body.LeftInner);
        rightInner = SCGSaveSystem.GetSpriteName (body.RightInner);

        wearShirt = body.isWearShirt;
        wearSkirt = body.isWearSkirt;
        wearCoat = body.isWearCoat;
        armCover = body.ArmCover;
        legCover = body.LegCover;

        glovesWrap = body.GlovesWrap;
        shoesWrap = body.ShoesWrap;

        head = new BodyPartData (body.head);
        neck = new BodyPartData (body.neck);
        flank = new BodyPartData (body.flank);

        shoulder = new BodyPartSocketData (body.shoulder);
        hip = new BodyPartSocketData (body.hip);

        breasts = new BodyPartData (body.breasts);
        belly = new BodyPartData (body.belly);

        armUpper = new BodyPartData (body.armUpper);
        armLower = new BodyPartData (body.armLower);
        hand = new BodyPartData (body.hand);

        minSkirtRadius = body.skirt.minRadius;
        maxSkirtRadius = body.skirt.maxRadius;
        extraSkirtHeight = body.skirt.extraHeight;

        minCoatRadius = body.coat.minRadius;
        maxCoatRadius = body.coat.maxRadius;
        extraCoatHeight = body.coat.extraHeight;

        legUpper = new BodyPartData (body.legUpper);
        legLower = new BodyPartData (body.legLower);
        foot = new BodyPartData (body.foot);

        innerLayers = new List<InnerShirtLayerData> ();
        if (body.innerPartContainer) {
            for (int i = 0; i <= body.innerPartContainer.childs.Count - 1; i++) {
                innerLayers.Add (new InnerShirtLayerData (body.innerPartContainer.childs[i].sprite, body.innerPartContainer.childs[i].color));
            }
        }

    }

}
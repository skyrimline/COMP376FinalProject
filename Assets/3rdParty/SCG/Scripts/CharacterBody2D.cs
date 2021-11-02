using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]

public class CharacterBody2D : MonoBehaviour {

    public enum Sex {
        Male = 0,
        Female = 1
    }

    public enum Age {

        Teen = 2,
        Adult = 3,
        MiddleAge = 4,
        Old = 5,

    }

    [SerializeField] protected int ageNumber;

    float GetAgeProportion () {
        if (ageNumber <= 19) {
            return (ageNumber - 10) / 9f;
        } else if (ageNumber <= 39) {
            return (ageNumber - 20) / 19f;
        } else if (ageNumber <= 59) {
            return (ageNumber - 40) / 19f;
        } else {
            return (ageNumber - 60) / 20f;
        }
    }

    public void ResizeByAgeNumber () {
        ApplyAge ();
        if ((int) age < 4) {
            size = minSize + ((maxSize - minSize) * GetAgeProportion ());
        } else {
            size = maxSize - ((maxSize - minSize) * GetAgeProportion ());

        }
    }

    public void CorrectAge (Age age) {
        switch (age) {
            case Age.Teen:
                ageNumber = 15;
                break;
            case Age.Adult:

                ageNumber = 25;
                break;
            case Age.MiddleAge:

                ageNumber = 45;
                break;
            case Age.Old:

                ageNumber = 70;
                break;
        }
        ResizeByAgeNumber ();

    }

    public void CorrectAge (int age) {
        if (age <= 19) {
            GetAge = Age.Teen;
        } else if (age <= 39) {
            GetAge = Age.Adult;
        } else if (age <= 59) {
            GetAge = Age.MiddleAge;
        } else {
            GetAge = Age.Old;
        }
        ResizeByAgeNumber ();

    }

    public int AgeNumber {
        get { return ageNumber; }
        set { ageNumber = value; }
    }

    public void SetAgeNumber (int age) {
        ageNumber = age;
        CorrectAge (age);
        Repaint ();
    }

    [SerializeField] protected Sex sex;

    public void SetSex (Sex sex) {
        this.sex = sex;
        Repaint ();

    }

    public Sex GetSex {
        get { return sex; }
        set { sex = value; }
    }

    [SerializeField] protected Sprite hairStyle;

    public Sprite HairStyle {
        get { return this.hairStyle; }
        set { hairStyle = value; }
    }

    public void SetHairStyle (Sprite hairStyle) {
        this.hairStyle = hairStyle;
        Repaint ();

    }

    [SerializeField] protected Sprite hatStyle;

    public Sprite HatStyle {
        get { return this.hatStyle; }
        set { hatStyle = value; }
    }

    public void SetHatStyle (Sprite hatStyle) {
        this.hatStyle = hatStyle;
        Repaint ();

    }

    [SerializeField] protected Color hatColor;

    public Color HatColor {
        get { return this.hatColor; }
        set { hatColor = value; }
    }

    public void SetHatColor (Color hatColor) {
        this.hatColor = hatColor;

        Repaint ();

    }

    [SerializeField] protected Sprite mustageStyle;

    public Sprite MustageStyle {
        get { return this.mustageStyle; }
        set { mustageStyle = value; }
    }

    public void SetMustageStyle (Sprite mustageStyle) {
        this.mustageStyle = mustageStyle;
        Repaint ();

    }

    [SerializeField] protected Sprite eyewearStyle;

    public Sprite EyewearStyle {
        get { return this.eyewearStyle; }
        set { eyewearStyle = value; }
    }

    public void SetEyewearStyle (Sprite eyewearStyle) {
        this.eyewearStyle = eyewearStyle;
        Repaint ();

    }

    [SerializeField] protected Color eyewearColor;

    public Color EyewearColor {
        get { return this.eyewearColor; }
        set { eyewearColor = value; }
    }

    public void SetEyewearColor (Color eyewearColor) {
        this.eyewearColor = eyewearColor;
        Repaint ();

    }

    [SerializeField] protected Sprite innerBodyStyle;

    public Sprite InnerBodyStyle {
        get { return this.innerBodyStyle; }
        set { innerBodyStyle = value; }
    }

    public void SetInnerBodyStyle (Sprite innerBodyStyle) {
        this.innerBodyStyle = innerBodyStyle;
        Repaint ();

    }

    [SerializeField] protected Sprite shoesStyle;

    public Sprite ShoesStyle {
        get { return this.shoesStyle; }
        set { shoesStyle = value; }

    }

    public void SetShoesStyle (Sprite shoesStyle) {
        this.shoesStyle = shoesStyle;
        Repaint ();

    }

    [SerializeField] protected Sprite toeStyle;

    public Sprite ToeStyle {
        get { return this.toeStyle; }
        set { toeStyle = value; }

    }

    public void SetToeStyle (Sprite toeStyle) {
        this.toeStyle = toeStyle;
        Repaint ();

    }

    [SerializeField] protected Sprite leftInner;

    public Sprite LeftInner {
        get { return this.leftInner; }
        set { leftInner = value; }
    }

    public void SetLeftInner (Sprite leftInner) {
        this.leftInner = leftInner;
        Repaint ();
    }

    [SerializeField] protected Color leftInnerColor;

    public Color LeftInnerColor {
        get { return this.leftInnerColor; }
        set { leftInnerColor = value; }
    }

    public void SetLeftInnerColor (Color leftInnerColor) {
        this.leftInnerColor = leftInnerColor;
        Repaint ();
    }

    [SerializeField] protected Sprite rightInner;

    public Sprite RightInner {
        get { return this.rightInner; }
        set { rightInner = value; }
    }

    public void SetRightInner (Sprite rightInner) {
        this.rightInner = rightInner;
        Repaint ();
    }

    [SerializeField] protected Color rightInnerColor;

    public Color RightInnerColor {
        get { return this.rightInnerColor; }
        set { rightInnerColor = value; }

    }

    public void SetRightInnerColor (Color rightInnerColor) {
        this.rightInnerColor = rightInnerColor;
        Repaint ();
    }

    [SerializeField] protected Color innerBodyColor;

    public Color InnerBodyColor {
        get { return this.innerBodyColor; }
        set { innerBodyColor = value; }
    }

    public void SetInnerBodyColor (Color innerBodyColor) {
        this.innerBodyColor = innerBodyColor;
        Repaint ();
    }

    [SerializeField] protected bool wearShirt = true;

    public bool isWearShirt {
        get { return wearShirt; }
        set { wearShirt = value; }
    }

    public void SetWearShirt (bool wearShirt) {
        this.wearShirt = wearShirt;
        Repaint ();

    }

    [SerializeField] protected bool wearSkirt = true;

    public bool isWearSkirt {
        get { return wearSkirt; }
        set { wearSkirt = value; }
    }

    public void SetWearSkirt (bool wearSkirt) {
        this.wearSkirt = wearSkirt;
        Repaint ();

    }

    [SerializeField] protected bool wearCoat = true;

    public bool isWearCoat {
        get { return wearCoat; }
        set { wearCoat = value; }
    }

    public void SetWearCoat (bool wearCoat) {
        this.wearCoat = wearCoat;
        Repaint ();
    }

    [SerializeField] protected int armCover = 2;

    public int ArmCover {
        get { return armCover; }
        set { armCover = value; }
    }

    public void SetArmCover (int armCover) {
        this.armCover = armCover;
        Repaint ();
    }

    [SerializeField] protected int legCover = 2;

    public int LegCover {
        get { return legCover; }
        set { legCover = value; }
    }

    public void SetLegCover (int legCover) {
        this.legCover = legCover;
        Repaint ();
    }

    [SerializeField] protected float size = 1f;

    public float Scale {
        get { return scale; }
        set { scale = value; }
    }

    public void SetScale (float scale) {
        this.scale = scale;
        Repaint ();
    }

    [SerializeField] protected float scale = 0.5f;

    public float Size {
        get { return size; }
        set { size = value; }
    }

    public void SetSize (float size) {
        this.size = size;
        Repaint ();
    }

    public string sortingLayerName = "Default";
    public List<GameObject> sortingOrders = new List<GameObject> ();

    public SpriteRenderer faceRenderer;

    public SpriteRenderer hatRenderer;
    public SpriteRenderer eyewearRenderer;
    public SpriteRenderer hairRenderer;
    public SpriteRenderer mustageRenderer;

    public SpriteRenderer bodyRenderer;

    public BodyPart2D head;
    public BodyPart2D neck;
    public BodyPart2D flank;

    public BodyPartSocket2D shoulder;
    public BodyPartSocket2D hip;

    public BodyPart2D breasts;
    public BodyPart2D belly;

    public BodyPart2D armUpper, armLower;
    public HandPart2D hand, leftHand;

    public SpriteRenderer pelvis;
    public Skirt2D skirt, coat;
    public BodyPart2D legUpper, legLower;

    public FootPart2D foot;

    public Color skinColor;

    public Color hairColor;

    public Color shirtColor;

    public Color pantColor;

    public Color sleeveColor;

    public Color innerSleeveColor;

    public Color shoesColor;

    public Color gloveColor;
    public Color grayHairColor;

    public void SetSkinColor (Color skinColor) {
        this.skinColor = skinColor;
        Repaint ();

    }

    public void SetHairColor (Color hairColor) {
        this.hairColor = hairColor;
        Repaint ();

    }

    public void SetShirtColor (Color shirtColor) {
        this.shirtColor = shirtColor;
        Repaint ();

    }

    public void SetPantColor (Color pantColor) {
        this.pantColor = pantColor;
        Repaint ();

    }

    public void SetSleeveColor (Color sleeveColor) {
        this.sleeveColor = sleeveColor;
        Repaint ();
    }

    public void SetInnerSleeveColor (Color innerSleeveColor) {
        this.innerSleeveColor = innerSleeveColor;
        Repaint ();
    }

    public void SetShoesColor (Color shoesColor) {
        this.shoesColor = shoesColor;
        Repaint ();

    }

    public void SetGloveColor (Color gloveColor) {
        this.gloveColor = gloveColor;
        Repaint ();

    }

    public void SetGrayHairColor (Color grayHairColor) {
        this.grayHairColor = grayHairColor;
        Repaint ();
    }

    [SerializeField] protected bool differentSleeve;

    public bool isDifferentSleeve {
        get { return differentSleeve; }
        set { differentSleeve = value; }
    }

    public void SetDifferentSleeve (bool differentSleeve) {
        this.differentSleeve = differentSleeve;
        Repaint ();

    }

    [SerializeField] protected bool innerSleeve = true;

    public bool isInnerSleeve {
        get { return innerSleeve; }
        set { innerSleeve = value; }
    }

    public void SetInnerSleeve (bool innerSleeve) {
        this.innerSleeve = innerSleeve;
        Repaint ();

    }

    [SerializeField] protected int gloves;

    public int Gloves {
        get { return gloves; }
        set { gloves = value; }
    }

    public void SetGloves (int gloves) {
        this.gloves = gloves;
        Repaint ();

    }

    [SerializeField] protected float glovesWrap;

    public float GlovesWrap {
        get { return glovesWrap; }
        set { glovesWrap = value; }
    }

    public void SetGlovesWrap (float glovesWrap) {
        this.glovesWrap = glovesWrap;
        Repaint ();

    }

    [SerializeField] protected float shoesWrap;

    public float ShoesWrap {
        get { return shoesWrap; }
        set { shoesWrap = value; }
    }

    public void SetShoesWrap (float shoesWrap) {
        this.shoesWrap = shoesWrap;
        Repaint ();

    }

    public float minSize, maxSize;

    [SerializeField] protected Age age = Age.Adult;

    public void SetAge (Age age) {
        this.age = age;
        CorrectAge (age);
        Repaint ();

    }

    public Age GetAge {
        get { return age; }
        set { age = value; }
    }

    [SerializeField] protected float height;
    [SerializeField] protected float width;

    [SerializeField] protected float fatness;

    [SerializeField] protected float bellyHeight;
    [SerializeField] protected float breastsHeight;
    [SerializeField] protected float shoulderHeight;
    [SerializeField] protected float hipWidth;
    [SerializeField] protected float faceHeight;
    [SerializeField] protected float faceWidth;

    public void SetHeight (float height) {
        this.height = height;
        ApplyOverallShape ();

    }

    public float Height {
        get { return height; }
        set { height = value; }
    }

    public void SetWidth (float width) {
        this.width = width;
        ApplyOverallShape ();

    }

    public float Width {
        get { return width; }
        set { width = value; }
    }

    public void SetFatness (float fatness) {
        this.fatness = fatness;
        ApplyOverallShape ();

    }

    public float Fatness {
        get { return fatness; }
        set { fatness = value; }

    }

    public void SetBellyHeight (float bellyHeight) {
        this.bellyHeight = bellyHeight;
        ApplyOverallShape ();

    }

    public float BellyHeight {
        get { return bellyHeight; }
        set { bellyHeight = value; }

    }

    public void SetBreastsHeight (float breastsHeight) {
        this.breastsHeight = breastsHeight;
        ApplyOverallShape ();

    }

    public float BreastsHeight {
        get { return breastsHeight; }
        set { breastsHeight = value; }
    }

    public void SetShoulderHeight (float shoulderHeight) {
        this.shoulderHeight = shoulderHeight;
        ApplyOverallShape ();

    }

    public float ShoulderHeight {
        get { return shoulderHeight; }
        set { shoulderHeight = value; }
    }

    public void SetHipWidth (float hipWidth) {
        this.hipWidth = hipWidth;
        ApplyOverallShape ();

    }
    public float HipWidth {
        get { return hipWidth; }
        set { hipWidth = value; }
    }

    public void SetFaceHeight (float faceHeight) {
        this.faceHeight = faceHeight;
        ApplyOverallShape ();

    }

    public float FaceHeight {
        get { return faceHeight; }
        set { faceHeight = value; }

    }

    public void SetFaceWidth (float faceWidth) {
        this.faceWidth = faceWidth;
        ApplyOverallShape ();

    }

    public float FaceWidth {
        get { return faceWidth; }
        set { faceWidth = value; }
    }

    [SerializeField] Sprite maleFace;

    public Sprite MaleFace {
        get { return maleFace; }
        set { maleFace = value; }
    }

    public void SetMaleFace (Sprite maleFace) {
        this.maleFace = maleFace;
        RefreshFace ();
    }

    [SerializeField] Sprite femaleFace;

    public Sprite FemaleFace {
        get { return femaleFace; }
        set { femaleFace = value; }

    }

    public void SetFemaleFace (Sprite femaleFace) {
        this.femaleFace = femaleFace;
        RefreshFace ();
    }

    [SerializeField] Sprite maleBody;

    public Sprite MaleBody {
        get { return maleBody; }
        set { maleBody = value; }

    }

    public void SetMaleBody (Sprite maleBody) {
        this.maleBody = maleBody;
        Repaint ();

    }

    [SerializeField] Sprite femaleBody;

    public Sprite FemaleBody {
        get { return femaleBody; }
        set { femaleBody = value; }

    }

    public void SetFemaleBody (Sprite femaleBody) {
        this.femaleBody = femaleBody;
        Repaint ();
    }

    public Transform floor;
    public PerspectiveHandle2D perspectiveHandle;
    public Transform sittingLevelTransform;

    public bool Sitting {
        get { return isSitting; }
        set {

            if (!isSitting) {

                SitManually ();

            }
            isSitting = value;

        }
    }
    private bool isSitting;

    protected CharacterBodyData myProfile = null;

    public CharacterBodyData MyProfile {
        get { return myProfile; }
    }

    public CharacterEditor2D editor;
    BodyPart2D firstLeg;

    public bool simulateGround = true;
    public bool simulatePerspective = true;
    [HideInInspector][SerializeField] float perspective;

    public float Perspective {
        get { return perspective; }
        set {
            this.perspective = value;

            if (simulatePerspective == false) this.perspective = 0;

            ApplyPerspective ();
        }
    }

    public void SetPerspective (float perspective) {

        if (perspectiveHandle && perspectiveHandle.gameObject.activeSelf) {
            perspectiveHandle.enabled = false;
            perspectiveHandle.SetValue (perspective);
        } else {
            Perspective = perspective;

        }

    }

    public void AutoPerspective () {
        if (perspectiveHandle) {
            perspectiveHandle.enabled = true;
        }
    }

    float armPerspectiveOffsetLeft, armPerspectiveOffsetRight;
    float oldPerspective;

    [HideInInspector] public bool perspectiveArms;

    public static float jointSize = 0.175f;

    [HideInInspector] public SpriteRenderer innerPart;

    [HideInInspector] public InnerPartContainer innerPartContainer;

    [HideInInspector] public CharacterGenerator2D generator;

    protected virtual void Awake () {

        if (SCGCore.isEditor ()) {

            simulateGround = true;
            Repaint ();
        } else {
            ValidateParts ();

        }
    }

    void OnDestroy () {
        if (SCGCore.isEditor ()) {
#if UNITY_EDITOR
            Undo.RecordObjects (GetComponentsInChildren<BodyPart2D> (), "Destroy a CharacterBody2D");
#endif
        }
    }

    public static Color DecryptColor (float[] origin) {

        return new Color (origin[0], origin[1], origin[2], origin[3]);

    }

    public void Save (string path) {
        SCGSaveSystem.SaveBodyData (path, this);
    }

    public bool Load (string path) {
        return Load (path, CharacterBodyData.Type.All);
    }

    public bool Load (string path, CharacterBodyData.Type loadType) {

        CharacterBodyData body = SCGSaveSystem.LoadBodyData (path, loadType);

        if (body != null) {

            myProfile = body;

            if ((int) body.loadType >= 0) {

                size = body.size;
                scale = body.scale;

                age = (Age) body.age;
                ageNumber = body.ageNumber;

                height = body.height;
                width = body.width;

                fatness = body.fatness;
                shoulderHeight = body.shoulderHeight;
                breastsHeight = body.breastsHeight;
                hipWidth = body.hipWidth;
                faceHeight = body.faceHeight;
                faceWidth = body.faceWidth;
                bellyHeight = body.bellyHeight;

                sex = (Sex) body.sex;

                hairColor = DecryptColor (body.hairColor);
                skinColor = DecryptColor (body.skinColor);

                grayHairColor = DecryptColor (body.grayHairColor);
                hairStyle = (SCGSaveSystem.LoadSprite (body.hairStyle));

                mustageStyle = (SCGSaveSystem.LoadSprite ((body.mustageStyle)));

                maleBody = (SCGSaveSystem.LoadSprite ((body.maleBody)));
                femaleBody = (SCGSaveSystem.LoadSprite ((body.femaleBody)));
                maleFace = (SCGSaveSystem.LoadSprite ((body.maleFace)));
                femaleFace = (SCGSaveSystem.LoadSprite ((body.femaleFace)));

                head.Load (body.head);
                neck.Load (body.neck);
                flank.Load (body.flank);

                shoulder.Load (body.shoulder);
                hip.Load (body.hip);

                breasts.Load (body.breasts);
                belly.Load (body.belly);

                armUpper.Load (body.armUpper);
                armLower.Load (body.armLower);
                hand.Load (body.hand);

                legUpper.Load (body.legUpper);
                legLower.Load (body.legLower);
                foot.Load (body.foot);
            }

            if ((int) body.loadType <= 0) {
                shirtColor = DecryptColor (body.shirtColor);
                pantColor = DecryptColor (body.pantColor);
                sleeveColor = DecryptColor (body.sleeveColor);
                innerSleeveColor = DecryptColor (body.innerSleeveColor);
                shoesColor = DecryptColor (body.shoesColor);
                gloveColor = DecryptColor (body.gloveColor);
                shoesStyle = (SCGSaveSystem.LoadSprite ((body.shoesStyle)));
                toeStyle = (SCGSaveSystem.LoadSprite ((body.toeStyle)));
                innerBodyColor = (DecryptColor (body.innerBodyColor));
                hatStyle = (SCGSaveSystem.LoadSprite ((body.hatStyle)));
                hatColor = (DecryptColor (body.hatColor));
                eyewearStyle = (SCGSaveSystem.LoadSprite ((body.eyewearStyle)));
                eyewearColor = (DecryptColor (body.eyewearColor));
                leftInner = SCGSaveSystem.LoadSprite ((body.leftInner));
                leftInnerColor = DecryptColor (body.leftInnerColor);
                rightInner = SCGSaveSystem.LoadSprite ((body.rightInner));
                rightInnerColor = DecryptColor (body.rightInnerColor);
                innerBodyStyle = (SCGSaveSystem.LoadSprite (body.innerBodyStyle));
                differentSleeve = (body.differentSleeve);
                innerSleeve = body.innerSleeve;
                gloves = body.gloves;
                glovesWrap = body.glovesWrap;
                shoesWrap = body.shoesWrap;
                wearShirt = body.wearShirt;
                wearSkirt = body.wearSkirt;
                wearCoat = (body.wearCoat);
                armCover = body.armCover;
                legCover = body.legCover;

                skirt.maxRadius = (body.maxSkirtRadius);
                skirt.minRadius = (body.minSkirtRadius);
                skirt.extraHeight = (body.extraSkirtHeight);

                coat.maxRadius = (body.maxCoatRadius);
                coat.minRadius = (body.minCoatRadius);
                coat.extraHeight = (body.extraCoatHeight);

                if (innerPartContainer) {
                    innerPartContainer.Load (body.innerLayers);
                }
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty (this);
#endif

            Validate ();
            Repaint ();

            return true;

        }
        return false;

    }

    Color GetRandomColor (List<Color> colors) {
        if (colors.Count == 0) return Color.white;
        return colors[Random.Range (0, colors.Count)];
    }

    public static Color GetRandomColor (List<Color> colors, bool includePalette, List<Color> palette) {

        if (includePalette) {

            if ((Random.value > 0.5f || colors.Count == 0) && palette.Count > 0) {
                return palette[Random.Range (0, palette.Count)];

            } else {
                if (colors.Count == 0) return Color.white;
                return colors[Random.Range (0, colors.Count)];

            }

        } else {
            if (colors.Count == 0) return Color.white;
            return colors[Random.Range (0, colors.Count)];
        }
    }

    Sprite GetRandomSprite (List<Sprite> sprites) {
        if (sprites.Count == 0) return null;
        return sprites[Random.Range (0, sprites.Count)];
    }
    InnerShirtEntry GetRandomEntry (List<InnerShirtEntry> sprites) {
        if (sprites.Count == 0) return null;
        return sprites[Random.Range (0, sprites.Count)];
    }

    void SetBool (ref bool targetVar, SCGVariants.Options option) {
        if (option == SCGVariants.Options.Yes) {
            targetVar = true;

        } else if (option == SCGVariants.Options.No) {
            targetVar = false;

        } else {
            targetVar = (Random.value > 0.5f);
        }
    }

    public CharacterBody2D Generate (SCGVariants variants) {

        if (variants != null) {
            ValidateParts ();

            size = Random.Range (variants.minActualSize, variants.maxActualSize);
            scale = Random.Range (variants.minScale, variants.maxScale);

            List<Age> ages = new List<Age> ();

            if (variants.teen) ages.Add (Age.Teen);
            if (variants.adult) ages.Add (Age.Adult);
            if (variants.middleAge) ages.Add (Age.MiddleAge);
            if (variants.old) ages.Add (Age.Old);

            if (ages.Count == 0) {
                age = Age.Adult;

            } else {
                age = ages[Random.Range (0, ages.Count)];

            }

            height = Random.Range (variants.minBodyHeight, variants.maxBodyHeight);
            width = Random.Range (variants.minBodyWidth, variants.maxBodyWidth);

            fatness = Random.Range (variants.minFatness, variants.maxFatness);
            shoulderHeight = Random.Range (variants.minShoulder, variants.maxShoulder);
            breastsHeight = Random.Range (variants.minBreasts, variants.maxBreasts);
            hipWidth = Random.Range (variants.minHip, variants.maxHip);
            faceHeight = Random.Range (variants.minFaceHeight, variants.maxFaceHeight);
            faceWidth = Random.Range (variants.minFaceWidth, variants.maxFaceWidth);
            bellyHeight = Random.Range (variants.minBelly, variants.maxBelly);

            List<Sex> sexs = new List<Sex> ();

            if (variants.male) sexs.Add (Sex.Male);
            if (variants.female) sexs.Add (Sex.Female);

            if (sexs.Count == 0) {
                sex = Sex.Male;

            } else {

                sex = sexs[Random.Range (0, sexs.Count)];
            }

            hairColor = GetRandomColor (variants.hairColors, variants.includeHairPalette, SCGSaveSystem.hairColors);
            skinColor = GetRandomColor (variants.skinColors, variants.includeSkinPalette, SCGSaveSystem.skinColors);
            shirtColor = GetRandomColor (variants.upperClothColors, variants.includeShirtPalette, SCGSaveSystem.dyeColors);
            pantColor = GetRandomColor (variants.pantColors, variants.includePantPalette, SCGSaveSystem.dyeColors);
            shoesColor = GetRandomColor (variants.shoesColors, variants.includeShoesPalette, SCGSaveSystem.leatherColors);
            sleeveColor = GetRandomColor (variants.sleeveColors, variants.includeSleevePalette, SCGSaveSystem.dyeColors);
            innerSleeveColor = GetRandomColor (variants.innerSleeveColors, variants.includeInnerSleevePalette, SCGSaveSystem.dyeColors);
            gloveColor = GetRandomColor (variants.glovesColors, variants.includeGlovesPalette, SCGSaveSystem.leatherColors);
            grayHairColor = GetRandomColor (variants.grayHairColors, variants.includeGrayHairPalette, SCGSaveSystem.hairColors);

            shoesStyle = GetRandomSprite (variants.shoes);
            toeStyle = GetRandomSprite (variants.toes);

            innerBodyColor = GetRandomColor (variants.innerPartColors, variants.includeInnerPartPalette, SCGSaveSystem.dyeColors);

            hairStyle = GetRandomSprite (variants.hairs);
            hatStyle = GetRandomSprite (variants.headwears);
            hatColor = GetRandomColor (variants.headwearColors);

            mustageStyle = GetRandomSprite (variants.mustages);

            eyewearStyle = GetRandomSprite (variants.eyewears);
            eyewearColor = GetRandomColor (variants.eyewearColors);

            leftInner = null;
            rightInner = null;

            innerBodyStyle = GetRandomSprite (variants.innerParts);
            if (innerPartContainer) {
                innerPartContainer.Clean ();
                if (innerBodyStyle) {
                    for (int i = 0; i <= variants.innerShirtLayers.Count - 1; i++) {
                        InnerShirtEntry tempEntry = GetRandomEntry (variants.innerShirtLayers[i].innerShirts);
                        if (tempEntry != null) {
                            innerPartContainer.Add (tempEntry);
                        } else {
                            break;
                        }
                    }
                }
            }

            SetBool (ref differentSleeve, variants.vest);
            SetBool (ref innerSleeve, variants.innerSleeveOption);
            SetBool (ref wearShirt, variants.shirtOn);
            SetBool (ref wearCoat, variants.longCoat);
            SetBool (ref wearSkirt, variants.skirtOn);

            List<int> glovesOption = new List<int> ();
            if (variants.noGloves) glovesOption.Add (0);
            if (variants.halfGloves) glovesOption.Add (1);
            if (variants.fullGloves) glovesOption.Add (2);

            if (glovesOption.Count == 0) {

                gloves = 0;
            } else {

                gloves = glovesOption[Random.Range (0, glovesOption.Count)];
            }

            glovesWrap = Random.Range (variants.minGlovesWrap, variants.maxGlovesWrap);
            shoesWrap = Random.Range (variants.minShoesWrap, variants.maxShoesWrap);

            List<int> armOption = new List<int> ();
            if (variants.noArms) armOption.Add (0);
            if (variants.halfArms) armOption.Add (1);
            if (variants.fullArms) armOption.Add (2);

            if (armOption.Count == 0) {

                armCover = 2;
            } else {

                armCover = armOption[Random.Range (0, armOption.Count)];
            }

            List<int> legOption = new List<int> ();
            if (variants.halfLeg) legOption.Add (1);
            if (variants.fullLeg) legOption.Add (2);

            if (legOption.Count == 0) {
                legCover = 2;
            } else {

                legCover = legOption[Random.Range (0, legOption.Count)];
            }

            skirt.maxRadius = variants.maxSkirtRadius;
            skirt.minRadius = variants.minSkirtRadius;
            skirt.extraHeight = Random.Range (variants.minSkirtHeight, variants.maxSkirtHeight);

            coat.maxRadius = variants.maxCoatRadius;
            coat.minRadius = variants.minCoatRadius;
            coat.extraHeight = Random.Range (variants.minCoatHeight, variants.maxCoatHeight);

            maleBody = GetRandomSprite (variants.maleBodies);
            femaleBody = GetRandomSprite (variants.femaleBodies);
            maleFace = GetRandomSprite (variants.maleFaces);
            femaleFace = GetRandomSprite (variants.femaleFaces);

#if UNITY_EDITOR
            if (SCGCore.isEditor ())
                EditorUtility.SetDirty (this);
#endif

            Repaint (false);
            ApplyOverallShape ();

        }
        return this;

    }

    void ValidateParts () {
        if (SCGCore.isEditor ()) {
            if (hip) {
                hip.body = this;
                hip.SetFloor (floor);
                shoulder.SetFloor (floor);
                if (flank == null) {
                    flank = hip.childPart;
                }

                if (flank) flank.body = this;

                if (bodyRenderer == null && flank) bodyRenderer = flank.renderer;
                if (skirt == null) skirt = hip.mySkirt;
                if (coat == null) coat = hip.myCoat;
                if (legUpper == null) {
                    if (hip.isSetupCompleted ()) {
                        legUpper = hip.right;

                    }
                }

                if (legUpper) {
                    legUpper.body = this;
                    if (legUpper.buddy) legUpper.buddy.body = this;
                }
            }
            if (legLower == null && legUpper) {
                legLower = legUpper.childPart;

            }
            if (legLower) {
                legLower.body = this;
                if (legLower.buddy) legLower.buddy.body = this;
            }
            if (foot == null && legLower) {
                foot = legLower.childPart.GetComponent<FootPart2D> ();

            }
            if (foot) {
                foot.body = this;
                if (foot.buddy) foot.buddy.body = this;
            }

            if (shoulder == null && flank && flank.childPart) {
                shoulder = flank.childPart.GetComponent<BodyPartSocket2D> ();

            }

            if (shoulder) shoulder.body = this;

            if (neck == null && shoulder) {
                neck = shoulder.childPart;

            }
            if (neck) neck.body = this;
            if (head == null && neck) {
                head = neck.childPart;

            }
            if (head) head.body = this;

            if (faceRenderer == null && head) faceRenderer = head.renderer;
            if (armUpper == null && shoulder) {

                if (shoulder.isSetupCompleted ()) {
                    armUpper = shoulder.right;

                }
            }

            if (armUpper) {
                armUpper.body = this;
                armUpper.buddy.body = this;

            }
            if (armLower == null && armUpper) armLower = armUpper.childPart;
            if (armLower) {
                armLower.body = this;
                armLower.buddy.body = this;

            }
            if (hand == null && armLower) {
                hand = armLower.childPart.GetComponent<HandPart2D> ();
            }

            if (leftHand == null && hand) leftHand = hand.buddy.GetComponent<HandPart2D> ();

            if (hand) {
                hand.body = this;
                hand.buddy.body = this;
            }
            if (leftHand) {
                leftHand.body = this;
                leftHand.buddy.body = this;
            }

            if (belly) belly.body = this;
            if (belly.buddy) belly.buddy.body = this;
            if (breasts) breasts.body = this;
            if (breasts.buddy) breasts.buddy.body = this;

            if (innerPart) {
                innerPartContainer = innerPart.GetComponent<InnerPartContainer> ();
            }
        }

        Sort ();

    }

    public void SwitchTurn () {
        SetTurn (-bodySide);
    }

    [HideInInspector] public int bodySide = 1;

    public void SetTurn (int i) {

        // if (i != bodySide) perspectiveHandle.Invert ();
        if (floor) floor.transform.localScale = new Vector3 (Mathf.Abs (floor.transform.localScale.x) * i, floor.transform.localScale.y, floor.transform.localScale.z);
        bodySide = i;
        if (hip) hip.LimitMovement ();
    }

    void Resize () {
        transform.localScale = Vector3.one;

        size = Mathf.Clamp (size, minSize, maxSize);

        if (floor) floor.transform.localScale = new Vector3 (bodySide * size * scale, size * scale, 1f);

        if (hip) {
            hip.GetGroundLevel ();
            hip.LimitMovement ();
        }

    }

    public void Validate () {
        ValidateParts ();
    }

    protected void ChangeOrder (SpriteRenderer target, int i) {
        if (target) {
            target.transform.localPosition = new Vector3 (target.transform.localPosition.x, target.transform.localPosition.y, 0);
            target.sortingLayerName = sortingLayerName;
            target.sortingOrder = i;
        }

    }

    public void DirectSort () {

        int index = 0;
        bool innerPartSetted = false;

        for (int i = sortingOrders.Count - 1; i >= 0; i--) {

            if (sortingOrders[i]) {

                BodyPart2D part = sortingOrders[i].GetComponent<BodyPart2D> ();

                if (part) {

                    if (sex == Sex.Female && wearSkirt && innerPartSetted && part == belly.buddy) {

                    } else {
                        index = part.Sort (sortingLayerName, (index)) + 1;

                    }

                } else {

                    SpriteRenderer targetRenderer = sortingOrders[i].GetComponent<SpriteRenderer> ();
                    if (targetRenderer && ((targetRenderer != pelvis) || (sex == Sex.Female && wearSkirt)) &&
                        ((targetRenderer == innerPart && wearCoat) || innerPartSetted == false || (targetRenderer != innerPart))) {
                        targetRenderer.sortingLayerName = sortingLayerName;
                        targetRenderer.sortingOrder = index;
                        if (targetRenderer == innerPart) {
                            innerPartSetted = true;
                            if (innerPartContainer) {
                                index = innerPartContainer.Sort (index, sortingLayerName);
                            }
                        }
                        index++;
                    } else {
                        Skirt2D targetSkirt = sortingOrders[i].GetComponent<Skirt2D> ();
                        if (targetSkirt) {
                            targetSkirt.Sort (sortingLayerName, index);
                            index++;
                        }
                    }

                }
            }

        }

    }

    void SwapCoatOrder () {

        int oldNeck = neck.sortingOrder;
        neck.sortingOrder = shoulder.sortingOrder;
        shoulder.sortingOrder = oldNeck;
        neck.Sort ();
        shoulder.Sort ();

        if (wearCoat) {

            int oldLeg = foot.sortingOrder;
            legUpper.sortingOrder = coat.sortingOrder;
            legUpper.Sort ();

            coat.sortingOrder = oldLeg;
            coat.SetColor (shirtColor);

            hip.colorRef = flank;
            hip.RefreshColor ();

            legUpper.buddy.circleColorRef = flank;
            legUpper.buddy.RefreshColor ();

            if (wearSkirt) {

                int oldCoat = hip.sortingOrder;
                hip.sortingOrder = skirt.sortingOrder;
                skirt.sortingOrder = oldCoat;
                skirt.Sort ();

            }

            coat.Sort ();

        }
    }

    public float GetInnerWidth () {
        if (innerPart.sprite == null) return 0;
        return (((flank.renderer.size.x / innerPart.sprite.bounds.size.x)) * (GetPerspectiveFactor () * GetPerspectiveFactor ()));
    }

    public float GetInnerPerFlankWidth () {
        if (innerPart.sprite == null) return 0;
        return (innerPart.sprite.bounds.size.x * flank.GetWidth () * 0.5f) * 0.5f;
    }

    public void RefreshInnerPart () {

        if (innerPart) {

            if (flank.childPart && (wearShirt || sex == Sex.Female) && innerPart.sprite) {

                if (perspective > 90f) {
                    innerPart.size = new Vector2 (0, SCGCore.DebugSize (innerPart.sprite.bounds.size.y));

                } else if (float.IsNaN (innerPart.sprite.bounds.size.x) == false) {
                    innerPart.size = new Vector2 (SCGCore.DebugSize (innerPart.sprite.bounds.size.x), SCGCore.DebugSize (innerPart.sprite.bounds.size.y));
                }

                float innerWidth = GetInnerWidth ();

                if (float.IsNaN (innerWidth) == false) {
                    innerPart.transform.localScale = new Vector3 (innerWidth, ((flank.renderer.size.y + (flank.childPart.renderer.size.y / 2f)) / (innerPart.size.y)) /*+ Mathf.Abs (Mathf.Clamp (flank.transform.rotation.z, hip.minRotation, hip.maxRotation) / 2f) */ , 1f);
                    innerPart.transform.localPosition = new Vector3 ((1 - Mathf.Abs (GetPerspectiveFactor ())), 0 + ((flank.childPart.renderer.size.y) / 4f) /*- Mathf.Abs (Mathf.Clamp (flank.transform.rotation.z, hip.minRotation, hip.maxRotation) / 2f)*/ , innerPart.transform.localPosition.z);
                }

                innerPart.enabled = true;

            } else {
                innerPart.enabled = false;
            }
            if (innerPartContainer) {
                innerPartContainer.Resize ();
            }

        }
    }

    public void Sort () {
        Sort (true);
    }

    public void Sort (bool forced) {

        if ((oldPerspective > 90f || forced) && perspective <= 90f) {
            if (oldPerspective > 90f && perspective <= 90f) SwapCoatOrder ();

            legUpper.buddy.circleColorRef = null;
            legUpper.buddy.RefreshColor ();
            hip.colorRef = legUpper;
            hip.RefreshColor ();

            DirectSort ();
        } else if ((oldPerspective <= 90f || forced) && perspective > 90f) {
            if (oldPerspective <= 90f && perspective > 90f) SwapCoatOrder ();
        }

    }

    void ControlFloor () {
        if (floor) {
            floor.transform.localPosition = new Vector3 (0, Mathf.Clamp (floor.transform.localPosition.y, 0, Mathf.Infinity), 0);
            floor.transform.localEulerAngles = Vector3.zero;
        }
    }
    void ControlSeat () {
        if (sittingLevelTransform) {
            sittingLevelTransform.transform.localPosition = new Vector3 (0, Mathf.Clamp (sittingLevelTransform.transform.localPosition.y, 0, Mathf.Infinity), 0);
            sittingLevelTransform.transform.localEulerAngles = Vector3.zero;
            sittingLevelTransform.transform.localScale = Vector3.one;
        }
    }

    void Update () {

        ControlFloor ();
        ControlSeat ();
        ApplyPerspective ();
    }

    public void ApplyAge () {

        ageNumber = Mathf.Clamp (ageNumber, 10, 80);

        switch (age) {
            case Age.Teen:
                minSize = 0.6f;
                maxSize = 0.8f;

                break;
            case Age.Adult:

                minSize = 0.8f;
                maxSize = 1.0f;

                break;
            case Age.MiddleAge:
                minSize = 0.95f;
                maxSize = 1.0f;

                break;
            case Age.Old:
                minSize = 0.85f;
                maxSize = 0.95f;

                break;
        }
        if (age != Age.Old) {
            hairRenderer.color = mustageRenderer.color = hairColor;

        } else {
            hairRenderer.color = mustageRenderer.color = grayHairColor;

        }

    }

    public void ApplyOverallShape () {

        hip.LerpWidth (width);
        flank.LerpWidth (1f - (hipWidth));
        shoulder.LerpWidth (1f - (hipWidth));

        head.LerpWidth (faceWidth);
        head.LerpHeight (faceHeight);
        shoulder.LerpHeight (shoulderHeight);
        breasts.LerpHeight (breastsHeight);
        belly.LerpHeight (bellyHeight);

        armUpper.LerpHeight (height);
        armLower.LerpHeight (height);
        legLower.LerpHeight (height);
        legUpper.LerpHeight (height);
        flank.LerpHeight (height);
        foot.LerpHeight (height);

        hand.LerpWidth (fatness);
        neck.LerpWidth (fatness);
        legLower.LerpWidth (fatness);
        legUpper.LerpWidth (fatness);
        armUpper.LerpWidth (fatness);
        armLower.LerpWidth (fatness);
        foot.LerpWidth (fatness);

        ValidateAll ();

    }

    public void RefreshLegs () {

        if (skirt) {
            if (isWearCoat && coat) {
                skirt.minRadius = Mathf.Clamp (skirt.minRadius, 0, coat.minRadius - 3f);
            }
        }

        if (skirt) skirt.gameObject.SetActive (sex == Sex.Female && wearSkirt);

        if (sex == Sex.Male || !wearSkirt) {
            switch (legCover) {

                case 1:

                    legUpper.SetColor (pantColor);
                    legLower.SetColor (skinColor);
                    break;
                case 2:
                    legUpper.SetColor (pantColor);
                    legLower.SetColor (pantColor);

                    break;

            }
        } else {
            legUpper.SetColor (pantColor);
            legLower.SetColor (skinColor);
            skirt.SetColor (pantColor);
        }

        if (legLower.wrapper) {
            legLower.wrapperHeightScale = shoesWrap;
            legLower.buddy.wrapperHeightScale = shoesWrap;

        }

    }

    public void RefreshArms () {

        if (!wearShirt && sex == Sex.Male) {
            armUpper.SetColor (skinColor);
            armLower.SetColor (skinColor);
            armLower.sleeve.Show (false);
            armLower.buddy.sleeve.Show (false);
        } else {
            switch (armCover) {
                case 0:

                    armUpper.SetColor (skinColor);
                    armLower.SetColor (skinColor);
                    armLower.sleeve.Show (false);
                    armLower.buddy.sleeve.Show (false);

                    break;
                case 1:
                    if (differentSleeve) {
                        armUpper.SetColor (sleeveColor);

                    } else {
                        armUpper.SetColor (shirtColor);

                    }
                    armLower.SetColor (skinColor);
                    armLower.sleeve.Show (false);
                    armLower.buddy.sleeve.Show (false);
                    break;
                case 2:
                    if (differentSleeve) {
                        armLower.SetColor (sleeveColor);

                    } else {
                        armLower.SetColor (shirtColor);

                    }
                    if (differentSleeve) {
                        armUpper.SetColor (sleeveColor);

                    } else {
                        armUpper.SetColor (shirtColor);

                    }

                    if (innerSleeve) {
                        armLower.sleeve.SetColor (innerSleeveColor);
                        armLower.buddy.sleeve.SetColor (innerSleeveColor);
                        armLower.sleeve.Show (true);
                        armLower.buddy.sleeve.Show (true);
                    } else {
                        armLower.sleeve.Show (false);
                        armLower.buddy.sleeve.Show (false);
                    }

                    break;

            }
        }

        if (gloves == 0) {
            hand.SetColor (skinColor, skinColor, gloves);
            if (leftHand) leftHand.SetColor (skinColor, skinColor, gloves);

            if (armLower.wrapper) {
                armLower.buddy.wrapperHeightScale = armLower.wrapperHeightScale = 0;
            }

        } else {
            hand.SetColor (gloveColor, skinColor, gloves);
            if (leftHand) leftHand.SetColor (gloveColor, skinColor, gloves);

            if (armLower.wrapper) {
                armLower.wrapperHeightScale = glovesWrap;
                armLower.buddy.wrapperHeightScale = glovesWrap;

            }

        }

        if (hand.side == BodyPart2D.PartSide.Left) {
            hand.itemRenderer.sprite = leftInner;
            hand.itemRenderer.color = leftInnerColor;

            hand.itemRenderer.sprite = rightInner;
            hand.itemRenderer.color = rightInnerColor;

        } else if (hand.side == BodyPart2D.PartSide.Right) {
            hand.itemRenderer.sprite = rightInner;
            hand.itemRenderer.color = rightInnerColor;

            leftHand.itemRenderer.sprite = leftInner;
            leftHand.itemRenderer.color = leftInnerColor;

        }

        if (hand.itemRenderer.sprite) {
            hand.itemRenderer.enabled = true;
        } else {
            hand.itemRenderer.enabled = false;

        }

        if (leftHand.itemRenderer.sprite) {
            leftHand.itemRenderer.enabled = true;
        } else {
            leftHand.itemRenderer.enabled = false;

        }

    }

    void RefreshBody () {

        RefreshInnerPart ();

        if (coat) {
            coat.gameObject.SetActive (wearCoat && wearShirt);
        }

        if (!differentSleeve) {
            sleeveColor = Color.white;
        }

        neck.SetColor (skinColor);
        faceRenderer.color = skinColor;
        coat.SetColor (shirtColor);
        if (shoesColor.a == 1) {
            foot.SetColor (shoesColor);

        } else {
            foot.SetColor (skinColor);

        }

        breasts.gameObject.SetActive (sex == Sex.Female);
        if (breasts.buddy) breasts.buddy.gameObject.SetActive (sex == Sex.Female);

        if (wearSkirt && sex != Sex.Male && !wearCoat) {
            hip.circleColorRef = legUpper;
        } else {
            hip.circleColorRef = flank;

        }

        if (flank) {
            if (!wearShirt && sex == Sex.Male) {
                flank.SetColor (skinColor);

            } else {
                flank.SetColor (shirtColor);
            }
            if (belly) belly.SetColor (flank.Color);
            if (breasts) breasts.SetColor (flank.Color);

        }

    }

    void RefreshFace () {
        foot.toeRenderer.sprite = toeStyle;
        foot.renderer.sprite = shoesStyle;
        innerPart.sprite = innerBodyStyle;
        innerPart.color = innerBodyColor;

        eyewearRenderer.color = eyewearColor;
        eyewearRenderer.sprite = eyewearStyle;
        mustageRenderer.sprite = mustageStyle;

        if (sex == Sex.Male) {
            bodyRenderer.sprite = maleBody;
            faceRenderer.sprite = maleFace;
        } else {
            bodyRenderer.sprite = femaleBody;
            faceRenderer.sprite = femaleFace;
        }

        if ((age == Age.MiddleAge || age == Age.Old) && sex == Sex.Male) {

            mustageRenderer.enabled = true;
        } else {
            mustageRenderer.enabled = false;

        }

        hairRenderer.sprite = hairStyle;
        hatRenderer.color = hatColor;
        hatRenderer.sprite = hatStyle;

    }

    public void Repaint () {
        Repaint (true);
    }

    public void Repaint (bool validate) {

        ValidateParts ();
        ApplyAge ();
        Resize ();
        RefreshFace ();

        RefreshBody ();
        RefreshArms ();
        RefreshLegs ();

        if (CharacterEditor2D.quickEdit) {

            ApplyOverallShape ();
            return;

        }
        if (validate) ValidateAll ();

    }

    void ValidateAll () {
        breasts.Validate ();
        belly.Validate ();
        hip.SetThisDirty ();
        hip.Validate ();
        RefreshInnerPart ();
    }

    public void ApplyPerspective () {

        if (perspective != oldPerspective) {

            Sort (false);

#if UNITY_EDITOR

            if (Application.isPlaying == false) {

                EditorUtility.SetDirty (this);
                EditorUtility.SetDirty (head);
                EditorUtility.SetDirty (armUpper);
                EditorUtility.SetDirty (armUpper.buddy);
                EditorUtility.SetDirty (hip);
                EditorUtility.SetDirty (legUpper);
                EditorUtility.SetDirty (legUpper.buddy);
                EditorUtility.SetDirty (skirt);
                EditorUtility.SetDirty (coat);
            }
#endif

            hip.ValidatePerspective ();
            shoulder.ValidatePerspective ();
            belly.ValidatePerspective ();
            breasts.ValidatePerspective ();
            flank.ValidatePerspective ();
            RefreshInnerPart ();
            oldPerspective = perspective;
        }

    }

    public void StraightenBody () {

        hip.Straighten ();

    }

    public void SitManually () {

        legUpper.transform.localEulerAngles = new Vector3 (0, 0, 90f);
        legUpper.buddy.transform.localEulerAngles = new Vector3 (0, 0, 90f);

        legLower.transform.localEulerAngles = new Vector3 (0, 0, -90f);
        legLower.buddy.transform.localEulerAngles = new Vector3 (0, 0, -90f);
    }

    public void Relax () {

        armUpper.transform.localEulerAngles = new Vector3 (0, 0, -30f);
        armUpper.buddy.transform.localEulerAngles = new Vector3 (0, 0, 15f);
        armLower.transform.localEulerAngles = new Vector3 (0, 0, 15f);
        armLower.buddy.transform.localEulerAngles = new Vector3 (0, 0, 15f);

        legUpper.transform.localEulerAngles = new Vector3 (0, 0, -5f);
        legUpper.buddy.transform.localEulerAngles = new Vector3 (0, 0, 5f);

        legLower.transform.localEulerAngles = new Vector3 (0, 0, 0f);
        legLower.buddy.transform.localEulerAngles = new Vector3 (0, 0, 0f);

        foot.transform.localEulerAngles = new Vector3 (0, 0, 5.5f);
        foot.buddy.transform.localEulerAngles = new Vector3 (0, 0, -5.5f);

    }
    public void APose () {

        StraightenBody ();

        armUpper.transform.localEulerAngles = new Vector3 (0, 0, -45f);
        armUpper.buddy.transform.localEulerAngles = new Vector3 (0, 0, 45f);
        armLower.transform.localEulerAngles = new Vector3 (0, 0, 0f);
        armLower.buddy.transform.localEulerAngles = new Vector3 (0, 0, 0f);

    }

    public void TPose () {

        StraightenBody ();

        armUpper.transform.localEulerAngles = new Vector3 (0, 0, -90f);
        armUpper.buddy.transform.localEulerAngles = new Vector3 (0, 0, 90f);
        armLower.transform.localEulerAngles = new Vector3 (0, 0, 0f);
        armLower.buddy.transform.localEulerAngles = new Vector3 (0, 0, 0f);

    }

    public float GetPerspectiveFactor () {

        if (perspective > 90f) {
            return -((Mathf.Abs (perspective - 90f) / 180f) + 0.5f);

        } else {
            return (Mathf.Abs (perspective - 90f) / 180f) + 0.5f;

        }
    }
    public float GetPerspectiveFactorForDualParts () {

        if (perspective > 90f) {
            return -(Mathf.Abs (perspective - 90f) / 90f);

        } else {
            return (Mathf.Abs (perspective - 90f) / 90f);

        }
    }

    public static SCGVariants CreateVariants (CharacterBody2D body) {
        GameObject variants = new GameObject (body.name + " Variants");
        variants.AddComponent (typeof (SCGVariants));
        variants.GetComponent<SCGVariants> ().Load (body);
        return variants.GetComponent<SCGVariants> ();
    }

    public SCGVariants CreateVaraints () {
        return CreateVariants (this);
    }

    public void Recycle (bool changeAppearance) {
        if (generator) {
            generator.Recycle (this, changeAppearance);
        }
    }

}
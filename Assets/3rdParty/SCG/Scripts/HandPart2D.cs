using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class HandPart2D : BodyPart2D {

    [Header ("Hand Control")]

    [Range (-1, 1)]
    [SerializeField] int handSide = 1;

    [Header ("Fingers Control")]

    [Range (0, 1)]
    public int thumb = 1;
    [Range (0, 1)]
    public int index = 1;
    [Range (0, 1)]
    public int middle = 1;
    [Range (0, 1)]
    public int ring = 1;
    [Range (0, 1)]
    public int pinky = 1;

    [Header ("Fingers Setup")]
    [SerializeField] List<FingerPart2D> mainFingers = new List<FingerPart2D> ();
    [SerializeField] FingerPart2D thumbFinger;
    [SerializeField] float fingerOffsetY;
    [SerializeField] float thumbOffsetX = -1f;

    [Header ("Item Setup")]

    public SpriteRenderer itemRenderer;
    [SerializeField] protected Vector2 itemOffset;

    public Vector2 ItemOffset {
        get { return itemOffset; }
    }

    public void SetItemOffset (Vector2 itemOffset) {
        this.itemOffset = itemOffset;
        Validate ();
    }

    public override bool isHand () {
        return true;
    }

    [SerializeField] protected Vector2 itemSize = new Vector2 (10f, 10f);

    public Vector2 ItemSize {
        get { return itemSize; }
    }

    public void SetItemSize (Vector2 itemSize) {
        this.itemSize = itemSize;
        Validate ();
    }

    public override void SetSide (int i) {
        handSide = i;
        base.SetSide (i);
        ControlSelf ();
        Sort ();
    }

    void RefreshPoses () {
        thumbFinger.currentPose = thumb;
        mainFingers[0].currentPose = pinky;
        mainFingers[1].currentPose = ring;
        mainFingers[2].currentPose = middle;
        mainFingers[3].currentPose = index;

    }

    public void SetPose (int i, int pose) {
        if (i == 4) {

            thumbFinger.currentPose = pose;
        } else {
            mainFingers[i].currentPose = pose;

        }
        SetupFingers ();

    }

    public void SetupFingers () {
        ValidateInHand ();

        for (int i = 0; i <= mainFingers.Count - 1; i++) {
            mainFingers[i].Set (new Vector3 (((width / 8f) + (i * width / 4f)) - (width / 2f), fingerOffsetY, 0), width / 4f);
        }

        thumbFinger.Set (new Vector3 ((width / 2f) + (thumbOffsetX * width), -height * 0.1f, 0), width / 2.25f);
    }

    protected override void Validate (bool mirror, bool child, bool setup) {

        base.Validate (mirror, false, setup);

        RefreshPoses ();
        if (handSide == 0) handSide = 1;
        SetupFingers ();

    }

    void ValidateInHand () {
        if (itemRenderer) {
            itemRenderer.transform.localScale = Vector3.one;
            itemRenderer.transform.localPosition = new Vector3 (itemOffset.x, (-pivot.y * (height)) + itemOffset.y, itemRenderer.transform.localPosition.z);
            itemRenderer.size = new Vector2 (SCGCore.DebugSize (itemSize.x), SCGCore.DebugSize (itemSize.y));
        }
    }

    public override void Sort () {
        Sort (sortingLayerName, sortingOrder);
    }

    int BackHand (string layer, int order) {
        for (int i = 0; i <= mainFingers.Count - 1; i++) {
            mainFingers[i].Sort (layer, order + 2);
        }
        thumbFinger.Sort (layer, order + 2);
        if (parentPart.sleeve && parentPart.sleeve.myRenderer) ChangeOrder (parentPart.sleeve.myRenderer, order + 3);
        if (parentPart.wrapper) ChangeOrder (parentPart.wrapper, order + 4);

        return base.Sort (layer, order);
    }

    int ForeHand (string layer, int order) {
        for (int i = 0; i <= mainFingers.Count - 1; i++) {
            mainFingers[i].Sort (layer, order + 2);
        }
        thumbFinger.Sort (layer, order);
        if (parentPart.sleeve && parentPart.sleeve.myRenderer) ChangeOrder (parentPart.sleeve.myRenderer, order + 4);
        if (parentPart.wrapper) ChangeOrder (parentPart.wrapper, order + 5);

        return base.Sort (layer, order + 3);
    }

    public override int Sort (string layer, int order) {

        sortingOrder = order;
        sortingLayerName = layer;

        if (itemRenderer) {

            ChangeOrder (itemRenderer, order + 1);

        }

        if (handSide * (int) side == -1) {

            return ForeHand (layer, order);

        } else {

            return BackHand (layer, order);

        }

    }
    public override void Sync (BodyPart2D origin) {
        itemOffset = origin.GetComponent<HandPart2D> ().itemOffset;
        base.Sync (origin);

    }

    public void SetColor (Color glovesColor, Color skinColor, int glovesWrap) {
        color = glovesColor;
        base.RefreshColor ();
        if (glovesWrap == 2) {
            for (int i = 0; i <= mainFingers.Count - 1; i++) {
                mainFingers[i].SetColor (color, color);
            }
            thumbFinger.SetColor (color, color);
        } else if (glovesWrap == 1) {
            for (int i = 0; i <= mainFingers.Count - 1; i++) {
                mainFingers[i].SetColor (skinColor, glovesColor);
            }
            thumbFinger.SetColor (skinColor, glovesColor);
        } else {
            for (int i = 0; i <= mainFingers.Count - 1; i++) {
                mainFingers[i].SetColor (skinColor, skinColor);
            }
            thumbFinger.SetColor (skinColor, skinColor);
        }

    }

    public void Hold () {
        thumb = 0;
        index = 0;
        middle = 0;
        ring = 0;
        pinky = 0;
        Validate ();

    }

    public void Straight () {
        thumb = 1;
        index = 1;
        middle = 1;
        ring = 1;
        pinky = 1;
        Validate ();

    }

    public override void SetThisDirty () {

#if UNITY_EDITOR
        base.SetThisDirty ();
        EditorUtility.SetDirty (itemRenderer);
        thumbFinger.SetThisDirty ();

        for (int i = 0; i <= mainFingers.Count - 1; i++) {
            mainFingers[i].SetThisDirty ();
        }

#endif
    }

}
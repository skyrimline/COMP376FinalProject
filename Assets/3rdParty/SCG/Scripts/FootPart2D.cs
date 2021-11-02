using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class FootPart2D : BodyPart2D {

    public Transform toeTransform;
    public SpriteRenderer toeRenderer;

    public override bool isFoot () {
        return true;
    }

    public override void LimitMovement () {
        base.LimitMovement ();
        if (floorTransform && transform.position.y - (height * floorTransform.localScale.y) / 2f < floorTransform.transform.position.y) {
            transform.eulerAngles = Vector3.zero;

        }

        RefreshToe ();
    }

    protected override void RefreshRenderer () {
        base.RefreshRenderer ();
        RefreshToe ();
    }

    public override void Sync (BodyPart2D origin) {
        floorTransform = origin.GetComponent<FootPart2D> ().floorTransform;
        toeRenderer.sprite = origin.GetComponent<FootPart2D> ().toeRenderer.sprite;
        base.Sync (origin);
        RefreshToe ();

    }

    float StretchFactor () {
        return 1f + ((toeTransform.localEulerAngles.z / 45f) / 2.5f);
    }

    void RefreshToe () {
        if (toeTransform) {
            toeTransform.localPosition = new Vector3 (((width) / 2f) - (pivot.x * ((width) / 2f)), -height, toeTransform.transform.localPosition.z);
            toeTransform.localScale = Vector3.one;
            LimitRotation (0, 45, toeTransform);
        }
        if (toeRenderer) {
            toeRenderer.size = new Vector2 (SCGCore.DebugSize (width * StretchFactor ()), SCGCore.DebugSize (height));
            toeRenderer.transform.localPosition = new Vector3 ((width * StretchFactor ()) / 2f, height / 2f, toeRenderer.transform.localPosition.z);
            toeRenderer.transform.localScale = Vector3.one;
        }

    }

    public override void RefreshColor () {
        base.RefreshColor ();
        if (toeRenderer) toeRenderer.color = color;
    }

    public override int Sort (string layer, int order) {
        ChangeOrder (toeRenderer, order);
        return base.Sort (layer, order);
    }

    public override void SortByOrder (int order) {
        ChangeOrder (renderer, order);
        ChangeOrder (ball, order - 1);
        if (sleeve && sleeve.myRenderer) ChangeOrder (sleeve.myRenderer, order + 3);
    }

    public override void SetThisDirty () {
#if UNITY_EDITOR
        base.SetThisDirty ();
        EditorUtility.SetDirty (toeRenderer);

#endif
    }

}
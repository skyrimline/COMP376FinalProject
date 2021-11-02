using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class Sleeve2D : MonoBehaviour {

    [Header ("Settings")]
    [SerializeField] float height = 0.2f;

    public float Height {
        get { return height; }
    }

    public void SetHeight (float height) {
        this.height = height;
        Validate ();
    }

    [SerializeField] Color color = Color.white;

    public Color GetColor {
        get { return color; }
    }
    public void SetColor (Color c) {
        color = c;
        Validate ();
    }

    [Header ("Setup")]
    BodyPart2D arm;
    BodyPart2D hand;
    public SpriteRenderer myRenderer;

    public void Validate () {

        if (FetchParts ()) {
            if (myRenderer) {
                myRenderer.color = color;
                myRenderer.size = new Vector2 (SCGCore.DebugSize ((arm.Width * 0.6f)), SCGCore.DebugSize (height));
                myRenderer.transform.localPosition = new Vector3 (0, (-arm.Height) + myRenderer.size.y / 2f, myRenderer.transform.localPosition.z);
                myRenderer.transform.localEulerAngles = Vector3.zero;
            }
        }
    }

    public void Show (bool on) {
        myRenderer.color = color;
        myRenderer.enabled = on;
        SetThisDirty ();

    }

    void SetThisDirty () {
#if UNITY_EDITOR
        if (this) EditorUtility.SetDirty (this);
        EditorUtility.SetDirty (myRenderer);
#endif

    }

    bool FetchParts () {

        arm = GetComponent<BodyPart2D> ();
        hand = arm.childPart;

        return (arm && hand);

    }

}
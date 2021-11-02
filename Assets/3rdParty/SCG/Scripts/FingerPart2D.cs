using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class FingerPart2D : BodyPart2D {

    [Header ("Finger Setting")]
    [SerializeField] protected bool isThumb;
    [Range (0, 1f)]
    public float proportionToMiddleFinger = 1f;
    [Range (0, 1)]
    [HideInInspector] public int currentPose;

    [SerializeField] protected Vector2[] poses;

    float proportionToHand = 1f;

    protected override void ConnectToParent () {

        transform.localPosition = new Vector3 (originLocalPos.x, originLocalPos.y, transform.localPosition.z);
    }

    public void Set (Vector3 pos, float width) {
        if (parentPart) {
            if (!isThumb) {

                if (currentPose == 1) {
                    originLocalPos = pos + new Vector3 (0, parentPart.GetLocalHeight (), 0);

                } else {
                    originLocalPos = pos + new Vector3 (0, parentPart.GetLocalHeight () - (proportionToMiddleFinger) * 0.125f, 0);

                }

            } else {
                originLocalPos = pos;

            }

        }
        SetWidth (width);
        ConnectToParent ();
    }

    protected override void KeepInCenter () { }
    public override void ValidateParentSize () {
        if (parentPart) {
            if (!isThumb) width = parentPart.Height / 4f;
            if (currentPose <= poses.Length - 1) {
                height = parentPart.Height * proportionToMiddleFinger * proportionToHand * poses[currentPose].y;
                transform.localEulerAngles = new Vector3 (0, 0, poses[currentPose].x);
            }
        }

    }

    public override void RefreshColor () { }

    public void SetColor (Color color, Color glovesColor) {
        this.color = color;
        if (isThumb) {
            ball.color = glovesColor;
        }
        renderer.color = color;

    }

}
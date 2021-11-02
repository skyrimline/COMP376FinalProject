using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class BodyPartSocket2D : BodyPart2D {

    [Header ("Parts Setup")]
    public BodyPart2D right;
    public BodyPart2D left;
    [SerializeField] Vector2 offset;

    public Vector2 Offset {
        get { return offset; }
    }

    public void SetOffset (Vector2 offset) {

        this.offset = offset;
        Validate ();

    }

    public override bool isDirectlyConnected () {
        return false;
    }

    public void Load (BodyPartSocketData part) {
        SetOffset (new Vector2 (part.offset[0], part.offset[1]));
        base.Load (part);
    }

    public bool isSetupCompleted () {
        return right && left;
    }

    public override void ValidatePerspective () {
        FetchOffset ();
        ValidatePartsPerspective ();

        base.ValidatePerspective ();

    }

    public override void Validate () {

        FetchOffset ();

        ValidateParts ();
        base.Validate ();

    }

    void FetchOffset () {
        if ((!isDirectlyConnected () && type == Type.Arm && isSetupCompleted ())) {
            offset = new Vector2 (Mathf.Abs (GetWidth () / 2f) - ((right.GetWidth () / 4f)), offset.y);
        }
    }

    void ValidatePartsPerspective () {

        if (type == Type.Arm) {

            right.SetControlOffset (new Vector2 (offset.x * Mathf.Abs (body.GetPerspectiveFactorForDualParts ()), offset.y));
            left.SetControlOffset (new Vector2 (offset.x * Mathf.Abs (body.GetPerspectiveFactorForDualParts ()), offset.y));
        } else {
            right.SetControlOffset (new Vector2 (offset.x * body.GetPerspectiveFactorForDualParts () * body.GetPerspectiveFactorForDualParts (), offset.y));
            left.SetControlOffset (new Vector2 (offset.x * body.GetPerspectiveFactorForDualParts () * body.GetPerspectiveFactorForDualParts (), offset.y));

        }
        right.ValidatePerspective ();
        left.ValidatePerspective ();

    }

    void ValidateParts () {
        if (isSetupCompleted ()) {

            if (type == Type.Arm) {

                right.SetControlOffset (new Vector2 (offset.x * Mathf.Abs (body.GetPerspectiveFactorForDualParts ()), offset.y));
                left.SetControlOffset (new Vector2 (offset.x * Mathf.Abs (body.GetPerspectiveFactorForDualParts ()), offset.y));
            } else {
                right.SetControlOffset (new Vector2 (offset.x * body.GetPerspectiveFactorForDualParts () * body.GetPerspectiveFactorForDualParts (), offset.y));
                left.SetControlOffset (new Vector2 (offset.x * body.GetPerspectiveFactorForDualParts () * body.GetPerspectiveFactorForDualParts (), offset.y));

            }

            right.SetFloor (floorTransform);
            left.SetFloor (floorTransform);

            right.Validate ();

        }

    }
    protected override void FetchChild () {
        if (isSetupCompleted () && !childPart) {
            for (int i = 0; i <= transform.childCount - 1; i++) {
                BodyPart2D temp = transform.GetChild (i).GetComponent<BodyPart2D> ();
                if ((temp) && (right != temp && left != temp && childPart.type != Type.Fat)) {
                    childPart = temp;
                    break;
                }
            }
        }

    }

    public override void ReceiveCoat (Skirt2D sprite) {
        myCoat = sprite;
        right.myCoat = sprite;
        left.myCoat = sprite;
    }

    public override void ReceiveSkirt (Skirt2D sprite) {
        mySkirt = sprite;
        right.mySkirt = sprite;
        left.mySkirt = sprite;
    }
    public override void GetGroundLevel () {
        if (!parentPart) {
            groundLevel = GetChildHeights () - height;
        }
    }

    float GetLowestFootY () {

        float rightY = Mathf.Abs ((right.childPart.childPart.transform.position.y - (right.childPart.childPart.renderer.size.y * body.Scale * body.Size)) - transform.position.y);
        float leftY = Mathf.Abs ((left.childPart.childPart.transform.position.y - (left.childPart.childPart.renderer.size.y * body.Scale * body.Size)) - transform.position.y);

        if (leftY == rightY) {

            right.childPart.childPart.transform.eulerAngles = Vector3.zero - right.childPart.childPart.GetComponent<FootPart2D> ().toeTransform.transform.localEulerAngles;
            left.childPart.childPart.transform.eulerAngles = Vector3.zero - left.childPart.childPart.GetComponent<FootPart2D> ().toeTransform.transform.localEulerAngles;
            return leftY;
        } else if (leftY > rightY) {
            left.childPart.childPart.transform.eulerAngles = Vector3.zero - left.childPart.childPart.GetComponent<FootPart2D> ().toeTransform.transform.localEulerAngles;
            return leftY;

        } else {
            right.childPart.childPart.transform.eulerAngles = Vector3.zero - right.childPart.childPart.GetComponent<FootPart2D> ().toeTransform.transform.localEulerAngles;
            return rightY;
        }

    }

    public override void EditorUpdate () {

        if (SCGCore.isEditor () || type == BodyPart2D.Type.Leg) {

            ControlSelf ();
        } else {
            enabled = false;
        }

    }

    public override void LimitMovement () {
        SimulateHip ();
        base.LimitMovement ();

    }

    float GetKneeAngle () {

        float seatPosY = body.sittingLevelTransform.transform.localPosition.y + (right.Width / 2f);
        float rotatePosY = (right.Width / 2f) + right.childPart.Height + right.childPart.childPart.Height;

        if (SCGCore.isEditor ()) {
            Debug.DrawLine (floorTransform.transform.position, floorTransform.transform.position + new Vector3 (0, rotatePosY, 0), Color.magenta);
            Debug.DrawLine (floorTransform.transform.position, floorTransform.transform.position + new Vector3 (0, seatPosY, 0), Color.green);

        }
        if (seatPosY >= rotatePosY) return -140f;

        float result = -90f + (Mathf.Acos (seatPosY / rotatePosY) * Mathf.Rad2Deg);

        return DebugRotation (result);
    }

    private float MinKneeAngleForSitting () {

        float result = -140f + Mathf.Clamp (140f * Mathf.Pow (Mathf.Clamp (body.sittingLevelTransform.transform.position.y - (body.floor.transform.position.y + (right.Width / 2f) + right.childPart.Height + right.childPart.childPart.Height), -Mathf.Infinity, 0), 2), 0, 140f);
        return result;
    }

    public void SimulateHip () {

        if (body && type == Type.Leg) {

            if (body.Sitting && body.sittingLevelTransform) {
                transform.position = new Vector3 (transform.position.x, Mathf.Clamp (body.sittingLevelTransform.transform.position.y, floorTransform.transform.position.y, Mathf.Infinity), transform.position.z);

                if (body.simulateGround) {

                    float angle = GetKneeAngle ();
                    if (float.IsNaN (angle) == false) {
                        right.childPart.transform.localEulerAngles = new Vector3 (0, 0, angle);
                        left.childPart.transform.localEulerAngles = new Vector3 (0, 0, angle);
                    }

                }
            } else {
                if (body.simulateGround) {
                    transform.position = new Vector3 (transform.position.x, ActualGround () + jumpOffset, transform.position.z);
                }
            }

        }

    }

    protected override float ActualGround () {

        if (floorTransform == null && parentPart) floorTransform = parentPart.floorTransform;
        if (floorTransform) {

            if (body && body.simulateGround) {

                return ((((GetLowestFootY ()) * (floorTransform.localScale.y / Mathf.Clamp (body.Scale, 0.1f, Mathf.Infinity))) / Mathf.Clamp (body.Size, 0.1f, Mathf.Infinity)) + floorTransform.transform.position.y);

            } else {
                return ((groundLevel * floorTransform.localScale.y) + floorTransform.transform.position.y);

            }

        }
        return groundLevel;

    }

    protected override void RefreshCircle () {
        if (ball) {
            ball.transform.localScale = Vector3.one;
            ball.size = new Vector2 (SCGCore.DebugSize (GetBallWidth ()), SCGCore.DebugSize (ball.size.y));
            ball.transform.localPosition = new Vector3 (0, (ball.size.y / 2f) + (height / 2), ball.transform.localPosition.z);
        }
    }

    public override void ValidateParentSize () {

        FetchHeight ();

        base.ValidateParentSize ();

        if (isSetupCompleted ()) {

            right.ValidateParentSize ();
            left.ValidateParentSize ();

        }

    }

    void FetchHeight () {
        if (isSetupCompleted () && type == Type.Leg) {
            height = right.GetCircleWidth ();

        }
    }

    public void RefreshGround () {

        FetchHeight ();
        GetGroundLevel ();
        RefreshDisplay ();
        LimitMovement ();

    }

    public override float GetChildHeights () {

        if (isSetupCompleted ()) {
            float rightValue = height + right.GetChildHeights ();
            float leftValue = height + left.GetChildHeights ();
            return Mathf.Max (rightValue, leftValue);
        } else {
            return height;
        }

    }

    public override void Straighten (bool mirroring) {

        base.Straighten (mirroring);

        if (isSetupCompleted ()) {
            right.Straighten (mirroring);
            left.Straighten (mirroring);
        }

    }

}
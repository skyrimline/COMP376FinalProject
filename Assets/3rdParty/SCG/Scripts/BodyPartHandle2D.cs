using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class BodyPartHandle2D : SCGHandle
{

    public enum Type
    {
        Leg,
        Arm
    }
    public Type type;

    float limitRadius;

    public BodyPart2D motherPart;
    public BodyPart2D handlePart;

    [HideInInspector] public float upperLegAngle;

    [HideInInspector] public float lowerLegAngle;

    Vector3 originalPos;

    void Awake()
    {
        if (SCGCore.isEditor())
        {
            if (motherPart)
            {
                PlaceSelf();
                originalPos = transform.localPosition;
            }

        }
    }

    void PlaceSelf()
    {
#if UNITY_EDITOR

        if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<PerspectiveHandle2D>())
        {
            return;
        }


        if (Selection.activeGameObject == this.gameObject)
        {

            if (type == Type.Leg)
            {

                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, motherPart.transform.localPosition.x - GetOffset(), motherPart.transform.localPosition.x + GetOffset()), Mathf.Clamp(transform.localPosition.y, GetY(), motherPart.transform.localPosition.y), body.transform.localPosition.z);
            }
            else
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, motherPart.transform.position.x - GetOffset(), motherPart.transform.position.x + GetOffset()), Mathf.Clamp(transform.position.y, GetY(-1), GetY(1)), body.transform.position.z);

            }
        }

#endif

    }

    protected override void LimitMovement()
    {
        if (motherPart && handlePart)
        {
            if (SCGCore.isEditor())
            {
#if UNITY_EDITOR

                handlePart.handle = this;
                PlaceSelf();

                if (Selection.activeGameObject == gameObject || transform.localPosition != originalPos)
                {
                    UpdateParts();
                    originalPos = transform.localPosition;
                }

#endif

            }
            else
            {
                if (transform.localPosition != originalPos)
                {
                    UpdateParts();
                    originalPos = transform.localPosition;
                }
            }
        }

    }

    float GetY(int i)
    {

        float height = GetHeight();

        if (type == Type.Arm)
        {
            return (motherPart.transform.position.y + (i * (Mathf.Sqrt(Mathf.Pow(height, 2f) - Mathf.Pow(Mathf.Clamp(transform.position.x, motherPart.transform.position.x - GetOffset(), motherPart.transform.position.x + GetOffset()) - motherPart.transform.position.x, 2)))));

        }
        else
        {
            return (motherPart.transform.localPosition.y + (i * (Mathf.Sqrt(Mathf.Pow(height, 2f) - Mathf.Pow(Mathf.Clamp(transform.localPosition.x, motherPart.transform.localPosition.x - GetOffset(), motherPart.transform.localPosition.x + GetOffset()) - motherPart.transform.localPosition.x, 2)))));

        }

    }

    float GetY()
    {
        return GetY(-1);
    }

    public override float GetOffset()
    {
        return (motherPart.renderer.size.y + motherPart.childPart.renderer.size.y + motherPart.childPart.childPart.renderer.size.y) * motherPart.body.Size * motherPart.body.Scale;
    }

    public void UpdateParts()
    {

        upperLegAngle = GetUpperAngle();
        lowerLegAngle = GetLowerAngle();

        if (!float.IsNaN(GetUpperAngle()) && !float.IsNaN(GetLowerAngle()))
        {

            if (type == Type.Arm)
            {
                motherPart.transform.eulerAngles = new Vector3(0, 0, upperLegAngle + (lowerLegAngle / 2f));
                motherPart.ControlSelf();

                handlePart.parentPart.transform.localEulerAngles = new Vector3(0, 0, -lowerLegAngle);
            }
            else
            {
                motherPart.transform.localEulerAngles = new Vector3(0, 0, (upperLegAngle));
                motherPart.ControlSelf();

                handlePart.parentPart.transform.localEulerAngles = new Vector3(0, 0, lowerLegAngle);
            }
            handlePart.ControlSelf();

        }

    }

    float GetHeight()
    {

        // if (type == Type.Arm) {
        return GetOffset();
        // }

        if (motherPart.floorTransform)
        {

            return Mathf.Abs(motherPart.transform.position.y - motherPart.floorTransform.position.y);
        }
        else
        {

            return Mathf.Abs(motherPart.transform.position.y - handlePart.transform.position.y);
        }
    }

    float DebugRotation(float angle)
    {
        angle = (angle > 180) ? angle - 360 : angle;
        return angle;
    }

    float GetUpperAngle()
    {
        float height = GetHeight();

        float angle = (((transform.position.x - motherPart.transform.position.x) / height) * Mathf.Clamp((Mathf.Abs(motherPart.minRotation) + Mathf.Abs(motherPart.maxRotation)) * 0.5f, 0, 360f));
        float result = angle + (((height - Vector3.Distance(motherPart.transform.position, transform.position)) / height) * 90f) * 2f;

        if (type == Type.Leg)
        {
            angle = (((transform.localPosition.x - motherPart.transform.localPosition.x) / height) * Mathf.Clamp((Mathf.Abs(motherPart.minRotation) + Mathf.Abs(motherPart.maxRotation)) * 0.5f, 0, 360f));
            result = angle + (((height - Vector3.Distance(motherPart.transform.localPosition, transform.localPosition)) / height) * 90f) * 2f;
        }

        if (type == Type.Arm)
        {

            if (transform.position.y > motherPart.transform.position.y)
            {
                return DebugRotation(-180f - (angle / 4f));

            }
            else
            {
                return DebugRotation(angle / 4f);

            }
        }

        return DebugRotation(result);
    }

    float GetLowerAngle()
    {
        float height = GetHeight();
        return DebugRotation(-(((height - Vector3.Distance(motherPart.transform.position, transform.position)) / height) * 180f) * 2f);
    }

}
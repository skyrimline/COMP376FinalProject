using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class Skirt2D : MonoBehaviour
{

    public enum Type
    {
        Skirt,
        Coat
    }

    public float maxRadius = 45f;

    public void SetMaxRadius(float radius)
    {
        maxRadius = radius;
        Simulate();
    }

    public float minRadius = 20f;

    public void SetMinRadius(float radius)
    {
        minRadius = radius;
        Simulate();
    }

    public float extraHeight = 0.2f;

    public void SetExtraHeight(float radius)
    {
        extraHeight = radius;
        Simulate();
    }

    [HideInInspector] [SerializeField] Color color;

    float width;

    float height;
    Vector3 offset;
    float rightLegAngle, leftLegAngle;
    float rightLowerLegAngle, leftLowerLegAngle;
    float rightLowerLegGlobalAngle, leftLowerLegGlobalAngle;
    float radius;

    [Header("Setup")]
    public Type type;
    public Color shadow = new Color(0.1f, 0.1f, 0.1f, 0);

    public void SetShadowColor(Color color)
    {
        shadow = color;
        Simulate();
    }

    public Color GetShadowColor
    {
        get { return shadow; }
    }

    public BodyPartSocket2D hip;
    BodyPart2D rightLeg;
    BodyPart2D leftLeg;

    public List<SkirtPiece2D> pieces = new List<SkirtPiece2D>();

    [HideInInspector] public int sortingOrder;
    string sortingLayer;

    public void SetColor(Color color)
    {
        this.color = color;
        Simulate();
    }

    public Color GetColor
    {
        get { return color; }
    }

    bool isSetupCompleted()
    {

        if (hip)
        {
            rightLeg = hip.right.childPart;
            leftLeg = hip.left.childPart;

        }

        return rightLeg && leftLeg && hip;
    }

    void SerializeBodyParts()
    {

        if (type == Type.Skirt)
        {

            hip.ReceiveSkirt(this);
        }
        else
        {

            hip.ReceiveCoat(this);
        }
    }

    float GetPieceRotation(int i)
    {

        float tempRadius = ((-radius) + (((i)) * (2f * radius / (float)(pieces.Count - 1))));


        return tempRadius;

    }

    void FetchSize()
    {
        offset = new Vector3(0, hip.Height / 4f, 0);
        width = hip.GetWidth();

        if (type == Type.Skirt)
        {
            height = (rightLeg.parentPart.Height + (offset.y) + (extraHeight * rightLeg.Height) + 0.2f);

        }
        else
        {
            height = ((extraHeight * (offset.y + rightLeg.parentPart.Height + rightLeg.Height)));

        }

        transform.localPosition = ((offset * rightLeg.floorTransform.transform.localScale.y));

    }

    void DebugRotation(ref float angle)
    {

        if (angle > 180f)
        {
            angle -= 360f;
        }

    }

    void FetchAngle()
    {

        rightLegAngle = rightLeg.parentPart.transform.localEulerAngles.z;
        DebugRotation(ref rightLegAngle);

        rightLowerLegAngle = rightLeg.transform.localEulerAngles.z;
        DebugRotation(ref rightLowerLegAngle);

        rightLowerLegGlobalAngle = rightLeg.transform.eulerAngles.z;
        DebugRotation(ref rightLowerLegGlobalAngle);

        leftLegAngle = leftLeg.parentPart.transform.localEulerAngles.z;
        DebugRotation(ref leftLegAngle);

        leftLowerLegAngle = leftLeg.transform.localEulerAngles.z;
        DebugRotation(ref leftLowerLegAngle);

        leftLowerLegGlobalAngle = leftLeg.transform.eulerAngles.z;
        DebugRotation(ref leftLowerLegGlobalAngle);

    }

    void RefreshRadius()
    {

        float upperRadius = 0;
        float lowerRight = 0;
        float lowerLeft = 0;

        if (rightLegAngle * leftLegAngle < 0)
        {
            upperRadius = (Mathf.Abs(rightLegAngle) + Mathf.Abs(leftLegAngle)) / 2f;

        }
        else
        {
            upperRadius = (Mathf.Abs(rightLegAngle - leftLegAngle)) / 2f;

        }

        lowerRight = Mathf.Clamp((Mathf.Abs(rightLowerLegAngle) / 2f) - (Mathf.Clamp(rightLegAngle, 0, maxRadius / 2f)), 0, maxRadius);
        lowerLeft = Mathf.Clamp((Mathf.Abs(leftLowerLegAngle) / 2f) - (Mathf.Clamp(leftLegAngle, 0, maxRadius / 2f)), 0, maxRadius);

        if (type == Type.Skirt)
        {

            if (hip.myCoat && hip.myCoat.gameObject.activeSelf)
            {

                radius = Mathf.Clamp((upperRadius + lowerRight + lowerLeft) + (15f), minRadius, 90f);
            }
            else
            {

                radius = Mathf.Clamp((upperRadius + lowerRight + lowerLeft) + (15f), minRadius, 180f);
            }

        }
        else
        {

            float rawRadius = (Mathf.Abs(rightLegAngle) + Mathf.Abs(leftLegAngle) + lowerRight + lowerLeft) * (Mathf.Abs(((rightLegAngle) + rightLowerLegAngle) - (leftLegAngle + leftLowerLegAngle)) / (maxRadius * 0.5f));

            radius = Mathf.Clamp(rawRadius, minRadius, maxRadius * 1.5f);

        }

        if (type == Type.Skirt)
        {

#if UNITY_EDITOR
            UnityEditor.TransformUtils.SetInspectorRotation(transform, new Vector3(0, 0, Mathf.Clamp((rightLegAngle + leftLegAngle + rightLowerLegAngle / 2f + leftLowerLegAngle / 4f) / 2f, -maxRadius, maxRadius)));
#else
            transform.localEulerAngles = new Vector3 (0, 0, Mathf.Clamp ((rightLegAngle + leftLegAngle + rightLowerLegAngle / 2f + leftLowerLegAngle / 4f) / 2f, -maxRadius, maxRadius));

#endif

        }
        else
        {

#if UNITY_EDITOR
            UnityEditor.TransformUtils.SetInspectorRotation(transform, new Vector3(0, 0, Mathf.Clamp((rightLegAngle + leftLegAngle + rightLowerLegAngle / 2f + leftLowerLegAngle / 4f) / 2f, -maxRadius / 6f, 0)));

#else

            transform.localEulerAngles = new Vector3 (0, 0, Mathf.Clamp ((rightLegAngle + leftLegAngle + rightLowerLegAngle / 2f + leftLowerLegAngle / 4f) / 2f, -maxRadius / 6f, 0));
#endif

        }

    }

    public void ControlSelf()
    {

        FetchSize();
        FetchAngle();
        RefreshRadius();

    }

    void Update()
    {
        Simulate();
    }

    void Simulate()
    {
        if (isSetupCompleted() && SCGAnimationTool.simulateSkirt)
        {

            SerializeBodyParts();
            ControlSelf();
            ControlPieces();

        }
    }

    void ControlPieces()
    {
        for (int i = 0; i <= pieces.Count - 1; i++)
        {

            if (type == Type.Skirt)
            {

#if UNITY_EDITOR
                UnityEditor.TransformUtils.SetInspectorRotation(pieces[i].transform, new Vector3(0, 0, GetPieceRotation(i)));
#else
                pieces[i].transform.localEulerAngles = new Vector3 (0, 0, GetPieceRotation (i));
#endif

            }
            else
            {

#if UNITY_EDITOR
                UnityEditor.TransformUtils.SetInspectorRotation(pieces[i].transform, new Vector3(0, 0, GetPieceRotation(i) * 0.5f));
#else
                pieces[i].transform.localEulerAngles = new Vector3 (0, 0, GetPieceRotation (i) * 0.5f);
#endif

            }

            if (pieces[i])
            {

                if (type == Type.Coat)
                {

                    if ((i == 0 || i == pieces.Count - 1) && (hip.body && hip.body.Perspective <= 90f))
                    {

                        if (i == 0)
                        {

                            pieces[i].Set(
                                hip.Width * (0.5f / Mathf.Clamp(hip.body.GetPerspectiveFactor(), 0.75f, 1f)), height + (hip.Height / 2f),
                                color,
                                GetPieceRotation(i) * (hip.GetWidth() * 0.4f / radius) * hip.body.GetPerspectiveFactor(),
                                offset.y);
                        }
                        else
                        {

                            pieces[i].Set(
                                width * 0.5f,
                                height, color - shadow,
                                GetPieceRotation(i) * (hip.GetWidth() * 0.35f / radius),
                                0);
                        }

                    }
                    else
                    {

                        if (hip.body && hip.body.Perspective <= 90f)
                        {

                            pieces[i].Set(width, height, color - shadow, GetPieceRotation(i) * (hip.GetWidth() * 0.2f / radius), 0.1f);
                        }
                        else
                        {

                            pieces[i].Set(width, height, color, -GetPieceRotation(i) * (hip.GetWidth() * 0.2f / radius), 0.1f);
                        }

                    }

                }
                else
                {

                    if (hip && hip.myCoat && hip.myCoat.gameObject.activeSelf)
                    {
                        float originalWidth = width / Mathf.Abs(hip.body.GetPerspectiveFactor());
                        float tempWidth = DebugCoat(originalWidth, i);
                        pieces[i].Set(tempWidth, height, color, 0, 0);

                    }
                    else
                    {
                        pieces[i].Set((width / Mathf.Abs(hip.body.GetPerspectiveFactor())), height, color, 0, 0);

                    }

                }

            }

        }
    }

    float DebugCoat(float value, int i)
    {
        if (i < Mathf.RoundToInt(2f / Mathf.Abs(hip.body.GetPerspectiveFactor())) && hip && hip.myCoat && hip.myCoat.gameObject.activeSelf && pieces[i].transform.eulerAngles.z < hip.myCoat.pieces[0].transform.eulerAngles.z)
        {
            return 0;
        }
        return value;
    }

    public void Sort(string layer, int order)
    {
        sortingLayer = layer;
        sortingOrder = order;

        for (int i = 0; i <= pieces.Count - 1; i++)
        {

            if (pieces[i]) pieces[i].Sort(sortingLayer, sortingOrder);
        }

        if (isSetupCompleted() && type == Type.Skirt && gameObject.activeSelf)
        {
            rightLeg.Sort(layer, sortingOrder - 2);
            leftLeg.Sort(layer, sortingOrder - 2);
        }
    }

    public void Sort()
    {

        Sort(sortingLayer, sortingOrder);

    }

}
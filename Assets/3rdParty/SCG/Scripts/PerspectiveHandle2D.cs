using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PerspectiveHandle2D : SCGHandle {

    public override void ControlSelf () {

        base.ControlSelf ();

        if (body) body.Perspective = GetPerspectiveValue ();

    }

    float GetPerspectiveValue () {

        return Mathf.Floor ((((transform.localPosition.x) / (GetOffset () * 2f)) + 0.5f) * 180f);
    }

    public override float GetValue () {
        return GetPerspectiveValue ();
    }

    public override void SetValue (float value) {
        SetValue (value, false);
    }

    public void SetValue (float value, bool record) {

        if (body) {

#if UNITY_EDITOR
            if (record) Undo.RecordObject (this, "Perspective Change");
#endif

            transform.position = new Vector3 ((body.transform.position.x - GetOffset ()) + ((value / 180f) * GetOffset () * 2f), transform.position.y, transform.position.x);

#if UNITY_EDITOR
            EditorUtility.SetDirty (this);
#endif

            ControlSelf ();
        }
    }

    public void Invert () {
        float currentAngle = GetPerspectiveValue ();
        if (currentAngle > 90f) {

            SetValue (90f - (currentAngle - 90f));
        } else {
            SetValue (90f + (90f - currentAngle));

        }
    }

}
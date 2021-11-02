using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class SCGHandle : MonoBehaviour {

    public CharacterBody2D body;

    protected float handleOffset = 1;
    void Start () {

    }

    public void Update () {
        ControlSelf ();
    }

    public virtual void ControlSelf () {
        if (SCGCore.isEditor ()) {
            if (body) {
                LimitMovement ();
            }
            KeepIntact ();
        }
    }

    protected virtual void LimitMovement () {
        transform.position = new Vector3 (Mathf.Clamp (transform.position.x, body.transform.position.x - GetOffset (), body.transform.position.x + GetOffset ()), body.transform.position.y, body.transform.position.z);

    }

    void KeepIntact () {

        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;

    }

    public virtual float GetValue () {
        return 0;
    }

    public virtual float GetOffset () {
        if (!body) return 0;
        return (handleOffset * body.Size * body.Scale) + handleOffset;
    }

    public virtual void SetValue (float value) {

    }

}
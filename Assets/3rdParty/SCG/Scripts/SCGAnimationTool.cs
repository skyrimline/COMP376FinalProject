using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SCGAnimationTool : MonoBehaviour {
    public CharacterBody2D body;

    public static bool simulateSkirt = true;

    void Update () {
        KeepIntact ();

    }

    void KeepIntact () {

#if UNITY_EDITOR

        if (SCGCore.isEditor ()) {

            if (Selection.activeGameObject == this.gameObject) {
                Tools.current = Tool.None;
            }
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = Vector3.one;
        }

#endif
    }

}
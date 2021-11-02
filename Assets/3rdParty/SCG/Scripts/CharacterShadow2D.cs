using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
[ExecuteInEditMode]
public class CharacterShadow2D : MonoBehaviour {
    SpriteRenderer renderer;
    public CharacterBody2D body;
    public float height = 1f;
    public float extraWidth = 0f;

    void OnValidate () {

        renderer = GetComponent<SpriteRenderer> ();
        renderer.drawMode = SpriteDrawMode.Sliced;
    }
    void LateUpdate () {
        if (body && renderer) {
            Resize ();
        }
    }

    float GetFarLeft () {
        float pos = Mathf.Min (body.hand.transform.position.x, body.hand.buddy.transform.position.x, body.foot.transform.position.x, body.foot.buddy.transform.position.x, body.armLower.transform.position.x, body.armLower.buddy.transform.position.x, body.legLower.transform.position.x, body.legLower.buddy.transform.position.x, body.head.transform.position.x - (body.head.renderer.bounds.size.y / 2f), body.head.transform.position.x + (body.head.renderer.bounds.size.y / 2f));
        return Mathf.Abs (pos - body.flank.transform.position.x);
    }
    float GetFarRight () {
        float pos = Mathf.Max (body.hand.transform.position.x, body.hand.buddy.transform.position.x, body.foot.transform.position.x, body.foot.buddy.transform.position.x, body.armLower.transform.position.x, body.armLower.buddy.transform.position.x, body.legLower.transform.position.x, body.legLower.buddy.transform.position.x, body.head.transform.position.x - (body.head.renderer.bounds.size.y / 2f), body.head.transform.position.x + (body.head.renderer.bounds.size.y / 2f));
        return Mathf.Abs (pos - body.flank.transform.position.x);
    }

    void Resize () {
        float sizeFactor = body.Size * body.Scale;
        float farLeftOffset = GetFarLeft ();
        float farRightOffset = GetFarRight ();
        renderer.size = new Vector2 (SCGCore.DebugSize (Mathf.Clamp ((body.hip.Width) + farLeftOffset + farRightOffset + extraWidth - body.floor.transform.localPosition.y, 0, Mathf.Infinity) * sizeFactor), SCGCore.DebugSize (Mathf.Clamp ((height * sizeFactor) - (body.floor.transform.localPosition.y / 2f), 0, height)));
        renderer.transform.localPosition = new Vector3 (body.flank.transform.localPosition.x + ((farRightOffset - farLeftOffset) / 2f), 0, 0);
    }
}
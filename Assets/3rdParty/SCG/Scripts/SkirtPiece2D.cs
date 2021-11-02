using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]

public class SkirtPiece2D : MonoBehaviour {
    public SpriteRenderer myRenderer;

    string sortingLayer;

    public void Set (float width, float height, Color color, float x, float y) {

        transform.localScale = Vector3.one;
        if (myRenderer) {
            myRenderer.transform.localScale = Vector3.one;
            myRenderer.color = color;

            myRenderer.size = new Vector2 (SCGCore.DebugSize (width), SCGCore.DebugSize (height));

            myRenderer.transform.localPosition = new Vector3 (x, -myRenderer.size.y / 2f + y, myRenderer.transform.localPosition.z);

        }
    }

    public void Sort (string layer, int order) {

        sortingLayer = layer;
        if (myRenderer) {
            ChangeOrder (myRenderer, order);
        }

    }

    protected void ChangeOrder (SpriteRenderer target, int i) {
        if (target) {
            target.sortingLayerName = sortingLayer;
            target.sortingOrder = i;
        }

    }

}
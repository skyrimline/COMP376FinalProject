using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BodyPartData {

    public float width;
    public float height;
    public float minWidth;
    public float maxWidth;
    public float minHeight;
    public float maxHeight;

    public float minRotation;
    public float maxRotation;

    public string rendererSprite;
    public string ballSprite;

    public BodyPartData (BodyPart2D part) {
        width = part.Width;
        height = part.Height;

        Vector2 widthScale = part.widthScale ();
        Vector2 heightScale = part.heightScale ();
        Vector2 rotation = part.rotationLimit ();

        minWidth = widthScale.x;
        minHeight = heightScale.x;
        maxWidth = widthScale.y;
        maxHeight = heightScale.y;
        minRotation = rotation.x;
        maxRotation = rotation.y;

        if (part.renderer) rendererSprite = SCGSaveSystem.GetSpriteName (part.renderer.sprite);
        if (part.ball) ballSprite = SCGSaveSystem.GetSpriteName (part.ball.sprite);

    }

}

[System.Serializable]
public class BodyPartSocketData : BodyPartData {
    public float[] offset;
    public BodyPartSocketData (BodyPartSocket2D part) : base (part) {
        offset = new float[2];
        offset[0] = part.Offset.x;
        offset[1] = part.Offset.y;
    }

}
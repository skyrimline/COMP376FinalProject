using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SCGColorDatabase {
    public float[] skinColors;
    public float[] hairColors;
    public float[] dyeColors;
    public float[] leatherColors;

    public SCGColorDatabase (Color[] skin, Color[] hair, Color[] dye, Color[] leather) {
        skinColors = new float[skin.Length * 4];
        int myIndex = 0;

        for (int i = 0; i <= skin.Length - 1; i++) {
            skinColors[myIndex] = skin[i].r;
            skinColors[myIndex + 1] = skin[i].g;
            skinColors[myIndex + 2] = skin[i].b;
            skinColors[myIndex + 3] = skin[i].a;
            myIndex += 4;

        }

        hairColors = new float[hair.Length * 4];
        myIndex = 0;

        for (int i = 0; i <= hair.Length - 1; i++) {
            hairColors[myIndex] = hair[i].r;
            hairColors[myIndex + 1] = hair[i].g;
            hairColors[myIndex + 2] = hair[i].b;
            hairColors[myIndex + 3] = hair[i].a;
            myIndex += 4;

        }

        dyeColors = new float[dye.Length * 4];
        myIndex = 0;

        for (int i = 0; i <= dye.Length - 1; i++) {
            dyeColors[myIndex] = dye[i].r;
            dyeColors[myIndex + 1] = dye[i].g;
            dyeColors[myIndex + 2] = dye[i].b;
            dyeColors[myIndex + 3] = dye[i].a;
            myIndex += 4;

        }

        leatherColors = new float[leather.Length * 4];
        myIndex = 0;

        for (int i = 0; i <= leather.Length - 1; i++) {
            leatherColors[myIndex] = leather[i].r;
            leatherColors[myIndex + 1] = leather[i].g;
            leatherColors[myIndex + 2] = leather[i].b;
            leatherColors[myIndex + 3] = leather[i].a;
            myIndex += 4;

        }

    }

    public List<Color> LoadColors (float[] myVar) {

        List<Color> result = new List<Color> ();
        int myIndex = 0;

        for (int i = 0; i <= myVar.Length - 1; i += 4) {

            result.Add (new Color (myVar[i], myVar[i + 1], myVar[i + 2], myVar[i + 3]));
            myIndex++;
        }
        return result;
    }

}
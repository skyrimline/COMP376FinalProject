using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (CharacterBody2D))]
[System.Serializable]
public class CharacterEditor2D : MonoBehaviour {

    public CharacterBody2D target;
    public string profileName = "None";

    public static bool quickEdit;

}
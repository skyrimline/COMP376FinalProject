using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToePart2D : MonoBehaviour {

    public FootPart2D footPart;
    public void StraightenSelf () {

        transform.localEulerAngles = Vector3.zero;

    }
}
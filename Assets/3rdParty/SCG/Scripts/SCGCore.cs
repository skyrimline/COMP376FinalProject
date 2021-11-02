using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCGCore {
    public static bool isEditor () {
        return Application.isEditor && !Application.isPlaying;
    }

    public static float DebugSize (float value) {
        if (value == 0) return 0.00001f;
        return value;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (CharacterGenerator2D))]

public class CharacterGenerator2DEditor : SCGEditor {

    CharacterGenerator2D generator;

    void Prepare () {
        generator = (CharacterGenerator2D) target;
    }

    public override void OnInspectorGUI () {
        base.OnInspectorGUI ();

        Prepare ();

        if (Button ("Generate")) {
            generator.Generate ();
        }

        Line ();

        Title ("Auto Spawning");

        EditorGUI.BeginChangeCheck ();
        bool autoSpawn = EditorGUILayout.Toggle ("Auto Spawn Enabled", generator.AutoSpawnEnabled);

        if (EditorGUI.EndChangeCheck ()) {
            generator.AutoSpawnEnabled = autoSpawn;
        }
        if (generator.AutoSpawnEnabled) {
            EditorGUI.indentLevel++;

            DrawMinMax ("Delay (Sec)", ref generator.minWaitForSeconds, ref generator.maxWaitForSeconds, 0.1f, 60f);

            generator.autoSpawnPositon = EditorGUILayout.Vector3Field ("Postion", generator.autoSpawnPositon);
            EditorGUI.indentLevel--;
        }

        Line ();

        Title ("Spawn Pooling");

        generator.poolingEnabled = EditorGUILayout.Toggle ("Auto Spawn Pooling", generator.poolingEnabled);
        generator.allowPoolExpansion = EditorGUILayout.Toggle ("Allow Pool Expansion", generator.allowPoolExpansion);

        if (generator.poolingEnabled) {
            EditorGUI.indentLevel++;

            generator.poolSize = EditorGUILayout.IntField ("Pool Size (Characters)", generator.poolSize);
            generator.prefixName = EditorGUILayout.TextField ("Prefix Name", generator.prefixName);

            EditorGUILayout.BeginHorizontal ();
            if (Button ("Create Pool")) {
                generator.CreatePool ();
            }

            if (Button ("Delete Pool")) {
                generator.ClearPool ();
            }
            EditorGUILayout.EndHorizontal ();

            ShowCharacterList ("Pool", ref generator.pool, generator);

            EditorGUI.indentLevel--;

        }

        Line ();

        if (GUI.changed) {
            Validate ();
        }

    }

    void Validate () {
        EditorUtility.SetDirty (target);
    }
}
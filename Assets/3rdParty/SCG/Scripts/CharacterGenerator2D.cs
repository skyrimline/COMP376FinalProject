using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CharacterGenerator2D : MonoBehaviour {

    [Header ("Setup")]
    public CharacterBody2D bodyBlueprint;

    public List<SCGVariants> variants = new List<SCGVariants> ();

    [HideInInspector][SerializeField] bool autoSpawnEnabled;
    [HideInInspector] public bool poolingEnabled = true;
    [HideInInspector] public bool allowPoolExpansion = true;
    [HideInInspector] public int poolSize;
    [HideInInspector] public string prefixName = "Character";

    [HideInInspector] public List<CharacterBody2D> pool = new List<CharacterBody2D> ();

    float spawnDelay;

    int poolIndex;

    public bool AutoSpawnEnabled {
        get { return autoSpawnEnabled; }
        set {
            autoSpawnEnabled = value;

        }
    }

    [HideInInspector] public float minWaitForSeconds = 1f;
    [HideInInspector] public float maxWaitForSeconds = 1f;

    [HideInInspector] public Vector3 autoSpawnPositon;

    [HideInInspector][SerializeField] int poolId = 0;

    public CharacterBody2D Generate () {
        return Generate (Vector3.zero);
    }

    public CharacterBody2D Generate (Vector3 position) {
        if (variants.Count > 0 && bodyBlueprint) {
            return Generate (variants[UnityEngine.Random.Range (0, variants.Count)], bodyBlueprint, position);
        }
        return null;
    }

    public CharacterBody2D Generate (CharacterBody2D target) {
        if (variants.Count > 0 && target) {
            return target.Generate (variants[UnityEngine.Random.Range (0, variants.Count)]);
        }
        return null;
    }

    public static CharacterBody2D Generate (SCGVariants blueprint, CharacterBody2D bodyBlueprint) {
        return Generate (blueprint, bodyBlueprint, Vector3.zero);
    }

    public static CharacterBody2D Generate (SCGVariants blueprint, CharacterBody2D bodyBlueprint, Vector3 position) {
        GameObject obj = Instantiate (bodyBlueprint.gameObject, null, true);
        obj.GetComponent<CharacterBody2D> ().Generate (blueprint);
        obj.transform.position = position;
        return obj.GetComponent<CharacterBody2D> ();
    }

    void Update () {
        if (SCGCore.isEditor () == false && autoSpawnEnabled) {
            if (spawnDelay > 0) {

                spawnDelay -= Time.deltaTime;

            } else {
                AutoSpawn ();
                spawnDelay = UnityEngine.Random.Range (minWaitForSeconds, maxWaitForSeconds);
            }
        }
    }

    void AutoSpawn () {
        if (poolingEnabled) {
            SpawnFromPool ();
        } else {
            Generate (autoSpawnPositon);
        }
    }

    void SpawnFromPool () {

        for (int i = 0; i <= pool.Count - 1; i++) {
            if (pool[i] && !pool[i].gameObject.activeSelf) {
                pool[i].transform.position = autoSpawnPositon;
                pool[i].gameObject.SetActive (true);
                return;
            }
        }

        if (allowPoolExpansion) {

            AddToPool ();
            pool.Last ().transform.position = autoSpawnPositon;
            pool.Last ().gameObject.SetActive (true);
        }

    }

    public void ClearPool () {
        for (int i = 0; i <= pool.Count - 1; i++) {
            DestroyImmediate (pool[i].gameObject);
        }

        poolId = 0;
        pool.Clear ();
    }

    public void AddToPool () {

        CharacterBody2D tempCharacter = Generate ();
        tempCharacter.generator = this;
        tempCharacter.gameObject.name = prefixName + " " + poolId;
        tempCharacter.transform.SetParent (transform);
        tempCharacter.gameObject.SetActive (false);
        pool.Add (tempCharacter);
        poolId++;
    }

    public void CreatePool () {
        for (int i = 1; i <= poolSize; i++) {
            AddToPool ();
        }
    }

    public void Recycle (CharacterBody2D target, bool changeAppearance) {

        target.gameObject.SetActive (false);
        if (changeAppearance && target) {
            Generate (target);
        }
    }

}
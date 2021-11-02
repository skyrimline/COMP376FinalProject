using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class InnerPartContainer : MonoBehaviour {
    public List<SpriteRenderer> childs = new List<SpriteRenderer> ();
    SpriteRenderer myRenderer;

    CharacterBody2D body;

    public void Clean () {
        for (int i = 0; i <= childs.Count - 1; i++) {
            if (childs[i])
                DestroyImmediate (childs[i].gameObject);
        }
        childs.Clear ();
    }
    public void Add (InnerShirtEntry entry) {

        AddLayer ();
        SetSprite (childs.Count - 1, entry.sprite);

        SetColor (childs.Count - 1, CharacterBody2D.GetRandomColor (entry.colors, entry.includeDyeColors, SCGSaveSystem.dyeColors));

    }

    public void Load (List<InnerShirtLayerData> data) {
        Clean ();

        for (int i = 0; i <= data.Count - 1; i++) {
            if (i > childs.Count - 1) {
                AddLayer ();
            }
            SetSprite (i, SCGSaveSystem.LoadSprite (data[i].sprite));
            SetColor (i, CharacterBody2D.DecryptColor (data[i].color));
        }
    }

    void Awake () {
        myRenderer = GetComponent<SpriteRenderer> ();
    }

    public void SetSprite (int index, Sprite sprite) {
        if (index <= childs.Count - 1 && childs[index] != null) {
            childs[index].sprite = sprite;
        }
    }
    public void SetColor (int index, Color color) {
        if (index <= childs.Count - 1 && childs[index] != null) {
            childs[index].color = color;
        }
    }

    public void Resize () {
        for (int i = 0; i <= childs.Count - 1; i++) {
            if (childs[i] != null) {
                KeepIntact (childs[i]);
            }
        }
    }
    public int Sort (int sortIndex, string layer) {
        for (int i = 0; i <= childs.Count - 1; i++) {
            if (childs[i] != null) {
                sortIndex++;
                Sort (childs[i], sortIndex, layer);
            }
        }
        return sortIndex;
    }

    void KeepIntact (SpriteRenderer renderer) {
        renderer.transform.localEulerAngles = Vector3.zero;
        renderer.transform.localPosition = Vector3.zero;
        renderer.transform.localScale = Vector3.one;
        renderer.drawMode = SpriteDrawMode.Tiled;
        if (myRenderer) {
            renderer.size = myRenderer.size;
            renderer.enabled = myRenderer.enabled;

        } else {
            renderer.enabled = false;

        }
    }

    void Sort (SpriteRenderer targetRenderer, int i, string layer) {
        targetRenderer.sortingLayerName = layer;
        targetRenderer.sortingOrder = i;
    }

    public void AddLayer () {
        SpriteRenderer tempRenderer = new GameObject ().AddComponent<SpriteRenderer> ();
        tempRenderer.transform.SetParent (transform);
        tempRenderer.name = "InnerPartLayer" + (childs.Count + 1);
        childs.Add (tempRenderer);
        KeepIntact (tempRenderer);
        if (body) body.Sort (true);
    }
    public void RemoveLayer (int index) {
        DestroyImmediate (childs[index].gameObject);
        childs.RemoveAt (index);
        if (body) body.Sort (true);
    }
}
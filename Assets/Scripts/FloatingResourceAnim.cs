using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingResourceAnim : MonoBehaviour
{
    private float timer = 3.0f; 

    private SpriteRenderer spr;

    public Text numbertext;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position += new Vector3(0, 0.001f, 0);
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, spr.color.a - 0.0005f);
            numbertext.transform.position += new Vector3(0, 0.001f, 0);

            
            return;
        }

        Destroy(gameObject);
    }
}

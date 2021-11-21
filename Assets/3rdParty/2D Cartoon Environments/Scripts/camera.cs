using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    private Transform myTransform;
    void Start()
    {
        myTransform = GetComponent <Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myTransform.Translate(new Vector3(0, 0.05f, 0));

        }
        if (Input.GetKey(KeyCode.A))
        {
            myTransform.Translate(new Vector3(-0.075f, 0, 0));

        }
        if (Input.GetKey(KeyCode.S))
        {
            myTransform.Translate(new Vector3(0, -0.05f, 0));

        }
        if (Input.GetKey(KeyCode.D))
        {
            myTransform.Translate(new Vector3(0.075f, 0, 0));

        }
        if (Input.GetKey(KeyCode.Z))
        {
            myTransform.Translate(new Vector3(0, 0, 0.04f));

        }
        if (Input.GetKey(KeyCode.X))
        {
            myTransform.Translate(new Vector3(0, 0, -0.04f));

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExampleWalkAI : MonoBehaviour {

    CharacterBody2D body;
    public float speed = 1f;

    public Vector3 direction = new Vector3 (-1f, 0, 0);
    public int side = -1;
    void OnEnable () {
        body = GetComponent<CharacterBody2D> ();
        body.SetTurn (side);
        body.simulatePerspective = false;
        transform.GetChild (0).GetComponent<Animator> ().SetTrigger ("Walk");
    }

    void Update () {
        if (transform.position.x < -20f) body.Recycle (true);
        transform.position = Vector3.MoveTowards (transform.position, transform.position + direction, Time.deltaTime * speed);
    }

}
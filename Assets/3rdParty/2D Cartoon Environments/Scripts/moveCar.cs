using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCar : MonoBehaviour
{
    
    public Transform target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}

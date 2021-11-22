using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Allocation: MonoBehaviour
{
    [SerializeField] private GameObject Food_Allocation_UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Food_Allocation_UI.SetActive(true);
        }
        else
            Food_Allocation_UI.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverInfo : MonoBehaviour
{

    [SerializeField] private GameObject extraInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        extraInfo.gameObject.SetActive(true);
        
    }

    private void OnMouseExit()
    {
        extraInfo.gameObject.SetActive(false);

    }
}

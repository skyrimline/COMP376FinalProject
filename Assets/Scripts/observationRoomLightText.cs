using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class observationRoomLightText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI lightText;

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
        lightText.gameObject.SetActive(true);
        
    }

    private void OnMouseExit()
    {
        lightText.gameObject.SetActive(false);

    }
}

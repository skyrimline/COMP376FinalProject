using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject extraInfo;

    // Start is called before the first frame update
    void Start()
    {
        extraInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        extraInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        extraInfo.SetActive(false);
    }
}

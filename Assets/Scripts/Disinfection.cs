using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Disinfection : MonoBehaviour, IPointerDownHandler
{

    public static bool disinfectionActive = false;

    private Image icon;

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!disinfectionActive)
            disinfectionActive = true;
        else
            disinfectionActive = false;
    }

    private void ChangeColor()
    {
        if (disinfectionActive)
        {
            icon.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            icon.color = new Color(1, 1, 1, 1f);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Execution : MonoBehaviour, IPointerDownHandler
{

    public static bool executionActive = false;

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
        if (!executionActive)
            executionActive = true;
        else
            executionActive = false;
    }

    private void ChangeColor()
    {
        if (executionActive)
        {
            icon.color = new Color(1,1,1, 0.5f);
        }
        else
        {
            icon.color = new Color(1, 1, 1, 1f);
        }
    }
}

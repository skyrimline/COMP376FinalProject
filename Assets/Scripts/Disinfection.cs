using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Disinfection : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static bool disinfectionActive = false;

    //setting cursor icon
    [SerializeField] private Texture2D default_cursor;
    [SerializeField] private Texture2D disinfectant_cursor;
    private Vector2 new_cursorHotspot;
    private Image icon;
    private Image coin;
    private Text text1;
    private Text text2;

    private bool isMouseOver;

    // Start is called before the first frame update
    void Start()
    {
        new_cursorHotspot = new Vector2(7, 5);
        icon = GetComponent<Image>();
        text1 = transform.GetChild(0).gameObject.GetComponent<Text>();
        text2 = transform.GetChild(1).gameObject.GetComponent<Text>();
        coin = transform.GetChild(2).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();

        //click the right mouse button to unset disinfection button
        if (Input.GetMouseButton(1))
        {
            unsetDisinfectionActive();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!disinfectionActive)
            setDisinfectionActive();
        else
            unsetDisinfectionActive();
    }

    private void ChangeColor()
    {
        if (disinfectionActive)
        {
            if (isMouseOver)
            {
                icon.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
                text1.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
                text2.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
                coin.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
            }
            else
            {
                icon.color = new Color(0.561f, 0.769f, 0.643f, 1f);
                text1.color = new Color(0.561f, 0.769f, 0.643f, 1f);
                text2.color = new Color(0.561f, 0.769f, 0.643f, 1f);
                coin.color = new Color(0.561f, 0.769f, 0.643f, 1f);
            }

        }
        else
        {
            if (isMouseOver)
            {
                icon.color = new Color(1, 1, 1, 0.5f);
                text1.color = new Color(1, 1, 1, 0.5f);
                text2.color = new Color(1, 1, 1, 0.5f);
                coin.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                icon.color = new Color(1, 1, 1, 1f);
                text1.color = new Color(1, 1, 1, 1f);
                text2.color = new Color(1, 1, 1, 1f);
                coin.color = new Color(1, 1, 1, 1f);
            }
        }
    }

    private void setDisinfectionActive()
    {
        disinfectionActive = true;
        Execution.executionActive = false;
        Cursor.SetCursor(disinfectant_cursor, new_cursorHotspot, CursorMode.Auto);
    }

    private void unsetDisinfectionActive()
    {
        disinfectionActive = false;
        Cursor.SetCursor(default_cursor, new_cursorHotspot, CursorMode.Auto);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}
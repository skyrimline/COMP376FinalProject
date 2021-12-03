using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Execution : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public static bool executionActive = false;

    //setting cursor icon
    [SerializeField] private Texture2D default_cursor;
    [SerializeField] private Texture2D c96_cursor;

    private Vector2 new_cursorHotspot;
    private Vector2 default_cursorHotspot;
    private Image icon;
    private Image coin;
    private Text text1;
    private Text text2;
    private Text text3;

    private bool isMouseOver = false;

    // Start is called before the first frame update
    void Start()
    {
        new_cursorHotspot = new Vector2(7, 5);
        Cursor.SetCursor(default_cursor, new_cursorHotspot, CursorMode.Auto);
        icon = GetComponent<Image>();
        text1 = transform.GetChild(0).gameObject.GetComponent<Text>();
        text2 = transform.GetChild(1).gameObject.GetComponent<Text>();
        coin = transform.GetChild(2).gameObject.GetComponent<Image>();
        text3 = transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!executionActive)
            {
                setExecutionActive();
            }
            else
            {
                unsetExecutionActive();
            }
        }
            

        //click the right mouse button to unset execution button
        if (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            unsetExecutionActive();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!executionActive)
            setExecutionActive();
        else
            unsetExecutionActive();
    }


    private void ChangeColor()
    {
        if (executionActive)
        {
            if (isMouseOver)
            {
                icon.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
                text1.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
                text2.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
                coin.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
                text3.color = new Color(0.561f, 0.769f, 0.643f, 0.5f);
            }
            else
            {
                icon.color = new Color(0.561f, 0.769f, 0.643f, 1f);
                text1.color = new Color(0.561f, 0.769f, 0.643f, 1f);
                text2.color = new Color(0.561f, 0.769f, 0.643f, 1f);
                coin.color = new Color(0.561f, 0.769f, 0.643f, 1f);
                text3.color = new Color(0.561f, 0.769f, 0.643f, 1f);
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
                text3.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                icon.color = new Color(1, 1, 1, 1f);
                text1.color = new Color(1, 1, 1, 1f);
                text2.color = new Color(1, 1, 1, 1f);
                coin.color = new Color(1, 1, 1, 1f);
                text3.color = new Color(1, 1, 1, 1f);
            }
        }
    }

    private void setExecutionActive()
    {
        executionActive = true;
        Disinfection.disinfectionActive = false;
        Cursor.SetCursor(c96_cursor, new_cursorHotspot, CursorMode.Auto);
    }

    private void unsetExecutionActive()
    {
        executionActive = false;
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

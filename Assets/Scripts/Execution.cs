using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Execution : MonoBehaviour, IPointerDownHandler
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

    // Start is called before the first frame update
    void Start()
    {
        new_cursorHotspot = new Vector2(7, 5);
        Cursor.SetCursor(default_cursor, new_cursorHotspot, CursorMode.Auto);
        icon = GetComponent<Image>();
        text1 = transform.GetChild(0).gameObject.GetComponent<Text>();
        text2 = transform.GetChild(1).gameObject.GetComponent<Text>();
        coin = transform.GetChild(2).gameObject.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();

        //click the right mouse button to unset execution button
        if (Input.GetMouseButton(1))
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
            icon.color = new Color(0.561f, 0.769f, 0.643f, 1f);
            text1.color = new Color(0.561f, 0.769f, 0.643f, 1f);
            text2.color = new Color(0.561f, 0.769f, 0.643f, 1f);
            coin.color = new Color(0.561f, 0.769f, 0.643f, 1f);
        }
        else
        {
            icon.color = new Color(1, 1, 1, 1f);
            text1.color = new Color(1, 1, 1, 1f);
            text2.color = new Color(1, 1, 1, 1f);
            coin.color = new Color(1, 1, 1, 1f);
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
}

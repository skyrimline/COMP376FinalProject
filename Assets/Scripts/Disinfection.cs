using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Disinfection : MonoBehaviour, IPointerDownHandler
{

    public static bool disinfectionActive = false;

    private Image icon;

    //setting cursor icon
    [SerializeField] private Texture2D default_cursor;
    [SerializeField] private Texture2D disinfectant_cursor;
    private Vector2 new_cursorHotspot;

    // Start is called before the first frame update
    void Start()
    {
        new_cursorHotspot = new Vector2(7, 5);
        

        icon = GetComponent<Image>();
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
            icon.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            icon.color = new Color(1, 1, 1, 1f);
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// implementing drag and drop events
public class Drag_And_Drop : MonoBehaviour, 
    IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private BoxCollider2D col = null;
    private Rigidbody2D rb = null;
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
        // make its collider to trigger and set the rigid body gravity scale to 0
        if (col != null)
        {
            col.isTrigger = true;
        }
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
    }

    // change the position of the thing
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //Debug.Log(eventData);
        //Debug.Log(cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10)));
        //rectTransform.anchoredPosition += eventData.delta;
        transform.position = cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        // make its collider back to normal and set the rigid body gravity scale to 1
        if (col != null)
        {
            col.isTrigger = false;
        }
        if (rb != null)
        {
            rb.gravityScale = 1;
        }
    }

}

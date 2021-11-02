using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// implementing drag and drop events
public class Drag_And_Drop : MonoBehaviour, 
    IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    private NPC_Movement npcMove = null;
    private Camera cam;
    private SpriteRenderer spr;
    private Vector3 startPos;

    private void Awake()
    {
        npcMove = GetComponent<NPC_Movement>();
        cam = Camera.main;
        spr = GetComponent<SpriteRenderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        // if the object being dragged is a npc
        if(npcMove != null)
        {
            npcMove.FreezePosAndDisableCol();
        }

        // change to transparent when dragging
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.5f);
    }


    // change the position of the game object
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // reset npc movement
        if(npcMove != null)
        {
            npcMove.ResetMoveAndEnableCol();
        }

        // reset transparency
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);


        /***************************************************/
        // also check if the drop position is droppable.
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        Room_Area room = null;
        foreach(var result in results)
        {
            // by checking if the ray casted drop position has a room area in it
            room = result.gameObject.GetComponent<Room_Area>();
            // as long as a room is located, break the loop to save resources
            if(room != null)
            {
                break;
            }
        }
        // reset the position if not dropped to the correct area
        if(room == null)
        {
            transform.position = startPos;
        }
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        startPos = transform.position;
    }
}

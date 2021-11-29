using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// implementing drag and drop events
public class Drag_And_Drop_NPC : MonoBehaviour, 
    IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    private NPC_Movement npcMove = null;
    private NPC_Logic npcLogic = null;
    private Camera cam;
    private Vector3 startPos;

    private void Awake()
    {
        npcMove = GetComponent<NPC_Movement>();
        npcLogic = GetComponent<NPC_Logic>();
        cam = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Disinfection.disinfectionActive || eventData.button != 0)
        {
            return;
        }
        
        // if the object being dragged is a npc
        if(npcMove != null)
        {
            npcMove.FreezePosAndDisableCol();
        }
    }


    // change the position of the game object
    public void OnDrag(PointerEventData eventData)
    {
        if (Disinfection.disinfectionActive || eventData.button != 0)
        {
            return;
        }
        transform.position = - new Vector3(0, 2f, 0) + cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Disinfection.disinfectionActive || eventData.button != 0)
        {
            return;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (Disinfection.disinfectionActive || eventData.button != 0)
        {
            return;
        }

        /***************************************************/
        // also check if the drop position is droppable.
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        Room_Area room = null;
        bool isAccept = false;
        foreach(var result in results)
        {
            // by checking if the ray casted drop position has a room area in it
            room = result.gameObject.GetComponent<Room_Area>();
            // When a room is located, break the loop
            if(room != null)
            {
                isAccept = room.CheckCapacity();
            }
        }
        // reset the position if not dropped to the correct area
        if(!isAccept)
        {
            transform.position = startPos;
        }
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        startPos = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Disinfection.disinfectionActive || eventData.button != 0)
        {
            return;
        }

        // reset npc movement
        if (npcMove != null)
        {
            if (!Execution.executionActive && !Disinfection.disinfectionActive)
            {
                npcMove.ResetMoveAndEnableCol();
            }
                
        }
    }
}

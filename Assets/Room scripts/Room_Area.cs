using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Room_Area : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        // get the object that is dropped 
        GameObject drop_obj = eventData.pointerDrag;

        // set isInRoom to true to enable the movement in room behavior
        NPC_Movement npc = drop_obj.GetComponent<NPC_Movement>();
        if(npc != null)
        {
            npc.isInRoom = true;
        }

        //// snap it to room position
        //if(drop_obj != null)
        //{
        //    drop_obj.transform.position = transform.position;
        //}
    }
}

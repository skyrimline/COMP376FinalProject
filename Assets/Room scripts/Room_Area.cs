using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Room_Area : MonoBehaviour, IDropHandler
{
    public enum Room_Type { dorm, observation, ICU};
    public Room_Type roomType;

    // keep a list of NPC objects (or keep track of the number of NPC)

    
    // capacity for different rooms, might increase or decrease. Can be set and get
    private int dormCapacity = 5;
    private int functionalRoomCapacity = 1;     // capacity for observation room and ICU
    public void OnDrop(PointerEventData eventData)
    {
        if (CheckCapacity())
        {
            // get the object that is dropped 
            GameObject drop_obj = eventData.pointerDrag;

            // set isInRoom to true to enable the movement in room behavior
            NPC_Movement npc = drop_obj.GetComponent<NPC_Movement>();
            if (npc != null)
            {
                npc.isInRoom = true;
            }

            // snap it to room position
            if (drop_obj != null)
            {
                drop_obj.transform.position = transform.position;
            }
        }
    }

    // called by drag and drop npc script to check capacity of the room.
    public bool CheckCapacity()
    {
        switch (roomType)
        {
            case Room_Type.dorm:
                break;
            case Room_Type.observation:
            case Room_Type.ICU:
                break;
        }

        return true;
    }

    // --------- getters and setters -----------
    public void SetDormCapacity(int c)
    {
        dormCapacity = c;
    }

    public int GetDormCapacity()
    {
        return dormCapacity;
    }
}

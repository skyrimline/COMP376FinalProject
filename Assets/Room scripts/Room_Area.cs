using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room_Area : MonoBehaviour, IDropHandler
{
    // keep a list of NPC objects to keep track of NPCs
    public List<NPC_Logic> NPCList = new List<NPC_Logic>();

    // capacity for different rooms, might increase or decrease. Can be set and get
    // The defult value is 2 (Chould chang)
    [SerializeField] private int roomCapacity = 1;


    [SerializeField] private Text remainingBedText = null;


    public void OnDrop(PointerEventData eventData)
    {
        if (CheckCapacity())
        {
            // get the object that is dropped 
            GameObject drop_obj = eventData.pointerDrag;

            // set isInRoom to true to enable the movement in room behavior
            NPC_Movement npc_move = drop_obj.GetComponent<NPC_Movement>();
            if (npc_move != null)
            {
                npc_move.isInRoom = true;
            }

            // snap it to room position
            if (drop_obj != null)
            {
                drop_obj.transform.position = transform.position - new Vector3(0, 2.3f, 0);
            }
        }
    }

    // called by drag and drop npc script to check capacity of the room.
    public bool CheckCapacity()
    {
        return NPCList.Count <= roomCapacity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other == null)
        {
            return;
        }
        if(other.tag.Equals("NPC") || other.tag.Equals("Zombie"))
        {
            NPCList.Add(other.gameObject.GetComponent<NPC_Logic>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other == null)
        {
            return;
        }
        if (other.tag.Equals("NPC") || other.tag.Equals("Zombie"))
        {
            NPCList.Remove(other.gameObject.GetComponent<NPC_Logic>());
        }
    }

    private void Update()
    {
        UpdateUIRemainingBed();
        RemoveNullFromList();
    }


    // Update the UI - remaining bed of dorm
    private void UpdateUIRemainingBed()
    {
        if(remainingBedText != null)
        {
            remainingBedText.text = (roomCapacity - NPCList.Count).ToString();
        }
    }


    private void RemoveNullFromList()
    {
        for(int i = 0; i < NPCList.Count; ++i)
        {
            if(NPCList[i] == null)
            {
                NPCList.RemoveAt(i);
                break;
            }
        }
    }


    // --------- getters and setters -----------
    public void SetCapacity(int c)
    {
        roomCapacity = c;
    }

    public int GetCapacity()
    {
        return roomCapacity;
    }


}

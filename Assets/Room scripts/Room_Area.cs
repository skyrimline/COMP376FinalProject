using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class Room_Area : MonoBehaviour, IDropHandler
{
    // keep a list of NPC objects to keep track of NPCs
    public List<NPC_Logic> NPCList = new List<NPC_Logic>();
    public List<NPC_Logic> ZombieList = new List<NPC_Logic>();

    // capacity for different rooms, might increase or decrease. Can be set and get
    [SerializeField] private int roomCapacity = 1;


    [SerializeField] private Text remainingBedText = null;

    public bool isRoomEnabled = false;
    [SerializeField] private GameObject ironDoor = null;
    [SerializeField] private GameObject otherUI = null;

    // for spawning error messages
    [SerializeField] private GameObject ErrorMessagePrefab;


    private void Start()
    {
        // preset the door
        if (isRoomEnabled)
        {
            ironDoor.SetActive(false);
            if (otherUI != null)
                otherUI.SetActive(true);
        }
        else
        {
            ironDoor.SetActive(true);
            if (otherUI != null)
                otherUI.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // disable room drop under these conditions!
        if (Disinfection.disinfectionActive || eventData.button != 0 || eventData.pointerDrag.tag != "NPC")
        {
            return;
        }
        
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
        if (!(NPCList.Count < roomCapacity))
        {
            GenerateErrorMessage("Room is Full");
        }
        if (!isRoomEnabled)
        {
            GenerateErrorMessage("Room is Currently Unavailable");
        }
        return NPCList.Count < roomCapacity && isRoomEnabled;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
        {
            return;
        }
        if (other.tag.Equals("NPC"))
        {
            NPCList.Add(other.gameObject.GetComponent<NPC_Logic>());
        }
        if (other.tag.Equals("Zombie"))
        {
            ZombieList.Add(other.gameObject.GetComponent<NPC_Logic>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null)
        {
            return;
        }
        if (other.tag.Equals("NPC"))
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
        if (remainingBedText != null)
        {
            remainingBedText.text = (roomCapacity - NPCList.Count).ToString();
        }
    }


    private void RemoveNullFromList()
    {
        for (int i = 0; i < NPCList.Count; ++i)
        {
            if (NPCList[i] == null)
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

    public void EnableRoom()
    {
        isRoomEnabled = true;
        ironDoor.SetActive(false);
        if (otherUI != null)
            otherUI.SetActive(true);
    }

    public void DisableRoom()
    {
        isRoomEnabled = false;
        ironDoor.SetActive(true);
        if (otherUI != null)
            otherUI.SetActive(false);
    }

    private void GenerateErrorMessage(string message)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        pos.y += 1f;
        GameObject g = Instantiate(ErrorMessagePrefab, pos, Quaternion.identity);
        var f = g.GetComponentInChildren<Floating_Info_Control>();
        if (f != null)
        {
            f.SetText(message);
        }
    }

}

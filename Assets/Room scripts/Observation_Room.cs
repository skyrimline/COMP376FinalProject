using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Observation_Room : MonoBehaviour
{
    private NPC_Logic npc = null;
    [SerializeField] private Text heartRateText;

    private float updateIntervalTimer = 0;
    private float updateInterval = 3f;


    //setting roomHeartRate for displaying and which roomHeartRate to display
    private int[] roomHeartRate = new int[5];
    private NPC_Logic npcRefer;
    private int roomHeartRateIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetNpcReference();
        setRoomHeartRate();
        //DisplayHeartRate();
    }

    private void GetNpcReference()
    {
        // get the npc from the other script
        if (GetComponent<Room_Area>().NPCList.Count > 0)
        {
            npc = GetComponent<Room_Area>().NPCList[0];
        }
        else
        {
            npc = null;
        }
    }

    private void setRoomHeartRate()
    {
        if (npc != null)
        {
            npcRefer = GetComponent<Room_Area>().NPCList[0].GetComponent<NPC_Logic>();
            roomHeartRate = npcRefer.heartRate;
        }

    }


    // based on the NPC type, display the heart rate
    //private void DisplayHeartRate()
    //{
    //    // display a number on UI based on the npc type

    //    if (roomHeartRateIndex >= 5)
    //        roomHeartRateIndex = 0;


    //    if (npc != null)
    //    {
    //        // only display once after the interval
    //        if (updateIntervalTimer > 0)
    //        {
    //            updateIntervalTimer -= Time.deltaTime;
    //            return;
    //        }
    //        //Debug.Log("111");
    //        // this is where you can change which npc can have which heart rate


    //        // reset timer
    //        updateIntervalTimer = updateInterval;
    //    }
    //    // continue to display 000 when 
    //    else
    //    {
    //        for (int i = 0; i < roomHeartRate.Length; i++)
    //        {
    //            roomHeartRate[i] = 0;
    //        }

    //        updateIntervalTimer = 0;
    //    }

    //    // display on UI


    //    heartRateText.text = roomHeartRate[roomHeartRateIndex].ToString("D3");


    //    roomHeartRateIndex += 1;

    //}
}

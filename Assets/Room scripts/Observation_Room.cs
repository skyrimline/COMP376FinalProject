using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Observation_Room : MonoBehaviour
{
    private NPC_Logic npc = null;
    private NPC_Logic zombie = null;
    [SerializeField] private Text heartRateText;

    private float updateIntervalTimer = 0;
    private float updateInterval = 3f;


    //setting roomHeartRate for displaying and which roomHeartRate to display
    private int[] roomHeartRate = new int[5];
    private NPC_Logic npcRefer;
    private int roomHeartRateIndex = 0;

    //seting room light
    private string[] roomLight = new string[5];

    [SerializeField] private Light2D light0;
    [SerializeField] private Light2D light1;
    [SerializeField] private Light2D light2;
    [SerializeField] private Light2D light3;
    [SerializeField] private Light2D light4;
    private float lightTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        light0.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        GetNpcReference();
        GetZombieReference();
        //setRoomHeartRate();
        //DisplayHeartRate();
        getRoomLight();
        setRoomLight();
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

    private void GetZombieReference()
    {
        // get the npc from the other script

        if (GetComponent<Room_Area>().ZombieList.Count > 0)
        {
            zombie = GetComponent<Room_Area>().ZombieList[0];
        }
        else
        {
            zombie = null;
        }
    }

    private void getRoomLight()
    {
        if (npc != null)
        {
            npcRefer = GetComponent<Room_Area>().NPCList[0].GetComponent<NPC_Logic>();
            roomLight = npcRefer.getActualColors();
        }

    }

    private void setRoomLight()
    {
        
        if (npc == null && zombie == null)
        {
            light0.color = Color.white;
            light1.color = Color.white;
            light2.color = Color.white;
            light3.color = Color.white;
            light4.color = Color.white;
            lightTimer = 0;
        }

        if (zombie != null)
        {
            light0.color = Color.red;
            light1.color = Color.red;
            light2.color = Color.red;
            light3.color = Color.red;
            light4.color = Color.red;
        }

        else
        {
            lightTimer += 1 * Time.deltaTime;

            if (lightTimer >= 3)
            {
                switch (roomLight[0])
                {
                    case "red":
                        light0.color = Color.red;
                        break;
                    case "yellow":
                        light0.color = Color.yellow;
                        break;
                    case "green":
                        light0.color = Color.green;
                        break;
                    default:
                        light0.color = Color.white;
                        break;
                }
            }
            if (lightTimer >= 6)
            {
                switch (roomLight[1])
                {
                    case "red":
                        light1.color = Color.red;
                        break;
                    case "yellow":
                        light1.color = Color.yellow;
                        break;
                    case "green":
                        light1.color = Color.green;
                        break;
                    default:
                        light1.color = Color.white;
                        break;
                }
            }
            if (lightTimer >= 9)
            {
                switch (roomLight[2])
                {
                    case "red":
                        light2.color = Color.red;
                        break;
                    case "yellow":
                        light2.color = Color.yellow;
                        break;
                    case "green":
                        light2.color = Color.green;
                        break;
                    default:
                        light2.color = Color.white;
                        break;
                }
            }
            if (lightTimer >= 12)
            {

                switch (roomLight[3])
                {
                    case "red":
                        light3.color = Color.red;
                        break;
                    case "yellow":
                        light3.color = Color.yellow;
                        break;
                    case "green":
                        light3.color = Color.green;
                        break;
                    default:
                        light3.color = Color.white;
                        break;
                }
            }
            if (lightTimer >= 15)
            {
                switch (roomLight[4])
                {
                    case "red":
                        light4.color = Color.red;
                        break;
                    case "yellow":
                        light4.color = Color.yellow;
                        break;
                    case "green":
                        light4.color = Color.green;
                        break;
                    default:
                        light4.color = Color.white;
                        break;
                }
            }



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
    private void DisplayHeartRate()
    {
        // display a number on UI based on the npc type

        if (roomHeartRateIndex >= 5)
            roomHeartRateIndex = 0;


        if (npc != null)
        {
            // only display once after the interval
            if (updateIntervalTimer > 0)
            {
                updateIntervalTimer -= Time.deltaTime;
                return;
            }
            //Debug.Log("111");
            // this is where you can change which npc can have which heart rate


            // reset timer
            updateIntervalTimer = updateInterval;
        }
        // continue to display 000 when 
        else
        {
            for (int i = 0; i < roomHeartRate.Length; i++)
            {
                roomHeartRate[i] = 0;
            }

            updateIntervalTimer = 0;
        }

        // display on UI


        heartRateText.text = roomHeartRate[roomHeartRateIndex].ToString("D3");




        roomHeartRateIndex += 1;

    }
}

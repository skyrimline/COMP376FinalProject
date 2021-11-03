using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Observation_Room : MonoBehaviour
{
    private NPC_Logic npc = null;
    [SerializeField] private Text heartRateText;

    private float updateIntervalTimer = 0.0f;
    private float updateInterval = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetNpcReference();
        DisplayHeartRate();
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

    // based on the NPC type, display the heart rate
    private void DisplayHeartRate()
    {
        // only display once after the interval
        if(updateIntervalTimer > 0)
        {
            updateIntervalTimer -= Time.deltaTime;
            return;
        }
        // reset timer
        updateIntervalTimer = updateInterval;
        // display a number on UI based on the npc type
        int heartRate = 0;
        if(npc != null)
        {
            // this is where you can change which npc can have which heart rate
            switch (npc.GetNPCType())
            {
                case NPC_Logic.NPC_Type.normal:
                    heartRate = Random.Range(70, 111);
                    break;
                case NPC_Logic.NPC_Type.infected:
                    heartRate = Random.Range(80, 121);
                    break;
                case NPC_Logic.NPC_Type.dying:
                    heartRate = Random.Range(170, 201);
                    break;
                case NPC_Logic.NPC_Type.zombie:
                    heartRate = Random.Range(10,20);
                    break;
            }
        }
        // continue to display 000 when 
        else
        {
            heartRate = 0;
        }

        // display on UI
        heartRateText.text = heartRate.ToString("D3");

    }
}

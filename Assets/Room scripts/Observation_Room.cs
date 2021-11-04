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
        // display a number on UI based on the npc type

        int[] heartRate = new int[10];

        int[] heartRateNormal = {Random.Range(80, 111), Random.Range(80, 111), Random.Range(80, 111), Random.Range(80, 111),
                                Random.Range(80, 111), Random.Range(80, 111), Random.Range(80, 111), Random.Range(80, 111),
                                Random.Range(80, 111), Random.Range(80, 111)};
        int[] heartRateInfected = {Random.Range(80, 121), Random.Range(80, 121), Random.Range(80, 121), Random.Range(80, 121),
                                Random.Range(80, 121), Random.Range(110, 121), Random.Range(80, 121), Random.Range(80, 121),
                                Random.Range(110, 121), Random.Range(80, 121)};
        int[] heartRateDying = {Random.Range(121, 181), Random.Range(121, 181), Random.Range(121, 181), Random.Range(121, 181),
                                Random.Range(121, 181), Random.Range(121, 181), Random.Range(121, 181), Random.Range(121, 181),
                                Random.Range(121, 181), Random.Range(121, 181)};
        int[] heartRateZombie = {Random.Range(200, 301), Random.Range(200, 301), Random.Range(200, 301), Random.Range(200, 301),
                                 Random.Range(200, 301), Random.Range(200, 301), Random.Range(200, 301), Random.Range(200, 301),
                                 Random.Range(200, 301), Random.Range(200, 301)};
        if (npc != null)
        {
            // only display once after the interval
            if (updateIntervalTimer > 0)
            {
                updateIntervalTimer -= Time.deltaTime;
                return;
            }
            Debug.Log("111");
            // this is where you can change which npc can have which heart rate
            switch (npc.GetNPCType())
            {
                case NPC_Logic.NPC_Type.normal:

                    heartRate = heartRateNormal;
                    break;
                case NPC_Logic.NPC_Type.infected:

                    heartRate = heartRateInfected;
                    break;
                case NPC_Logic.NPC_Type.dying:

                    heartRate = heartRateDying;
                    break;
                case NPC_Logic.NPC_Type.zombie:

                    heartRate = heartRateZombie;
                    break;
            }
            // reset timer
            updateIntervalTimer = updateInterval;
        }
        // continue to display 000 when 
        else
        {
            for (int i = 0; i < heartRate.Length; i++)
            {
                heartRate[i] = 0;
            }
            
            updateIntervalTimer = 0;
        }

        // display on UI
        for (int i = 0; i < heartRate.Length; i++)
        {
            heartRateText.text = heartRate[i].ToString("D3");
        }
        



    }
}

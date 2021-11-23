using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunter_Room : MonoBehaviour
{
    // global game logic reference
    private GameLogic gameLogicReference;

    // room area reference
    private Room_Area room;
    // a temp NPC list to store the out hunters
    private List<NPC_Logic> tempNPCList = new List<NPC_Logic>();

    // array of 5 resources, generated randomly each time
    private int[] resources;
    private int resourceIndex_1;
    private int resourceIndex_2;

    // panel for send out hunder button
    [SerializeField] public GameObject sendButtonPanel;
    // panel for hunter return timer
    [SerializeField] public GameObject returnTimerPanel;
    // text for hunter timer
    [SerializeField] private Text hunterTimerText;

    private float hunterTime;   // 60
    private float hunterTimer;

    // chance that a hunter will die
    private float deathRate = 0.3f;

    private bool isHunterOut = false;

    // Start is called before the first frame update
    void Start()
    {
        hunterTime = 10.0f;
        hunterTimer = hunterTime;

        gameLogicReference = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();

        room = GetComponent<Room_Area>();

        // initially all panels are not active, have to put someone into the room to activate it
        sendButtonPanel.SetActive(false);
        returnTimerPanel.SetActive(false);
    }

    void Update()
    {
        UpdateTimerText();
        ShowSendOutButton();
        if (isHunterOut)
        {
            HunterOutTimerTick();
        }
    }

    // timer start
    private void HunterOutTimerTick()
    {
        if(hunterTimer > 0)
        {
            hunterTimer -= Time.deltaTime;
            return;
        }
        HunterBack();
    }

    private void HunterBack()
    {
        // reset timer
        hunterTimer = hunterTime;
        // reset isHunterOut
        isHunterOut = false;
        // disable the timer panel
        returnTimerPanel.SetActive(false);
        // reset room availibility
        room.isRoomEnabled = true;
        // collect resources
        CollectResources();
        // add the remaining people back to the room
        foreach (NPC_Logic l in tempNPCList)
        {
            l.gameObject.SetActive(true);
            //room.NPCList.Add(l);
        }
        // empty the tempNPC list
        tempNPCList.Clear();
    }

    // function called when click on hunter button
    public void SendOutHunters()
    {
        // set the room to be inactive, just change the isRoomEnabled to false, dont change other UI
        // so no new npc can be dragged into it
        room.isRoomEnabled = false;

        // deduct food based on number of people goint out to hunt
        gameLogicReference.foodNum -= room.NPCList.Count;

        // 1. disable sendout button panel (by removing npcs from the room area list)
        // 2. record the npcs in the temp list and set npcs to inactive
        foreach(NPC_Logic l in room.NPCList)
        {
            // record
            tempNPCList.Add(l);
        }
        foreach(NPC_Logic l in tempNPCList)
        {
            // set active false only in this list, cuz the other list will have conflict with trigger auto remove function
            l.gameObject.SetActive(false);
        }
        // clear the NPC list in room area
        room.NPCList.Clear();
        // and bringup the timer panel
        returnTimerPanel.SetActive(true);
        // check death and destroy those NPCs
        CheckDeath();
        // generate resources, dont add yet!
        GenerateResources();
        // start the timer of hunter
        isHunterOut = true;
    }

    private void CheckDeath()
    {
        // there's a chance that NPC will die
        int i = 0;
        while(i < tempNPCList.Count)
        {
            if(Random.Range(0f, 1f) < deathRate)
            {
                Debug.Log("Dead");
                // kill NPC
                tempNPCList[i].Die();
                // remove from list
                tempNPCList.RemoveAt(i);
                continue;
            }
            ++i;
        }
    }

    private void GenerateResources()
    {
        // food, money, vac_A, vac_B, vac_C
        int[] _resources = { Random.Range(5, 10), Random.Range(50, 100), Random.Range(3, 7), Random.Range(5, 10), Random.Range(3, 7)};
        resources = _resources;
        resourceIndex_1 = Random.Range(0, 5);
        resourceIndex_2 = Random.Range(0, 5);
    }

    private void CollectResources()
    {
        int resourceCount1 = resources[resourceIndex_1] * tempNPCList.Count;
        Debug.Log("Collect Resource" + resourceIndex_1 + ": " + resourceCount1);
        switch (resourceIndex_1)
        {
            case 0:
                // give food
                gameLogicReference.foodNum += resourceCount1;
                break;
            case 1:
                // give money
                gameLogicReference.money += resourceCount1;
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += resourceCount1;
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += resourceCount1;
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += resourceCount1;
                break;
        }

        int resourceCount2 = resources[resourceIndex_2] * tempNPCList.Count;
        Debug.Log("Collect Resource" + resourceIndex_2 + ": " + resourceCount2);
        switch (resourceIndex_2)
        {
            case 0:
                // give food
                gameLogicReference.foodNum += resourceCount2;
                break;
            case 1:
                // give money
                gameLogicReference.money += resourceCount2;
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += resourceCount2;
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += resourceCount2;
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += resourceCount2;
                break;
        }
    }

    private void ShowSendOutButton()
    {
        // only show the button panel if there's someone in the room
        if(room.NPCList.Count > 0)
        {
            sendButtonPanel.SetActive(true);
        }
        else
        {
            sendButtonPanel.SetActive(false);
        }
    }

    private void UpdateTimerText()
    {
        if (returnTimerPanel.activeInHierarchy)
        {
            hunterTimerText.text = ((int)Mathf.Round(hunterTimer)).ToString("D3");
        }
    }

}

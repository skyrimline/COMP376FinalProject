using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    //scoreboard reference
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private Text titleText;
    [SerializeField] private Text requiredText;
    [SerializeField] private Text savedText;
    [SerializeField] private Text rewardText;

    //npc count 
    private GameObject[] allNPC_obj;
    private int npcNum;


    // resource amount
    public int foodNum;
    public int vaccineA_num;
    public int vaccineB_num;
    public int vaccineC_num;

    //amount of money
    public int money;

    //ui for resources
    [SerializeField] private Text foodNumText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text dayText;
    [SerializeField] private Text dayTimerText;
    [SerializeField] private Text vaccineA_numText;
    [SerializeField] private Text vaccineB_numText;
    [SerializeField] private Text vaccineC_numText;

    //reference for airdrop
    [SerializeField] private GameObject airDrop;
    [SerializeField] private Transform airDropLeftX;
    [SerializeField] private Transform airDropRightX;

    // game time
    private int day = 5;
    private float dayTime = 40;
    private float dayTimer = 40f;

    //airdrop timer
    private float airDropTimer = 10f;

    //targated saving number of npcs and present saved npcs
    private int levelTargetSavedNPCS = 10;
    private int savedNormalNPCs;
    private int rewardFactor = 200;


    // Start is called before the first frame update
    void Start()
    {   //set initial numbers
        foodNum = 20;
        money = 500;
        vaccineA_num = 20;
        vaccineB_num = 30;
        vaccineC_num = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // when day countdown == 0, show final scoreboard
        showScoreBoard();

        //count how many saved npcs are normal


        //count how many NPCs are there in each frame
        npcNum = countNPC();
        //updating the number of resources
        updateResourceNumber();
        //updating the timer
        ClockTick();

        //airdrop
        generateAirDrop();

    }

    //this method is used to make time pass and distribute food when one day passes
    private void ClockTick()
    {
        if (dayTimer >= 0)
        {
            dayTimer -= Time.deltaTime;

        }
        else
        {
            day--;
            dayTimer = dayTime;
            distributeFood();
        }
        // 触发UI的事件
        // 游戏暂停
        // pop up window
        // allow player to send NPC to hunt


    }

    private void distributeFood()
    {
        for (int i = 0; i < allNPC_obj.Length; i++)
        {
            if (allNPC_obj[i].GetComponent<NPC_Movement>().isInRoom && allNPC_obj[i].GetComponentInChildren<Button_Food_Allocation>().allow_Food_Allocation)
            {
                if (foodNum > 0)
                {
                    foodNum--;

                }

                else
                {// no food, deduct life
                    allNPC_obj[i].GetComponent<NPC_Logic>().DeductLife();
                }
            }

        }
    }

    //count how many npcs are in the scene, including inroom and outroom
    private int countNPC()
    {
        allNPC_obj = GameObject.FindGameObjectsWithTag("NPC");
        int npcCount = allNPC_obj.Length;
        return npcCount;
    }

    private int countSavedNormalNPC()
    {
        allNPC_obj = GameObject.FindGameObjectsWithTag("NPC");
        int savedNPCCount = 0;
        for (int i = 0; i < allNPC_obj.Length; i++)
        {
            if (allNPC_obj[i].GetComponent<NPC_Movement>().isInRoom && allNPC_obj[i].GetComponent<NPC_Logic>().GetNPCType() == NPC_Logic.NPC_Type.normal)
            {
                savedNPCCount += 1;
            }

        }
        return savedNPCCount;
    }

    //show how many food, how much money, what day, how much time for the day remains
    private void updateResourceNumber()
    {
        foodNumText.text = foodNum.ToString("D3");
        moneyText.text = money.ToString("D4");
        dayText.text = day.ToString("D2");
        dayTimerText.text = ((int) Mathf.Round(dayTimer)).ToString("D3");
        vaccineA_numText.text = vaccineA_num.ToString("D3");
        vaccineB_numText.text = vaccineB_num.ToString("D3");
        vaccineC_numText.text = vaccineC_num.ToString("D3");
    }

    private void generateAirDrop()
    {
        if(GameObject.FindGameObjectsWithTag("AirDrop").Length == 0)
        {
            if (airDropTimer >= 0)

            {
                airDropTimer -= Time.deltaTime;

            }
            else
            {
                //instantiate airdrop
                Instantiate(airDrop, new Vector3(Random.Range(airDropLeftX.position.x, airDropRightX.position.x), 35, 0), Quaternion.identity);
                airDropTimer = Random.Range(dayTime, 1.5f*dayTime);
            }
        }
    }

    private void showScoreBoard()
    {
        //count how many normal NPCs are saved
        savedNormalNPCs = countSavedNormalNPC();

        if (day == 0)
        {
            Time.timeScale = 0;
            // set texts
            if(savedNormalNPCs < levelTargetSavedNPCS)
            {
                titleText.text = "apocalypse".ToUpper();
                titleText.color = new Color(0.576f, 0, 0);
                savedText.color = new Color(0.576f, 0, 0);
            }
            else
            {
                titleText.text = "Genesis".ToUpper();
                titleText.color = new Color(1,1,1);
                savedText.color = new Color(0, 1, 0);
            }

            requiredText.text = levelTargetSavedNPCS.ToString("D2");
            savedText.text = savedNormalNPCs.ToString("D2");

            rewardText.text = (savedNormalNPCs - levelTargetSavedNPCS) > 0 ? ((savedNormalNPCs - levelTargetSavedNPCS) * rewardFactor).ToString("D3") : 0.ToString("D3");

            ScoreBoard.SetActive(true);

        }
    }


}
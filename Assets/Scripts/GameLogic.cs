using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private AudioClip airplan;
    [SerializeField] private AudioSource source;

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

    public int productivity;

    public int maxRulePower;
    public int rulePower;

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
    private int day;
    private float dayTime;
    private float dayTimer;
    public int phase;

    //airdrop timer
    private float airDropTimer = 10f;

    private GameFlowControl gameFlowControl;

    private void Awake()
    {
        productivity = 0;
        maxRulePower = 10;
        rulePower = maxRulePower;
        foodNum = 20;
        money = 300;
        vaccineA_num = 10;
        vaccineB_num = 10;
        vaccineC_num = 10;

        dayTime = 60;
        dayTimer = dayTime;
    }

    private void Start()
    {
        gameFlowControl = GetComponent<GameFlowControl>();
        
    }


    // Update is called once per frame
    void Update()
    {
        //count how many NPCs are there in each frame
        npcNum = countNPC();
        //updating the number of resources
        updateResourceNumber();
        //updating the timer
        ClockTick();

        //airdrop
        generateAirDrop();

        UpdateProductivity();

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
    }

    private void distributeFood()
    {
        bool isEveryoneGetsFood = true;
        for (int i = 0; i < allNPC_obj.Length; i++)
        {
            bool allowFood = allNPC_obj[i].GetComponentInChildren<Button_Food_Allocation>().allow_Food_Allocation;
            if (allNPC_obj[i].GetComponent<NPC_Movement>().isInRoom)
            {
                if (foodNum > 0 && allowFood)
                {
                    foodNum--;
                    allNPC_obj[i].GetComponent<NPC_Logic>().AddLife();
                }
                else if(foodNum <= 0 || !allowFood)
                {
                    // no food or not allow food, deduct life
                    allNPC_obj[i].GetComponent<NPC_Logic>().DeductLife();

                    isEveryoneGetsFood = false;
                }
            }

        }

        if (isEveryoneGetsFood)
        {
            // rulepower +2
            ChangeRulePower(2);
        }
    }

    //count how many npcs are in the scene, including inroom and outroom
    private int countNPC()
    {
        allNPC_obj = GameObject.FindGameObjectsWithTag("NPC");
        int npcCount = allNPC_obj.Length;
        return npcCount;
    }


    public int countSavedNormalNPC()
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
                source.PlayOneShot(airplan);
                Instantiate(airDrop, new Vector3(Random.Range(airDropLeftX.position.x, airDropRightX.position.x), 35, 0), Quaternion.identity);
                airDropTimer = Random.Range(dayTime, 1.5f*dayTime);
            }
        }
    }

    // productivity is calculated as the NPCs in dorm
    private void UpdateProductivity()
    {
        GameObject[] allDorm_obj = GameObject.FindGameObjectsWithTag("room_dorm");
        int count = 0;
        for (int i = 0; i < allDorm_obj.Length; i++)
        {
            count += allDorm_obj[i].GetComponent<Room_Area>().NPCList.Count;
        }

        productivity = count;

    }

    public void ChangeRulePower(int delta)
    {
        if(rulePower + delta > maxRulePower)
        {
            rulePower = maxRulePower;
        }
        else
        {
            rulePower += delta;
        }
    }

    public float getTime()
    {
        return dayTime;
    }

    public float getTimer()
    {
        return dayTimer;
    }

    public void setTimer(float i)
    {
        dayTimer = i;
    }

    public int getPhase()
    {
        return phase;
    }

    public int getDay()
    {
        return day;
    }

    public void setDay(int i)
    {
        day = i;
    }

    public int getMaxDayInPhase()
    {
        switch (phase)
        {
            case 1:
                return gameFlowControl.phaseDayCount[0];
            case 2:
                return gameFlowControl.phaseDayCount[1];
            case 3:
                return gameFlowControl.phaseDayCount[2];
            default:
                return 0;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Story_Control : MonoBehaviour
{
    public float storyTimer = 0;

    private GameLogic gameLogicReference;

    public GameObject[] story = new GameObject[10];

    private bool isStoryTriggeredInDay;
    // two sets of stories have dependencies: 6-7, 4-8 
    private bool[] storyCalled = {false, false, false, false, false, false, true, true, false, false};

    private float storyPopTimeInDay;

    // Start is called before the first frame update
    void Start()
    {

        gameLogicReference = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        storyPopTimeInDay = Random.Range(5f, gameLogicReference.getTime() - 30);

    }

    // Update is called once per frame
    void Update()
    {
        //storyTimer += 1 * Time.deltaTime;

        // ??????????????????????????????????????????????????
        int phase = gameLogicReference.getPhase();
        int day = gameLogicReference.getDay();
        float time = gameLogicReference.getTimer();

        day = gameLogicReference.getMaxDayInPhase() - day + 1;

        if (time <= 1)
            isStoryTriggeredInDay = false;

        if (isStoryTriggeredInDay)
            return;

        if (phase == 1)
        {
            if (day == 1 || day == 3 || day == 5)
            {
                if (Mathf.Abs(time - storyPopTimeInDay) <= 1f)
                {   

                    CallStory();
                    
                    storyPopTimeInDay = Random.Range(5f, gameLogicReference.getTime() - 5);

                }
            }
        }
        else if (phase == 2)
        {
            if (day == 2 || day == 4 || day == 6)
            {
                if (Mathf.Abs(time - storyPopTimeInDay) <= 1f)
                {
                    CallStory();
                    storyPopTimeInDay = Random.Range(5f, gameLogicReference.getTime() - 5);
                }
            }
        }
        else if (phase == 3)
        {
            if (day == 3 || day == 7 || day == 9 || day == 13)
            {
                if (Mathf.Abs(time - storyPopTimeInDay) <= 1f)
                {
                    CallStory();
                    storyPopTimeInDay = Random.Range(5f, gameLogicReference.getTime() - 5);
                }
            }
        }

    }

    private void CallStory()
    {

        Time.timeScale = 0;
        int randomStoryNum = Random.Range(0, 10);

        while (storyCalled[randomStoryNum] != false)
        {
            randomStoryNum = Random.Range(0, 10);
        }

        story[randomStoryNum].SetActive(true);

        storyCalled[randomStoryNum] = true;

        isStoryTriggeredInDay = true;
    }

    public void choice1Story1()
    {
        gameLogicReference.ChangeRulePower(10);
        gameLogicReference.foodNum = gameLogicReference.foodNum  - 10 < 0 ? 0 : gameLogicReference.foodNum - 10;
        story[0].SetActive(false);
        Time.timeScale = 1;
    }

    public void choice2Story1()
    {
        //reduce resources
        gameLogicReference.foodNum += 15;
        gameLogicReference.ChangeRulePower(-15);
        story[0].SetActive(false);
        Time.timeScale = 1;
    }
    //Effect1: (food-10, medicine-10, dominance-3)
    //Effect2: (dominance-5)
    //Effect3: (vaccine A -5, dominance -10)
    public void choice1Story2()
    {
        //Because he has made a lot of credit in the search team, he was given some food and medicine to exile him to fend for himself
        gameLogicReference.foodNum = gameLogicReference.foodNum - 10 < 0 ? 0 : gameLogicReference.foodNum - 10;
        gameLogicReference.ChangeRulePower(10);
        Time.timeScale = 1;
        story[1].SetActive(false);
    }

    public void choice2Story2()
    {
        gameLogicReference.ChangeRulePower(-15);
        Time.timeScale = 1;
        story[1].SetActive(false);
        //For the safety of others, directly execute
    }
    public void choice3Story2()
    {
        Time.timeScale = 1;
        story[1].SetActive(false);
        gameLogicReference.vaccineA_num = gameLogicReference.vaccineA_num - 15 < 0 ? 0 : gameLogicReference.vaccineA_num - 15;
        gameLogicReference.ChangeRulePower(-5);
        //Leave him for spiritual healing 
    }
    //Effect1: (money-100, dominance -5)
    //Effect2: (serum -1, vaccineA-5, dominance -2)
    public void choice1Story3()
    {
        //Direct execution 
        gameLogicReference.money = gameLogicReference.money - 200 < 0 ? 0 : gameLogicReference.money - 200;
        gameLogicReference.ChangeRulePower(10);
        Time.timeScale = 1;
        story[2].SetActive(false);
    }

    public void choice2Story3()
    {
        gameLogicReference.vaccineA_num = gameLogicReference.vaccineA_num - 10 < 0 ? 0 : gameLogicReference.vaccineA_num - 10;
        gameLogicReference.vaccineB_num = gameLogicReference.vaccineA_num - 10 < 0 ? 0 : gameLogicReference.vaccineA_num - 10;
        gameLogicReference.vaccineC_num = gameLogicReference.vaccineA_num - 5 < 0 ? 0 : gameLogicReference.vaccineA_num - 5;
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story[2].SetActive(false);

    }

    public void choice1Story4()
    {
        //Direct execution 
        gameLogicReference.foodNum = gameLogicReference.foodNum - 10 < 0 ? 0 : gameLogicReference.foodNum - 10;
        gameLogicReference.ChangeRulePower(10);
        Time.timeScale = 1;
        story[3].SetActive(false);
        // set storyCalled 8 to false
        storyCalled[7] = false;
    }

    public void choice2Story4()
    {
        gameLogicReference.ChangeRulePower(-15);
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story[3].SetActive(false);
        // set storyCalled 8 to false
        storyCalled[7] = false;
    }

    public void choice1Story5()
    {
        //Direct execution
        gameLogicReference.money += 500;
        gameLogicReference.foodNum = gameLogicReference.foodNum - 20 < 0 ? 0 : gameLogicReference.foodNum - 20;
        gameLogicReference.ChangeRulePower(-15);
        Time.timeScale = 1;
        story[4].SetActive(false);
    }

    public void choice2Story5()
    {
        gameLogicReference.ChangeRulePower(10);
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story[4].SetActive(false);

    }

    public void choice1Story6()
    {
        //Direct execution
        gameLogicReference.ChangeRulePower(-15);
        Time.timeScale = 1;
        story[5].SetActive(false);
        // set storyCalled 7 to false
        storyCalled[6] = false;
    }

    public void choice2Story6()
    {
        gameLogicReference.foodNum = gameLogicReference.foodNum - 10 < 0 ? 0 : gameLogicReference.foodNum - 10;
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story[5].SetActive(false);
        // set storyCalled 7 to false
        storyCalled[6] = false;
    }

    public void choice1Story7()
    {
        //Direct execution
        gameLogicReference.ChangeRulePower(-15);
        Time.timeScale = 1;
        story[6].SetActive(false);
    }

    public void choice2Story7()
    {

        gameLogicReference.vaccineA_num = gameLogicReference.vaccineA_num - 10 < 0 ? 0 : gameLogicReference.vaccineA_num - 10;
        gameLogicReference.vaccineB_num = gameLogicReference.vaccineA_num - 10 < 0 ? 0 : gameLogicReference.vaccineA_num - 10;
        gameLogicReference.vaccineC_num = gameLogicReference.vaccineA_num - 5 < 0 ? 0 : gameLogicReference.vaccineA_num - 5;
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story[6].SetActive(false);

    }

    public void choice1Story8()
    {
        //Direct execution
        gameLogicReference.foodNum = gameLogicReference.foodNum - 10 < 0 ? 0 : gameLogicReference.foodNum - 10;
        gameLogicReference.ChangeRulePower(10);
        gameLogicReference.money = gameLogicReference.money - 50 < 0 ? 0 : gameLogicReference.money - 50;
        Time.timeScale = 1;
        story[7].SetActive(false);
    }

    public void choice2Story8()
    {
        gameLogicReference.ChangeRulePower(-15);
        
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story[7].SetActive(false);

    }

    public void choice1Story9()
    {
        //Direct execution
        gameLogicReference.foodNum = gameLogicReference.foodNum - 10 < 0 ? 0 : gameLogicReference.foodNum - 10;
        gameLogicReference.ChangeRulePower(10);
        Time.timeScale = 1;
        story[8].SetActive(false);
    }

    public void choice2Story9()
    {
        gameLogicReference.ChangeRulePower(-15);

        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story[8].SetActive(false);

    }

    public void choice1Story10()
    {
        //Direct execution

        gameLogicReference.money += 300;
        gameLogicReference.ChangeRulePower(-15);
        Time.timeScale = 1;
        story[9].SetActive(false);
    }

    public void choice2Story10()
    {
        gameLogicReference.ChangeRulePower(-5);

        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story[9].SetActive(false);

    }
}


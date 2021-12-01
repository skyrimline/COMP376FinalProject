using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Story_Control : MonoBehaviour
{
    public float storyTimer = 0;

    private GameLogic gameLogicReference;


    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public GameObject story4;
    public GameObject story5;
    public GameObject story6;
    public GameObject story7;
    public GameObject story8;
    public GameObject story9;
    public GameObject story10;
    private GameObject[] story = new GameObject[10];


    private bool story1called;
    private bool story2called;
    private bool story3called;
    private bool story4called;
    private bool story5called;
    private bool story6called;
    private bool story7called;
    private bool story8called;
    private bool story9called;
    private bool story10called;


    private bool[] storyCalled = new bool[10];

    private float storyPopTimeInDay;

    // Start is called before the first frame update
    void Start()
    {

        gameLogicReference = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        storyPopTimeInDay = Random.Range(5f, gameLogicReference.getTime() - 5);
        story[0] = story1;
        story[1] = story2;
        story[2] = story3;
        story[3] = story4;
        story[4] = story5;
        story[5] = story6;
        story[6] = story7;
        story[7] = story8;
        story[8] = story9;
        story[9] = story10;
       


        storyCalled[0] = story1called;
        storyCalled[1] = story2called;
        storyCalled[2] = story3called;
        storyCalled[3] = story4called;
        storyCalled[4] = story5called;
        storyCalled[5] = story6called;
        storyCalled[6] = story7called;
        storyCalled[7] = story8called;
        storyCalled[8] = story9called;
        storyCalled[9] = story10called;

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

        //print(day);
        print(storyPopTimeInDay);
        //print(time);

        if (phase == 1)
        {
            if (day == 1 || day == 2 || day == 3)
            {
                if ((time - storyPopTimeInDay) <= 1f)
                {

                    CallStory();
                    // ????????????????????????????, ????????dowhile??bool[]????????????????????????????
                    storyPopTimeInDay = Random.Range(5f, gameLogicReference.getTime() - 5);

                }
            }
        }
        else if (phase == 2)
        {
            if (day == 2 || day == 4 || day == 6)
            {
                if ((time - storyPopTimeInDay) <= 1f)
                {
                    // ????????????????????????????, ????????dowhile??bool[]????????????????????????????
                    storyPopTimeInDay = Random.Range(5f, gameLogicReference.getTime() - 5);
                }
            }
        }
        else if (phase == 3)
        {
            if (day == 2 || day == 4 || day == 6)
            {
                if ((time - storyPopTimeInDay) <= 1f)
                {
                    // ????????????????????????????, ????????dowhile??bool[]????????????????????????????
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


    }

    public void choice1Story1()
    {
        gameLogicReference.ChangeRulePower(10);
        gameLogicReference.foodNum -= 5;

       
        story2.SetActive(false);
        Time.timeScale = 1;
    }

    public void choice2Story1()
    {

       

        //reduce resources
        gameLogicReference.foodNum += 5;
        gameLogicReference.ChangeRulePower(-10);



        story1.SetActive(false);
        Time.timeScale = 1;
    }
    //Effect1: (food-10, medicine-10, dominance-3)
    //Effect2: (dominance-5)
    //Effect3: (vaccine A -5, dominance -10)
    public void choice1Story2()
    {
        //Because he has made a lot of credit in the search team, he was given some food and medicine to exile him to fend for himself
        gameLogicReference.foodNum -= 10;
        gameLogicReference.ChangeRulePower(10);
        Time.timeScale = 1;
        story2.SetActive(false);
    }

    public void choice2Story2()
    {


        gameLogicReference.ChangeRulePower(5);

        Time.timeScale = 1;
        story2.SetActive(false);
        //For the safety of others, directly execute
    }
    public void choice3Story2()
    {

        Time.timeScale = 1;
        story2.SetActive(false);
        
        gameLogicReference.vaccineA_num -= 5;
        gameLogicReference.ChangeRulePower(-10);
        //Leave him for spiritual healing 
    }
    //Effect1: (money-100, dominance -5)
    //Effect2: (serum -1, vaccineA-5, dominance -2)
    public void choice1Story3()
    {
        //Direct execution 
        gameLogicReference.money -= 50;
        gameLogicReference.ChangeRulePower(5);
        Time.timeScale = 1;
        story3.SetActive(false);
    }

    public void choice2Story3()
    {
        gameLogicReference.vaccineA_num -= 2;
        gameLogicReference.vaccineB_num -= 2;
        gameLogicReference.vaccineC_num -= 2;
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story3.SetActive(false);

    }

    public void choice1Story4()
    {
        //Direct execution 
        gameLogicReference.foodNum -= 2;
        gameLogicReference.ChangeRulePower(5);
        Time.timeScale = 1;
        story4.SetActive(false);
    }

    public void choice2Story4()
    {
        gameLogicReference.ChangeRulePower(-10);
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story4.SetActive(false);

    }

    public void choice1Story5()
    {
        //Direct execution
        gameLogicReference.money += 200;
        gameLogicReference.foodNum -= 30;
        gameLogicReference.ChangeRulePower(-10);
        Time.timeScale = 1;
        story5.SetActive(false);
    }

    public void choice2Story5()
    {
        gameLogicReference.ChangeRulePower(5);
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story5.SetActive(false);

    }

    public void choice1Story6()
    {
        //Direct execution
        
       
        gameLogicReference.ChangeRulePower(-10);
        Time.timeScale = 1;
        story6.SetActive(false);
    }

    public void choice2Story6()
    {
        gameLogicReference.foodNum -= 10;
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story6.SetActive(false);

    }

    public void choice1Story7()
    {
        //Direct execution


        gameLogicReference.ChangeRulePower(-10);

        
        Time.timeScale = 1;
        story7.SetActive(false);
    }

    public void choice2Story7()
    {
        
        gameLogicReference.vaccineA_num -= 5;
        gameLogicReference.vaccineB_num -= 5;
        gameLogicReference.vaccineC_num -= 5;
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story7.SetActive(false);

    }

    public void choice1Story8()
    {
        //Direct execution

        gameLogicReference.foodNum -= 5;
        gameLogicReference.ChangeRulePower(10);
        gameLogicReference.money -= 50;


        Time.timeScale = 1;
        story8.SetActive(false);
    }

    public void choice2Story8()
    {
        gameLogicReference.ChangeRulePower(-10);
        
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story8.SetActive(false);

    }

    public void choice1Story9()
    {
        //Direct execution

        gameLogicReference.foodNum -= 5;
        gameLogicReference.ChangeRulePower(10);
        


        Time.timeScale = 1;
        story9.SetActive(false);
    }

    public void choice2Story9()
    {
        gameLogicReference.ChangeRulePower(-10);

        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story9.SetActive(false);

    }

    public void choice1Story10()
    {
        //Direct execution

        gameLogicReference.money += 100;
        gameLogicReference.ChangeRulePower(-15);



        Time.timeScale = 1;
        story10.SetActive(false);
    }

    public void choice2Story10()
    {
        gameLogicReference.ChangeRulePower(10);

        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story10.SetActive(false);

    }
}


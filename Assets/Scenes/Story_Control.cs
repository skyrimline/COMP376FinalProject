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

    private bool story1called;
    private bool story2called;
    private bool story3called;



    // Start is called before the first frame update
    void Start()
    {
         gameLogicReference = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        return;
        storyTimer += 1 * Time.deltaTime;

        //call story 1

        if (storyTimer >= 3 )
        {
            if (!story1called)
            {
                callStory1();
                storyTimer = 0;

            }
            else if (!story2called)
            {
                callStory1();
                storyTimer = 0;

            }
            else if (!story3called)
            {
                callStory1();
                storyTimer = 0;

            }
        }
    }

    private void callStory1()
    {
        Time.timeScale = 0;
        story1.SetActive(true);
        story2.SetActive(false);
        story3.SetActive(false);

        story1called = true;
    }
    private void callStory2()
    {
        Time.timeScale = 0;
        story1.SetActive(false);
        story2.SetActive(true);
        story3.SetActive(false);

        story2called = true;
    }
    private void callStory3()
    {
        Time.timeScale = 0;
        story1.SetActive(false);
        story2.SetActive(false);
        story3.SetActive(true);

        story1called = true;
    }
    public void choice1Story1()
    {
        //rulepower-10
        
        Time.timeScale = 1;
        story1.SetActive(false);
    }

    public void choice2Story1()
    {
        
        Time.timeScale = 1;
        story1.SetActive(false);

        //reduce resources
        gameLogicReference.foodNum -= 5;
    }
    //Effect1: (food-10, medicine-10, dominance-3)
    //Effect2: (dominance-5)
    //Effect3: (vaccine A -5, dominance -10)
    public void choice1Story2()
    {
        //Because he has made a lot of credit in the search team, he was given some food and medicine to exile him to fend for himself
        gameLogicReference.foodNum -= 10;
        gameLogicReference.vaccineB_num -= 10;
        gameLogicReference.vaccineC_num -= 3;
        Time.timeScale = 1;
        story2.SetActive(false);
    }

    public void choice2Story2()
    {

        Time.timeScale = 1;
        story2.SetActive(false);
        gameLogicReference.vaccineC_num -= 5;
        //For the safety of others, directly execute
    }
    public void choice3Story2()
    {

        Time.timeScale = 1;
        story2.SetActive(false);
        gameLogicReference.vaccineC_num -= 10;
        gameLogicReference.vaccineA_num -= 5;
        //Leave him for spiritual healing 
    }
    //Effect1: (money-100, dominance -5)
    //Effect2: (serum -1, vaccineA-5, dominance -2)
    public void choice1Story3()
    {
        //Direct execution 
        gameLogicReference.money -= 100;
        gameLogicReference.vaccineB_num -= 5;
        Time.timeScale = 1;
        //story3.SetActive(false);
    }

    public void choice2Story3()
    {
        gameLogicReference.vaccineA_num -= 5;
        //Serum cures him and stabilizes his emotions
        Time.timeScale = 1;
        story3.SetActive(false);

    }
}


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

    private bool story1called;

    

    // Start is called before the first frame update
    void Start()
    {
         gameLogicReference = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        storyTimer += 1 * Time.deltaTime;

        //call story 1

        if (storyTimer >= 3 && !story1called)
        {
            callStory1();
        }
    }

    private void callStory1()
    {
        Time.timeScale = 0;
        story1.SetActive(true);

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
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private GameObject[] allNPC_obj;



    // food - UI
    private int food;

    // game time:
    private int dayRemaining = 10;
    private float dayTimer = 60;

    private int levelTarget = 10;

    private int money = 1000;

    // Start is called before the first frame update
    void Start()
    {
        allNPC_obj = GameObject.FindGameObjectsWithTag("NPC");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ClockTick()
    {
        if (dayTimer >= 0)
        {
            dayTimer -= Time.deltaTime;
            return;
        }

        // 触发UI的事件
        // 游戏暂停
        // pop up window
        // allow player to send NPC to hunt

        dayRemaining--;
        dayTimer = 60f;
    }

    private void distributeFood()
    {
        for (int i = 0; i < allNPC_obj.Length; i++)
        {
            if (food > 0)
            {
                food--;
                continue;
            }

            // no food, deduct life
            allNPC_obj[i].GetComponent<NPC_Logic>().DeductLife();
        }
    }
}
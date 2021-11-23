using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DayNightMovingTarget : MonoBehaviour
{
    private GameObject shop;
    private GameLogic gL;
    private float dTimer;
    private float highNoon;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.Find("MovingShop");
        animator = shop.GetComponent<Animator>();
        gL = GetComponent<GameLogic>();
        highNoon = gL.getTime()/2;
    }

    // Update is called once per frame
    void Update()
    {
        dTimer = gL.getTimer();
        if (dTimer < highNoon)
        {
            animator.SetBool("isDay", true);
        }
        else
        {
            animator.SetBool("isDay", false);
        }
    }
}

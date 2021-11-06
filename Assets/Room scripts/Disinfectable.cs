using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Disinfectable : MonoBehaviour, IPointerDownHandler
{
    private Dorm dorm;

    private int cost = 50;

    private void Start()
    {
        dorm = GetComponent<Dorm>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (dorm.isDormInfected && Disinfection.disinfectionActive)
        {
            beingDisinfected();
        }
    }

    private void beingDisinfected()
    {
        // deduct money
        GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>().money -= cost;
        dorm.Disinfect();
    }
}
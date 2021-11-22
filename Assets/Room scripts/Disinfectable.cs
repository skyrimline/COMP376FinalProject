using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Disinfectable : MonoBehaviour, IPointerDownHandler
{
    private Dorm dorm;
    // the corresponding room area script of the dorm
    private Room_Area roomArea;

    private int cost = 50;

    private GameLogic gl;

    private void Start()
    {
        dorm = GetComponent<Dorm>();
        gl = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        roomArea = dorm.gameObject.GetComponent<Room_Area>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // all conditions must be met in order to do the execution
        // 1. dorm is infected
        // 2. disinfection tool is selected
        // 3. remaining money is enough - Money error effect
        // 4. room area is empty, i.e. no NPC is in the area - room not cleared error message
        if (dorm.isDormInfected && Disinfection.disinfectionActive && gl.money >= cost && roomArea.NPCList.Count == 0)
        {
            beingDisinfected();
        }
    }

    private void beingDisinfected()
    {
        // deduct money
        gl.money -= cost;
        dorm.Disinfect();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Executable_NPC : MonoBehaviour, IPointerDownHandler
{
    private NPC_Logic npc;

    private int cost = 50;

    private void Start()
    {
        npc = GetComponent<NPC_Logic>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Execution.executionActive) {
            beingExecuted();
        }
    }

    private void beingExecuted()
    {
        // deduct money
        GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>().money -= cost;
        npc.Die();
    }
}

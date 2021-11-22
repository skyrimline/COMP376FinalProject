using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Executable_NPC : MonoBehaviour, IPointerDownHandler
{
    private NPC_Logic npc;

    private int cost = 50;

    private GameLogic gl;

    private void Start()
    {
        npc = GetComponent<NPC_Logic>();
        gl = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // can only execute when have enough money
        if (Execution.executionActive && gl.money >= cost) {
            beingExecuted();
        }
    }

    private void beingExecuted()
    {
        // deduct money
        gl.money -= cost;
        npc.Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 This is the base class for all 4 types of NPCs
 Defines basic functions common to ALL NPC
 Provide methods to be overwritten by other NPCs
 */

public class NPC_Base : MonoBehaviour
{
    // infectionPhase can be 1 or 2
    // 1 - NPC1, 2, 3 are all at phase 1. Phase 1 can be cured.
    // 2 - NPC4 (zombie) Phase 2 cannot be cured. Can only be killed.
    public int infectionPhase;

    // true if is in room, false if is outside seeking help.
    private bool isInRoom;

    // walking speed of the NPC. Adjustable in editor window.
    [SerializeField] private float walkingSpeed;
    // walking direction, can only take in two values: Vector3.left or Vector3.right
    private Vector3 moveDir;

    void Update()
    {
        Movement();
    }

    // called by Update()
    private void Movement()
    {
        if (isInRoom)
        {
            MovementInRoom();
        }
        else
        {
            MovementOutside();
        }
    }

    // walks randomly left or right, turns around if collides with room wall, dont turn if collides with other NPC
    private void MovementInRoom()
    {

    }

    // walks randomly left or right, turns around if collides with building
    // turns around randomly over a small time period,
    // make sure npc doesn't walk outside the scene.
    private void MovementOutside()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    // components of the npc
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer spr;
    
    
    // true if is in room, false if is outside seeking help.
    public bool isInRoom { get; set; } = false;

    // walking speed of the NPC.
    private float walkingSpeed = 2;
    // walking direction, can only take in two values: Vector3.left or Vector3.right
    private Vector3 moveDir;
    private Vector3[] possibleMoveDir = {Vector3.left, Vector3.right, Vector3.zero };

    // movement in room timers
    private float switchMovementTimerInRoom;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        // set initial timer and move dir
        switchMovementTimerInRoom = Random.Range(1f,3f);
        moveDir = Vector3.zero;
    }


    void FixedUpdate()
    {
        Movement();
        UpdateAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // set another direction if collides with wall
        if (collision.gameObject.layer == 6)
        {
            Vector3 oldDir = moveDir;
            do
            {
                moveDir = possibleMoveDir[Random.Range(0, 3)];
            } while (moveDir == oldDir);
        }

    }

    // called by Update()
    private void Movement()
    {
        if (isInRoom)
        {
            SetDirectionInRoom();
        }
        else
        {
            SetDirectionOutside();
        }

        // only move when it's not being dragged (by checking if its collider is enabled)
        if (col.enabled)
        {
            transform.position = (transform.position + moveDir * walkingSpeed * Time.deltaTime);
        }
    }

    // walks randomly left or right, turns around if collides with room wall
    private void SetDirectionInRoom()
    {
        // time's not up, keep the current state
        if(switchMovementTimerInRoom >= 0)
        {
            switchMovementTimerInRoom -= Time.fixedDeltaTime;
            return;
        }

        // when time's up, reset the timer and change the moving direction
        switchMovementTimerInRoom = Random.Range(1f, 3f);
        moveDir = possibleMoveDir[Random.Range(0, 3)];
    }

    // walks randomly left or right, turns around if collides with building
    // turns around randomly over a small time period,
    // make sure npc doesn't walk outside the scene.
    private void SetDirectionOutside()
    {

    }

    public void FreezePosAndDisableCol()
    {
        col.enabled = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    public void ResetMoveAndEnableCol()
    {
        col.enabled = true;
        rb.gravityScale = 25;
    }

    private void UpdateAnimation()
    {
        if(moveDir == Vector3.right)
        {
            spr.flipX = false;
        }
        else if(moveDir == Vector3.left)
        {
            spr.flipX = true;
        }
        else if(moveDir == Vector3.zero)
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Logic : MonoBehaviour
{
    // enum for NPC type.
    public enum NPC_Type { normal, infected, dying, zombie };


    // The following fields are set in the editor, and should be modified through getter and setter
    // infectionPhase can be 1 or 2
    // 1 - NPC1, 2, 3 are all at phase 1. Phase 1 can be cured.
    // 2 - NPC4 (zombie) Phase 2 cannot be cured. Can only be killed.
    [SerializeField] private int infectionPhase;
    [SerializeField] private NPC_Type type;

    // get a reference to its own zombie, for later spawing
    [SerializeField] private GameObject zombiePrefab = null;


    /* Logics to be implemented, while implementing, may introduce new variables and functions 
        Some logics might be better to implement in room, noted with (*)
     */
    // 1. All NPC(except from zombie) has an integer indicating their HP, normal NPC will have more lives than the others, vice versa.
    // will -1 for those who didn't get food
    [SerializeField] private int life;
    // 2. food is allocated automatically by the game.
    // 3. All NPC (except from zombie) will die if their HP <= 0
    // 6. normal NPC can turn into infected NPC if stayed in an infected room for x, say 40 seconds
    private float NormalToInfectedTimer = 40.0f;
    private float NormalToInfectedTime = 40.0f;
    public bool infectedByRoom = false;
    // 6.1. NPC1 can be applied with vaccine, then it will never turn into NPC2
    public bool isVaccinated = false;


    // 7. infected NPC can turn into dying NPC if not treated with serum(血清) after 1 minute (implicit timer)
    private float InfectedToDyingTimer = 20f;
    // 8. (*) infected NPC will contaminate a dorm immediately
    // 9. dying NPC has a explicit timer (progress bar) shown on top of head, if not treated with serum, will turn into zombie after 30 seconds
    private float DyingToZombieTimer = 30f;
    private float DyingToZombieTime = 30f;
    private Progress_bar zombieProgress;
    // 11. zombies cannot be dragged (already done, simply remove the Drag_And_Drop.cs on zombie prefab)
    // 12. zombies can only be killed by special agents.
    // 13. zombies can kill other NPC on contact (can check the collision tag). 






    // Start is called before the first frame update
    void Start()
    {
        if(transform.Find("Timer_UI_NPC") != null)
        {
            zombieProgress = transform.Find("Timer_UI_NPC").gameObject.GetComponent<Progress_bar>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        NormalToInfected();
        InfectedToDying();
        DyingToZombie();
        TimerUIDisplay();
    }


    // ---------- some state transition function -------------
    public void Die()
    {
        // play dead animation
        // destry object
        transform.eulerAngles = new Vector3(0, 0, 90);
        Destroy(gameObject, 2);
    }

    private void NormalToInfected()
    {
        if (infectedByRoom && type == NPC_Type.normal)
        {
            // timer start count down
            if(NormalToInfectedTimer >= 0)
            {
                NormalToInfectedTimer -= Time.deltaTime;
                return;
            }

            // when time's up, set type to infected.
            type = NPC_Type.infected;
        }
        else
        {
            NormalToInfectedTimer = NormalToInfectedTime;
        }
    }

    private void InfectedToDying()
    {
        // logic is the same as previous
        if (type == NPC_Type.infected)
        {
            // start the timer
            if(InfectedToDyingTimer >= 0)
            {
                InfectedToDyingTimer -= Time.deltaTime;
                return;
            }

            // when time's up, turn into dying type:
            type = NPC_Type.dying;
        }
    }

    private void DyingToZombie()
    {
        if(type == NPC_Type.dying)
        {
            if(DyingToZombieTimer >= 0)
            {
                DyingToZombieTimer -= Time.deltaTime;
                // also set the max and current for progress bar
                zombieProgress.current = DyingToZombieTime - DyingToZombieTimer;
                return;
            }

            // when times up, turn into zombie!
            // Instantiate a zombie here and destroy this game object
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    public void CureBySerum()
    {
        // only can cure if infection phase is 1, might check this attribute in other places.
        // destroy the current NPC and instantiate a normal NPC. 
        // set type to normal
        // reset timers
        // reset life
    }

    public void Vaccinate()
    {
        // might check NPC type. can only give vaccine to normal NPC (npc1)
        // might no need to check type since type is checked elsewhere (e.g. when applying the vaccine)
        isVaccinated = true;
    }

    // ------------ Other helpers -----------
    private void TimerUIDisplay()
    {
        if(zombieProgress == null)
        {
            return;
        }

        if(type== NPC_Type.dying)
        {
            zombieProgress.gameObject.SetActive(true);
        }
        else
        {
            zombieProgress.gameObject.SetActive(false);
        }
    }


    // ----------- Getters and Setters --------------
    public void SetInfectionPhase(int i) 
    {
        infectionPhase = i;
    }

    public int GetInfectinPhase()
    {
        return infectionPhase;
    }

    public void SetNPCType(NPC_Type t)
    {
        type = t;
    }

    public NPC_Type GetNPCType()
    {
        return type;
    }

    public void DeductLife()
    {
        life--;
    }

    public int GetLife()
    {
        return life;
    }

    public void SetLife(int l)
    {
        life = l;
    }
}

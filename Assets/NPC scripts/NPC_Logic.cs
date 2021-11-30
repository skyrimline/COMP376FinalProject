using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Logic : MonoBehaviour
{
    // enum for NPC type.
    public enum NPC_Type { normal, infected, dying, zombie };

    //give a heartRate for all NPCS
    public int[] heartRate = new int[5];

    // The following fields are set in the editor, and should be modified through getter and setter
    // 1 - NPC1, 2, 3 are all at phase 1. Phase 1 can be cured.
    // 2 - NPC4 (zombie) Phase 2 cannot be cured. Can only be killed.
    [SerializeField] private NPC_Type type;


    /* Logics to be implemented, while implementing, may introduce new variables and functions 
        Some logics might be better to implement in room, noted with (*)
     */
    // 1. All NPC(except from zombie) has an integer indicating their HP, normal NPC will have more lives than the others, vice versa.
    // will -1 for those who didn't get food
    [SerializeField] private int life;
    // 2. food is allocated automatically by the game.
    // 3. All NPC (except from zombie) will die if their HP <= 0
    // 6. normal NPC can turn into infected NPC if stayed in an infected room for x, say 40 seconds
    private float NormalToInfectedTimer;
    private float NormalToInfectedTime;
    public bool infectedByRoom = false;
    // 6.1. NPC1 can be applied with vaccine, then it will never turn into NPC2
    public bool isVaccinated = false;

    // 7. infected NPC can turn into dying NPC if not treated with serum(血清) after 1 minute (implicit timer)
    private float InfectedToDyingTime;
    private float InfectedToDyingTimer;
    // 8. (*) infected NPC will contaminate a dorm immediately
    // 9. dying NPC has a explicit timer (progress bar) shown on top of head, if not treated with serum, will turn into zombie after 30 seconds
    private float DyingToZombieTimer;
    private float DyingToZombieTime;

    // UI
    private Progress_bar zombieProgress;
    private GameObject vaccinatedUI;

    // the NPC_Movement reference of this game object
    private NPC_Movement npcMovement;


    //setting NPC observation property
    private string[] targetColors = new string[5];
    public string[] actualColors = new string[] { null, null, null, null, null };

    private void Awake()
    {
        // this is where to set three timers
        NormalToInfectedTime = 5.0f;
        NormalToInfectedTimer = NormalToInfectedTime;

        InfectedToDyingTime = 60.0f;
        InfectedToDyingTimer = InfectedToDyingTime;

        DyingToZombieTime = 30.0f;
        DyingToZombieTimer = 30.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.Find("Timer_UI_NPC") != null)
        {
            zombieProgress = transform.Find("Timer_UI_NPC").gameObject.GetComponent<Progress_bar>();
        }

        if(type != NPC_Type.zombie)
        {
            vaccinatedUI = transform.Find("Vaccinated_UI_NPC").gameObject;
        }
        else
        {
            vaccinatedUI = null;
        }

        npcMovement = gameObject.GetComponent<NPC_Movement>();

        setNPCColor();


    }

    // Update is called once per frame
    void Update()
    {
        NormalToInfected();
        InfectedToDying();
        DyingToZombie();
        TimerUIDisplay();
        CheckLife();
        SetVaccinationUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // other npc touch zombie, they die
        if (this.type != NPC_Type.zombie && collision.gameObject.tag.Equals("Zombie"))
        {
            Die();
        }
    }



    // ---------- some state transition function -------------
    public void Die()
    {
        // disable collider and stuff and disable rigid body
        // play dead animation
        // destry object
        npcMovement.FreezePosAndDisableCol();
        npcMovement.isDying = true;
        Destroy(gameObject, 2);
    }


    private void CheckLife()
    {
        if (life <= 0)
        {
            Die();
        }
    }


    private void NormalToInfected()
    {
        if (infectedByRoom && type == NPC_Type.normal)
        {
            // timer start count down
            if (NormalToInfectedTimer >= 0)
            {
                NormalToInfectedTimer -= Time.deltaTime;
                return;
            }


            // when time's up, 0.5 chance set type to infected.
            if (Random.Range(0f, 1f) < 0.5f)
            {
                type = NPC_Type.infected;
                // this code was brought up from else
                NormalToInfectedTimer = NormalToInfectedTime;
                infectedByRoom = false;
                setNPCColor();
            }
            else
            {
                NormalToInfectedTimer = NormalToInfectedTime;
            }
        }
    }

    private void InfectedToDying()
    {
        // logic is the same as previous
        if (type == NPC_Type.infected)
        {
            // start the timer
            if (InfectedToDyingTimer >= 0)
            {
                InfectedToDyingTimer -= Time.deltaTime;
                return;
            }

            // when time's up, turn into dying type:
            type = NPC_Type.dying;
            InfectedToDyingTimer = InfectedToDyingTime;
            setNPCColor();
        }
    }

    private void DyingToZombie()
    {
        if (type == NPC_Type.dying)
        {
            if (DyingToZombieTimer >= 0)
            {
                DyingToZombieTimer -= Time.deltaTime;
                // also set the max and current for progress bar
                zombieProgress.current = DyingToZombieTime - DyingToZombieTimer;
                return;
            }

            // set animation transition
            npcMovement.FreezePosAndDisableCol();
            npcMovement.isBecomingZombie = true;
        }
    }

    public void CureBySerum()
    {
        // only can cure if infection phase is 1, might check this attribute in other places.
        // set type to normal
        SetNPCType(NPC_Type.normal);
        // reset timers
        InfectedToDyingTimer = InfectedToDyingTime;
        DyingToZombieTimer = DyingToZombieTime;
    }

    public void Vaccinate()
    {
        // might check NPC type. can only give vaccine to normal NPC (npc1)
        // might no need to check type since type is checked elsewhere (e.g. when applying the vaccine)
        isVaccinated = true;
    }

    private void SetVaccinationUI()
    {
        if(vaccinatedUI != null)
        {
            if (isVaccinated)
            {
                vaccinatedUI.SetActive(true);
            }
            else
            {
                vaccinatedUI.SetActive(false);
            }
        }

    }

    // ------------ Other helpers -----------
    private void TimerUIDisplay()
    {
        if (zombieProgress == null)
        {
            return;
        }

        if (type == NPC_Type.dying)
        {
            zombieProgress.gameObject.SetActive(true);
        }
        else
        {
            zombieProgress.gameObject.SetActive(false);
        }
    }


    // ----------- Getters and Setters --------------

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

    private void GenerateHeartRate()
    {



        int[] heartRateNormal = {Random.Range(70, 111), Random.Range(70, 111), Random.Range(70, 111),
                                Random.Range(70, 111), Random.Range(70, 111)};
        int[] heartRateInfected = {Random.Range(80, 121), Random.Range(80, 121), Random.Range(80, 121),
                                Random.Range(80, 121), Random.Range(111, 121)};
        int[] heartRateDying = {Random.Range(121, 181), Random.Range(121, 181), Random.Range(121, 181),
                                Random.Range(121, 181), Random.Range(121, 181)};
        int[] heartRateZombie = {Random.Range(200, 301), Random.Range(200, 301), Random.Range(200, 301),
                                 Random.Range(200, 301), Random.Range(200, 301)};


        switch (GetNPCType())
        {
            case NPC_Type.normal:

                heartRate = heartRateNormal;
                break;
            case NPC_Type.infected:

                heartRate = heartRateInfected;
                break;
            case NPC_Type.dying:

                heartRate = heartRateDying;
                break;
            case NPC_Type.zombie:

                heartRate = heartRateZombie;
                break;
        }


    }

    public string[] getActualColors()
    {
        return actualColors;
    }

    private void setNPCColor()
    {
        if (GetNPCType() == NPC_Type.normal)
        {

            targetColors[0] = "red";
            targetColors[1] = "yellow";
            targetColors[2] = "yellow";
            targetColors[3] = "green";
            targetColors[4] = "green";


        }
        if (GetNPCType() == NPC_Type.infected)
        {

            targetColors[0] = "red";
            targetColors[1] = "red";
            targetColors[2] = "yellow";
            targetColors[3] = "yellow";
            targetColors[4] = "green";
        }
        if (GetNPCType() == NPC_Type.dying)
        {

            targetColors[0] = "red";
            targetColors[1] = "red";
            targetColors[2] = "red";
            targetColors[3] = "yellow";
            targetColors[4] = "yellow";


        }

        if (GetNPCType() == NPC_Type.zombie)
        {

            targetColors[0] = "red";
            targetColors[1] = "red";
            targetColors[2] = "red";
            targetColors[3] = "red";
            targetColors[4] = "red";


        }

        for(int i = 0; i < 5; i++)
        {
            actualColors[i] = null;
        }

        for (int i = 0; i < 5; i++)
        {
            int actualColorsIndex = Random.Range(0, 5);
            while (actualColors[actualColorsIndex] == "red" || actualColors[actualColorsIndex] == "yellow" || actualColors[actualColorsIndex] == "green")
            {
                actualColorsIndex = Random.Range(0, 5);
            }
            actualColors[actualColorsIndex] = targetColors[i];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorm : MonoBehaviour
{
    private List<NPC_Logic> npcList;

    // add a particle system later. 
    public bool isDormInfected = false;
    public bool hasInfectedNPC = false;

    [SerializeField] private AudioClip clean;
    [SerializeField] private AudioSource source;

    //[SerializeField] private GameObject infectionParticle;
    private ParticleSystem infectionParticle_;

    private float infectionCheckTime = 10;
    private float infectionCheckTimer;

    // Start is called before the first frame update
    void Start()
    {

        infectionParticle_ = GetComponent<ParticleSystem>();
        // change the simulation speed of the particle system
        var main = infectionParticle_.main;
        main.simulationSpeed = 0.1f;

        npcList = GetComponent<Room_Area>().NPCList;

        infectionCheckTimer = infectionCheckTime;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRoomInfection();
        SetRoomInfection();
        InfectNPC();
    }

    private void FixedUpdate()
    {
        //check if play infection particle effect

        changeInfectionParticle();
    }

    private void SetRoomInfection()
    {
        if (hasInfectedNPC && !isDormInfected)
        {
            if (infectionCheckTimer > 0)
            {
                infectionCheckTimer -= Time.deltaTime;
                return;
            }

            // throw a dice and then set room is infected
            if (Random.Range(0f, 1f) < 0.5f)
            {
                isDormInfected = true;
            }
            else
            {
                infectionCheckTimer = infectionCheckTime;
            }
        }
    }

    // update里循环list，如果有npc的type不是normal，感染当前房间
    private void CheckRoomInfection()
    {
        foreach (NPC_Logic n in npcList)
        {
            if (n.GetNPCType() != NPC_Logic.NPC_Type.normal)
            {
                hasInfectedNPC = true;
                //isDormInfected = true;
                return;
            }
        }
        // 循环已经结束了，就可以把hasInfectedNPC给设置回false
        hasInfectedNPC = false;
    }

    private void InfectNPC()
    {
        if (isDormInfected)
        {
            foreach (NPC_Logic n in npcList)
            {
                if (n.GetNPCType() == NPC_Logic.NPC_Type.normal)
                {
                    // only infect npc if is not vaccinated
                    if(!n.isVaccinated)
                        n.infectedByRoom = true;
                }
            }
        }

    }


    public void Disinfect()
    {
        isDormInfected = false;
        source.PlayOneShot(clean);
    }

    private void changeInfectionParticle()
    {
        if (isDormInfected)
        {
            infectionParticle_.Play();
        }

        else
        {
            infectionParticle_.Stop();
            infectionParticle_.Clear();
        }

    }

}
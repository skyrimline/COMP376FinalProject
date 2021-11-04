using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorm : MonoBehaviour
{
    private List<NPC_Logic> npcList;

    // add a particle system later. 
    public bool isDormInfected = false;
    
    // Start is called before the first frame update
    void Start()
    {
        npcList = GetComponent<Room_Area>().NPCList;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRoomInfection();
    }

    // update里循环list，如果有npc的type不是normal，感染当前房间
    private void CheckRoomInfection()
    {
        foreach (NPC_Logic n in npcList)
        {
            if (n.GetNPCType() != NPC_Logic.NPC_Type.normal)
            {
                isDormInfected = true;
            }
        }
    }

    private void InfectNPC()
    {
        if (isDormInfected)
        {
            foreach (NPC_Logic n in npcList)
            {
                if(n.GetNPCType() == NPC_Logic.NPC_Type.normal)
                {
                    n.infectedByRoom = true;
                }
            }
        }

    }


    private void Disinfect()
    {
        isDormInfected = false;
    }

}

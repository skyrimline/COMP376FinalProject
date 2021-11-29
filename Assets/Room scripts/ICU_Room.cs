using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ICU_Room : MonoBehaviour, IDropHandler
{
    // a reference to room, to get the NPC list
    private Room_Area room;

    // a reference to vaccine lab, to alter the vaccine/serum count when successfully applied
    [SerializeField] private Vaccine_Lab vaccineLab;

    [SerializeField] private ParticleSystem gasParticleSystem_vaccine;
    [SerializeField] private ParticleSystem gasParticleSystem_serum;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);

        // only do action when room is not empty
        if(room.NPCList.Count > 0)
        {
            // safe to reference, a room only contains at most one npc
            NPC_Logic npc = room.NPCList[0];
            // play particle system once
            // vaccine or syrum num--
            if (eventData.pointerDrag.tag == "vaccine")
            {
                vaccineLab.DeductVaccine();
                gasParticleSystem_vaccine.Play();
            }
            else if(eventData.pointerDrag.tag == "serum")
            {
                vaccineLab.DeductSerum();
                gasParticleSystem_serum.Play();
            }

            // depend on NPC type, call the cure function.
            // if normal, vaccinate the NPC
            if(npc.GetNPCType() == NPC_Logic.NPC_Type.normal)
            {
                npc.Vaccinate();
            }
            else if(npc.GetNPCType() == NPC_Logic.NPC_Type.infected || npc.GetNPCType() == NPC_Logic.NPC_Type.dying)
            {
                npc.CureBySerum();
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        room = GetComponent<Room_Area>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

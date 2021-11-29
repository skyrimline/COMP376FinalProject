using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ObservationProperty : MonoBehaviour
{
    
    private string[] targetColors = new string[5];
    private string[] actualColors = new string[] {null, null, null, null, null};

    private NPC_Logic npclogicreference;
    // Start is called before the first frame update
    void Start()
    {
        npclogicreference = GetComponent<NPC_Logic>();
        if (npclogicreference.GetNPCType() == NPC_Logic.NPC_Type.normal)
        {

            targetColors[0] = "red";
            targetColors[1] = "yellow";
            targetColors[2] = "yellow";
            targetColors[3] = "green";
            targetColors[4] = "green";


        }
        if (npclogicreference.GetNPCType() == NPC_Logic.NPC_Type.infected)
        {

            targetColors[0] = "red";
            targetColors[1] = "red";
            targetColors[2] = "yellow";
            targetColors[3] = "yellow";
            targetColors[4] = "green";


        }
        if (npclogicreference.GetNPCType() == NPC_Logic.NPC_Type.dying)
        {

            targetColors[0] = "red";
            targetColors[1] = "red";
            targetColors[2] = "red";
            targetColors[3] = "yellow";
            targetColors[4] = "yellow";


        }
        if (npclogicreference.GetNPCType() == NPC_Logic.NPC_Type.zombie)
        {

            targetColors[0] = "red";
            targetColors[1] = "red";
            targetColors[2] = "red";
            targetColors[3] = "red";
            targetColors[4] = "red";


        }

        for (int i = 0; i < 5; i++)
        {
            int actualColorsIndex = Random.Range(0, 5);
            while (actualColors[actualColorsIndex] != null)
            {
                actualColorsIndex = Random.Range(0, 5);
            }
            actualColors[actualColorsIndex] = targetColors[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] getActualColors()
    {
        return actualColors;
    }
}

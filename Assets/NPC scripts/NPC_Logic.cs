using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Logic : MonoBehaviour
{
    // infectionPhase can be 1 or 2
    // 1 - NPC1, 2, 3 are all at phase 1. Phase 1 can be cured.
    // 2 - NPC4 (zombie) Phase 2 cannot be cured. Can only be killed.
    public int infectionPhase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

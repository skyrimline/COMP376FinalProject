using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_Box_Control : MonoBehaviour
{

    float dialogboxTimer = 0;

    [SerializeField] private GameObject outsideDialog1;

    [SerializeField] private GameObject insideDialog1;

    private int randomnumber;

    private NPC_Movement NPCMovementReference;

    // Start is called before the first frame update
    void Start()
    {
        NPCMovementReference = GetComponentInParent<NPC_Movement>();
        StartCoroutine("randomNumberGenerator");
    }

    // Update is called once per frame
    void Update()
    {
       
        //playOutsideDialog();
    }

    private void playDialog()
    {
        //play outside dialog 1
        if (!NPCMovementReference.isInRoom && randomnumber == 1)
        {
            outsideDialog1.SetActive(true);
        }

        //play inside dialog 1
        else if (NPCMovementReference.isInRoom && randomnumber == 1)
        {
            insideDialog1.SetActive(true);
        }

        //don't play any dialog
        else
        {
            outsideDialog1.SetActive(false);
            insideDialog1.SetActive(false);
        }
            
       
    }

    IEnumerator randomNumberGenerator()
    {
        while (true)
        {
            
            randomnumber = Random.Range(0, 2);

            playDialog();
            yield return new WaitForSeconds(2f);
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter_Room : MonoBehaviour
{
    // Start is called before the first frame update



    //To add resource image to the screen
    [SerializeField] private GameObject foatingResourcePrefab;
    [SerializeField] private Sprite[] resourceSprs;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
<<<<<<< Updated upstream
=======

    private void GenerateResources()
    {
        // food, money, vac_A, vac_B, vac_C
        int[] _resources = { Random.Range(5, 10), Random.Range(50, 100), Random.Range(3, 7), Random.Range(5, 10), Random.Range(3, 7)};
        resources = _resources;
        resourceIndex_1 = Random.Range(0, 5);
        resourceIndex_2 = Random.Range(0, 5);
    }

    private void CollectResources()
    {
        int resourceCount1 = resources[resourceIndex_1] * tempNPCList.Count;
        //Debug.Log("Collect Resource" + resourceIndex_1 + ": " + resourceCount1);
        switch (resourceIndex_1)
        {
            case 0:
                // give food
                gameLogicReference.foodNum += resourceCount1;
                break;
            case 1:
                // give money
                gameLogicReference.money += resourceCount1;
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += resourceCount1;
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += resourceCount1;
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += resourceCount1;
                break;
        }

        int resourceCount2 = resources[resourceIndex_2] * tempNPCList.Count;
        //Debug.Log("Collect Resource" + resourceIndex_2 + ": " + resourceCount2);
        switch (resourceIndex_2)
        {
            case 0:
                // give food
                gameLogicReference.foodNum += resourceCount2;
                break;
            case 1:
                // give money
                gameLogicReference.money += resourceCount2;
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += resourceCount2;
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += resourceCount2;
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += resourceCount2;
                break;
        }
        //First rescource image will show on the left 
        Vector3 pos_left = transform.position + new Vector3(-2f, 2, 0);
        GameObject notice1 = Instantiate(foatingResourcePrefab, pos_left, Quaternion.identity);
        notice1.GetComponent<SpriteRenderer>().sprite = resourceSprs[resourceIndex_1];
        notice1.GetComponent<FloatingResourceAnim>().numbertext.text = "+ " + resourceCount1.ToString();

        //second rescource image will show on the right
        Vector3 pos_right = transform.position + new Vector3(2f, 2f, 0);
        GameObject notice2 = Instantiate(foatingResourcePrefab, pos_right, Quaternion.identity);
        notice2.GetComponent<SpriteRenderer>().sprite = resourceSprs[resourceIndex_2];
        notice2.GetComponent<FloatingResourceAnim>().numbertext.text = "+ " + resourceCount2.ToString();
    }

    private void ShowSendOutButton()
    {
        // only show the button panel if there's someone in the room
        if(room.NPCList.Count > 0)
        {
            sendButtonPanel.SetActive(true);
        }
        else
        {
            sendButtonPanel.SetActive(false);
        }
    }

    private void UpdateTimerText()
    {
        if (returnTimerPanel.activeInHierarchy)
        {
            hunterTimerText.text = ((int)Mathf.Round(hunterTimer)).ToString("D3");
        }
    }

>>>>>>> Stashed changes
}

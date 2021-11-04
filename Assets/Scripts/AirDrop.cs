using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AirDrop : MonoBehaviour, IPointerDownHandler
{
    private int food_supply;
    private int money_supply;
    private int vaccineA_supply;
    private int vaccineB_supply;
    private int vaccineC_supply;
    private int supply_label1;
    private int supply_label2;

    private GameLogic gameLogicReference;

    private bool isClickable = false;

    // Start is called before the first frame update
    void Start()
    {
        //initialized the number of resources
        food_supply = Random.Range(5, 10);
        money_supply = Random.Range(50, 100);
        vaccineA_supply = Random.Range(3, 7);
        vaccineB_supply = Random.Range(5, 11);
        vaccineC_supply = Random.Range(1, 3);

        //initialized the labels of supply
         supply_label1 = Random.Range(0, 5);
         supply_label2 = Random.Range(0, 5);

        //
        gameLogicReference = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isClickable = true;
    }


    public void OnPointerDown(PointerEventData eventData)
    {

        if (!isClickable)
        {
            return;
        }
        
        //decide which supply this airDrop gives
        switch (supply_label1)
        {
            case 0:
                // give food
                gameLogicReference.foodNum += food_supply;
                break;
            case 1:
                // give money
                gameLogicReference.money += money_supply;
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += vaccineA_supply;
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += vaccineB_supply;
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += vaccineC_supply;
                break;

        }

        switch (supply_label2)
        {
            case 0:
                // give food
                gameLogicReference.foodNum += food_supply;
                break;
            case 1:
                // give money
                gameLogicReference.money += money_supply;
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += vaccineA_supply;
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += vaccineB_supply;
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += vaccineC_supply;
                break;

        }

        Destroy(gameObject);
    }
}

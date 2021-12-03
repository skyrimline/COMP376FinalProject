using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuffSell : MonoBehaviour
{
    [SerializeField] private Text Name;
    [SerializeField] private Text priceUI;
    [SerializeField] private Sprite ObjUI;

    [SerializeField] private int price = 5;// 5g for selling each item 
    [SerializeField] private GameObject ErrorMessagePrefab;
    [SerializeField] private GameObject FloatingResourcePrefab;


    [SerializeField] private float discount = 1;

    private GameLogic gameLogicReference;

    // Start is called before the first frame update
    void Start()
    {
        gameLogicReference = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        priceUI.text = (price * discount).ToString();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SellClick()
    {
        //check if the item is enough
        int NUMofItem = 0;
        switch (Name.text)
        {
            case "Food":
                // give food
                NUMofItem = gameLogicReference.foodNum;
                break;
            case "Food x6":
                // give money
                NUMofItem = gameLogicReference.foodNum;
                break;
            case "VaccineA":
                // give money
                NUMofItem = gameLogicReference.vaccineA_num;
                break;
            case "VaccineB":
                // give vaccineA
                NUMofItem = gameLogicReference.vaccineB_num;
                break;
            case "VaccineC":
                // give vaccineB
                NUMofItem = gameLogicReference.vaccineC_num;
                break;
        }
        // if number of this item is more than one
        if (NUMofItem > 0)
        {
            //Increase the money 
            GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>().money += price;
            //Decrease the item
            
            switch (Name.text)
            {
                case "Food":
                    // give food
                    gameLogicReference.foodNum -= 1;
                    break;
                case "Food x6":
                    // give food
                    if (NUMofItem < 6)
                    {
                        GenerateErrorMessage("You don't have item to sell");
                        return;
                    }
                 
                    gameLogicReference.foodNum -= 6;
                    break;
                case "VaccineA":
                    // give money
                    gameLogicReference.vaccineA_num -= 1;
                    break;
                case "VaccineB":
                    // give vaccineA
                    gameLogicReference.vaccineB_num -= 1;
                    break;
                case "VaccineC":
                    // give vaccineB
                    gameLogicReference.vaccineC_num -= 1;
                    break;
            }

            SpawnFloatingResourceInfo();
        }
        else
        {
            //Moey not enough 
            GenerateErrorMessage("You don't have item to sell");
        }
    }

    private void GenerateErrorMessage(string message)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z += 1f;
        //pos.y = 0f;
        GameObject g = Instantiate(ErrorMessagePrefab, pos, Quaternion.identity);
        var f = g.GetComponentInChildren<Floating_Info_Control>();
        if (f != null)
        {
            f.SetText(message);
        }
    }

    private void SpawnFloatingResourceInfo()
    {
        // need to spawn 2 of these
        Vector3 pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos1.z += 1f;
        //pos1.y = 0f;
        //pos1.x = 0f;
        GameObject g1 = Instantiate(FloatingResourcePrefab, pos1, Quaternion.identity);
        var f1 = g1.GetComponentInChildren<Floating_Info_Control>();
        if (f1 != null)
        {
            f1.SetText("-  ");
            f1.SetImage(ObjUI);
        }
    }
}

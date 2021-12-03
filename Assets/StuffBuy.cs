using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuffBuy : MonoBehaviour
{
    [SerializeField] private Text Name;
    [SerializeField] private Text priceUI;
    [SerializeField] private Sprite ObjUI;

    [SerializeField] private int price = 20;// 20g for purchase each item 
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

    public void Buyclick()
    {
        //check if the money is enough
        int money = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>().money;
        if (money >= price)
        {
            //Pruchase is available
            switch (Name.text)
            {
                case "Food":
                    // give food
                    gameLogicReference.foodNum += 1;
                    break;
                case "Food x6":
                    // give food x6
                    gameLogicReference.foodNum += 6;
                    break;
                case "VaccineA":
                    // give money
                    gameLogicReference.vaccineA_num += 1;
                    break;
                case "VaccineB":
                    // give vaccineA
                    gameLogicReference.vaccineB_num += 1;
                    break;
                case "VaccineC":
                    // give vaccineB
                    gameLogicReference.vaccineC_num += 1;
                    break;
                case "Vaccine3in1":
                    gameLogicReference.vaccineA_num += 1;
                    gameLogicReference.vaccineB_num += 1;
                    gameLogicReference.vaccineC_num += 1;
                    break;
            }
            //Decreasae the money 
            GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>().money = money - price;
            SpawnFloatingResourceInfo();
        }
        else
        {
            //Moey not enough 
            GenerateErrorMessage("You don't have enough money");
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
            f1.SetText("+ 1 ");
            f1.SetImage(ObjUI);
        }
    }
}

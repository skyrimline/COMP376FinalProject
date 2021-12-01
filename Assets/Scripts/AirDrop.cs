using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AirDrop : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip plan;

    private int supply_label1;
    private int supply_label2;

    private int[] supplys;

    private GameLogic gameLogicReference;

    private bool isClickable = false;

    [SerializeField] private GameObject FloatingResourcePrefab;
    [SerializeField] private Sprite[] resourceSprs;

    // Start is called before the first frame update
    void Start()
    {
        int food_supply;
        int money_supply;
        int vaccineA_supply;
        int vaccineB_supply;
        int vaccineC_supply;
        //initialized the number of resources
        food_supply = Random.Range(5, 10);
        money_supply = Random.Range(50, 100);
        vaccineA_supply = Random.Range(3, 7);
        vaccineB_supply = Random.Range(5, 11);
        vaccineC_supply = Random.Range(1, 3);
        int[] supplys_ = { food_supply, money_supply, vaccineA_supply, vaccineB_supply, vaccineC_supply};
        supplys = supplys_;

        //initialized the labels of supply
         supply_label1 = Random.Range(0, 5);
         supply_label2 = Random.Range(0, 5);

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
                gameLogicReference.foodNum += supplys[supply_label1];
                break;
            case 1:
                // give money
                gameLogicReference.money += supplys[supply_label1];
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += supplys[supply_label1];
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += supplys[supply_label1];
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += supplys[supply_label1];
                break;

        }

        switch (supply_label2)
        {
            case 0:
                // give food
                gameLogicReference.foodNum += supplys[supply_label2];
                break;
            case 1:
                // give money
                gameLogicReference.money += supplys[supply_label2];
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += supplys[supply_label2];
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += supplys[supply_label2];
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += supplys[supply_label2];
                break;

        }
        SpawnFloatingResourceInfo();
        Destroy(gameObject);
    }

    private void SpawnFloatingResourceInfo()
    {
        // need to spawn 2 of these
        Vector3 pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos1.z = 0;
        pos1.y += 1f;
        pos1.x -= 1f;
        GameObject g1 = Instantiate(FloatingResourcePrefab, pos1, Quaternion.identity);
        var f1 = g1.GetComponentInChildren<Floating_Info_Control>();
        if (f1 != null)
        {
            f1.SetText("+" + supplys[supply_label1].ToString("D3"));
            f1.SetImage(resourceSprs[supply_label1]);
        }

        Vector3 pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos2.z = 0;
        pos2.y += 1f;
        pos2.x += 3f;
        GameObject g2 = Instantiate(FloatingResourcePrefab, pos2, Quaternion.identity);
        var f2 = g2.GetComponentInChildren<Floating_Info_Control>();
        if (f2 != null)
        {
            f2.SetText("+" + supplys[supply_label2].ToString("D3"));
            f2.SetImage(resourceSprs[supply_label2]);
        }
    }
}

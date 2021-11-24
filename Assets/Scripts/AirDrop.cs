using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private int NUMofLabel1;
    private int NUMofLabel2;

    //To add resource image to the screen
    [SerializeField] private GameObject foatingResourcePrefab;
    [SerializeField] private Sprite[] resourceSprs;

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
                NUMofLabel1 = food_supply;
                break;
            case 1:
                // give money
                gameLogicReference.money += money_supply;
                NUMofLabel1 = money_supply;
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += vaccineA_supply;
                NUMofLabel1 = vaccineA_supply;
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += vaccineB_supply;
                NUMofLabel1 = vaccineB_supply;
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += vaccineC_supply;
                NUMofLabel1 = vaccineC_supply;
                break;

        }

        switch (supply_label2)
        {
            case 0:
                // give food
                gameLogicReference.foodNum += food_supply;
                NUMofLabel2 = food_supply;
                break;
            case 1:
                // give money
                gameLogicReference.money += money_supply;
                NUMofLabel2 = money_supply;
                break;
            case 2:
                // give vaccineA
                gameLogicReference.vaccineA_num += vaccineA_supply;
                NUMofLabel2 = vaccineA_supply;
                break;
            case 3:
                // give vaccineB
                gameLogicReference.vaccineB_num += vaccineB_supply;
                NUMofLabel2 = vaccineB_supply;
                break;
            case 4:
                // give vaccineC
                gameLogicReference.vaccineC_num += vaccineC_supply;
                NUMofLabel2 = vaccineC_supply;
                break;

        }

        //First rescource image will show on the left 
        Vector3 pos_left = transform.position + new Vector3(-2f, 2, 0);
        GameObject notice1 = Instantiate(foatingResourcePrefab, pos_left, Quaternion.identity);
        notice1.GetComponent<SpriteRenderer>().sprite = resourceSprs[supply_label1];
        notice1.GetComponent<FloatingResourceAnim>().numbertext.text = "+ " + NUMofLabel1.ToString();

        //second rescource image will show on the right
        Vector3 pos_right = transform.position + new Vector3(2f, 2f, 0);
        GameObject notice2 = Instantiate(foatingResourcePrefab, pos_right, Quaternion.identity);
        notice2.GetComponent<SpriteRenderer>().sprite = resourceSprs[supply_label2];
        notice2.GetComponent<FloatingResourceAnim>().numbertext.text = "+ " + NUMofLabel2.ToString();


        Destroy(gameObject);
    }
    public int getNUMofLabel1()
    {
        return NUMofLabel1;
    }
    public int getNUMofLabel2()
    {
        return NUMofLabel2;
    }

}

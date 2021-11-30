using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Room_Upgradable : MonoBehaviour
{
    //To show current Num of beds and avliable next level
    [SerializeField] private Text CurrentBedText;
    [SerializeField] private Text NextBedText;
    [SerializeField] private Text CostText;

    [SerializeField] private GameObject UpgradeMenu;
    [SerializeField] private int TotoalLevels = 6; //The upper limit of the capacity

    [SerializeField] private GameObject ErrorMessagePrefab;

    //Upgrad setting:
    //Default Levels: 6
    //Default Capacity (level 1): 1  Cost: 10g
    //Level 2 Capacity: 2   Cost:20g
    //Level 3 Capacity: 3   Cost:30g
    //Level 4 Capacity: 4   Cost:40g
    //Level 5 Capacity: 5   Cost:50g
    //Level 6 Capacity: 6   Cost:60g

    private bool Menueshow = false;
    private int Capacity;
    private int cost;

    // Start is called before the first frame update
    void Start()
    {
        UpgradeMenu.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Capacity = GetComponent<Room_Area>().GetCapacity();
        cost = Capacity * 10;
        CostText.text = (cost).ToString();

        //If the Upgrade is using
        if (Menueshow)
        {
            //the Upgrade panel will shows
            UpgradeMenu.transform.GetChild(0).gameObject.SetActive(true);

            //Current num of bed will use the getter
            CurrentBedText.text = (Capacity).ToString();
            if (Capacity < TotoalLevels) //If upgradable
            {
                //Next level will add one to current
                NextBedText.text = (Capacity + 1).ToString();
            }
            else if (Capacity == TotoalLevels) //If not upgradable
            {
                //Next level will keep the same
                NextBedText.text = (Capacity).ToString();
            }

        }
        else if (!Menueshow)
        {
            //the upgrade panel will not show
            UpgradeMenu.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    //Click to change open or close the panel
    public void UpgradeMenuClick()
    {
        Menueshow = !Menueshow;
    }

    //Click the upgrade button
    public void UpgradeClick()
    {
        //Number of upgrades (which level we are now) and capacity synchronization
        //Find the money player has and the cost is the current level times 10
        int money = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>().money;


        if (Capacity < TotoalLevels && cost <= money)
        {
            //click the upgrad button, the capacity will add one
            Capacity++;
            //update the room cap and money remains
            GetComponent<Room_Area>().SetCapacity(Capacity);
            GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>().money = money - cost;

        }
        else if (Capacity == TotoalLevels)//Showing No more upgrade
        {
            // send error message here
            GenerateErrorMessage("Room capacity has been maximized");
        }
        else if (cost > money)
        {
            // error message here
            GenerateErrorMessage("You don't have enough money");
        }

    }

    public int GetTotalLevels()
    {
        return TotoalLevels;
    }

    private void GenerateErrorMessage(string message)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        pos.y += 1f;
        GameObject g = Instantiate(ErrorMessagePrefab, pos, Quaternion.identity);
        var f = g.GetComponentInChildren<Floating_Info_Control>();
        if(f != null)
        {
            f.SetText(message);
        }
    }
}
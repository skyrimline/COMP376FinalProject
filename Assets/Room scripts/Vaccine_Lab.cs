using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vaccine_Lab : MonoBehaviour
{
    private int vaccineNum = 0;
    private int serumNum = 0;

    [SerializeField] Text vaccineNumText;
    [SerializeField] Text serumNumText;

    private GameLogic gameLogic;

    [SerializeField] private float vaccineTimeOriginal = 10;
    private float vaccineTime;
    [SerializeField] private float serumTimeOriginal = 18;
    private float serumTime;
    [SerializeField] private float maxDiscount = 8;
    private float timer;

    private bool isSerumProducing = false;
    private bool isVaccineProducing = false;

    [SerializeField] private Progress_bar progress;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject TimerPanelObj;

    [SerializeField] private GameObject ErrorMessagePrefab;

    private int maxProductivity;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        // set max productivity
        GameObject[] tempRooms = GameObject.FindGameObjectsWithTag("room_dorm");
        maxProductivity = tempRooms.Length * tempRooms[0].GetComponent<Room_Upgradable>().GetTotalLevels();

        vaccineTime = vaccineTimeOriginal;
        serumTime = serumTimeOriginal;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNumber();

        if (isVaccineProducing || isSerumProducing)
        {
            TimerPanelObj.SetActive(true);

            if(isVaccineProducing)
                ProduceVaccineCountDown();
            if(isSerumProducing)
                ProduceSerumCountDown();
        }
        else
        {
            TimerPanelObj.SetActive(false);
        }

        CalculateProducingSpeed();
    }

    private void ProduceVaccineCountDown()
    {
        // update timer here.
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            progress.current = timer;
            timerText.text = ((int)timer).ToString("D2");
            return;
        }

        // when timer stops, vaccine num++
        vaccineNum++;
        isVaccineProducing = false;

    }

    // two functions for clicking and producing
    public void ProduceVaccine()
    {
        if (isVaccineProducing || isSerumProducing)
        {
            // generate error message
            GenerateErrorMessage("Lab In Production!");
            return;
        }
        
        if(gameLogic.vaccineA_num > 0 && gameLogic.vaccineB_num > 0)
        {
            gameLogic.vaccineA_num -= 1;
            gameLogic.vaccineB_num -= 1;
        }
        else
        {
            // generate error message: no enough material
            GenerateErrorMessage("You don't have enough material!");
            return;
        }
        // start timer
        timer = vaccineTime;
        isVaccineProducing = true;
        progress.max = vaccineTime;

    }

    private void ProduceSerumCountDown()
    {
        // update timer here.
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            progress.current = timer;
            timerText.text = ((int)timer).ToString("D2");
            return;
        }

        // when timer stops, vaccine num++
        serumNum++;
        isSerumProducing = false;
    }

    public void ProduceSerum()
    {
        if (isVaccineProducing || isSerumProducing)
        {
            GenerateErrorMessage("Lab In Production!");
            return;
        }
        
        // if there's still enough material, deduct material and num ++
        if (gameLogic.vaccineA_num > 0 && gameLogic.vaccineB_num > 0 && gameLogic.vaccineC_num > 0)
        {
            gameLogic.vaccineA_num -= 3;
            gameLogic.vaccineB_num -= 3;
            gameLogic.vaccineC_num -= 3;
        }
        else
        {
            // generate error message
            GenerateErrorMessage("You don't have enough material!");
            return;
        }

        timer = serumTime;
        isSerumProducing = true;
        progress.max = serumTime;
    }

    // adjust the producing speed according to productivity from game logic scirpt
    private void CalculateProducingSpeed()
    {
        int discount = (int)(((float)gameLogic.productivity / maxProductivity) * maxDiscount);
        vaccineTime = vaccineTimeOriginal - discount;
        serumTime = serumTimeOriginal - discount;
    }

    private void UpdateNumber()
    {
        vaccineNumText.text = vaccineNum.ToString("D2");
        serumNumText.text = serumNum.ToString("D2");
    }


    public int GetVaccineNum()
    {
        return vaccineNum;
    }

    public int GetSerumNum()
    {
        return serumNum;
    }

    public void DeductVaccine()
    {
        vaccineNum--;
    }

    public void DeductSerum()
    {
        serumNum--;
    }


    private void GenerateErrorMessage(string message)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        pos.y += 1f;
        GameObject g = Instantiate(ErrorMessagePrefab, pos, Quaternion.identity);
        var f = g.GetComponentInChildren<Floating_Info_Control>();
        if (f != null)
        {
            f.SetText(message);
        }
    }
}

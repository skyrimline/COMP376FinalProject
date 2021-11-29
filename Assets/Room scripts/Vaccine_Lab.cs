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

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNumber();
    }

    // two functions for clicking and producing
    public void ProduceVaccine()
    {
        // if there's still enough material, deduct material and num ++
        if(gameLogic.vaccineA_num > 0 && gameLogic.vaccineB_num > 0)
        {
            gameLogic.vaccineA_num--;
            gameLogic.vaccineB_num--;

            vaccineNum++;
        }
    }

    public void ProduceSerum()
    {
        // if there's still enough material, deduct material and num ++
        if (gameLogic.vaccineA_num > 0 && gameLogic.vaccineB_num > 0 && gameLogic.vaccineC_num > 0)
        {
            gameLogic.vaccineA_num--;
            gameLogic.vaccineB_num--;
            gameLogic.vaccineC_num--;

            serumNum++;
        }
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

}

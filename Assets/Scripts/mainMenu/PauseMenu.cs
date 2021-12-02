using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    private bool isTimeStoppedBefore;

    void Start()
    {
        isPaused = false;
    }
        // Update is called once per frame
    void Update()
    {
        
        
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                isTimeStoppedBefore = true;
            }
            else
            {
                isTimeStoppedBefore = false;
            }
            Time.timeScale = 0;
            transform.GetChild(0).gameObject.SetActive(true);
            isPaused = true;
        }
        else if(isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isTimeStoppedBefore)
            {
                Time.timeScale = 1;
            }
            transform.GetChild(0).gameObject.SetActive(false);
            isPaused = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{

    public bool ignoreEsc = false;

    // Update is called once per frame
    void Update()
    {
        if (!ignoreEsc && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Skip()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}

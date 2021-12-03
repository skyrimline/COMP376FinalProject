using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void ChangeSceneClick(string sname)
    {
        SceneManager.LoadScene(sname);
    }

    public void QuitClick()
    {
       Application.Quit();
    }


}

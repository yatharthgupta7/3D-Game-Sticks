using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    public void start(int levelindex)
    {
        SceneManager.LoadScene(levelindex);
    }

    public void quitgame()
    {
        Application.Quit();
    }

    public void sound()
    {

    }

    public void shop(int shopindex)
    {
        SceneManager.LoadScene(shopindex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    GameObject sfx;
    public Text soundText;
    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        if (PlayerPrefs.GetFloat("Volume") == 0)
            PlayerPrefs.SetFloat("Volume", 1);

        if (PlayerPrefs.GetFloat("Volume") == 0.1f)
        {
            sfx.GetComponent<AudioSource>().volume = 0f;
            soundText.text = "Sound:ON";
        }
        if (PlayerPrefs.GetFloat("Volume") == 1f)
        {
            sfx.GetComponent<AudioSource>().volume = 0.5f;
            soundText.text = "Sound:OFF";
        }

    }
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
        if (PlayerPrefs.GetFloat("Volume") == 0.1f)
        {
            sfx.GetComponent<AudioSource>().volume = 0.5f;
            PlayerPrefs.SetFloat("Volume", 1);
            soundText.text = "Sound:OFF";
        }
        else
        {
            sfx.GetComponent<AudioSource>().volume = 0.5f;
            PlayerPrefs.SetFloat("Volume", 0.1f);
            soundText.text = "Sound:ON";
        }
    }

    public void shop(int shopindex)
    {
        SceneManager.LoadScene(shopindex);
    }
}

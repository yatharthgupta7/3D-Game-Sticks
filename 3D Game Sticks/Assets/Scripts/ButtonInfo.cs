using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int itemID;
    public GameObject priceTxt;

    private void Awake()
    {
        if(PlayerPrefs.GetInt(gameObject.name) == 0)
        {
            PlayerPrefs.SetInt(gameObject.name, 0);
        }

        if(PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }


    public void deselect()
    {
        if(itemID==PlayerPrefs.GetInt("LastSelectedPlayer"))
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}

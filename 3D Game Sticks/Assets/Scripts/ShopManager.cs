using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4];
    public GameObject[] selectText;
    public Text Coins;
    public GameObject WarningTXT;
    // Start is called before the first frame update
    void Start()
    {
        
       if(PlayerPrefs.GetInt("LastSelectedGun") == 0)
       {
            PlayerPrefs.SetInt("LastSelectedGun", 1);
       }

        selectText[PlayerPrefs.GetInt("LastSelectedGun") - 1].SetActive(true);

        WarningTXT.SetActive(false);
        Coins.text = PlayerPrefs.GetInt("TotalCoins").ToString();
       
        //Item ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;

        //Item price
        shopItems[2, 1] = 0;
        shopItems[2, 2] = 1000;
        shopItems[2, 3] = 2000;

        //Item purchased status
        shopItems[3, 1] = 1;
        shopItems[3, 2] = PlayerPrefs.GetInt("Item 2");
        shopItems[3, 3] = PlayerPrefs.GetInt("Item 3");

    }

    public void backtomain()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Buy()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        
        if (shopItems[3 , buttonRef.GetComponent<ButtonInfo>().itemID] == 0)
        {
            if(PlayerPrefs.GetInt("TotalCoins") >= shopItems[2, buttonRef.GetComponent<ButtonInfo>().itemID])
            {
                shopItems[3, buttonRef.GetComponent<ButtonInfo>().itemID] = 1;
                PlayerPrefs.SetInt(buttonRef.name, 1);
                PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - shopItems[2, buttonRef.GetComponent<ButtonInfo>().itemID]);
                Coins.text = PlayerPrefs.GetInt("TotalCoins").ToString();
                Destroy(buttonRef.GetComponent<ButtonInfo>().priceTxt);
            }
            else
            {
                WarningTXT.SetActive(true);
                Invoke("hidetxt", 2);
                return;
            }
        }

        if(shopItems[3, buttonRef.GetComponent<ButtonInfo>().itemID] == 1 && buttonRef.GetComponent<ButtonInfo>().itemID != PlayerPrefs.GetInt("LastSelectedPlayer"))
        {
            selectText[PlayerPrefs.GetInt("LastSelectedGun") - 1].SetActive(false);

            selectText[buttonRef.GetComponent<ButtonInfo>().itemID - 1].SetActive(true);
            PlayerPrefs.SetInt("LastSelectedGun", buttonRef.GetComponent<ButtonInfo>().itemID);
        }
        
    }

    void hidetxt()
    {
        WarningTXT.SetActive(false);
    }

}

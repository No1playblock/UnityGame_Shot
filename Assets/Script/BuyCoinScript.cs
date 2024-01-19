using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuyCoinScript : MonoBehaviour {
    public Text coinText;
    public Text GemText;

    public GameObject GemStore;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void BuyCoin()
    {
        int gem, coin;
        if (EventSystem.current.currentSelectedGameObject.name.Equals("10Gem"))
        {
            gem = 10;
            coin = 1000;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("30Gem"))
        {
            gem = 30;
            coin = 3300;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("50Gem"))
        {
            gem = 50;
            coin = 5800;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("100Gem"))
        {
            gem = 100;
            coin = 12500;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("300Gem"))
        {
            gem = 300;
            coin = 41500;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("500Gem"))
        {
            gem = 500;
            coin = 77500;
        }
        else
        {
            gem = 0;
            coin = 0;
        }
        if (PlayerPrefs.GetInt("Gem") >= gem)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + coin);
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") - gem);
            coinText.text = "" + PlayerPrefs.GetInt("Coin");      //coin
            GemText.text = "" + PlayerPrefs.GetInt("Gem");
        }
        else
        {
            GemStore.SetActive(true);
        }
    }
}

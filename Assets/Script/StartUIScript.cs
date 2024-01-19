using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartUIScript : MonoBehaviour {

    //Screen.width <= 현재 해상도 가로     960f
    //Screen.width <= 현재 해상도 세로     540f    
    public Text coinText;
    public Text GemText;

    public GameObject QuestionPanel;
    public GameObject GemStore;

    public AudioSource CloseTabSFX;
    //public Scrollbar SFXScrollBar;
    private void Awake()
    {
        //coinText = GameObject.Find("CoinText").GetComponent<Text>();
        //GemText = Game
        if (PlayerPrefs.HasKey("Coin") == false)                    //게임이 시작할때
        {
            
            PlayerPrefs.SetInt("Coin", 0);
            PlayerPrefs.SetInt("HaveOak", 1);
            PlayerPrefs.SetString("Target", "Target_Oak");
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("Gem") == false)
        {
            PlayerPrefs.SetInt("Gem", 0);
        }

        coinText.text = "" + PlayerPrefs.GetInt("Coin");      //coin
        GemText.text = "" + PlayerPrefs.GetInt("Gem");
    }
    void Start()
    {
        CloseTabSFX.volume = PlayerPrefs.GetFloat("SFX");
    }
    void Update()
    {
        
    }
   /* public void BuyCoin()
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
    }*/

    IEnumerator LoadScene()
    {

        yield return null;
        AsyncOperation ao = SceneManager.LoadSceneAsync("PlayScene");
        ao.allowSceneActivation = false;

    }
    public void coinUp()
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 500);
        PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 500);
        PlayerPrefs.Save();
        
    }

    public void OnchangeSFX()
    {
        CloseTabSFX.volume = PlayerPrefs.GetFloat("SFX");
    }
    
    
    public void OnclickModeBtn()
    {
        StartCoroutine(BiggerScale());

    }
    IEnumerator BiggerScale()
    {
        QuestionPanel.SetActive(true);
        while (QuestionPanel.transform.localScale != new Vector3(1.0f, 1.0f, 1.0f))
        {
            yield return null;
            QuestionPanel.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
    public void OnclickNoBtn()
    {
        StartCoroutine(SmallerScale());
        CloseTabSFX.Play();
    }
    IEnumerator SmallerScale()
    {
        while (QuestionPanel.transform.localScale != new Vector3(0.0f, 0.0f, 0.0f))
        {
            yield return null;
            QuestionPanel.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        QuestionPanel.SetActive(false);
    }
}

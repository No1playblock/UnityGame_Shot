using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnchangeScript : MonoBehaviour {

    public GameObject GameOver;
    ManyScript many;
    SpeedScript speed;
    public AudioSource GameOverSFX;

    public void OnChangeScore()                                 
    {
        
        AllWorldScript.Instance.ScoreText.text = "Score : " + AllWorldScript.score;
        if (AllWorldScript.score == AllWorldScript.result && AllWorldScript.score != 0)        //다맞추면
        {
            if (AllWorldScript.mode.Equals("Many"))
            {
                Debug.Log("Many");
                GameObject.Find("GameMgr").GetComponent<ManyScript>().RoundSystem();
            }
                
            else
                GameObject.Find("GameMgr").GetComponent<SpeedScript>().RoundSystem();
            GameObject.Find("GameMgr").GetComponent<ObjectScript>().ExecuteSpawn();
        }
        else if (AllWorldScript.bulletcount == 0 && AllWorldScript.score != AllWorldScript.result && GameOver.activeSelf == false)       //게임오버되면
        {
            Debug.Log("score : " + AllWorldScript.score + "result : " + AllWorldScript.result);
            Debug.Log("bulletcount : " + AllWorldScript.bulletcount);
            gameOverFunc();
            OnchangeCoin();
        }
    }
    public void OnchangeCoin()                  //플레이어가 가지고 있는 코인의 수 startscene에서
    {
      
        //Debug.Log(PlayerPrefs.GetInt("Coin"));
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + AllWorldScript.coin);
        PlayerPrefs.Save();
        
    }
    public void gameOverFunc()
    {
        GameOver.SetActive(true);
        GameOverSFX.volume = PlayerPrefs.GetFloat("SFX");
        GameOverSFX.Play();
        Debug.Log("gameover");
        
        if (PlayerPrefs.HasKey("ManyHighScore") == false && AllWorldScript.mode.Equals("Many"))             //저장된 키값이 없을때
        {
            PlayerPrefs.GetInt("ManyHighScore", 0);
            PlayerPrefs.Save();
            Debug.Log("생성");
        }
        else if(PlayerPrefs.HasKey("SpeedHighScore") == false && AllWorldScript.mode.Equals("Speed"))       //저장된 키값이 없을때
        {
            PlayerPrefs.GetInt("SpeedHighScore", 0);
            PlayerPrefs.Save();
            Debug.Log("생성");
        }
        if (AllWorldScript.mode.Equals("Many") && AllWorldScript.score > PlayerPrefs.GetInt("ManyHighScore"))   //highscore 바꾸기
        {
            // AllWorldScript.highScore = AllWorldScript.score;
            PlayerPrefs.SetInt("ManyHighScore", AllWorldScript.score);
            PlayerPrefs.Save();
            Debug.Log("HighScore change");
        }
        else if (AllWorldScript.mode.Equals("Speed") && AllWorldScript.score > PlayerPrefs.GetInt("SpeedHighScore"))    //highscore 바꾸기
        {
            // AllWorldScript.highScore = AllWorldScript.score;
            PlayerPrefs.SetInt("SpeedHighScore", AllWorldScript.score);
            PlayerPrefs.Save();
            Debug.Log("HighScore change");
        }

        AllWorldScript.Instance.gameOverScoreText.text = "-" + AllWorldScript.score + "-";
        AllWorldScript.Instance.gameOverCoinText.text = "-" + AllWorldScript.coin + "-";
        if (AllWorldScript.mode.Equals("Many"))                                         //highscore가 바뀌지 않을때도 있기 때문에 위 if 문에 넣지 않았다.
        {
            AllWorldScript.Instance.gameOverHighScoreText.text = "-" + PlayerPrefs.GetInt("ManyHighScore") + "-";      //many highscore
        }
        else if (AllWorldScript.mode.Equals("Speed"))
        {
            AllWorldScript.Instance.gameOverHighScoreText.text = "-" + PlayerPrefs.GetInt("SpeedHighScore") + "-";      //speed highscore
        }
    }
    public void OnchangeRound()
    {
        AllWorldScript.Instance.RoundText.text = "제 " + AllWorldScript.round + "라운드";
    }

    public void OnChangeBulletCount()
    {

        AllWorldScript.bulletcount--;
        Debug.Log("bulletcount : " + AllWorldScript.bulletcount);
        AllWorldScript.Instance.BulletText.text = "" + AllWorldScript.bulletcount;


    }
}

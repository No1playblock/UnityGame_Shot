using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//모드선택, 텍스트 출력, 설정창 카운트


public class UIScript : MonoBehaviour {
    //public int bulletcount = 0;
    public Text countText ;
    public Text RoundText;

    

    public Scrollbar SFXScrollBar;

    public GameObject GameMgr;
    
    // Use this for initialization
  
    // Update is called once per frame
    void Awake () {
        //int count = 1;
        
        SpeedScript spmodecheck = GameMgr.GetComponent<SpeedScript>();
        ManyScript mnmodecheck = GameMgr.GetComponent<ManyScript>();
        spmodecheck.enabled = false;
        mnmodecheck.enabled = false;
        AllWorldScript.coin = 0;
        //AllWorldScript.Instance.Start();
        AllWorldScript.Instance.ScoreText.text = "Score : " + AllWorldScript.score;         //총쏘기 전(OnChange 전) 스코어와 코인 표시
        AllWorldScript.Instance.CoinText.text = "" + AllWorldScript.coin;


        if (AllWorldScript.mode.Equals("Speed"))        //speed game 이면 
        {
            //AllWorldScript.bulletcount = 1;
            //SpeedScript modecheck = GameMgr.GetComponent<SpeedScript>();     //speedscript 켜기
            spmodecheck.enabled = true;
            AllWorldScript.Instance.BulletText.text = "" + 1;

            Debug.Log("Speed");

            Debug.Log("success");

            
        }
        
        else if(AllWorldScript.mode.Equals("Many"))
        {
            
            //ManyScript modecheck = GameMgr.GetComponent<ManyScript>();     //manyscript 켜기
            mnmodecheck.enabled = true;
            AllWorldScript.Instance.BulletText.text = "" + 5;
            Debug.Log("Many");
            Debug.Log("success");
        }
        
        
    }
    void Start()
    {
        AllWorldScript.Instance.RoundClearSFX.volume = PlayerPrefs.GetFloat("SFX");
        AllWorldScript.shot.volume = PlayerPrefs.GetFloat("SFX");  //소리조절
        
    }
    void Update()
    {
        
    }
    public void OnchangeSFX()                      //optionpanel에서 x 버튼이 눌릴때 호출
    {
        AllWorldScript.Instance.RoundClearSFX.volume = PlayerPrefs.GetFloat("SFX");
        AllWorldScript.shot.volume = PlayerPrefs.GetFloat("SFX");

    }
    public void OnclickPause()
    {
        Time.timeScale = 0;   
    }

    public void OnclickX()
    {
        AllWorldScript.XbuttonCheck = true;
        StartCoroutine(AllWorldScript.Instance.Count());
        AllWorldScript.XbuttonCheck = false;

    }

    public void OnclickRestart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //AllWorldScript.optionBtnCheck = false;                        //Awake 에 넣어놨다.
        AllWorldScript.Instance.restart();                //timescale = 1,  UI와 result 값 0으로 초기화


    }
    
    public void OnclickQuit()
    {
        AllWorldScript.Instance.restart();
        SceneManager.LoadScene("StartScene");

    }



}

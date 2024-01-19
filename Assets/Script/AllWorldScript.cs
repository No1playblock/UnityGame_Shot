using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllWorldScript : MonoBehaviour
{
    //public Text ScoreText;
    private static AllWorldScript _instnce = null;
    public static int score = 0;
    public static int bulletcount = 0;
    public static string mode = "";
    public static int objectnum = 0;            //생성될 오크통의 개수
    public static bool start = false;
    public static int round = 0;
    public static int coin = 0;
    public static bool roundplus = true;
    public static int highScore = 0;
   // public static bool modecheck = false;       //many는 1 speed는 0
    public static float oakspeed = 500.0f;
    public static int result = 0;
    public static float destroycount = 4.0f;
    public Text CountText;
    public Text ExplainText;
    public Text ScoreText;
    public Text CoinText;
    public Text RoundText;
    public Text BulletText;
    public Text gameOverScoreText;
    public Text gameOverHighScoreText;
    public Text gameOverCoinText;


    public static bool XbuttonCheck =false;                     //????      뭔가 X버튼을 누르는 과정에서 count함수와 충돌이 있었던듯 하다
    public static Animator anim;
    public static AudioSource shot;
    public static string targetName;
    public static int targetCost;
    public static float ObjectCount = 6.0f;
    //
    //public AudioClip reload;
    public AudioClip Shoot;
    public AudioSource RoundClearSFX;
    public static float ExplainCount = 2.5f;                //설명 띄우는 시간
    public GameObject Explainpanel;
    public static AllWorldScript Instance
    {
        get
        {
            if(_instnce == null)
            {
                _instnce = FindObjectOfType(typeof(AllWorldScript)) as AllWorldScript;

                if (_instnce == null)
                {
                    Debug.LogError("There's no active AllWorldScript object");
                }
            }
            return _instnce;
        }
    }

    

    public IEnumerator Count()
    {
        CountText.raycastTarget = true;                 //true는 터치 인식
        //Time.timeScale = 1;
        if (!XbuttonCheck)                              //xbutton 누른게 아니면
        {   
            yield return new WaitForSeconds(ExplainCount);  //설명 띄우는 텀
        }
        
        Time.timeScale = 0;
        CountText.text = "3";
        yield return new WaitForSecondsRealtime(1.0f);
        CountText.text = "2";
        yield return new WaitForSecondsRealtime(1.0f);
        CountText.text = "1";
        yield return new WaitForSecondsRealtime(1.0f);
        CountText.text = "";

        CountText.raycastTarget = false;            //터치 무시
        Time.timeScale = 1;
        
    }
    public IEnumerator manyExplain()
    {
        //ExplainText.raycastTarget = true; 
        yield return new WaitForSeconds(0.4f);
        RoundClearSFX.Play();
        Explainpanel.SetActive(true);
        ExplainText.text = bulletcount + "발의 총알로 \n" + objectnum + "개의 오크통을 맞추세요";
        yield return new WaitForSeconds(1.8f);
        Explainpanel.SetActive(false);
        ExplainText.text = "";
        

    }

    public IEnumerator speedExplain()
    {
        yield return new WaitForSeconds(0.4f);
        Explainpanel.SetActive(true);
        ExplainText.text = bulletcount + "발의 총알로 \n" + oakspeed+ "속도의 타겟" + objectnum + "개를 맞추세요";
        yield return new WaitForSeconds(1.8f);
        Explainpanel.SetActive(false);
        ExplainText.text = "";

    }



    public void restart()
    {
        Time.timeScale = 1;
        AllWorldScript.score = 0;
        AllWorldScript.round = 0;
        AllWorldScript.coin = 0;
        AllWorldScript.result = 0;
        AllWorldScript.oakspeed = 500.0f;
    }


}
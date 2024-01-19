using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedScript : MonoBehaviour {
    
    
    public GameObject GameOver;
    

    public void RoundSystem()
    {

        StartCoroutine(AllWorldScript.Instance.Count());

        AllWorldScript.round++;                                         //라운드를 증가

        GameObject.Find("GameMgr").GetComponent<OnchangeScript>().OnchangeRound();

        AllWorldScript.bulletcount ++;          //총알 개수를 증가
        AllWorldScript.oakspeed += 300.0f;
        AllWorldScript.objectnum = 1;
        Debug.Log(AllWorldScript.objectnum);
        StartCoroutine(AllWorldScript.Instance.speedExplain());
        AllWorldScript.result = AllWorldScript.round * 10;
        AllWorldScript.Instance.BulletText.text = "" + AllWorldScript.bulletcount;         //라운드가 끝나고 재시작할때 총알개수 변경

    }

}

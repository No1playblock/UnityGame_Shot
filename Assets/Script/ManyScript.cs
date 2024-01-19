using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManyScript : MonoBehaviour {


    // Update is called once per frame
    private int count = 0;

    public void RoundSystem()
    {
        Debug.Log("ManyRoundSystem");
        StartCoroutine(AllWorldScript.Instance.Count());
        
        AllWorldScript.round++;                                         //라운드를 증가

        GameObject.Find("GameMgr").GetComponent<OnchangeScript>().OnchangeRound();
        if (AllWorldScript.round % 3 == 1)
        {
            count++;
            AllWorldScript.bulletcount = count * 5 +5;
            
        }
        else if(AllWorldScript.round%3 == 2)
        {
            AllWorldScript.bulletcount = count * 5 + 3;
            
        }
        else
        {
            AllWorldScript.bulletcount = count * 5;
            
        }
        AllWorldScript.result += count * 50;             //대입은 안되나?    
        //AllWorldScript.bulletcount = AllWorldScript.round * 5;          //총알 개수를 증가
        AllWorldScript.objectnum = count * 5;            //오크통 개수를 증가
        Debug.Log(AllWorldScript.objectnum);
        StartCoroutine(AllWorldScript.Instance.manyExplain());
        
        AllWorldScript.Instance.BulletText.text = "" + AllWorldScript.bulletcount;         //라운드가 끝나고 재시작할때 총알개수 변경
    }

    
}

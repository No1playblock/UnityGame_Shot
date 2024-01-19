using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityAdsMgrScript : MonoBehaviour {

    private const string game_id = "3275923";
    private const string rewarded_video_id = "rewardedVideo";

    void Start () {
        Debug.Log("ADS START");
        Intialize();
	}
    private void Intialize()
    {
        Advertisement.Initialize(game_id);
    }

    public void ShowRewardedAd()
    {
        
        if (Advertisement.IsReady(rewarded_video_id))
        {
            var optins = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(rewarded_video_id, optins);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    //광고 시청 완료시
                    Debug.Log(AllWorldScript.coin);
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + AllWorldScript.coin);
                    PlayerPrefs.Save();
                    Debug.Log(AllWorldScript.coin);
                    AllWorldScript.Instance.gameOverCoinText.text = "-" + AllWorldScript.coin *2 + "-";
                }
                break;
            case ShowResult.Skipped:
                {
                    //광고 시청 스킵시
                }
                break;
            case ShowResult.Failed:
                {
                    //광고 시청 실패시
                }
                break;
        }
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

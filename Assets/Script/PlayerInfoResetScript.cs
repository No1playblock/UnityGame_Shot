using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoResetScript : MonoBehaviour {

	public void OnclickResetBtn()
    {
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey(string);    원하는 것만 없애기
        LoadingSceneScript.LoadScene("StartScene");
    }
}

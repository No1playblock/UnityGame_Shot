using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    public void ButtonClick()
    {
        AllWorldScript.start = true;
        //SceneManager.LoadScene("PlayScene");
        LoadingSceneScript.LoadScene("PlayScene");

    }
    public void TargetBtnClick()
    {
        LoadingSceneScript.LoadScene("TargetScene");
    }
    public void DrawingBtnclick()
    {
        LoadingSceneScript.LoadScene("DrawingScene");
    }

}

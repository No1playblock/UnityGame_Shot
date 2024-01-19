using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneScript : MonoBehaviour {

    public static string nextScene;
    public Text LoadingText;
    public Text ExplainText;

    [SerializeField]
    Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
        StartCoroutine(LoadingMessage());
        int rand = Random.Range(1, 6);
        if (rand == 1)
        {
            //ExplainText.text = "타겟은 앞으로 점점 늘어날 계획입니다...";
            ExplainText.text = "";
        }
        else if (rand == 2)
        {
            ExplainText.text = "";
        }
        else if (rand == 3)
        {
            ExplainText.text = "";
        }
        else if (rand == 4)
        {
            ExplainText.text = "";
        }
        else if (rand == 5)
        {
            ExplainText.text = "";
        }
    }
 

    IEnumerator LoadingMessage()
    {
        while (true)
        {
            
            LoadingText.text = "Loading.";
            yield return new WaitForSeconds(0.2f);
            LoadingText.text = "Loading..";
            yield return new WaitForSeconds(0.2f);
            LoadingText.text = "Loading...";
            yield return new WaitForSeconds(0.2f);
        }

    }

    string nextSceneName;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                    op.allowSceneActivation = true;
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
        }
    }

}

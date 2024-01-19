using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneScript : MonoBehaviour {

    public GameObject Click;
    public AudioSource BGM;
    // Use this for initialization
    void Start()
    {
        //Screen.SetResolution(1920, 1080, false);
        Click.SetActive(false);
        StartCoroutine(ShowReady());
        if (PlayerPrefs.HasKey("BGM") == false)
        {
            BGM.volume = 1.0f;
        }
        else
            BGM.volume = PlayerPrefs.GetFloat("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LoadingSceneScript.LoadScene("StartScene");
        }

    }

    IEnumerator ShowReady()
    {

        while (true)
        {
            Click.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            Click.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);

        }

    }
}

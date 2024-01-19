using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionBtnScript : MonoBehaviour {

    public Scrollbar SFXScrollBar;
    public Scrollbar BGMScrollBar;

    public AudioSource BGM;
    public AudioSource BtnClickSFX;

    public Image SFXImg;
    public Image BGMImg;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("SFX") == false)
        {
            Debug.Log("호출");
            SFXScrollBar.GetComponent<Scrollbar>().value = 1.0f;
            PlayerPrefs.GetFloat("SFX", SFXScrollBar.GetComponent<Scrollbar>().value);
            PlayerPrefs.Save();
        }
        else
        {
            SFXScrollBar.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("SFX");
            

        }
        if (PlayerPrefs.HasKey("BGM") == false)
        {
            Debug.Log("호출");
            BGMScrollBar.GetComponent<Scrollbar>().value = 1.0f;
            PlayerPrefs.GetFloat("BGM", BGMScrollBar.GetComponent<Scrollbar>().value);
            PlayerPrefs.Save();
        }
        else
        {
            BGMScrollBar.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("BGM");

        }
        StartCoroutine(updateBGM());
        
        BtnClickSFX.volume = SFXScrollBar.GetComponent<Scrollbar>().value;          //소리조절은 if 문 관계없이 해야해서 밖으로 뺌
        if(SceneManager.GetActiveScene().name == "PlayScene")
        {
            AllWorldScript.shot.volume = SFXScrollBar.GetComponent<Scrollbar>().value;  //소리조절
            AllWorldScript.Instance.RoundClearSFX.volume = SFXScrollBar.GetComponent<Scrollbar>().value; ;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        BGM.volume = BGMScrollBar.GetComponent<Scrollbar>().value;

        if (BGM.volume == 0)
        {
            BGMImg.sprite = Resources.Load<Sprite>("Img/BGMOFFUI.2d") as Sprite;
        }
        else
            BGMImg.sprite = Resources.Load<Sprite>("Img/BGMUI.2d") as Sprite;
        if (SFXScrollBar.GetComponent<Scrollbar>().value == 0)
        {
            SFXImg.sprite = Resources.Load<Sprite>("Img/SoundOffUI.2d") as Sprite;
        }
        else
            SFXImg.sprite = Resources.Load<Sprite>("Img/SoundOnUI.2d") as Sprite;


    }
    IEnumerator updateBGM()
    {
        while (true)
        {
            yield return null;
            BGM.volume = BGMScrollBar.GetComponent<Scrollbar>().value;
        }
    }

    public void OnchangeBGM()                           //optionpanel 에서 x 버틍니 눌릴때 BGM값 저장
    {
        PlayerPrefs.SetFloat("BGM", BGMScrollBar.GetComponent<Scrollbar>().value);
        PlayerPrefs.Save();
    }

    public void OnchangeSFX()                      //optionpanel에서 x 버튼이 눌릴때 호출
    {
        BtnClickSFX.volume = SFXScrollBar.GetComponent<Scrollbar>().value;
        PlayerPrefs.SetFloat("SFX", SFXScrollBar.GetComponent<Scrollbar>().value);
        PlayerPrefs.Save();
    }
    public void OnclickBtn()
    {
        BtnClickSFX.Play();
    }
}

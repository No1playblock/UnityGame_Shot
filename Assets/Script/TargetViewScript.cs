using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetViewScript : MonoBehaviour {

    GameObject target;
    GameObject Btn;
    Image BtnImage;
    Text BtnText;
    Vector3 basicScale;
    Vector3 bigScale;
    Quaternion basicRotation;

    

    // Use this for initialization
    void Awake () {

        target = this.gameObject;
        basicRotation = target.transform.rotation;

        Btn = GameObject.Find("ChooseBtn");
        BtnText = GameObject.Find("BtnText").GetComponent<Text>();
        BtnImage = Btn.GetComponent<Image>();

        basicScale = target.transform.localScale;
        bigScale = new Vector3(basicScale.x + 6.0f, basicScale.y + 6.0f, basicScale.z + 6.0f);



	}
	
	// Update is called once per frame
	void Update () {

        if (target.name.Equals(AllWorldScript.targetName))
        {
            TargetSpin();           //타겟이 돌아간다.
            OnchangeBtnImage();     //버튼 이미지를 바꿔주고
            OnchangeScale();        //크기를 키워준다.
        }
        else
        {
            returnScale();
            returnRotation();
        }
            

    }

     void TargetSpin()
    {
        target.transform.Rotate(0, 1, 0, Space.World);
    }
    void OnchangeBtnImage()
    {
        if (PlayerPrefs.GetInt("Have" + AllWorldScript.targetName) == 0)         //오크통을 가지고있지 않다면 구매이미지
        {
            BtnImage.sprite = Resources.Load<Sprite>("Btn/CostBtn(Gem).2d") as Sprite;     //가격표 이미지가 저거다.
            BtnText.text = "" + AllWorldScript.targetCost;                                                   //가격
        }
        else if (PlayerPrefs.GetInt("Have" + AllWorldScript.targetName) == 1)
        {
            BtnImage.sprite = Resources.Load<Sprite>("Btn/BasicBtn.2d") as Sprite;
            BtnText.text = "SELECT";
        }
        if (PlayerPrefs.GetString("Target").Equals("Target_" + AllWorldScript.targetName))               //선택한 타겟            선택됨이미지설정
        {
            BtnImage.sprite = Resources.Load<Sprite>("Btn/ChooseBtn.2d") as Sprite;
            BtnText.text = "";
        }
    }

    void OnchangeScale()
    {
        target.transform.localScale = Vector3.Lerp(target.transform.localScale, bigScale, 0.1f);
    }
    void returnScale()
    {
        target.transform.localScale = Vector3.Lerp(target.transform.localScale, basicScale, 0.1f);
    }

    void returnRotation()
    {
        target.transform.rotation = basicRotation;
    }
}

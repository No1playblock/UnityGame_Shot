using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlideMgrScript : MonoBehaviour {


    public Scrollbar MyScrollBar;
                    //오브젝트 추가할때 추가해라       AllWorldScript가서 object count
                                                                                        //스크롤 위치 보정, 해당위치 타겟설정
                   //OnclickChooseBtn  Awake    Scroll2개    
    //private float ScrollValue;

    public Text BtnText;
    private int cost;
    public GameObject Btn;
    Image BtnImage;

    public Text TargetName;
    public Text TargetExplain;

    public Text coinText;
    public Text GemText;
    void Awake()
    {
        BtnImage = Btn.GetComponent<Image>();


        coinText.text = "" + PlayerPrefs.GetInt("Coin");      //coin
        GemText.text = "" + PlayerPrefs.GetInt("Gem");
        if (PlayerPrefs.HasKey("Target") == false)
        {
            PlayerPrefs.SetString("Target", "Target_Oak");
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetString("Target").Equals("Target_Oak"))
        {
            MyScrollBar.GetComponent<Scrollbar>().value = 0f;
        }
        else if (PlayerPrefs.GetString("Target").Equals("Target_Cocacola"))
        {
            MyScrollBar.GetComponent<Scrollbar>().value = 1 * 1 / (AllWorldScript.ObjectCount - 1);
        }
        else if (PlayerPrefs.GetString("Target").Equals("Target_Pepsi"))
        {
            MyScrollBar.GetComponent<Scrollbar>().value = 2 * 1 / (AllWorldScript.ObjectCount - 1);
        }
        else if (PlayerPrefs.GetString("Target").Equals("Target_Cider"))
        {
            MyScrollBar.GetComponent<Scrollbar>().value = 3 * 1 / (AllWorldScript.ObjectCount - 1);
        }
        else if(PlayerPrefs.GetString("Target").Equals("Target_Watermelon"))
        {
            MyScrollBar.GetComponent<Scrollbar>().value = 4 * 1 / (AllWorldScript.ObjectCount - 1);
        }
        StartCoroutine(Scroll());

    }
    IEnumerator Scroll()
    {
        yield return null;
        yield return null;
        while (true)
        {
           
            if (!Input.GetMouseButton(0))           //마우스를 누르는중이 아닐때 실행     //스크롤위치 조정
            {
                if (MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2)            //첫번째 타겟범위일때 타겟앞으로 이동
                {
                    MyScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(MyScrollBar.GetComponent<Scrollbar>().value, 0, 0.1f);

                }
                else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 1 / (AllWorldScript.ObjectCount - 1))   //두번째 타겟범위일때 타겟앞으로 이동
                {
                    MyScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(MyScrollBar.GetComponent<Scrollbar>().value, 1 * 1 / (AllWorldScript.ObjectCount - 1), 0.1f);
                }
                else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 + 1 * 1 / (AllWorldScript.ObjectCount - 1) && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 2 * 1 / (AllWorldScript.ObjectCount - 1)) //세번째 타겟범위일때 타겟앞으로 이동
                {
                    MyScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(MyScrollBar.GetComponent<Scrollbar>().value, 2 * 1 / (AllWorldScript.ObjectCount - 1), 0.1f);
                }
                else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 + 2 * 1 / (AllWorldScript.ObjectCount - 1) && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 3 * 1 / (AllWorldScript.ObjectCount - 1)) //네번째 타겟범위일때 타겟앞으로 이동
                {
                    MyScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(MyScrollBar.GetComponent<Scrollbar>().value, 3 * 1 / (AllWorldScript.ObjectCount - 1), 0.1f);
                }
                else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 + 3 * 1 / (AllWorldScript.ObjectCount - 1) && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 4 * 1 / (AllWorldScript.ObjectCount - 1)) //네번째 타겟범위일때 타겟앞으로 이동
                {
                    MyScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(MyScrollBar.GetComponent<Scrollbar>().value, 4 * 1 / (AllWorldScript.ObjectCount - 1), 0.1f);
                }
                else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 + 4 * 1 / (AllWorldScript.ObjectCount - 1) && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 5 * 1 / (AllWorldScript.ObjectCount - 1)) //다섯번째 타겟범위일때 타겟앞으로 이동
                {
                    MyScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(MyScrollBar.GetComponent<Scrollbar>().value, 5 * 1 / (AllWorldScript.ObjectCount - 1), 0.1f);
                }
            }                                                       //범위에 들어왔을때                         //해당위치 타겟설정
            if (MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2)            //크기 키우기, 버튼이미지바꾸기
            {   
                AllWorldScript.targetName = "Oak";                                  //TargetViewScript
                TargetName.text = "오크통";
                TargetExplain.text = "기본으로 주어지는 오크통이다.";
            }
            else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 1 * 1 / (AllWorldScript.ObjectCount - 1))
            {
                AllWorldScript.targetName = "Cocacola";
                TargetName.text = "빨간뚜껑 콜라";
                TargetName.fontSize = 107;
                TargetExplain.text = "왠지 북극곰이 마실것만 같다.";
            }
            else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 + 1 * 1 / (AllWorldScript.ObjectCount - 1) && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 2 * 1 / (AllWorldScript.ObjectCount - 1))
            {
                AllWorldScript.targetName = "Pepsi";
                TargetName.text = "파란뚜껑 콜라";
                TargetName.fontSize = 107;
                TargetExplain.text = "태극무늬 비슷한게 그려져있어야만 할것 같다.";
            }
            else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 + 2 * 1 / (AllWorldScript.ObjectCount - 1) && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 3 * 1 / (AllWorldScript.ObjectCount - 1))
            {
                AllWorldScript.targetName = "Cider";
                TargetName.text = "사이다";
                TargetExplain.text = "개발자의 사연이 담겨있는 사이다이다.";
            }
            else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 + 3 * 1 / (AllWorldScript.ObjectCount - 1) && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 4 * 1 / (AllWorldScript.ObjectCount - 1))
            {
                AllWorldScript.targetName = "Watermelon";
                TargetName.text = "수박";
                TargetExplain.text = "속이 꽉찬 수박이다.";//가격표시
            }
            else if (MyScrollBar.GetComponent<Scrollbar>().value >= 1 / (AllWorldScript.ObjectCount - 1) / 2 + 4 * 1 / (AllWorldScript.ObjectCount - 1) && MyScrollBar.GetComponent<Scrollbar>().value < 1 / (AllWorldScript.ObjectCount - 1) / 2 + 5 * 1 / (AllWorldScript.ObjectCount - 1))
            {
                AllWorldScript.targetName = "Egg";
                TargetName.text = "겨란";
                TargetExplain.text = "삶은 겨란이다.";
            }
            AllWorldScript.targetCost = 10;               //가격은 10보석

            yield return null;
        }
       
    }

	void OnclickBtn()       
    {
        if (BtnImage.sprite.name.Equals("CostBtn(Gem).2d"))                  //만약에 누른 버튼의 이미지 이름이 저거라면(가격표이미지라면)
        {
            OnclickBuyBtn();                                            //구매버튼을 누른것이다.(보석결제로 변경)
        }
        else if(BtnImage.sprite.name.Equals("BasicBtn.2d")){
            OnClickChooseBtn();
        }
    }
	
    public void OnClickChooseBtn()                                              //구매한 타겟을 고르는 버튼    //이것도 수정
    {
        if (AllWorldScript.targetName.Equals("Oak"))
        {
            PlayerPrefs.SetString("Target", "Target_Oak");
            PlayerPrefs.Save();
        }
        else if (AllWorldScript.targetName.Equals("Cocacola"))
        {
            PlayerPrefs.SetString("Target", "Target_Cocacola");
            PlayerPrefs.Save();
        }
        else if (AllWorldScript.targetName.Equals("Pepsi"))
        {
            PlayerPrefs.SetString("Target", "Target_Pepsi");
            PlayerPrefs.Save();
        }
        else if (AllWorldScript.targetName.Equals("Cider"))
        {
            PlayerPrefs.SetString("Target", "Target_Cider");
            PlayerPrefs.Save();
        }
        else if (AllWorldScript.targetName.Equals("Watermelon"))
        {
            PlayerPrefs.SetString("Target", "Target_Watermelon");
            PlayerPrefs.Save();
        }
        else if (AllWorldScript.targetName.Equals("Egg"))
        {
            PlayerPrefs.SetString("Target", "Target_Egg");
            PlayerPrefs.Save();
        }
        //PlayerPrefs.SetString("Target", "Target_" + AllWorldScript.targetName);
        //PlayerPrefs.Save();
        //LoadingSceneScript.LoadScene("StartScene");
    }
    public void OnclickBuyBtn()                         //구매하지 않은 버튼을 구매하는 버튼
    {
        if (PlayerPrefs.GetInt("Gem") >= AllWorldScript.targetCost)                                                      //돈이되면
        {
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") - AllWorldScript.targetCost);
            PlayerPrefs.SetInt("Have" + AllWorldScript.targetName, 1);
            PlayerPrefs.Save();
            GemText.text = "" + PlayerPrefs.GetInt("Gem");
        }
        else
        {
            //구매불가
        }

    }
    



    public void OnclickBackBtn()
    {
        LoadingSceneScript.LoadScene("StartScene");
    }
}

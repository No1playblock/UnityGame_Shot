using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomTargetScript : MonoBehaviour {

    private GameObject[] target;            //타겟묶음
    private GameObject Target;              //생성된 타겟
    private GameObject result;              //뽑은 타겟
    private GameObject tmp;

    private int ArrayLength;

    //public GameObject Oak;                  //바깥쪽 오크통(선물박스)
    public GameObject TargetSpawnPoint;
    public GameObject CapsuleSpawnPoint;
    public GameObject Ground;               //바닥 제거를 위한 바닥오브젝트
    public GameObject DrawingMachine;       //바닥제거 할때 뽑기기계도 같이 없앤다.
    //public PhysicMaterial bounce;           //오크통이 처음에 튕기는 물리엔진
    public GameObject Coinbag;
    public GameObject Capsule;

    public ParticleSystem UndertheOakParticle;     //오크통 아래서 반짝이
    public ParticleSystem GlowParticle;
    public GameObject LightParticle;        //터질때 파티클 오브젝트
    
    
    public Text TargetName;
    public Text TargetExplain;
    public Text CoinText;
    public Text CostText;
    public Text GemText;

    public AudioSource GetSFX;
    public AudioSource TargetAppearSFX;


    public GameObject Btn;
    //Image BtnImage;
    public Text BtnText;
    // public GameObject Target;
    // Use this for initialization


    //resources 밑에 폴더를 하나 더만들어서 새로운씬에 추가해야하니 prefab을 새로 만들자. 그러면 태그도 새로달수 있고 크기도 변경가능하다.


    void Start () {
        //배열에 오브젝트를 넣어놓고 i * 0.1f 해서 나온값과 i 에있는걸로 ㄱ //나온값/0.1f 가 i값
        //BtnImage = Btn.GetComponent<Image>();
        GetSFX.volume = PlayerPrefs.GetFloat("SFX");
        TargetAppearSFX.volume = PlayerPrefs.GetFloat("SFX");
        CoinText.text = "" + PlayerPrefs.GetInt("Coin");      //coin
        GemText.text = "" + PlayerPrefs.GetInt("Gem");

        target = Resources.LoadAll<GameObject>("Target/");
        ArrayLength = target.Length;
        
        
        if (PlayerPrefs.GetInt("Coin") >= 1000)
        {
            CostText.color = new Color(255, 255, 255);
        }
        else
        {
            CostText.color = new Color(255, 0, 0);
        }

	}
    private GameObject Avoidduplicates()
    {
        int rand;
        

        while (true)
        {
            rand = Random.Range(0, ArrayLength);

            Debug.Log(ArrayLength);
            
            
            if(ArrayLength < 0)
            {
                return Coinbag;
            }
            else
            {
                result = target[rand];
                if (PlayerPrefs.HasKey("Have" + result.name.Substring(7)))          //해당타겟이 있으면
                {
                    for (int i = rand; i < ArrayLength-1; i++)
                    {
                        target[i] = target[i + 1];                                  //한칸씩 땡겨라 그거 빼고
                    }
                    ArrayLength--;
                }
                else
                    return result;
            }
            
        }
        
    }
    public void OnclickBtn()
    {
        //float rand = Random.value; //0.0~1.0 이내의 랜덤 float값을 가져옵니다.
        //int rand = Random.Range(1, (int)AllWorldScript.ObjectCount);
        //result = target[rand];                                //%를 같게 할경우
        result = Avoidduplicates();
        Debug.Log(result.name);

        if (PlayerPrefs.GetInt("Coin") >= 1000)                                                      //돈이되면
        {
            StartCoroutine(DrawingSFX());               //3.3초 뒤에 획득하는 소리
            StartCoroutine(TargetAppear());             //슈와악 하는 소리

            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 1000);      //1000원 빼고
            PlayerPrefs.SetInt("Have" + result.name.Substring(7), 1);                        //가진걸로 하고
            PlayerPrefs.Save();                                                 //저장
            CoinText.text = "" + PlayerPrefs.GetInt("Coin");                    //돈 뺀거 UI적용

            TargetName.text = "";                                                 //아이템 이름이랑 설명란 비우고
            TargetExplain.text = "";
            addrigid(Capsule);                                                      //오크통 터뜨리고
            Target = Instantiate(result, TargetSpawnPoint.transform.position, Quaternion.identity);   //뽑은거 생성
            Target.GetComponent<Rigidbody>().useGravity = false;                                //중력끄고
            StartCoroutine(TargetSpin());                                                       //뽑은거 돌려
                                                                             
            LightParticle.GetComponent<ParticleSystem>().Play();                                //뽑은거 반짝이 온
            StartCoroutine(BiggerScale());                                                      //사이즈 키워주고
            UndertheOakParticle.Stop();                                                         //뽑기전 반짝이 끄고
            StartCoroutine(DeleteGround(result));                                               //바닥 없애

            Btn.SetActive(false);                                                               //버튼 지워
            StartCoroutine(InputTouch());
            
        }
        else
        {
            CostText.color = new Color(255, 0, 0);
        }
    }
    IEnumerator InputTouch()
    {
        yield return new WaitForSeconds(3.5f);
        while (true)
        {
            yield return null;
            // || Input.GetTouch(0).phase == TouchPhase.Began
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                /*Instantiate(Capsule, CapsuleSpawnPoint.transform.position, Quaternion.identity);
                ItemName.text = "";                                                 //아이템 이름이랑 설명란 비우고
                ItemExplain.text = "";
                Target.SetActive(false);
                Ground.SetActive(true);
                DrawingMachine.SetActive(true);
                GlowParticle.Stop();
                Btn.SetActive(true);
                UndertheOakParticle.Play();*/
                
            }

        }
    }
    IEnumerator DrawingSFX()                    //가지고 나서 획득소리를 출력
    {
        yield return new WaitForSeconds(3.3f);
        GetSFX.Play();

    }
    IEnumerator TargetAppear()
    {
        yield return new WaitForSeconds(0.3f);
        TargetAppearSFX.Play();
    }
    public void OnchangeSFX()
    {
        GetSFX.volume = PlayerPrefs.GetFloat("SFX");
        TargetAppearSFX.volume = PlayerPrefs.GetFloat("SFX");
        
    }
    void ItemText(GameObject result)
    {
        if (result.name.Equals("Target_Oak"))
        {
            TargetName.text = "오크통";
            TargetExplain.text = "기본으로 주어지는 오크통이다.";
        }
        else if (result.name.Equals("Target_Cocacola"))
        {
            TargetName.text = "빨간뚜껑 콜라";
            TargetName.fontSize = 107;
            TargetExplain.text = "왠지 북극곰이 마실것만 같다.";
        }
        else if (result.name.Equals("Target_Pepsi"))
        {
            TargetName.text = "파란뚜껑 콜라";
            TargetName.fontSize = 107;
            TargetExplain.text = "태극무늬 비슷한게 그려져있어야만 할것 같다.";
        }
        else if (result.name.Equals("Target_Watermelon"))
        {
            TargetName.text = "수박";
            TargetExplain.text = "속이 꽉찬 수박이다.";
        }
        else if (result.name.Equals("Target_Egg"))
        {
            TargetName.text = "겨란";
            TargetExplain.text = "삶은 겨란이다.";

        }
        else if (result.name.Equals("Target_Cider"))
        {
            TargetName.text = "사이다";
            TargetExplain.text = "개발자의 사연이 담겨있는 사이다이다.";
        }
        else if(result.name.Equals("a lot of Coin 2"))
        {
            TargetName.text = "1000코인";
            TargetExplain.text = "다 뽑으셔서 남는 타겟이 없어요... \n대신 1000코인은 돌려드릴게요";
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1000);
            CoinText.text = "" + PlayerPrefs.GetInt("Coin");

        }
    }           //아이템 추가할때
    IEnumerator TargetSpin()
    {
        while (true)
        {
            yield return null;
            Target.transform.Rotate(0, 1, 0, Space.World);
        }
    }

    IEnumerator DeleteGround(GameObject result)
    {
        yield return new WaitForSeconds(1.6f);
        Ground.SetActive(false);
        DrawingMachine.SetActive(false);
        GlowParticle.Play();
        yield return new WaitForSeconds(1.7f);
        ItemText(result);
    }
    

    void addrigid(GameObject oak)           //rigidbody 추가
    {
        Collider[] hitColliders = Physics.OverlapSphere(oak.transform.position, 5.0f);
        int i = 0;
        foreach (Transform child in oak.transform)
        {
            if (child.tag.Equals("targetChild"))
            {
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.layer = 10;
            }
            
            
        }
        CapsuleCollider c = oak.gameObject.GetComponent<CapsuleCollider>();     //collider 해제
        c.enabled = !c.enabled;

        Rigidbody rig = oak.gameObject.GetComponent<Rigidbody>();               //rigid 해제
        Destroy(rig);
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag.Equals("targetChild") && hitColliders[i].GetComponent<Rigidbody>() != null)     //태그가 targetchild이고 리지드바디가 있으면
                hitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(200.0f, oak.transform.position, 100.0f);            //폭발힘을 줘라.
            i++;
        }

    }
    
    IEnumerator BiggerScale()
    {
        while (true)
        {
            yield return null;
            LightParticle.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f); 
        }
    }
    public void OnclickBackBtn()
    {
        LoadingSceneScript.LoadScene("StartScene");
    }
    

}

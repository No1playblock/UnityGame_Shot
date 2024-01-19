using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class camray : MonoBehaviour {
    //총알 나가는 코드
    private Vector3 hitpoint;
    public Transform firePos;
    private float bulletspeed = 1200.0f;
    public GameObject bullet;
    public GameObject sparkeffect;
    public GameObject gun;
    public GameObject coin;
    private GameObject[] _pool;         //코인 풀링
    private int maxCount = 40;
    private int fingerID = 0;               //0은 모바일, -1은 마우스클릭

    private GameObject[] _pool_bullet;
    private int maxBulletCount = 10;
    // Update is called once per frame
    private void coinSpawn()
    {
        _pool = new GameObject[maxCount];               //오브젝트가 저장되어있는 변수
        for (int j = 0; j < maxCount; j++)
        {

            GameObject gm = (GameObject)Instantiate(coin, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

            gm.SetActive(false);                     //Active를 비활성화   
            _pool[j] = gm;                         //전역변수 _pool에 집어넣는다.
        }
    }
    private void bulletSpawn()
    {
        Debug.Log("bulletSpawn");
        _pool_bullet = new GameObject[maxBulletCount];               //오브젝트가 저장되어있는 변수
        for (int j = 0; j < maxBulletCount; j++)
        {
            Debug.Log("ForbulletSpawn");
            GameObject gm = (GameObject)Instantiate(coin, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

            gm.SetActive(false);                     //Active를 비활성화   
            _pool_bullet[j] = gm;                         //전역변수 _pool에 집어넣는다.
        }
    }
    private void Awake()
    {
        coinSpawn();
        bulletSpawn();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && Time.timeScale == 1)
        {
          
            if(AllWorldScript.bulletcount==0)
            {
                //GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<GunMotionScript>().Reload();
               
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                hitpoint = hitInfo.point - firePos.position;
                //Debug.Log(hitpoint);
                bulletshot(hitpoint, hitInfo);                                                          //총알 쏘는거
                gun.transform.LookAt(hitInfo.point);
                gun.transform.Rotate(0.0f, -90.0f, 0.0f);
                              
            }
        }

    }
    void bulletshot(Vector3 point, RaycastHit Info)
    {
        int count = 0;
        int bulletcount = 0;
        if (Info.collider.CompareTag("target") && AllWorldScript.bulletcount!=0)                                     //맞은게 타깃이면
        
        {
            
            AllWorldScript.score += 10;
           

            addrigid(Info.collider.gameObject, Info);                                 //타깃의 리지드바디 추가 자식들에게 리지드바디추가후 리지드바디와 콜라이더삭제
            //info.collider.gameObject 가 타겟이다.
            
            
            int r = (int)Random.Range(0.0f, 4.0f);      
            if (r == 0)             //                                              //동전생성
            {
               for(int i =0; i<3; i++)
                {
                    _pool[count + i].transform.position = new Vector3(Info.point.x+i*0.1f, Info.point.y + i * 0.1f, Info.point.z + i * 0.1f);       //생성위치
                    
                    _pool[count + i].GetComponent<Rigidbody>().AddExplosionForce(200.0f, Info.point, 100.0f);       //동전돌리는 힘
                    _pool[count + i].SetActive(true);
                }
                count += 3;
                if (count == maxCount)
                    count = 0;


                AllWorldScript.coin+=10;
            }
            AllWorldScript.Instance.CoinText.text = "" + AllWorldScript.coin;            //코인이 안뜨면 표시가 안나와서 아마 윗줄이 아닌 여기 넣은듯함

        }
        if (AllWorldScript.bulletcount != 0 && !EventSystem.current.IsPointerOverGameObject(fingerID))                  //총알이 있을때 //UI를 쏘는것이 아닐때     //총알이 0개면 총알 X      
        {
           
            GameObject.FindGameObjectWithTag("Weapon").transform.Find("Weapon").GetComponent<GunMotionScript>().Shot();     //weapon의 태그를 가진오브젝트 밑에있는 weapon이라는 이름의 오브젝트
            Instantiate(sparkeffect, firePos.position, Quaternion.identity);                            //이펙트생성

            /* GameObject newBullet = _pool_bullet[bulletcount];
             bulletcount++;
             if (bulletcount == 10)
             {
                 bulletcount = 0;
             }
             newBullet.transform.position = firePos.position;
             newBullet.transform.rotation = firePos.rotation;*/
            GameObject newBullet = Instantiate(bullet, firePos.position, firePos.rotation);             //총알생성
            newBullet.GetComponent<Rigidbody>().AddForce(point * bulletspeed);
            GameObject.Find("GameMgr").GetComponent<OnchangeScript>().OnChangeBulletCount();            //총알 감소;
        }
        GameObject.Find("GameMgr").GetComponent<OnchangeScript>().OnChangeScore();


    }


    void addrigid(GameObject oak, RaycastHit Info)           //rigidbody 추가
    {
        Collider[] hitColliders = Physics.OverlapSphere(Info.point, 5.0f);
        int i = 0;
        foreach (Transform child in oak.transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.layer = 10;
            
        }
        CapsuleCollider c = oak.gameObject.GetComponent<CapsuleCollider>();     //collider 해제
        c.enabled = !c.enabled;

        Rigidbody rig = oak.gameObject.GetComponent<Rigidbody>();               //rigid 해제
        Destroy(rig);
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag.Equals("targetChild") && hitColliders[i].GetComponent<Rigidbody>() != null)     //태그가 targetchild이고 리지드바디가 있으면
                hitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(200.0f, Info.point, 100.0f);            //폭발힘을 줘라.
            i++;
        }

    }
    
    



}

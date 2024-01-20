using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {
    //오크통 생성하는 콛,

    int maxCount = 1000;
    public GameObject prefabs;
    private GameObject[] target;
    private int count = 0;              //_pool 배열의 카운트 개수(for문에서 매번 증가)
 
    

    private bool enableSpawn = false;
    GameObject t;
    void SpawntargetR()      //오른쪽 스폰
    {
        float randomY = Random.Range(2.0f, 8.0f);
        
        if (enableSpawn)
        {
            // t = (GameObject)Instantiate(target, new Vector3(10.0f, randomY, 0.0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
            target[count].SetActive(true);
            target[count].transform.position = new Vector3(10.4f, randomY, 0.0f);
            
            float bombx = target[count].transform.position.x;
            bombx += 9;
            Vector3 bomb = new Vector3(bombx, target[count].transform.position.y, target[count].transform.position.z);
            target[count].GetComponent<Rigidbody>().AddExplosionForce(200.0f, bomb, 100.0f, 100.0f );        //오브젝트를 돌리는 힘
            
            bomb.y = bomb.y + 4;
            target[count].GetComponent<Rigidbody>().AddExplosionForce(AllWorldScript.oakspeed, bomb, 100.0f);              //오브젝트를 미는힘
            enableSpawn = false;
            count++;
            if (count == maxCount)
                count = 0;
        }
        
        
    }
    void SpawntargetL()      //왼쪽 스폰
    {
        float randomY = Random.Range(2.0f, 8.0f);
        
        if (enableSpawn)
        {
            
            target[count].SetActive(true);
            target[count].transform.position = new Vector3(-16.4f, randomY, 0.0f);
            
            float bombx = target[count].transform.position.x;
            bombx -= 9;
            Vector3 bomb = new Vector3(bombx, target[count].transform.position.y, target[count].transform.position.z);
            target[count].GetComponent<Rigidbody>().AddExplosionForce(200.0f, bomb, 100.0f, 100.0f);      //오브젝트를 돌리는 힘
            bomb.y = bomb.y + 4;

            target[count].GetComponent<Rigidbody>().AddExplosionForce(AllWorldScript.oakspeed, bomb, 100.0f);              //오브젝트를 미는힘*/
            enableSpawn = false;
            count++;
            if (count == maxCount)
                count = 0;

        }
    }

    public void ExecuteSpawn()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(AllWorldScript.ExplainCount);                  //1초있다가 오크통 생성

        while (AllWorldScript.objectnum != 0)                               //objectnum 이 0이면 끝
        {
            yield return new WaitForSeconds(0.5f);
            oakbottle();
 
        }
        StopCoroutine(spawn());
    }
    void oakbottle()
    {
        enableSpawn = true;


        int r = (int)Random.Range(0.0f, 2.0f);
        if (r == 0)
        {
            SpawntargetL();
            AllWorldScript.objectnum--;
        }
        else if (r == 1)
        {
            SpawntargetR();
            AllWorldScript.objectnum--;
        }

    }
    public void objectSpawn()               //오브젝트 풀링
    {
        target = new GameObject[maxCount];               //오브젝트가 저장되어있는 변수
        for (int j = 0; j < maxCount; j++)
        {   
            GameObject gm = (GameObject)Instantiate(prefabs, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
             
            gm.SetActive(false);                     //Active를 비활성화       
            target[j] = gm;                         //전역변수 _pool에 집어넣는다.
        }
    }

    private void Awake()
    {
        //prefabs = Resources.Load("../Prefab/" + PlayerPrefs.GetString("Target"), typeof(GameObject)) as GameObject;
        prefabs = Resources.Load("Target/" + PlayerPrefs.GetString("Target"), typeof(GameObject)) as GameObject;
        Debug.Log(prefabs.name);
        objectSpawn();
        if (AllWorldScript.mode.Equals("Many"))
            GameObject.Find("GameMgr").GetComponent<ManyScript>().RoundSystem();
        else if (AllWorldScript.mode.Equals("Speed"))
            GameObject.Find("GameMgr").GetComponent<SpeedScript>().RoundSystem();
        //프로젝트가 시작할때 위 함수 실행
        ExecuteSpawn();
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneScript : MonoBehaviour {

    public GameObject target;
    public bool enableSpawn = false;
    public float speed = 1000.0f;
    GameObject t;
    void SpawntargetR()      //오른쪽 스폰
    {
        float randomY = Random.Range(2.0f, 8.0f);

        if (enableSpawn)
        {
            t = (GameObject)Instantiate(target, new Vector3(10.0f, randomY, 15.0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
            //t.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            float bombx = t.transform.position.x;
            bombx += 9;
            Vector3 bomb = new Vector3(bombx, t.transform.position.y, t.transform.position.z);
            t.GetComponent<Rigidbody>().AddExplosionForce(200.0f, bomb, 100.0f, 100.0f);        //오브젝트를 돌리는 힘

            bomb.y = bomb.y + 4;
            t.GetComponent<Rigidbody>().AddExplosionForce(speed, bomb, 100.0f);              //오브젝트를 미는힘
            enableSpawn = false;


        }


    }
    void SpawntargetL()      //왼쪽 스폰
    {
        float randomY = Random.Range(2.0f, 8.0f);

        if (enableSpawn)
        {

            t = (GameObject)Instantiate(target, new Vector3(-10.0f, randomY, 15.0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
            //t.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            float bombx = t.transform.position.x;
            bombx -= 9;
            Vector3 bomb = new Vector3(bombx, t.transform.position.y, t.transform.position.z);
            t.GetComponent<Rigidbody>().AddExplosionForce(200.0f, bomb, 100.0f, 100.0f);      //오브젝트를 돌리는 힘
            bomb.y = bomb.y + 4;

            t.GetComponent<Rigidbody>().AddExplosionForce(speed, bomb, 100.0f);              //오브젝트를 미는힘*/
            enableSpawn = false;




        }


    }



    void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            oakbottle();
        }
    }
    void oakbottle()
    {
        enableSpawn = true;


        int r = (int)Random.Range(0.0f, 2.0f);
        if (r == 0)
        {
            SpawntargetL();
        }
        else if (r == 1)
        {
            SpawntargetR();
        }

    }
    
}

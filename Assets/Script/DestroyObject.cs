using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//오크통 없애는 스크립트

public class DestroyObject : MonoBehaviour {

    GameObject target;
	// Use this for initialization
	void Start () {
        target = this.gameObject;
        StartCoroutine(remove());
        //StartCoroutine(remove2());
        
	}

    // Update is called once per frame
    IEnumerator remove() {            //target.transform.position.x >= 16 || target.transform.position.x <= -25 //맞으면 2초있다가 삭제
        yield return new WaitUntil(() => GetComponent<Rigidbody>() == null);
        yield return new WaitForSeconds(AllWorldScript.destroycount);
            
        target.SetActive(false);

    }
    IEnumerator remove2()
    {            //target.transform.position.x >= 16 || target.transform.position.x <= -25      //시간이 지나면 삭제
        yield return new WaitForSeconds(AllWorldScript.destroycount);

        target.SetActive(false);

    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//총알없애는 스크립트

public class BulletDestroy : MonoBehaviour {    
    GameObject bullet;
	// Use this for initialization
	void Start () {
        bullet = this.gameObject;
        StartCoroutine(remove());
    }

    // Update is called once per frame
    IEnumerator remove()
    {            
        yield return new WaitForSeconds(2.0f);
        //bullet.SetActive(false);
        //Destroy(bullet);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour {
    GameObject effect;
    // Use this for initialization
    void Start()
    {
        effect = this.gameObject;
        StartCoroutine(remove());
    }

    // Update is called once per frame
    IEnumerator remove()
    {            //target.transform.position.x >= 16 || target.transform.position.x <= -25
        yield return new WaitForSeconds(1.0f);
        Destroy(effect);

    }
}

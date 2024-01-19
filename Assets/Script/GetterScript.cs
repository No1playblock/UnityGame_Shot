using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                                                //모드와 무기를 받아옴
public class GetterScript : MonoBehaviour {

    public void OnclickManyButton()
    {

        AllWorldScript.mode = "Many";
          
            Debug.Log(AllWorldScript.mode);
       
    }
    public void OnclickSpeedButton()
    {

        AllWorldScript.mode = "Speed";
        Debug.Log("Speed");

    }
    
}

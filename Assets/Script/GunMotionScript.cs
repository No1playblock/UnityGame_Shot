using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//총 모션 스크립트(버튼을 눌렀을때)       //음성

public class GunMotionScript : MonoBehaviour {

    public float animspeed = 0.0f;
    public Text NobulletText;


    // Use this for initialization
    void Awake()
    {
        AllWorldScript.anim = GetComponent<Animator>();
        AllWorldScript.shot = this.GetComponent<AudioSource>();             //shot 을 여기서 설정
        
        
    }
    public void Shot()
    {
        AllWorldScript.anim.Play("Revolver_anim", -1, animspeed);              //애니메이션
        AllWorldScript.shot.clip = AllWorldScript.Instance.Shoot;              //오디오소스
        AllWorldScript.shot.Play();
    }
    /*public void Reload()
    {
        NobulletText.text = "no bullet";
        AllWorldScript.shot.clip = AllWorldScript.Instance.reload;
        AllWorldScript.shot.Play();
    }*/
    

    
}

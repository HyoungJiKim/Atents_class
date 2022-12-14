using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 스크립트를 가지는 게임 오브젝트는 반드시 Animator을 가진다.
[RequireComponent(typeof(Animator))]
public class Explosion : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //anim.enabled = true;
        //Destroy(this.gameObject, anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
    private void OnEnable()
    {
        Destroy(this.gameObject, anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
}

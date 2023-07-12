using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLeafShake : MonoBehaviour
{
    public Animator animator;
    public GeralScript geralScript;
    // Update is called once per frame
    void Update()
    {
        if(geralScript.shake)
        {
            animator.SetBool("shake", true);
            animator.SetBool("idle", false);
        }else{
        //if(geralScript.shake == false)
            animator.SetBool("shake", false);
            animator.SetBool("idle", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFoodShake : MonoBehaviour
{
    public Animator animator;
    public FishScript fishScript;

    // Update is called once per frame
    void Update()
    { 
        if(fishScript.canShake)
        {
            animator.SetBool("shake", true);
            animator.SetBool("idle", false);
        }
        else
        {
            animator.SetBool("shake", false);
            animator.SetBool("idle", true);
        }
    }
}

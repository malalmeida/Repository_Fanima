using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinShake : MonoBehaviour
{
    public Animator animator;
    public FrogScript frogScript;

    // Update is called once per frame
    void Update()
    { 
        if(frogScript.canShake)
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

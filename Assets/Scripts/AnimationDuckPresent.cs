using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPresentCloseDuck : MonoBehaviour
{
    public Animator animator;
    FrogScript frogScript;

    // Update is called once per frame
    void Update()
    {
        if(frogScript.validationDone)
        {
            animator.SetBool("close", true);
            frogScript.validationDone = false;
        }
    }
}

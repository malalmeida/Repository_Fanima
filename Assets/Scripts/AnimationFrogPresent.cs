using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFrogPresent : MonoBehaviour
{
    public Animator animator;
    public FrogScript frogScript;

    // Update is called once per frame
    void Update()
    {

        if(frogScript.closeBox)
        {
            animator.SetBool("close", true);
        }
        else
        {
            animator.SetBool("close", false);
        }
    }
}

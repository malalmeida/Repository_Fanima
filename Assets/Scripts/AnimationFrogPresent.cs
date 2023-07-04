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
        if(frogScript.canShowImage)
        {
            animator.SetBool("open", true);
            animator.SetBool("close", false);
            animator.SetBool("animation", false);
        }
        if(frogScript.validationDone)
        {
            animator.SetBool("open", false);
            animator.SetBool("animation", true);
            animator.SetBool("close", true);
        }
    }
}

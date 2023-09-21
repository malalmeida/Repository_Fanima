using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideImageChameleon2 : MonoBehaviour
{
    public ChameleonScript chameleonScript;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(chameleonScript.hide2)
        {
            animator.SetBool("hide", true);
        }
        else
        {
            animator.SetBool("hide", false);

        }
    }
}

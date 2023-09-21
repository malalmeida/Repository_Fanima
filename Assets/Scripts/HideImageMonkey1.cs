using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideImageMonkey1 : MonoBehaviour
{
    public MonkeyScript monkeyScript;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(monkeyScript.hide1)
        {
            animator.SetBool("hide", true);
        }
        else
        {
            animator.SetBool("hide", false);

        }
    }
}

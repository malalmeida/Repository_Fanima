using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonPop : MonoBehaviour
{
    public Animator animator;
    public OwlScript owlScript;

    // Update is called once per frame
    void Update()
    {
        if(owlScript.pop)
        {
            animator.SetBool("pop", true);
        }
        else
        {
            animator.SetBool("pop", false);

        }
    }
}

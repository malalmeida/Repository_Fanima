using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMicro : MonoBehaviour
{
    public GameController GameController;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(GameController.speak)
        {
            animator.SetBool("speak", true);
        }
        else
        {
            animator.SetBool("speak", false);

        }
    }
}

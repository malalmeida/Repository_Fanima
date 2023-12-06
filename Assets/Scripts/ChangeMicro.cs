using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMicro : MonoBehaviour
{
    public GameController gameController;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(gameController.speak)
        {
            animator.SetBool("speak", true);
            gameController.startMicro = true;
        }
        else
        {
            animator.SetBool("speak", false);
        }
    }
}

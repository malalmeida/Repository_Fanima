using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class HomeScript : MonoBehaviour
{
    public GameObject femaleGuide;
    public GameObject maleGuide;
    public GameObject ten;
    public GameObject twenty;
    public GameObject thirty;
    public GameObject forty;
    public GameObject fifty;
    public GameObject sixty;
    public GameObject seventy;
    public GameObject eighty;
    public GameObject ninety;
    public GameObject oneHundred;
    public Animator balloAnimator;
    public bool doAnimation = false; 
    public bool wordsDone = false; 
    public MoveObject ballon;

    // Start is called before the first frame update
    void Start()
    {
        maleGuide.SetActive(false);
        femaleGuide.SetActive(false);
        ten.SetActive(false);
        twenty.SetActive(false);
        thirty.SetActive(false);
        forty.SetActive(false);
        fifty.SetActive(false);
        sixty.SetActive(false);
        seventy.SetActive(false);
        eighty.SetActive(false);
        ninety.SetActive(false);
        oneHundred.SetActive(false);

        balloAnimator.SetBool("isFull", true);
    }

    void Update()
    {
        if(doAnimation)
        {
            StartCoroutine(WaitToMoveUp());
        }
    }

    IEnumerator WaitToMoveUp()
    {
        if(wordsDone == true)
        {
            //transform.position += new Vector3 (0, 1, 0);
            yield return new WaitForSeconds(1.0f);
            ballon.starAnimation = true;
            doAnimation = false;
        }
        if (oneHundred.activeSelf)
        {
            oneHundred.SetActive(false);
            //transform.position += new Vector3 (0, 1, 0);
            yield return new WaitForSeconds(1.0f);
            ballon.starAnimation = true;
            doAnimation = false;
            wordsDone = true;

        }
        else if (ninety.activeSelf)
        {
            ninety.SetActive(false);
            oneHundred.SetActive(true);
            doAnimation = false;
        }
        else if (eighty.activeSelf)
        {
            eighty.SetActive(false);
            ninety.SetActive(true);
            doAnimation = false;
        }
        else if (seventy.activeSelf)
        {
            seventy.SetActive(false);
            eighty.SetActive(true);
            doAnimation = false;
        }
        else if (sixty.activeSelf)
        {
            sixty.SetActive(false);
            seventy.SetActive(true);
            doAnimation = false;
        }
        else if (fifty.activeSelf)
        {
            fifty.SetActive(false);
            sixty.SetActive(true);
            doAnimation = false;
        }
        else if (forty.activeSelf)
        {
            forty.SetActive(false);
            fifty.SetActive(true);
            doAnimation = false;
        }
        else if (thirty.activeSelf)
        {
            thirty.SetActive(false);
            forty.SetActive(true);
            doAnimation = false;
        }
        else if (twenty.activeSelf)
        {
            twenty.SetActive(false);
            thirty.SetActive(true);
            doAnimation = false;
        }
        else if(ten.activeSelf)
        {
            ten.SetActive(false);
            twenty.SetActive(true);
            doAnimation = false;
        }
        
        else if(ten.activeSelf == false && twenty.activeSelf == false && thirty.activeSelf == false && forty.activeSelf == false && fifty.activeSelf == false && sixty.activeSelf == false && seventy.activeSelf == false && eighty.activeSelf == false && ninety.activeSelf == false && oneHundred.activeSelf == false && wordsDone == false)
        {  
            ten.SetActive(true);
            doAnimation = false;
        }
        StartCoroutine(WaitForAnimationDone());
    }

    IEnumerator WaitForAnimationDone()
    {
        ballon.animationDone = false;
        yield return new WaitUntil(() => ballon.animationDone);
        doAnimation = false;
    }
}

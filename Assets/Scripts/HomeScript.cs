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

    public GameObject currentObject;
    public SpriteRenderer rend;

    public bool canShowImage = false;
    public string currentWord = "";

    public Animator balloAnimator;
    public bool doAnimation = false; 
    public bool wordsDone = false; 
    public MoveObject ballon;
    public bool animationDone = false;

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
        if(canShowImage)
        {
            WaitToShowObj();
        }
    }

    public void WaitToShowObj()
    {
        string gameObjName = currentWord + "Obj";       
        Debug.Log("OBJ " + gameObjName);

        currentObject = GameObject.Find(gameObjName);

        rend = currentObject.GetComponent<SpriteRenderer>();
        rend.sortingOrder = 10;

        currentObject.SetActive(true);
        canShowImage = false;
    }

    IEnumerator WaitToMoveUp()
    {
        if(wordsDone == true)
        {
            ballon.starAnimation = true;
            doAnimation = false;
            animationDone = false;
            yield return StartCoroutine(WaitForAnimationDone());
        }
        if (oneHundred.activeSelf)
        {
            rend.sortingOrder = -1;
            oneHundred.SetActive(false);
            ballon.starAnimation = true;
            doAnimation = false;
            wordsDone = true;
        }
        else if (ninety.activeSelf)
        {
            rend.sortingOrder = -1;
            ninety.SetActive(false);
            oneHundred.SetActive(true);
            doAnimation = false;
        }
        else if (eighty.activeSelf)
        {
            rend.sortingOrder = -1;
            eighty.SetActive(false);
            ninety.SetActive(true);
            doAnimation = false;
        }
        else if (seventy.activeSelf)
        {
            rend.sortingOrder = -1;
            seventy.SetActive(false);
            eighty.SetActive(true);
            doAnimation = false;
        }
        else if (sixty.activeSelf)
        {
            rend.sortingOrder = -1;
            sixty.SetActive(false);
            seventy.SetActive(true);
            doAnimation = false;
        }
        else if (fifty.activeSelf)
        {
            rend.sortingOrder = -1;
            fifty.SetActive(false);
            sixty.SetActive(true);
            doAnimation = false;
        }
        else if (forty.activeSelf)
        {
            rend.sortingOrder = -1;
            forty.SetActive(false);
            fifty.SetActive(true);
            doAnimation = false;
        }
        else if (thirty.activeSelf)
        {
            rend.sortingOrder = -1;
            thirty.SetActive(false);
            forty.SetActive(true);
            doAnimation = false;
        }
        else if (twenty.activeSelf)
        {
            rend.sortingOrder = -1;
            twenty.SetActive(false);
            thirty.SetActive(true);
            doAnimation = false;
        }
        else if(ten.activeSelf)
        {
            rend.sortingOrder = -1;
            ten.SetActive(false);
            twenty.SetActive(true);
            
            doAnimation = false;
        }
        
        else if(ten.activeSelf == false && twenty.activeSelf == false && thirty.activeSelf == false && forty.activeSelf == false && fifty.activeSelf == false && sixty.activeSelf == false && seventy.activeSelf == false && eighty.activeSelf == false && ninety.activeSelf == false && oneHundred.activeSelf == false && wordsDone == false)
        {  
            ten.SetActive(true);
            rend.sortingOrder = -1;
            doAnimation = false;
        }
    }

    IEnumerator WaitForAnimationDone()
    {
        ballon.animationDone = false;
        yield return new WaitUntil(() => ballon.animationDone);
        animationDone = true;
        doAnimation = false;
    }
}

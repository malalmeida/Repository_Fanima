using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;

public class GeralScript : MonoBehaviour
{
    public GameObject femaleGuide;

    public GameObject currentObject;
    public SpriteRenderer rend;

    public GameObject currentLeaf;

    public bool canShowImage = false;
    public string currentWord = "";

    public Animator balloAnimator;
    public bool doAnimation = false; 
    public bool wordsDone = false; 
    public MoveObject ballon;
    public bool animationDone = false;
    public bool showBoard = false;

    public bool shake = false;
    public bool idle = true;

    public bool startValidation = false;
    public int leafNumber = 0;

    public AudioSource leavesShaking;

    public Image barImage;
    public float incrementAmountWords = 0.1f;
    public float incrementAmountSentences = 0.17f;

    // Start is called before the first frame update
    void Start()
    {
        balloAnimator.SetBool("isFull", true);

        barImage.fillAmount = 0.0f;
    }

    void Update()
    {
        if(doAnimation)
        {
            StartCoroutine(MoveBallon());
            startValidation = false;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    if(hit.collider.CompareTag("Leaf0"))
                    {
                        ShowObj();
                        shake = false;
                        leavesShaking.Stop();
                        startValidation = true;
                        barImage.fillAmount += incrementAmountWords;
                    }
                }
            }
        }
    }

    public void ShowObj()
    {
        string gameObjName = currentWord + "Obj";       
        Debug.Log("OBJ " + gameObjName);

        currentObject = GameObject.Find(gameObjName);

        rend = currentObject.GetComponent<SpriteRenderer>();
        rend.sortingOrder = 10;

        leavesShaking.Stop();

        currentObject.SetActive(true);
        canShowImage = false;
    }

    IEnumerator MoveBallon()
    {
        ballon.starAnimation = true;
        doAnimation = false;
        animationDone = false;
        yield return StartCoroutine(WaitForAnimationDone());
        if(wordsDone == false)
        {
            shake = true;
            leavesShaking.Play();
        }
    /**
        if(wordsDone == true)
        {
            ballon.starAnimation = true;
            doAnimation = false;
            animationDone = false;
            rend.sortingOrder = -1;
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
        **/
    }

    IEnumerator WaitForAnimationDone()
    {
        ballon.animationDone = false;
        yield return new WaitUntil(() => ballon.animationDone);
        animationDone = true;
        doAnimation = false;
    }
}

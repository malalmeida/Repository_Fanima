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
    public GameObject ondeHundred;
    public Animator balloAnimator;
    public bool moveUp = false; 
    public int upTimes = 0;           
    //private Rigidbody2D rb;


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
        ondeHundred.SetActive(false);

        balloAnimator.SetBool("isFull", true);
        //rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        StartCoroutine(WaitToMoveUp());
    }

/*
   private void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if(other.gameObject.CompareTag("Rock"))
        {
            maleGuide.SetActive(false);
            femaleGuide.SetActive(false);

            balloAnimator.SetBool("isFull", true);

            StartCoroutine(WaitAndTakeOff());
        }
        
    }
*/
    IEnumerator WaitToMoveUp()
    {
        yield return new WaitUntil(() => moveUp);
        //if(Input.GetKeyDown(KeyCode.Space))
        if (upTimes == 1)
        {
            ten.SetActive(true);
            moveUp = false;
        }

        if (upTimes == 2)
        {
            ten.SetActive(false);
            twenty.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 3)
        {
            twenty.SetActive(false);
            thirty.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 4)
        {
            thirty.SetActive(false);
            forty.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 5)
        {
            forty.SetActive(false);
            fifty.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 6)
        {
            fifty.SetActive(false);
            sixty.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 7)
        {
            sixty.SetActive(false);
            seventy.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 8)
        {
            seventy.SetActive(false);
            eighty.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 9)
        {
            eighty.SetActive(false);
            ninety.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 10)
        {
            ninety.SetActive(false);
            ondeHundred.SetActive(true);
            moveUp = false;
        }
        if (upTimes == 14)
        {
            transform.position += new Vector3 (0, -4, 0); 
            upTimes = 16;
            moveUp = false;  
        }
        if(upTimes == 18)
        {
            SceneManager.LoadScene("Travel");
        }
        if (upTimes > 10)
        {
            transform.position += new Vector3 (0, 1, 0);
            moveUp = false;
            //rb.velocity = Vector2.down * 10 * Time.deltaTime;
        }
    }
}

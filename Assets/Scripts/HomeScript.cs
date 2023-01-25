using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class HomeScript : MonoBehaviour
{
   // public GameObject femaleGuide;
   // public GameObject maleGuide;
    public Animator balloAnimator;
    public bool moveUp = false; 
    public int upTimes = 0;           
    //private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        StartCoroutine(WaitToMove());
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
    IEnumerator WaitToMove()
    {
        yield return new WaitUntil(() => moveUp);
        //if(Input.GetKeyDown(KeyCode.Space))
            if (upTimes == 6)
            {
                transform.position += new Vector3 (0, -4, 0);

                //for(int i=0; i <=3; i++)
                //{
                    //transform.position += new Vector3 (0, -1, 0);
                    upTimes += 4;
                    moveUp = false;
                //}
            }

            if (upTimes == 15)
            {
                transform.position += new Vector3 (0, -7, 0);

                //for(int i=0; i <=5; i++)
                //{
                    //transform.position += new Vector3 (0, -3, 0);
                    upTimes += 7;
                    moveUp = false;
                //}
            }

            else
            {
                transform.position += new Vector3 (0, 1, 0);
                upTimes ++;
                moveUp = false;
            //rb.velocity = Vector2.down * 10 * Time.deltaTime;
            //transform.position += new Vector3 (0, 1, 0);
            }
    }
    

    IEnumerator WaitAndTakeOff()
    {
        yield return new WaitForSeconds(2f);

        //rb.velocity = Vector2.up * 4900 * Time.deltaTime;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Travel");

    }

    IEnumerator Teste()
    {
        yield return new WaitForSeconds(1.5f);
         //if(other.gameObject.CompareTag("Rock"))
        //{
            //maleGuide.SetActive(false);
            //femaleGuide.SetActive(false);

            balloAnimator.SetBool("isFull", true);

            StartCoroutine(WaitAndTakeOff());
        //}
       // SceneManager.LoadScene("Frog");

    }
}

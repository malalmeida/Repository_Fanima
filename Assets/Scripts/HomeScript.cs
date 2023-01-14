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
    public Animator balloAnimator;    
    
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(Teste());
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
            
            //rb.velocity = Vector2.down * 10 * Time.deltaTime;
        //}
    }

     private void OnCollisionEnter2D(Collision2D other)
    {
      if(other.gameObject.CompareTag("Rock"))
      {
        maleGuide.SetActive(false);
        femaleGuide.SetActive(false);

        balloAnimator.SetBool("isFull", true);

       StartCoroutine(WaitAndTakeOff());
       

      }
    }

    IEnumerator WaitAndTakeOff()
    {
        yield return new WaitForSeconds(2f);
        rb.velocity = Vector2.up * 4900 * Time.deltaTime;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Travel");

    }

    IEnumerator Teste()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Frog");

    }
}

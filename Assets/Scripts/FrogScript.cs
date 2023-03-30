using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

public class FrogScript : MonoBehaviour
{
  public Animator animator;
    
  private float playerSpeed;
  private float jumpSpeed;
  private bool isJumping;
  private float move;
  private Rigidbody2D rb;
  public AudioSource waterSplashSound;
  public int numberOfJumps = 0;
  public int randomIndex = -1;
  public bool isCaught = false;

  public GameObject bug1;
  public GameObject bug2;
  public GameObject bug3;
  public GameObject bug4;
  public GameObject bug5;
  public GameObject bug6;
  public GameObject bug7;
  public GameObject bug8;
  public GameObject bug9;
  public GameObject bug10;
  public GameObject bug11;
  public GameObject bug12;
  public GameObject bug13;
  public GameObject bug14;

  public GameObject bugTouch1;
  public GameObject bugTouch2;
  public GameObject bugTouch3;
  public GameObject bugTouch4;
  public GameObject bugTouch5;
  public GameObject bugTouch6;
  public GameObject bugTouch7;
  public GameObject bugTouch8;
  public GameObject bugTouch9;
  public GameObject bugTouch10;
  public GameObject bugTouch11;
  public GameObject bugTouch12;
  public GameObject bugTouch13;
  public GameObject bugTouch14;
  public List<GameObject> bugList;
  public int bugsFound = 0;

  public GameObject currentBug;


  [SerializeField] private ParticleSystem confetti;

  private float dist;
  private bool dragging  = false;
  private Vector3 offset;
  private Transform toDrag;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    bugList = new List<GameObject>();
    bugList.Add(bug1);
    bugList.Add(bug2);
    bugList.Add(bug3);
    bugList.Add(bug4);
    bugList.Add(bug5);
    bugList.Add(bug6);
    bugList.Add(bug7);
    bugList.Add(bug8);
    bugList.Add(bug9);
    bugList.Add(bug10);
    bugList.Add(bug11);
    bugList.Add(bug12);
    bugList.Add(bug13);
    bugList.Add(bug14);
    
    bug1.SetActive(false);
    bug2.SetActive(false);
    bug3.SetActive(false);
    bug4.SetActive(false);
    bug5.SetActive(false);
    bug6.SetActive(false);
    bug7.SetActive(false);
    bug8.SetActive(false);
    bug9.SetActive(false);
    bug10.SetActive(false);
    bug11.SetActive(false);
    bug12.SetActive(false);
    bug13.SetActive(false);
    bug14.SetActive(false);
    
  }

  void Update()
  {
    if(randomIndex > -1)
    {
        WaitToPickBug();
    }
        
    if(Input.GetMouseButtonDown(0))
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if(Physics.Raycast(ray, out hit))
      {
        if(hit.collider != null)
        {                  
          
          if(hit.collider.CompareTag("Bug1"))
          {
            if(randomIndex == 0)
            {
              bugTouch1.SetActive(false);
              bug1.SetActive(false);
              bugsFound ++;
              isCaught = true; 
            }
          } 
          if(hit.collider.CompareTag("Bug2"))
          {
            if(randomIndex == 1)
            {
              bugTouch2.SetActive(false);
              bug2.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug3"))
          {
            if(randomIndex == 2)
            {
              bugTouch3.SetActive(false);
              bug3.SetActive(false);     
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug4"))
          {
            if(randomIndex == 3)
            {
              bugTouch4.SetActive(false);
              bug4.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            
          } 
          if(hit.collider.CompareTag("Bug5"))
          {
            if(randomIndex == 4)
            {
              bugTouch5.SetActive(false);
              bug5.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug6"))
          {
            if(randomIndex == 5)
            {
              bugTouch6.SetActive(false);
              bug6.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug7"))
          {
            if(randomIndex == 6)
            {
              bugTouch7.SetActive(false);
              bug7.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug8"))
          {
            if(randomIndex == 7)
            {
              bugTouch8.SetActive(false);
              bug8.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug9"))
          {
            if(randomIndex == 8)
            {
              bugTouch9.SetActive(false);
              bug9.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug10"))
          {
            if(randomIndex == 9)
            {
              bugTouch10.SetActive(false);
              bug10.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug11"))
          {
            if(randomIndex == 10)
            {
              bugTouch11.SetActive(false);
              bug11.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug12"))
          {
            if(randomIndex == 11)
            {
              bugTouch12.SetActive(false);
              bug12.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          } 
          if(hit.collider.CompareTag("Bug13"))
          {
            if(randomIndex == 12)
            {
              bugTouch13.SetActive(false);
              bug13.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          }  
          if(hit.collider.CompareTag("Bug14"))
          {
            if(randomIndex == 13)
            {
              bugTouch14.SetActive(false);
              bug14.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
          }   
        }
      }
      Debug.Log("bugsFound " + bugsFound);
    }
    Vector3 v3;
/*
    if(Input.touchCount != 1)
    {
      dragging = false;
      return;
    }

    Touch touch = Input.touches[0];
    Vector3 pos = touch.position;

    if(touch.phase == TouchPhase.Began)
    {
      Ray ray = Camera.main.ScreenPointToRay(pos);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        if(hit.collider.tag =="bug")
        {
          toDrag = hit.transform;
          dist = hit.transform.position.z - Camera.main.transform.position.z;
          v3 = new Vector3(pos.x, pos.y, dist);
          v3 = Camera.main.ScreenToWorldPoint(v3);
          offset = toDrag.position - v3;
          dragging = true;
        }    
      }
    }

    if(dragging && touch.phase == TouchPhase.Moved)
    {
      v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
      v3 = new Camera.main.ScreenToWorldPoint(v3);
      toDrag.position = v3 + offset;
    }

    if(dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
    {
      dragging = false;
    }
*/
    rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
      
    animator.SetFloat("yVelocity", rb.velocity.y);

    for (int i =0; i<2 ; i++)
    {
      if(bugsFound == 14 && !isJumping)
      {
        animator.SetBool("Jump", true);
        playerSpeed = 1.8f;

        rb.AddForce(new Vector2(rb.velocity.x, 500));

        isJumping = true;
      }
    }
   /*
    if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
    {
      animator.SetBool("Jump", true);
      playerSpeed = 1.8f;

      rb.AddForce(new Vector2(rb.velocity.x, 500));

      isJumping = true;
    }
    */
  }

  public void  WaitToPickBug()
  {
    Debug.Log("BUG NUMBER " + randomIndex);
    bugList[randomIndex].SetActive(true);

    randomIndex = -1;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if(other.gameObject.CompareTag("Leaf0"))
    {
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;

      waterSplashSound.Play();    
    }
    
    if(other.gameObject.CompareTag("Leaf1"))
    {
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;

      waterSplashSound.Play();

    }

    if(other.gameObject.CompareTag("Leaf2"))
    {
      playerSpeed = 0;
      animator.SetBool("Jump", false);
        
      //animator.SetBool("Catch", true);
      isJumping = false;

      waterSplashSound.Play();  

      //if(bugsFound == 13)
      //{
        confetti.Play();
      //}     
    }
  }
}

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
  public int bugNumber = -1;
  public bool isCaught = false;
  public List<int> removedBugs;
  public bool chapterFinished = false;
  public bool canShow = false;

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

  public GameObject dialogCloud;

  public GameObject currentBug;

  public GameObject currentObject;
  public SpriteRenderer rend;
  public bool canShowImage = false;
  public string currentWord = "";

  public bool validationDone = false;


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
    removedBugs = new List<int>();
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
    if(canShowImage)
    {
      WaitToShowObj();
    }
    if(canShow)
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
            if(bugNumber == 0)
            {
              bugTouch1.SetActive(false);
              HideObj();
              canShow = false;
              bug1.SetActive(false);
              bugsFound ++;
              isCaught = true; 
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug2"))
          {
            if(bugNumber == 1)
            {
              bugTouch2.SetActive(false);
              HideObj();
              canShow = false;
              bug2.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug3"))
          {
            if(bugNumber == 2)
            {
              bugTouch3.SetActive(false);
              HideObj();
              canShow = false;
              bug3.SetActive(false);     
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug4"))
          {
            if(bugNumber == 3)
            {
              bugTouch4.SetActive(false);
              HideObj();
              canShow = false;
              bug4.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
            
          } 
          else if(hit.collider.CompareTag("Bug5"))
          {
            if(bugNumber == 4)
            {
              bugTouch5.SetActive(false);
              HideObj();
              canShow = false;
              bug5.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug6"))
          {
            if(bugNumber == 5)
            {
              bugTouch6.SetActive(false);
              HideObj();
              canShow = false;
              bug6.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug7"))
          {
            if(bugNumber == 6)
            {
              bugTouch7.SetActive(false);
              HideObj();
              canShow = false;
              bug7.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug8"))
          {
            if(bugNumber == 7)
            {
              bugTouch8.SetActive(false);
              HideObj();
              canShow = false;
              bug8.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug9"))
          {
            if(bugNumber == 8)
            {
              bugTouch9.SetActive(false);
              HideObj();
              canShow = false;
              bug9.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug10"))
          {
            if(bugNumber == 9)
            {
              bugTouch10.SetActive(false);
              HideObj();
              canShow = false;
              bug10.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug11"))
          {
            if(bugNumber == 10)
            {
              bugTouch11.SetActive(false);
              HideObj();
              canShow = false;
              bug11.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug12"))
          {
            if(bugNumber == 11)
            {
              bugTouch12.SetActive(false);
              HideObj();
              canShow = false;
              bug12.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          else if(hit.collider.CompareTag("Bug13"))
          {
            if(bugNumber == 12)
            {
              bugTouch13.SetActive(false);
              HideObj();
              canShow = false;
              bug13.SetActive(false);
              bugsFound ++;
              isCaught = true;
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          }  
          else if(hit.collider.CompareTag("Bug14"))
          {
            if(bugNumber == 13)
            {
              bugTouch14.SetActive(false);
              HideObj();
              canShow = false;
              bug14.SetActive(false);
              bugsFound ++;
              isCaught = true;
              dialogCloud.SetActive(false);
            }
            else
            {
              Debug.Log("PROCURA MELHOR");
            }
          } 
          Debug.Log("removedBugs " + bugsFound);  
        }
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
    Vector3 v3;
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

  public void HideObj()
  {
    rend.sortingOrder = -1;
  }


  public void WaitToPickBug()
  {
    bugList[bugNumber].SetActive(true);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if(other.gameObject.CompareTag("Leaf0"))
    {
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
    }
    
    if(other.gameObject.CompareTag("Leaf1"))
    {
      //confetti.Play();
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
        
      isJumping = false;

      waterSplashSound.Play();  
      chapterFinished = true;
      
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;
using UnityEngine.UI;


public class FrogScript : MonoBehaviour
{
  public Animator animator;
    
  private float playerSpeed;
  private float jumpSpeed;
  private bool isJumping;
  private float move;
  private Rigidbody2D rb;
  public AudioSource waterSplashSound;
  public AudioSource coinSound;
  public AudioSource nonoSound;
  public int numberOfJumps = 0;
  public int bugNumber = -1;
  public bool isCaught = false;
  public List<int> removedBugs;
  public bool chapterFinished = false;
  public bool canShowBug = false;

   public MoveObject youCantSeeMe;

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
  public bool canShake = false;
  public MoveObject circle;

  [SerializeField] private ParticleSystem confetti;

  private float dist;
  private bool dragging  = false;
  private Vector3 offset;
  private Transform toDrag;

  public bool closeBox = false;
  public int rock = -1;

  public Image barImage;
  public float incrementAmount = 0.066f;

  public GameObject rewardBoard;
  public SpriteRenderer rendRewardBoard;

  public bool showReward = false;

  public int coinPosition = -1;

  // Start is called before the first frame update
  void Start()
  {
    barImage.fillAmount = 0.0f;

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
    if(showReward)
    {
      ShowRewardBoard();
    }

    if(canShowImage)
    {
      ShowObj();
    }
    //if(canShowBug)
    //{
        //WaitToPickBug();

    //}
    if(validationDone)
    {
        //closeBox = true;
        validationDone = false;
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
            //if(currentWord == "pato" && canShake == true)
            if(coinPosition == 0 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
          } 
          else if(hit.collider.CompareTag("Bug2"))
          {
            //if(currentWord == "sapo" && canShake == true)
            if(coinPosition == 1 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug3"))
          {
            //if(currentWord == "bolo" && canShake == true)
            if(coinPosition == 2 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug4"))
          {
            //if(currentWord == "lobo" && canShake == true) 
            if(coinPosition == 3 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
            
          } 
          else if(hit.collider.CompareTag("Bug5"))
          {
            //if(currentWord == "teia" && canShake == true) 
            if(coinPosition == 4 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug6"))
          {
            //if(currentWord == "dado" && canShake == true)
            if(coinPosition == 5 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug7"))
          {
            //if(currentWord == "camisa" && canShake == true)
            if(coinPosition == 6 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug8"))
          {
            //if(currentWord == "vaca" && canShake == true)
            if(coinPosition == 7 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug9"))
          {
            //if(currentWord == "gato" && canShake == true)
            if(coinPosition == 8 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug10"))
          {
            //if(currentWord == "dragão" && canShake == true)
            if(coinPosition == 9 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug11"))
          {
            //if(currentWord == "mesa" && canShake == true)
            if(coinPosition == 10 && canShake == true){
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug12"))
          {
            //if(currentWord == "goma" && canShake == true)
            if(coinPosition == 11 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          else if(hit.collider.CompareTag("Bug13"))
          {
            //if(currentWord == "novelo" && canShake == true)
            if(coinPosition == 12 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          }  
          else if(hit.collider.CompareTag("Bug14"))
          {
            //if(currentWord == "chinelo" && canShake == true)
            if(coinPosition == 13 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
           else if(hit.collider.CompareTag("Bug15"))
          {
            //if(currentWord == "joaninha" && canShake == true)
            if(coinPosition == 14 && canShake == true)
            {
              barImage.fillAmount += incrementAmount;
              canShake = false;
              FrogJump();
              //MoveCamera();
            }
            else
            {
              //nonoSound.Play();
            }
          } 
          //Debug.Log("removedBugs " + bugsFound);
          //closeBox = false;

        }
      }
    }
    Vector3 v3;
    rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
                
    animator.SetFloat("yVelocity", rb.velocity.y);

    //for (int i =0; i<2 ; i++)
    //{
      //if(bugsFound == 14 && !isJumping)
        //{
        //animator.SetBool("Jump", true);
        //playerSpeed = 1.8f;

        //rb.AddForce(new Vector2(rb.velocity.x, 500));
        //isJumping = true;
      //}
    //}
  }

  IEnumerator WaitForCameraAnimationDone()
    {
        circle.starAnimation = true;
        yield return new WaitUntil(() => circle.starAnimation);
        circle.animationDone = false;
        //canShake = false;
        yield return new WaitUntil(() => circle.animationDone);
        //HideObj();

    }

    public void MoveCamera()
    {
        //if(canShake)
        //{
      StartCoroutine(WaitForCameraAnimationDone());
        //}
    }

  public void FrogJump()
  {
    if(isJumping == false)
    {
      animator.SetBool("Jump", true);
      //playerSpeed = 1.8f;
      playerSpeed = 2.0f;
      if( rock == 1 || rock == 3 || rock == 5 || rock == 7 || rock == 9 || rock == 11 || rock == 13 || rock == 15)
      {
        playerSpeed = playerSpeed - 0.2f;
      }
      else
      {
        playerSpeed = playerSpeed + 0.1f;
      }
      //playerSpeed = playerSpeed - 0.1f ;
      rb.AddForce(new Vector2(rb.velocity.x, 500));
      isJumping = true;
    } 
  }

  public void ShowObj()
  {
    string gameObjName = currentWord + "Obj" + coinPosition.ToString();       
    Debug.Log("OBJ " + gameObjName);

    currentObject = GameObject.Find(gameObjName);
    currentObject.SetActive(true);

    rend = currentObject.GetComponent<SpriteRenderer>();
    rend.sortingOrder = 10;

    canShowImage = false;
  }

  public void HideObj()
  {
    rend.sortingOrder = -1;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if(other.gameObject.CompareTag("Rock1"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 1;
    }
    
    if(other.gameObject.CompareTag("Rock2"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 2;
    }

    if(other.gameObject.CompareTag("Rock3"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 3;
    }
    if(other.gameObject.CompareTag("Rock4"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 4;
    }
    if(other.gameObject.CompareTag("Rock5"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 5;
    }
    if(other.gameObject.CompareTag("Rock6"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 6; 
    }
    if(other.gameObject.CompareTag("Rock7"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 7;
    }
    if(other.gameObject.CompareTag("Rock8"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 8;
    }
    if(other.gameObject.CompareTag("Rock9"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 9;
    }
    if(other.gameObject.CompareTag("Rock10"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 10;
    }
    if(other.gameObject.CompareTag("Rock11"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 11;
    }
    if(other.gameObject.CompareTag("Rock12"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 12;
    }
    if(other.gameObject.CompareTag("Rock13"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 13;
    }
    if(other.gameObject.CompareTag("Rock14"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 14;
    }
    if(other.gameObject.CompareTag("Rock15"))
    {
      coinSound.Play();
      playerSpeed = 0;
      animator.SetBool("Jump", false);
      animator.SetBool("Catch", false);
      isJumping = false;
      HideObj();
      isCaught = true; 
      rock = 15;

    }
  }
  
  public void ShowRewardBoard()
    {
        rendRewardBoard = rewardBoard.GetComponent<SpriteRenderer>();
        rendRewardBoard.sortingOrder = 20;
    }

}

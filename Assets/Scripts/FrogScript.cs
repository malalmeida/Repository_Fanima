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

  [SerializeField] private AudioSource userRecording;
  [SerializeField] private ParticleSystem confetti;

  private float dist;
  private bool dragging  = false;
  private Vector3 offset;
  private Transform toDrag;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
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

    if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
    {
      animator.SetBool("Jump", true);
      playerSpeed = 1.5f;

      rb.AddForce(new Vector2(rb.velocity.x, 500));

      isJumping = true;
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if(other.gameObject.CompareTag("Leaf0"))
    {
        
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
      animator.SetBool("Catch", false);
      isJumping = false;

      waterSplashSound.Play();    
    }
      
    if(other.gameObject.CompareTag("Leaf3"))
    {
      playerSpeed = 0;
      animator.SetBool("Jump", false);
        
      animator.SetBool("Catch", true);
      isJumping = false;

      waterSplashSound.Play();  

      if(numberOfJumps == 14)
      {
        confetti.Play();
      }        
    }
  }
}

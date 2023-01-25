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
    //[SerializeField] private TextMeshProUGUI wordToSay;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
      
      animator.SetFloat("yVelocity", rb.velocity.y);

      if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
      {
        animator.SetBool("Jump", true);
        playerSpeed = 1.5f;

        //rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
using TMPro;

public class FrogScript : MonoBehaviour
{
    public GameInputController GameInputController;
    //List<string> listOfWords;

    public Animator animator;
    
    private float playerSpeed;
    private float jumpSpeed;
    private bool isJumping;
    private float move;
    private Rigidbody2D rb;

    [SerializeField] private AudioSource userRecording;
    [SerializeField] private TextMeshProUGUI wordToSay;

    void Awake(){
     
    }

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
        wordToSay.text = PlayerPrefs.GetString("Frog0");
        RecordSound();      
      }
    
      if(other.gameObject.CompareTag("Leaf1"))
      {

        wordToSay.text = PlayerPrefs.GetString("Frog1");

        playerSpeed = 0;
        animator.SetBool("Jump", false);
        animator.SetBool("Catch", false);
        isJumping = false;
        SaveSound(PlayerPrefs.GetString("Frog0"));
        RecordSound(); 

      }

      if(other.gameObject.CompareTag("Leaf2"))
      {

        wordToSay.text = PlayerPrefs.GetString("Frog2");

        playerSpeed = 0;
        animator.SetBool("Jump", false);
        animator.SetBool("Catch", false);
        isJumping = false;
        SaveSound(PlayerPrefs.GetString("Frog1"));
        RecordSound(); 
      }

      
      if(other.gameObject.CompareTag("Leaf3"))
      {

        wordToSay.text = PlayerPrefs.GetString("Frog3");

        playerSpeed = 0;
        animator.SetBool("Jump", false);
        animator.SetBool("Catch", false);
        isJumping = false;
        SaveSound(PlayerPrefs.GetString("Frog2"));
        RecordSound();
      }

      if(other.gameObject.CompareTag("Leaf4"))
      {

        wordToSay.text = PlayerPrefs.GetString("Frog4");

        playerSpeed = 0;
        animator.SetBool("Jump", false);
        animator.SetBool("Catch", false);
        isJumping = false;
        SaveSound(PlayerPrefs.GetString("Frog3"));
        RecordSound();
      }
      
      if(other.gameObject.CompareTag("Leaf5"))
      {
        wordToSay.text = "Boa!";
        playerSpeed = 0;
        animator.SetBool("Jump", false);
        animator.SetBool("Catch", true);
        isJumping = false;
        SaveSound(PlayerPrefs.GetString("Frog4"));
      }


    }
    
    void RecordSound()
    {
      userRecording = GetComponent<AudioSource>();
      userRecording.clip = Microphone.Start("", true, 2, 48000);
    }

    void SaveSound(string fileName)
    {
      SavWav.Save(fileName + ".wav", userRecording.clip);
    }
}

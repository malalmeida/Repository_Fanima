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
    List<string> listOfWords = new List<string>();

    public Animator animator;
    
    private float playerSpeed;
    private float jumpSpeed;
    private bool isJumping;
    private float move;
    private Rigidbody2D rb;

    [SerializeField] private AudioSource userRecording;

    void Awake(){
      listOfWords = GameInputController.repositoryOfWords;
    }

    // Start is called before the first frame update
    void Start()
    {
      //words.Add("Pato");
      //words.Add("Bola");
      //words.Add("Cadeira");
      //words.Add("Mola");
      //words.Add("Dado"); 

      rb = GetComponent<Rigidbody2D>();
  
    }

    void Update()
    {
      rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
      
      animator.SetFloat("yVelocity", rb.velocity.y);

      if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
      {
        Debug.Log(listOfWords[0]);

        animator.SetBool("Jump", true);
        playerSpeed = 1.5f;

        //rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
        rb.AddForce(new Vector2(rb.velocity.x, 500));

        isJumping = true;
      }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if(other.gameObject.CompareTag("Leaf"))
      {
        playerSpeed = 0;
        animator.SetBool("Jump", false);
        animator.SetBool("Catch", false);
        isJumping = false;
      }

      if(other.gameObject.CompareTag("LastLeaf"))
      {
        playerSpeed = 0;
        animator.SetBool("Jump", false);
        animator.SetBool("Catch", true);
        isJumping = false;
      }


    }

    void ShowWord()
    {
      //nextWord.text = words[0];
    }

    //private List<string> PrepWords(List<string> rep)
    //{
      //rep.Sort((a, b) => a.Length.CompareTo(b.Length));
      //return rep;
    //}
    
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

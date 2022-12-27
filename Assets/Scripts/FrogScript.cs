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

    bool jump;
    //bool catch;


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

      //frogAnimator.SetBool("jump", false);
      //frogAnimator.SetBool("lastWord", false);  

      rb = GetComponent<Rigidbody2D>();
  
      Debug.Log(listOfWords[0]);
    }

    void Update()
    {

     // rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
      rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

    
      if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
      {
        animator.SetBool("Jump", true);
        jump = true;
        playerSpeed = 1.5f;

        //rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
        rb.AddForce(new Vector2(rb.velocity.x, 500));

        isJumping = true;
      }

      if(Input.GetKeyUp(KeyCode.Space))
      {
        jump = false;
      }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if(other.gameObject.CompareTag("Leaf"))
      {
        //playerSpeed = -1.5f;
        animator.SetBool("Jump", false);
        animator.SetBool("Catch", false);
        isJumping = false;
      }
    }
    


    void Jump()
    {
      
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

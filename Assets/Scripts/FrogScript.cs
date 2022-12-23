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

      Debug.Log(listOfWords[0]);
    }

    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Space))
      {
        animator.SetBool("Jump", true);
        jump = true;
      }
       

      if(Input.GetKeyUp(KeyCode.Space))
        jump = false;

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

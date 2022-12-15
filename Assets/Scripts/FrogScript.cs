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
    List<string> words = new List<string>();
    public Animator frogAnimator;
    [SerializeField] private AudioSource userRecording;

    [SerializeField] Transform[] Positions;
    [SerializeField] float ObjectSpeed;
    Transform NextPosition;
    int NextPositionIndex;

    // Start is called before the first frame update
    void Start()
    {
      words.Add("Pato");
      words.Add("Bola");
      words.Add("Cadeira");
      words.Add("Mola");
      words.Add("Dado");


      //frogAnimator.SetBool("jump", false);
      //frogAnimator.SetBool("lastWord", false);

      //words = PrepWords(GameInputController.repositoryOfWords);
      //foreach (string item in words)
      //{
         //Debug.Log("PALAVRA " + item);
      //}

      NextPosition = Positions[0];
    }

    void Update()
    {
      
    }

    void Jump()
    {
      
    }

    void MoveObject()
    {
        if(transform.position == NextPosition.position)
        {
          NextPositionIndex ++;
          if (NextPositionIndex >= Positions.Length)
          {
            NextPositionIndex = 0;
          }
          NextPosition = Positions[NextPositionIndex];
        }
        else
        {
          transform.position = Vector3.MoveTowards(transform.position, NextPosition.position, ObjectSpeed = Time.deltaTime);
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

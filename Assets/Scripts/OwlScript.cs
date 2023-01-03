using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;

public class OwlScript : MonoBehaviour
{
    public Animator animator;
    
    [SerializeField] private AudioSource userRecording;

    // Start is called before the first frame update
    void Start()
    {
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureVoice : MonoBehaviour
{
    [SerializeField] private AudioSource userRecording;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
        RecordSound();
        }
        if (Input.GetKeyDown("up")) 
        {
        SavWav.Save("input.wav", userRecording.clip);
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

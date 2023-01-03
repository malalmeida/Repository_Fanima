using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
public class SpeechRecognizer : MonoBehaviour
{
   private List<string> repository = new List<string>();
    //public Animator frogAnimator;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    [SerializeField] private AudioSource userRecording;
    private bool actionDone = false;


    // Start is called before the first frame update
    void Start()
    {
        PrepRepository();

        while (repository.Count > 0) 
        {
            AskToSpeak(repository[0]);
            repository.RemoveAt(0);
            actions.Remove(repository[0]);
        }
    }

    //from short to longest 
    void SortByStringLength()
    {
        repository.Sort((a, b) => a.Length.CompareTo(b.Length));
    }

    void PrepRepository()
    {   
        repository.Add("Pato");
        repository.Add("Sapato");
        repository.Add("Sopa");
        repository.Add("Portugal");
        repository.Add("Porta");

        SortByStringLength();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    
    void RecordSound()
    {
        userRecording = GetComponent<AudioSource>();
        userRecording.clip = Microphone.Start("", true, 2, 48000);
    }

    private void StartRecognize()
    {
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

     void SaveSound(string fileName)
    {
        SavWav.Save(fileName + ".wav", userRecording.clip);
    }

    private void AskToSpeak(string word)
    {
        actions.Add(word, Jump);
        StartRecognize();
        RecordSound();

        while (actionDone == false)
        {
        SaveSound(word);
        }
    }

    private void Jump()
    {
        Debug.Log("ouvi");
        actionDone = true;
    }
}

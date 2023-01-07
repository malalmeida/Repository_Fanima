using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
  public bool structReqDone = false;
  public bool respositoryReqDone = false;
  public int gameexecutionid = -1;
  public List<actionClass> contentList;
  public List<dataSource> dataList;
  public List<string> repositoryOfWords; 
  public WebRequests webRequests;
    
  [SerializeField] private AudioSource userRecording;


  void Awake()
  {

  }

  // Start is called before the first frame update
  void Start()
  {
  //Debug.Log("DATA E HORA: " + System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + " UTC");
    List<string> repositoryOfWords = new List<string>();  
    if(SceneManager.GetActiveScene().name == "Home")
    {
      StartCoroutine(InitiateGame());
      StartCoroutine(PreparedToStart());   
    } 
  }

  void Update()
  {
    //if(SceneManager.GetActiveScene().name == "Frog");


  }

  IEnumerator InitiateGame()
  {
    Debug.Log("Waiting for structure...");
    yield return new WaitUntil(() => structReqDone);
    Debug.Log("Structure request completed! Actions: " + contentList.Count);
    
    Debug.Log("Waiting for word repository...");
    yield return new WaitUntil(() => respositoryReqDone);
    Debug.Log("Repository request completed! Words: " + dataList.Count);

    //prep repository of strings
    for (int i = 0; i < contentList.Count; i++)
    {
      for (int j = 0; j < dataList.Count; j++)
      {
        if(contentList[i].word == dataList[j].id)
        {
          repositoryOfWords.Add(dataList[j].name);
          PlayerPrefs.SetString("Frog" + i , dataList[j].name);
        }
      }
    }
        
    foreach (string w in repositoryOfWords)
    {  
      Debug.Log("Words -> " + w);
    }
    
    Debug.Log("Repository " + repositoryOfWords.Count);
    }

    IEnumerator PreparedToStart()
    {
      Debug.Log("Waiting for execution ID...");
      yield return new WaitUntil(() => gameexecutionid > 0);
      Debug.Log("Game Execution request completed! ID -> " + gameexecutionid);
      //PlayerPrefs.SetInt("GAMEEXID",gameexecutionid);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if(other.gameObject.CompareTag("Leaf0"))
      {
        RecordSound();
      }
    
      if(other.gameObject.CompareTag("Leaf1"))
      {        
        SaveSound(PlayerPrefs.GetString("Frog0"));
        byte[] byteArray = SavWav.audiobyte;

        StartCoroutine(webRequests.PostSample(byteArray, "181", gameexecutionid.ToString()));
        
        RecordSound(); 
      }

      if(other.gameObject.CompareTag("Leaf2"))
      {
        SaveSound(PlayerPrefs.GetString("Frog1"));
        byte[] byteArray = SavWav.audiobyte;
        RecordSound(); 
      }

      
      if(other.gameObject.CompareTag("Leaf3"))
      {
        SaveSound(PlayerPrefs.GetString("Frog2"));
        byte[] byteArray = SavWav.audiobyte;
        RecordSound();
      }

      if(other.gameObject.CompareTag("Leaf4"))
      {
        SaveSound(PlayerPrefs.GetString("Frog3"));
        byte[] byteArray = SavWav.audiobyte;
        RecordSound();
      }
      
      if(other.gameObject.CompareTag("Leaf5"))
      {
        SaveSound(PlayerPrefs.GetString("Frog4"));
        byte[] byteArray = SavWav.audiobyte;
      }

    }

    void RecordSound()
    {
      userRecording = GetComponent<AudioSource>();
      userRecording.clip = Microphone.Start("", true, 10, 48000);
    }

    void SaveSound(string fileName)
    {
      SavWav.Save(fileName + ".wav", userRecording.clip);
    }
  
}

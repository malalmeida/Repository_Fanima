using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameController : MonoBehaviour
{
  public bool structReqDone = false;
  public bool respositoryReqDone = false;
  public int gameExecutionID = -1;
  public int gameSampleID = -1;
  public List<actionClass> contentList;
  public List<dataSource> dataList;
  public List<string> listOfWordsToPlay; 
  public WebRequests webRequests;
  
  private Rigidbody2D rb;

  public List<actionClass> listOfChapterActions;
    
  [SerializeField] private AudioSource userRecording;

  public List<string> listOfChaptersToPlay;

  [SerializeField] private TextMeshProUGUI wordToSay;

  string startTime;
  string endTime;


  void Awake()
  {

  }

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();

    List<string> listOfWordsToPlay = new List<string>();  

    if(SceneManager.GetActiveScene().name == "Home")
    {        
      StartCoroutine(PrepareGameStructure());
      StartCoroutine(PreparedgameExecutionID());   
    } 

    if(SceneManager.GetActiveScene().name == "Frog")
    {
      PrepareChapterActions("Oclusivas");
    }
    if(SceneManager.GetActiveScene().name == "Chameleon")
    {
      PrepareChapterActions("Fricativas");
    }
    if(SceneManager.GetActiveScene().name == "Fish")
    {
      PrepareChapterActions("Vibrantes e Laterias");
    }

      //listOfChaptersToPlay.Add("Oclusivas");      
      //listOfChaptersToPlay.Add("Vibrantes e Laterais");
  }

  void Update()
  {
    

  }

  IEnumerator PrepareGameStructure()
  {
    Debug.Log("Waiting for structure...");
    yield return new WaitUntil(() => structReqDone);
    Debug.Log("Structure request completed! Actions: " + contentList.Count);
    
    Debug.Log("Waiting for word repository...");
    yield return new WaitUntil(() => respositoryReqDone);
    Debug.Log("Repository request completed! Words: " + dataList.Count);
    /*
    //prep repository of strings
    for (int i = 0; i < contentList.Count; i++)
    {
      for (int j = 0; j < dataList.Count; j++)
      {
        if(contentList[i].word == dataList[j].id)
        {
          listOfWordsToPlay.Add(dataList[j].name);
          // PARA APAGAR ?? PlayerPrefs.SetString("Frog" + i , dataList[j].name);
        }
      }
    }
        
    foreach (string w in listOfWordsToPlay)
    {  
      Debug.Log("Words -> " + w);
    }
    
    Debug.Log("Repository " + listOfWordsToPlay.Count);
    */
  }


  void PrepareChapterActions(string sequenceName)
  {
    foreach (actionClass ac in contentList)
    {
      Debug.Log("ENTROU NO" + sequenceName);

      if(String.Compare(ac.sequence, sequenceName) == 0)
      {
        Debug.Log("ENTROU NO IF");
        listOfChapterActions.Add(ac);
        Debug.Log("ADICIONOU ACAO");
        Debug.Log("Action to play " + ac);
      }
    }
      /*PRAPARAR AS PALAVRAS PARA PARA O CAPITULO
    for (int i = 0; i < listOfChapterActions.Count; i++)
    {
      for (int j = 0; j < dataList.Count; j++)
      { 
        if(listOfChapterActions[i].word == dataList[j].id)
        {
          listOfWordsToPlay.Add(dataList[j].name);
        }
      }
    }
    */
  }


/*
    void GetChapterToPlay()
    {
      //VER O CAPUTILO QUE SE PRETENDE JOGAR
      foreach (string s in listOfChaptersToPlay)
      {
        if(String.Compare(s, "Oclusivas") == 0)
        {
          PrepareChapter(s);
          //ESCOLHER A SCENE RESPETIVA A ESSE CAPITULO
          SceneManager.LoadScene("Frog");
        }

        if(String.Compare(s, "Fricativas") == 0)
        {
          PrepareChapter(s);
          SceneManager.LoadScene("Chameleon");
        }

        if(String.Compare(s, "Vibrnates e Laterais") == 0)
        {
          PrepareChapter(s);
          SceneManager.LoadScene("Fish");
        }
      }
    }
    */

  IEnumerator PreparedgameExecutionID()
  {
    Debug.Log("Waiting for execution ID...");
    yield return new WaitUntil(() => gameExecutionID > 0);
    Debug.Log("Game Execution request completed! ID -> " + gameExecutionID);
  }

  //FROG GAME 
  private void OnCollisionEnter2D(Collision2D other)
  {
    foreach (actionClass action in listOfChapterActions)
    {
      Debug.Log("ENTROU NO LOOP DAS ACTION" + action.id);

      if(other.gameObject.CompareTag("Leaf0"))
      {
        startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

        foreach (dataSource w in dataList)
        {
          if(action.word == w.id)
          {            
            wordToSay.text = w.name;
            Debug.Log("PALAVRA DA ACTION ->" + w.name);
          }
        }  
        
        RecordSound();
      }
        Debug.Log("MUDOU A ACTION? " + action.id);
      
      if(other.gameObject.CompareTag("Leaf1"))
      {
        Debug.Log("MUDOU A ACTION? " + action.id);
        endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");          
        SaveSound(wordToSay.text);
        byte[] byteArray = SavWav.audiobyte;

        StartCoroutine(webRequests.PostSample(byteArray, listOfChapterActions[0].id.ToString(), gameExecutionID.ToString()));
        StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));
        StartCoroutine(webRequests.PostGameResult("0", "0", listOfChapterActions[0].id.ToString(), gameExecutionID.ToString(), startTime, endTime));
          
        foreach (dataSource w in dataList)
        {
          if(action.word == w.id)
          {
            wordToSay.text = w.name;
            Debug.Log("PALAVRA DA ACTION ->" + w.name);
          }
          //RecordSound(); 
        }
      }
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

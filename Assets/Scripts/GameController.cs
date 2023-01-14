using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameController : MonoBehaviour
{
  const int PLAYGAMEID = 29;
  public bool structReqDone = false;
  public bool respositoryReqDone = false;
  public int gameExecutionID = -1;
  public int gameSampleID = -1;
  public List<actionClass> contentList;
  public List<dataSource> dataList;
  
  public WebRequests webRequests;
  
  private Rigidbody2D rb;

  public List<actionClass> listOfChapterActions;
    
  [SerializeField] private AudioSource userRecording;

  public List<string> listOfChaptersToPlay;

  [SerializeField] private TextMeshProUGUI wordToSay;

  string startTime;
  string endTime;

  string activeChapter;

  public  List<actionClass> chapterHomeActionList;
  public  List<actionClass> chapterFrogActionList;
  public  List<actionClass> chapterChameleonActionList;
  public  List<actionClass> chapterFishActionList;

  public  List<string> listOfWordsToSay; 

  void Awake()
  {
    StartCoroutine(PrepareGameStructure());
    StartCoroutine(PreparedgameExecutionID());
  }

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();

    List<actionClass> chapterHomeActionList = new List<actionClass>();
    List<actionClass> chapterFrogActionList = new List<actionClass>();
    List<actionClass> chapterChameleonActionList = new List<actionClass>();
    List<actionClass> chapterFishActionList = new List<actionClass>();
    List<string> listOfWordsToSay = new List<string>(); 

    if(SceneManager.GetActiveScene().name == "Home")
    {        
      
    } 

    if(SceneManager.GetActiveScene().name == "Frog")
    {

    }

    if(SceneManager.GetActiveScene().name == "Chameleon")
    {
      
    }

    if(SceneManager.GetActiveScene().name == "Fish")
    {
      
    }

      //listOfChaptersToPlay.Add("Oclusivas");      
      //listOfChaptersToPlay.Add("Vibrantes e Laterais");
  }

  void Update()
  {
    if(SceneManager.GetActiveScene().name == "Home")
    {
      if(Input.GetKeyDown(KeyCode.Space))
      {
        for (int i = 0; i < chapterHomeActionList.Count; i++)
        {
          for (int j = 0; j < dataList.Count; j++)
          {
            if(chapterHomeActionList[i].word == dataList[j].id)
            {
              listOfWordsToSay.Add(dataList[j].name);
              Debug.Log("Words -> " + dataList[j].name);
            }
          }
        }
        Debug.Log("Words -> " + listOfWordsToSay.Count);
      }

      if (Input.GetKeyUp("down"))
      {
        Debug.Log("COMEÇOU A GRAVAR");
        startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        Debug.Log("PALAVRA DA ACTION ->" + listOfWordsToSay[0]);
        RecordSound();
      }

      if (Input.GetKeyUp("up"))
      {
        endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");          
        SaveSound(listOfWordsToSay[0]);
        byte[] byteArray = SavWav.audiobyte;

        StartCoroutine(webRequests.PostSample(byteArray, listOfChapterActions[0].id.ToString(), gameExecutionID.ToString()));
        StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));
        StartCoroutine(webRequests.PostGameResult("0", "0", listOfChapterActions[0].id.ToString(), gameExecutionID.ToString(), startTime, endTime));  
      }
    }
  }

  IEnumerator PrepareGameStructure()
  {
    //Debug.Log("Waiting for structure...");
    yield return new WaitUntil(() => structReqDone);
    Debug.Log("Structure request completed! Actions: " + contentList.Count);
    
    //Debug.Log("Waiting for word repository...");
    yield return new WaitUntil(() => respositoryReqDone);
    Debug.Log("Repository request completed! Words: " + dataList.Count);

    for (int i = 0; i < contentList.Count; i++)
    {
      if(String.Compare(contentList[i].sequence, "Geral") == 0)
      {
        chapterHomeActionList.Add(contentList[i]);
      }

      if(String.Compare(contentList[i].sequence, "Oclusivas") == 0)
      {
        chapterFrogActionList.Add(contentList[i]);
      }

      if(String.Compare(contentList[i].sequence, "Fricativas") == 0)
      {
        chapterChameleonActionList.Add(contentList[i]);
      }

      if(String.Compare(contentList[i].sequence, "Vibrantes e Laterais") == 0)
      {
        chapterFishActionList.Add(contentList[i]);
      }
    }

    /*
    //prep repository of strings
    for (int i = 0; i < contentList.Count; i++)
    {
      for (int j = 0; j < dataList.Count; j++)
      {
        if(contentList[i].word == dataList[j].id)
        {
          listOfWordsToSay.Add(dataList[j].name);
          // PARA APAGAR ?? PlayerPrefs.SetString("Frog" + i , dataList[j].name);
        }
      }
    }
        
    /*foreach (string w in listOfWordsToSay)
    {  
      Debug.Log("Words -> " + w);
    }
    Debug.Log("TOTAL WORDS TO SAY " + listOfWordsToSay.Count);
    */
    
  }


/*
  void PrepareChapterActions()
  {
    Debug.Log("CONTENT LIST " + contentList[0]);
    for (int i = 0; i < contentList.Count; i++)
    {
      if(String.Compare(contentList[i].sequence, activeChapter) == 0)
      {
        listOfChapterActions.Add(contentList[i]);
        Debug.Log("Action to play " + contentList[i]);
      }
    }
    
    
      /*PRAPARAR AS PALAVRAS PARA PARA O CAPITULO
    for (int i = 0; i < listOfChapterActions.Count; i++)
    {
      for (int j = 0; j < dataList.Count; j++)
      { 
        if(listOfChapterActions[i].word == dataList[j].id)
        {
          listOfWordsToSay.Add(dataList[j].name);
        }
      }
    }
   
  }
   */


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
    //Debug.Log("Waiting for execution ID...");
    yield return new WaitUntil(() => gameExecutionID > 0);
    Debug.Log("Game Execution request completed! ID -> " + gameExecutionID);
  }

  //FROG GAME 
  
  private void OnCollisionEnter2D(Collision2D other)
  {
    //foreach (actionClass action in listOfChapterActions)
    //{
      //Debug.Log("ENTROU NO LOOP DAS ACTION" + action.id);

      if(other.gameObject.CompareTag("Leaf0"))
      {
        
        //startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

        //foreach (dataSource w in dataList)
        //{
          //if(action.word == w.id)
          //{            
            //wordToSay.text = w.name;
            //Debug.Log("PALAVRA DA ACTION ->" + w.name);
          //}
        //}  
        
        //RecordSound();
      }
        //Debug.Log("MUDOU A ACTION? " + action.id);
      
      if(other.gameObject.CompareTag("Leaf1"))
      {
        Debug.Log("PALAVRA BOLAS = " + listOfWordsToSay.Count);
        Debug.Log("ACTION NUMBER = " + chapterFrogActionList.Count);

        foreach (actionClass ac in chapterFrogActionList)
        {  
          Debug.Log("ACTION -> " + ac);
        }

        //Debug.Log("MUDOU A ACTION? " + action.id);
        //endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");          
        //SaveSound(wordToSay.text);
        //byte[] byteArray = SavWav.audiobyte;

        //StartCoroutine(webRequests.PostSample(byteArray, listOfChapterActions[0].id.ToString(), gameExecutionID.ToString()));
        //StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));
        //StartCoroutine(webRequests.PostGameResult("0", "0", listOfChapterActions[0].id.ToString(), gameExecutionID.ToString(), startTime, endTime));
          
        //foreach (dataSource w in dataList)
        //{
          //if(action.word == w.id)
         // {
        //    wordToSay.text = w.name;
        //    Debug.Log("PALAVRA DA ACTION ->" + w.name);
         // }
          //RecordSound(); 
        //}
      }
    //}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System;
using TMPro;

public class GameController : MonoBehaviour
{
  public string wsURL = "ws://193.137.46.11/";
  public string appName = "Fanima"; 
  const int therapistID = 51;
  const int PLAYGAMEID = 29;
  public int userID = 52;
  public bool structReqDone = false;
  public bool respositoryReqDone = false;
  public bool sampleReqDone = false;
  public int gameExecutionID = -1;
  public int gameSampleID = -1;
  public List<actionClass> contentList;
  public List<dataSource> dataList;
  
  public WebRequests webRequests;
  public WebSockets webSockets;
  
  private Rigidbody2D rb;

  public List<actionClass> listOfChapterActions;
    
  [SerializeField] private AudioSource userRecording;

  public List<int> listOfChaptersToPlay;

  [SerializeField] private TextMeshProUGUI wordToSay;
  
  public GameObject startMenuUI;

  public HomeScript homeScript;
  public ChameleonScript chameleonScript;

  string startTime;
  string endTime;

  string activeChapter;

  //public  List<actionClass> chapterHomeActionList;
  //public  List<actionClass> chapterFrogActionList;
  //public  List<actionClass> chapterChameleonActionList;
  //public  List<actionClass> chapterFishActionList;

  public  List<string> listOfWordsToSay; 
  public string currentWord;
  public int currentAtionID = -1;

  public bool validationDone = false;
  public bool alreadyRequestLevels = false;


  void Awake()
  {
    StartCoroutine(PreparedGameExecutionID());
    StartCoroutine(PrepareGameStructure());
  }

  // Start is called before the first frame update
  void Start()
  {
    if(SceneManager.GetActiveScene().name == "Home")
    {
      StartCoroutine(PreparedGameExecutionID());
      activeChapter = "Geral";      
      StartCoroutine(GameLoop());    
    }
    
    StartCoroutine(PrepareGameStructure());

    rb = GetComponent<Rigidbody2D>();
    webSockets = new WebSockets();
    webSockets.SetupClient(wsURL, userID, PLAYGAMEID, appName);
    webSockets.StartClient();
    
    List<actionClass> chapterHomeActionList = new List<actionClass>();
    List<actionClass> chapterFrogActionList = new List<actionClass>();
    List<actionClass> chapterChameleonActionList = new List<actionClass>();
    List<actionClass> chapterFishActionList = new List<actionClass>();
    List<string> listOfWordsToSay = new List<string>(); 

    listOfChaptersToPlay.Add(0);
    listOfChaptersToPlay.Add(2);
 
    if(SceneManager.GetActiveScene().name == "Frog")
    {
      activeChapter = "Oclusivas";     
      StartCoroutine(GameLoop()); 
    }

    if(SceneManager.GetActiveScene().name == "Chameleon")
    {
      activeChapter = "Fricativas";      
      StartCoroutine(GameLoop());
    }

    if(SceneManager.GetActiveScene().name == "Fish")
    { 
      activeChapter = "Vibrantes e Laterais";
      StartCoroutine(GameLoop());      
    }
  }

  IEnumerator GameLoop()
  {
    if((SceneManager.GetActiveScene().name == "Home"))
    {
      yield return StartCoroutine(PreparedGameExecutionID());
    }

    yield return StartCoroutine(PrepareGameStructure());

    for(int i = 0; i < contentList.Count; i++)
    {
      if(contentList[i].sequence == activeChapter)
      {
        currentAtionID = i;
        startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        // O REPOSITORIO DE PALAVRAS COMEÇA COM O ID 1, POR ISSO O -1
        currentWord = dataList[contentList[i].word - 1].name;
        Debug.Log("DIZ -> " + currentWord); 
        wordToSay.text = currentWord;
        RecordSound();
        yield return StartCoroutine(WaitForValidation());
      }            
      else
      {
        Debug.Log("ACABOU A SEQUENCIA");
        //Debug.Log("FAZER O PEDIDO DOS NIVEIS A JOGAR");
    
        if(alreadyRequestLevels == false)
        {
          yield return new WaitUntil(() => webSockets.socketIsReady);
          webSockets.LevelsToPlayRequest(therapistID);

          yield return new WaitUntil(() => webSockets.getLevelsDone);
          alreadyRequestLevels = true;

          Debug.Log("1º CHAP " + webSockets.chapList[0]);
          

          
          if(webSockets.levelsList[0] == "1")
          {
            SceneManager.LoadScene("Frog");
          }
          else if(webSockets.levelsList[0] == "2")
          {
            SceneManager.LoadScene("Chameleon");
          }
          else  if(webSockets.levelsList[0] == "3")
          {
            SceneManager.LoadScene("Octopus");
          }

        } 
      }
    }
  }

  IEnumerator WaitForValidation()
  {
    //yield return new WaitForSeconds(2.0f);
    endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");         
    SavWav.Save(currentWord + ".wav", userRecording.clip);
    yield return StartCoroutine(webRequests.PostSample(currentWord, contentList[currentAtionID].id.ToString(), gameExecutionID.ToString(), contentList[currentAtionID].word.ToString()));
    
    yield return new WaitUntil(() => webSockets.socketIsReady);
    gameSampleID = PlayerPrefs.GetInt("GAMESAMPLEID");
    yield return StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));

    if (SceneManager.GetActiveScene().name == "Home")
    {
      webSockets.ActionClassificationGeralRequest(therapistID, contentList[currentAtionID].word, gameSampleID);
    }
    else
    {
      webSockets.ActionClassificationRequest(therapistID, contentList[currentAtionID].word, gameSampleID);
    }

    yield return new WaitUntil(() => webSockets.validationDone);
    yield return new WaitUntil(() => webSockets.validationValue > -2);

    if(webSockets.validationValue == -1)
    {
      startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
      Debug.Log("DIZ -> " + currentWord); 
      RecordSound();
      webSockets.validationValue = -2;
      yield return StartCoroutine(WaitForValidation());
    }

    if(webSockets.validationValue == 0)
    {
      if (SceneManager.GetActiveScene().name == "Home")
      {
        homeScript.doAnimation = true;
        webSockets.validationValue = -2;
      }

      else if (SceneManager.GetActiveScene().name == "Chameleon")
      {
        chameleonScript.randomIndex = Random.Range(0, 12);
        webSockets.validationValue = -2;
      }
    }
    yield return StartCoroutine(webRequests.PostGameResult("1", "0", contentList[currentAtionID].id.ToString(), gameExecutionID.ToString(), startTime, endTime, currentWord));     
  }


  IEnumerator PrepareGameStructure()
  {
    //Debug.Log("Waiting for structure...");
    Debug.Log("Structure request completed! Actions: " + contentList.Count);
    
    //Debug.Log("Waiting for word repository...");
    yield return new WaitUntil(() => respositoryReqDone);
    Debug.Log("Repository request completed! Words: " + dataList.Count);
  }

  IEnumerator PreparedGameExecutionID()
  {
    if (SceneManager.GetActiveScene().name == "Home")
    {
      Debug.Log("Waiting for execution ID...");
      yield return new WaitUntil(() => gameExecutionID > 0);
      Debug.Log("Game Execution request completed! ID -> " + gameExecutionID);
    }
  }

  //FROG GAME 
  private void OnCollisionEnter2D(Collision2D other)
  {
      if(other.gameObject.CompareTag("Leaf0"))
      {
        
      }
      
      if(other.gameObject.CompareTag("Leaf1"))
      {
        
      }
  }

  void RecordSound()
  {
    userRecording = GetComponent<AudioSource>();
    userRecording.clip = Microphone.Start("", true, 1, 48000);
  }

  void SaveSound(string fileName)
  {
    SavWav.Save(fileName + ".wav", userRecording.clip);
  }

  void OnApplicationQuit()
  {
    Debug.Log("Stop WS client and logut");
    webSockets.StopClient();
  }

}

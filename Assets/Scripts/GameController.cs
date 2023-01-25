using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
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

  void Awake()
  {
    StartCoroutine(PreparedGameExecutionID());
    StartCoroutine(PrepareGameStructure());
  }

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    //homeScript = new HomeScript();
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

    if(SceneManager.GetActiveScene().name == "Home")
    {
      activeChapter = "Geral";      
      StartCoroutine(WaitForAction());
    } 

    if(SceneManager.GetActiveScene().name == "Frog")
    {
      activeChapter = "Oclusivas";      
    }

    if(SceneManager.GetActiveScene().name == "Chameleon")
    {
      activeChapter = "Fricativas";      
      StartCoroutine(WaitForAction());
    }

    if(SceneManager.GetActiveScene().name == "Fish")
    { 
      activeChapter = "Vibrantes e Laterais";      
    }
  }

  void Update()
  {
    if(SceneManager.GetActiveScene().name == "Home" && !startMenuUI.activeSelf)
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
  }

  IEnumerator WaitForAction()
  {
    yield return StartCoroutine(PrepareGameStructure());
    yield return StartCoroutine(PreparedGameExecutionID());

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
        Debug.Log("FAZER O PEDIDO DOS NIVEIS A JOGAR");
      
        yield return new WaitUntil(() => webSockets.socketIsReady);
        webSockets.GetLevelsToPlay(therapistID, gameExecutionID);

        //if(SceneManager.GetActiveScene().name == "Home")
        //{
          //SceneManager.LoadScene("Travel");
        //}
      }
    }
  }

  IEnumerator WaitForValidation()
  {
    Debug.Log("ESPERAR PELA VALIDAÇÂO DA TERAPEUTA");
    yield return new WaitForSeconds(2.0f);
    Debug.Log("VALIDAÇÃO FEITA");
    if (SceneManager.GetActiveScene().name == "Home")
    {
      homeScript.moveUp = true;
      homeScript.upTimes ++;
    }
    endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");         
    SavWav.Save(currentWord + ".wav", userRecording.clip);
    //Debug.Log("ACTION ID " + contentList[currentAtionID].id);
    //Debug.Log("GAMEEXECUTIONID " + gameExecutionID);
    yield return StartCoroutine(PrepareGameResult());
  }

/*
  IEnumerator PrepareChapterActions()
  {
    yield return StartCoroutine(PrepareGameStructure());
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
*/
  IEnumerator PrepareGameResult()
  {
    yield return StartCoroutine(webRequests.PostSample(currentWord, contentList[currentAtionID].id.ToString(), gameExecutionID.ToString(), contentList[currentAtionID].word.ToString()));
    
    gameSampleID = PlayerPrefs.GetInt("GAMESAMPLEID");
  
    yield return new WaitUntil(() => webSockets.socketIsReady);
    webSockets.GetActionEvaluation(therapistID, gameSampleID);

    //Debug.Log("GAMESAMPLEID " + gameSampleID);
    yield return StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));

    StartCoroutine(webRequests.PostGameResult("1", "0", contentList[currentAtionID].id.ToString(), gameExecutionID.ToString(), startTime, endTime, currentWord));     
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
    Debug.Log("Waiting for execution ID...");
    yield return new WaitUntil(() => gameExecutionID > 0);
    Debug.Log("Game Execution request completed! ID -> " + gameExecutionID);
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

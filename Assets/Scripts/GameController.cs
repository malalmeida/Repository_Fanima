using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
  public bool gameExecutionDone = false;
  public int gameExecutionID = -1;
  public int gameSampleID = -1;
  public List<actionClass> contentList;
  public List<dataSource> dataList;
  
  public WebRequests webRequests;
  public WebSockets webSockets;
  
  private Rigidbody2D rb;
    
  [SerializeField] private AudioSource userRecording;
  [SerializeField] private TextMeshProUGUI wordToSay;
  
  public GameObject startMenuUI;

  public HomeScript homeScript;
  public ChameleonScript chameleonScript;
  public FrogScript frogScript;
  public TravelScript travelScript;

  string startTime;
  string endTime;

  string activeChapter;
  public List<actionClass> sequenceToPlayList;
  public  List<string> listOfWordsToSay; 
  public string currentWord;
  public int currentActionID = -1;
  public int currentWordID = -1;

  public bool validationDone = false;

  public bool selectionDone;
  public bool postGameResultDone = false;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    webSockets = new WebSockets();
    webSockets.therapistID = therapistID;
    webSockets.SetupClient(wsURL, userID, PLAYGAMEID, appName);
    webSockets.StartClient();

    List<string> listOfWordsToSay = new List<string>(); 
    List<actionClass> sequenceToPlayList = new List<actionClass>(); 

    if(SceneManager.GetActiveScene().name == "Home")
    {
      activeChapter = "Geral"; 
      PlayerPrefs.SetString("LEVELSELECTION", "NOTDONE");   
      PlayerPrefs.SetInt("NumberOfChaptersPlayed", 1);  
      StartCoroutine(GameLoop());    
    }

    else if(SceneManager.GetActiveScene().name == "Travel")
    {      
      if(PlayerPrefs.GetString("LEVELSELECTION").Equals("NOTDONE"))
      {
        StartCoroutine(PrepareLevels());
      }
      StartCoroutine(PrepareNextLevel());
    }
      
    else if(SceneManager.GetActiveScene().name == "Frog")
    {
      activeChapter = "Oclusivas";     
      StartCoroutine(GameLoop()); 
    }

    else if(SceneManager.GetActiveScene().name == "Chameleon")
    {
      activeChapter = "Fricativas";      
      StartCoroutine(GameLoop());
    }

    else if(SceneManager.GetActiveScene().name == "Octopus")
    { 
      activeChapter = "Vibrantes e Laterais";
      StartCoroutine(GameLoop());      
    }
  }

  void Update()
  {
    if(PlayerPrefs.GetString("LEVELSELECTION").Equals("DONE"))
    {
      selectionDone = true;
    }
    else
    {
      selectionDone = false;
    }
  }

  IEnumerator GameLoop()
  {
    if((SceneManager.GetActiveScene().name == "Home"))
    {
      yield return StartCoroutine(PreparedGameExecutionID());
    }

    yield return StartCoroutine(PrepareSequence());

    for(int i = 0; i < sequenceToPlayList.Count; i++)
    {
      postGameResultDone = false;
      currentActionID = sequenceToPlayList[i].id;
      currentWordID = sequenceToPlayList[i].word;
      startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
      // O REPOSITORIO DE PALAVRAS COMEÇA COM O ID 1, POR ISSO O -1
      currentWord = dataList[sequenceToPlayList[i].word - 1].name;
      string payload = "{\"therapist\": " + therapistID + ", \"game\": \"" + PLAYGAMEID + "\", \"status\": " + 0 + ", \"order\": " + 0 + ", \"level\": \"" + contentList[i].level + "\", \"sequence\": \"" + contentList[i].sequence + "\", \"action\": \"" + contentList[i].id + "\", \"percent\": " + 0 + ", \"time\": " + 0 + "}";        
      webSockets.PrepareMessage("game", payload); 
      Debug.Log("DIZ -> " + currentWord); 
      wordToSay.text = currentWord;
      RecordSound();
      yield return StartCoroutine(WaitForValidation());
    }
    Debug.Log("ACABOU O SEQUENCIA");
    //yield return new WaitUntil(() => postGameResultDone);
    //SceneManager.LoadScene("Travel"); 
  }

  IEnumerator PrepareNextLevel()
  {    
    yield return new WaitUntil(() => selectionDone);
    
    if(PlayerPrefs.GetInt("NumberOfChaptersPlayed") == 1)
    {
      PlayerPrefs.SetInt("NumberOfChaptersPlayed", 2);
      SceneManager.LoadScene(PlayerPrefs.GetString("ChapterOne")); 
    }
    else if(PlayerPrefs.GetInt("NumberOfChaptersPlayed") == 2)
    {
       PlayerPrefs.SetInt("NumberOfChaptersPlayed", 3);
      SceneManager.LoadScene(PlayerPrefs.GetString("ChapterTwo")); 
    }
    else if(PlayerPrefs.GetInt("NumberOfChaptersPlayed") == 3){
      SceneManager.LoadScene(PlayerPrefs.GetString("ChapterThree")); 
    }
  }

  IEnumerator PrepareSequence()
  {
    yield return StartCoroutine(PrepareGameStructure());
    for(int i = 0; i < contentList.Count; i++)
    {
      if(contentList[i].sequence == activeChapter)
      {
        sequenceToPlayList.Add(contentList[i]);
      } 
    }     
  }

  IEnumerator PrepareLevels()
  {
    if (PlayerPrefs.HasKey("ChapterOne"))
    {
      PlayerPrefs.DeleteKey("ChapterOne");
    }
    if (PlayerPrefs.HasKey("ChapterTwo"))
    {
      PlayerPrefs.DeleteKey("ChapterTwo");
    }
    if (PlayerPrefs.HasKey("ChapterThree"))
    {
      PlayerPrefs.DeleteKey("ChapterThree");
    }
   
    yield return new WaitUntil(() => webSockets.socketIsReady);
    webSockets.LevelsToPlayRequest(therapistID);
    yield return new WaitUntil(() => webSockets.getLevelsDone);
    PlayerPrefs.SetString("LEVELSELECTION", "DONE");

    if(webSockets.levelsList.Count == 1)
    {
      PlayerPrefs.SetInt("NumberOfChaptersToPlay", 1);

      if(webSockets.levelsList[0].Equals("1"))
      {
        PlayerPrefs.SetString("ChapterOne", "Chameleon");
      }
      else if(webSockets.levelsList[0].Equals("2"))
      {
        PlayerPrefs.SetString("ChapterOne", "Frog");
      }
      else if(webSockets.levelsList[0].Equals("3"))
      {
        PlayerPrefs.SetString("ChapterOne", "Octopus");
      }
    }
    else if(webSockets.levelsList.Count == 2)
    {
      PlayerPrefs.SetInt("NumberOfChaptersToPlay", 2);
      if(webSockets.levelsList[0].Equals("1"))
      {
        PlayerPrefs.SetString("ChapterOne", "Chameleon");
        if(webSockets.levelsList[1].Equals("2"))
        {
          PlayerPrefs.SetString("ChapterTwo", "Frog");
        }
        else if(webSockets.levelsList[1].Equals("3"))
        {
          PlayerPrefs.SetString("ChapterTwo", "Octopus");
        }
      }
      if(webSockets.levelsList[0].Equals("2"))
      {
        PlayerPrefs.SetString("ChapterOne", "Frog");
        if(webSockets.levelsList[1].Equals("3"))
        {
          PlayerPrefs.SetString("ChapterTwo", "Octopus");
        }
      }
    }
    else if(webSockets.levelsList.Count == 3)
    {
      PlayerPrefs.SetInt("NumberOfChaptersToPlay", 3);
      PlayerPrefs.SetString("ChapterOne", "Chameleon");
      PlayerPrefs.SetString("ChapterTwo", "Frog");
      PlayerPrefs.SetString("ChapterThree", "Octopus");
    }
    PlayerPrefs.SetString("LEVELSELECTION", "DONE");
  }

  IEnumerator WaitForValidation()
  {
    yield return new WaitUntil(() => webSockets.socketIsReady);
    
    if (SceneManager.GetActiveScene().name == "Home")
    {
      yield return new WaitForSeconds(5);
      endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");         
      SavWav.Save(currentWord + ".wav", userRecording.clip);

      gameExecutionID = PlayerPrefs.GetInt("GAMEEXECUTIONID");
      yield return StartCoroutine(webRequests.PostSample(currentWord, currentActionID.ToString(), gameExecutionID.ToString(), currentWordID.ToString()));
    
      gameSampleID = PlayerPrefs.GetInt("GAMESAMPLEID");
      yield return StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));

      webSockets.ActionClassificationGeralRequest(therapistID, currentWordID, gameSampleID);
    }
    else
    {
      yield return new WaitForSeconds(5);
      webSockets.ActionClassificationRequest(therapistID, currentWordID, gameSampleID);
    }

    yield return new WaitUntil(() => webSockets.validationDone);
    yield return new WaitUntil(() => webSockets.validationValue > -2);
/*
    endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");         
    SavWav.Save(currentWord + ".wav", userRecording.clip);

    gameExecutionID = PlayerPrefs.GetInt("GAMEEXECUTIONID");
    yield return StartCoroutine(webRequests.PostSample(currentWord, currentActionID.ToString(), gameExecutionID.ToString(), currentWordID.ToString()));
    
    Debug.Log("LOG POST SAMPLE");
    Debug.Log("WORD: " + currentWord + " |  ACTIONID: " +  currentActionID.ToString() + " | GAMEEXECUTIONID: " +  gameExecutionID.ToString() + " | WORDID: " + currentWordID.ToString());
   
    gameSampleID = PlayerPrefs.GetInt("GAMESAMPLEID");
    yield return StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));
    Debug.Log("GAMESAMPLEID: " + gameSampleID.ToString());
*/
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
      else if (SceneManager.GetActiveScene().name == "Frog")
      {
        frogScript.randomIndex = Random.Range(0, 14);
        webSockets.validationValue = -2;
      }
      else if (SceneManager.GetActiveScene().name == "Chameleon")
      {
        chameleonScript.randomIndex = Random.Range(0, 12);
        webSockets.validationValue = -2;
      }
    }
    yield return StartCoroutine(webRequests.PostGameResult("1", "0", currentActionID.ToString(),  gameExecutionID.ToString(), startTime, endTime, currentWord));     
    Debug.Log("LOG POST GAMERESULT");
    Debug.Log("ACTIONID: " +  currentActionID.ToString() + "GAMEEXECUTIONID: " +  gameExecutionID.ToString() + "WORD: " + currentWord);
    Debug.Log("SCR " + postGameResultDone);
    postGameResultDone = true;
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
      yield return new WaitUntil(() => gameExecutionDone);
      Debug.Log("Game Execution request completed! ID -> " + PlayerPrefs.GetString("GAMEEXECUTIONID"));
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
    userRecording.clip = Microphone.Start("", true, 5, 48000);
  }

  void SaveSound(string fileName)
  {
    SavWav.Save(fileName + ".wav", userRecording.clip);
  }

  void OnApplicationQuit()
  {
    Debug.Log("Stop WS client and logut");
    string payload = "{\"therapist\": " + therapistID + "}";
    webSockets.PrepareMessage("status", payload);
    webSockets.StopClient();
  }

}

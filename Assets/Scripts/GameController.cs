using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;


public class GameController : MonoBehaviour
{
  public string wsURL = "ws://193.137.46.11/";
  public string appName = "Fanima"; 
  public int therapistID;
  const int PLAYGAMEID = 29;
  public int patientID;
  public bool structReqDone = false;
  public bool respositoryReqDone = false;
  public bool sampleReqDone = false;
  public bool gameExecutionDone = false;
  public bool errorDone = false;
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
  public MonkeyScript monkeyScript;
  public OwlScript owlScript;
  public OctopusScript octopusScript;
  public FishScript fishScript;

  //public CollisionCircleScript collisionCircleScript;
  //public CollisionSquareScript collisionSquareScript;
  //public CollisionTriangleScript collisionTriangleScript;

  string startTime;
  string endTime;

  string activeChapter = "";
  public List<actionClass> sequenceToPlayList;
  public List<string> listOfWordsToSay; 
  public string currentWord;
  public int currentActionID = -1;
  public int currentWordID = -1;
  public int sequenceID = -1;
  public int timer = -1;
  public int errorStatus = -1;
  public int repSampleID = -1;

  public bool validationDone = false;

  public bool selectionDone;
  public bool postGameResultDone = false;
  public bool errorDetected = false;
  public bool readyForNextWord = false;
  public bool prepareLevelsDone = false; 
  public bool repetition = false;
  public bool actionValidated = false;

  public List<errorClass> phonemeList;

  // Start is called before the first frame update
  void Start()
  {
    /*
    DontDestroyOnLoad(GameObject.FindWithTag("GC"));
    DontDestroyOnLoad(GameObject.FindWithTag("WR"));
    DontDestroyOnLoad(GameObject.FindWithTag("WS"));
    DontDestroyOnLoad(GameObject.FindWithTag("GS"));
    DontDestroyOnLoad(GameObject.FindWithTag("Menu"));
    */

    rb = GetComponent<Rigidbody2D>();
    webSockets = new WebSockets();
    therapistID = PlayerPrefs.GetInt("THERAPISTID");
    webSockets.therapistID = therapistID;
    patientID = PlayerPrefs.GetInt("PATIENTID");
    webSockets.SetupClient(wsURL, patientID, PLAYGAMEID, appName);
    webSockets.StartClient();

    List<string> listOfWordsToSay = new List<string>(); 
    List<actionClass> sequenceToPlayList = new List<actionClass>(); 
    List<errorClass> phonemeList = new List<errorClass>(); 
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

    else if(SceneManager.GetActiveScene().name == "Fish")
    { 
      activeChapter = "Vibrantes e Laterais";
      StartCoroutine(GameLoop());      
    }

    else if(SceneManager.GetActiveScene().name == "Monkey")
    {
      StartCoroutine(BonusGameLoop());
    }

    else if(SceneManager.GetActiveScene().name == "Owl")
    {
      StartCoroutine(BonusGameLoop());
    }

    else if(SceneManager.GetActiveScene().name == "Octopus") 
    {
      StartCoroutine(BonusGameLoop());
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

  IEnumerator BonusGameLoop()
  {
    gameExecutionID = PlayerPrefs.GetInt("GAMEEXECUTIONID");
    sequenceID = PlayerPrefs.GetInt("SEQUENCEID");
    yield return StartCoroutine(webRequests.GetChapterErrors(gameExecutionID.ToString(), sequenceID.ToString()));
    yield return new WaitUntil(() => webRequests.chapterErrorListDone);
    
    for(int i = 0; i < webRequests.chapterErrorList.Count; i ++)
    { 
      activeChapter = "Fonema /" + webRequests.chapterErrorList[i].phoneme + "/";
      Debug.Log("FONEMA: " + activeChapter);
      
      yield return StartCoroutine(PrepareSequence());
      Debug.Log("PALAVRAS" + sequenceToPlayList.Count);
      yield return new WaitUntil(() => sequenceToPlayList.Count > 0);
      Debug.Log("PALAVRAS" + sequenceToPlayList.Count);
      for(int j = 0; j < sequenceToPlayList.Count; j++)
      {      
        currentActionID = sequenceToPlayList[j].id;
        currentWordID = sequenceToPlayList[j].word;
        startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        currentWord = dataList[sequenceToPlayList[j].word - 1].name;
        string payload = "{\"therapist\": " + therapistID + ", \"game\": \"" + PLAYGAMEID + "\", \"status\": " + 0 + ", \"order\": " + 0 + ", \"level\": \"" + sequenceToPlayList[j].level + "\", \"sequence\": \"" + sequenceToPlayList[j].sequence + "\", \"action\": \"" + sequenceToPlayList[j].id + "\", \"percent\": " + 0 + ", \"time\": " + 0 + "}";        
        webSockets.PrepareMessage("game", payload); 
        Debug.Log("DIZ -> " + currentWord); 
        wordToSay.text = currentWord;
        timer = sequenceToPlayList[i].time;
        RecordSound(timer);
        yield return StartCoroutine(WaitForValidation());
      }   
    }
    Debug.Log("ACABOU CAPITULO BONUS");
    SceneManager.LoadScene("Travel");      
  }

  IEnumerator GameLoop()
  {
    if((SceneManager.GetActiveScene().name == "Home"))
    {
      yield return StartCoroutine(PreparedGameExecutionID());
    }

    yield return StartCoroutine(PrepareSequence());
    yield return new WaitUntil(() => sequenceToPlayList.Count > 0);
    for(int i = 0; i < sequenceToPlayList.Count; i++)
    {
      currentActionID = sequenceToPlayList[i].id;
      currentWordID = sequenceToPlayList[i].word;
      PlayerPrefs.SetInt("SEQUENCEID", sequenceToPlayList[i].sequenceid);
      startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
      // O REPOSITORIO DE PALAVRAS COMEÇA COM O ID 1, POR ISSO O -1
      currentWord = dataList[sequenceToPlayList[i].word - 1].name;
      string payload = "{\"therapist\": " + therapistID + ", \"game\": \"" + PLAYGAMEID + "\", \"status\": " + 0 + ", \"order\": " + 0 + ", \"level\": \"" + sequenceToPlayList[i].level + "\", \"sequence\": \"" + sequenceToPlayList[i].sequence + "\", \"action\": \"" + sequenceToPlayList[i].id + "\", \"percent\": " + 0 + ", \"time\": " + 0 + "}";        
      webSockets.PrepareMessage("game", payload); 
      Debug.Log("DIZ -> " + currentWord); 
      wordToSay.text = currentWord;
      timer = sequenceToPlayList[i].time;
      RecordSound(timer);
      yield return StartCoroutine(WaitForValidation());
    }
    Debug.Log("ACABOU O SEQUENCIA");

    if((SceneManager.GetActiveScene().name == "Home"))
    {
      SceneManager.LoadScene("Travel"); 
    }

    else if((SceneManager.GetActiveScene().name == "Frog"))
    {
      if(errorDetected == true)
      {
        SceneManager.LoadScene("Monkey"); 
      }
      else
      {
        SceneManager.LoadScene("Travel"); 
      }
    }

    else if((SceneManager.GetActiveScene().name == "Chameleon"))
    {
      if(errorDetected == true)
      {
        SceneManager.LoadScene("Owl"); 
      }
      else
      {
        SceneManager.LoadScene("Travel"); 
      }
    }

    else if((SceneManager.GetActiveScene().name == "Octopus"))
    {
      if(errorDetected == true)
      {
        SceneManager.LoadScene("Fish"); 
      }
      else
      {
        SceneManager.LoadScene("Travel"); 
      }
    }
  }

  IEnumerator PrepareNextLevel()
  {    
    yield return new WaitUntil(() => selectionDone);
    //yield return new WaitUntil(() => prepareLevelsDone);
   // yield return new WaitUntil(() => travelScript.patientInteractionDone);
    
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
    if(sequenceToPlayList.Count > 0 )
    {
      sequenceToPlayList.Clear();
    }

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
    //yield return new WaitUntil(() => travelScript.patientInteractionDone);
    PlayerPrefs.SetString("LEVELSELECTION", "DONE");

    if(webSockets.levelsList.Count == 1)
    {
      PlayerPrefs.SetInt("NumberOfChaptersToPlay", 1);

      if(webSockets.levelsList[0].Equals("1"))
      {
        PlayerPrefs.SetString("ChapterOne", "Frog");
      }
      else if(webSockets.levelsList[0].Equals("2"))
      {
        PlayerPrefs.SetString("ChapterOne", "Chameleon");
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
        PlayerPrefs.SetString("ChapterOne", "Frog");

        if(webSockets.levelsList[1].Equals("2"))
        {
          PlayerPrefs.SetString("ChapterTwo", "Chameleon");
        }
        else if(webSockets.levelsList[1].Equals("3"))
        {
          PlayerPrefs.SetString("ChapterTwo", "Octopus");
        }
      }
      else if(webSockets.levelsList[0].Equals("2"))
      {
        PlayerPrefs.SetString("ChapterOne", "Chameleon");

        //if(webSockets.levelsList[1].Equals("3"))
        //{
          PlayerPrefs.SetString("ChapterTwo", "Octopus");
        //}
      }
    }
    else if(webSockets.levelsList.Count == 3)
    {
      PlayerPrefs.SetInt("NumberOfChaptersToPlay", 3);
      PlayerPrefs.SetString("ChapterOne", "Frog");
      PlayerPrefs.SetString("ChapterTwo", "Chameleon");
      PlayerPrefs.SetString("ChapterThree", "Octopus");
    }
    PlayerPrefs.SetString("LEVELSELECTION", "DONE");
  prepareLevelsDone = true;
  }

  IEnumerator WaitForValidation()
  {
    yield return new WaitUntil(() => webSockets.socketIsReady);
    
    if (SceneManager.GetActiveScene().name == "Home")
    {
      yield return new WaitForSeconds(timer);
      endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");         
      SavWav.Save(currentWord + ".wav", userRecording.clip);

      gameExecutionID = PlayerPrefs.GetInt("GAMEEXECUTIONID");
       
      if(repetition == true)
      {
        yield return StartCoroutine(webRequests.PostRepSample(currentWord, currentActionID.ToString(), gameExecutionID.ToString(), currentWordID.ToString(), repSampleID.ToString()));   
      }
      else
      {
        yield return StartCoroutine(webRequests.PostSample(currentWord, currentActionID.ToString(), gameExecutionID.ToString(), currentWordID.ToString()));
        gameSampleID = PlayerPrefs.GetInt("GAMESAMPLEID");
        Debug.Log("SAMPLE ID " + gameSampleID);
        yield return StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));
      }

      webSockets.ActionClassificationGeralRequest(therapistID, currentWordID, gameSampleID);
    }
    else
    {
      yield return new WaitForSeconds(timer);     
      endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");         
      SavWav.Save(currentWord + ".wav", userRecording.clip);

      gameExecutionID = PlayerPrefs.GetInt("GAMEEXECUTIONID");
      yield return StartCoroutine(webRequests.PostSample(currentWord, currentActionID.ToString(), gameExecutionID.ToString(), currentWordID.ToString()));
    
      gameSampleID = PlayerPrefs.GetInt("GAMESAMPLEID");
      yield return StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));

      webSockets.ActionClassificationRequest(therapistID, currentWordID, gameSampleID);
    }
    //ESPERAR ATE QUE A VALIDACAO SEJA FEITA
    yield return new WaitUntil(() => webSockets.validationDone);
    yield return new WaitUntil(() => webSockets.validationValue > -2);

    if(webSockets.validationValue == -1)
    {
      startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
      Debug.Log("DIZ -> " + currentWord); 
      RecordSound(timer);
      webSockets.validationValue = -2;
      repetition = true;
      repSampleID = gameSampleID;
      Debug.Log("REP SAMPLE ID" + repSampleID);
      yield return StartCoroutine(WaitForValidation());
    }

    else if(webSockets.validationValue >= 0)
    {
      if(repetition == true)
      {     
        repetition = false;
      }

      //actionValidated = true;

      if(webSockets.validationValue > 0)
      {
        errorDetected = true;
      }

      if (SceneManager.GetActiveScene().name == "Home")
      {
        //homeScript.doAnimation = true;
        webSockets.validationValue = -2;      
      }
      else if (SceneManager.GetActiveScene().name == "Frog")
      {
        frogScript.randomIndex = Random.Range(0, 14);
        yield return new WaitUntil(() => frogScript.isCaught);
        frogScript.isCaught = false;
        webSockets.validationValue = -2;      
      }
      else if (SceneManager.GetActiveScene().name == "Chameleon")
      {
        chameleonScript.randomIndex = Random.Range(0, 12);
        yield return new WaitUntil(() => chameleonScript.isCaught);
        chameleonScript.isCaught = false;
        webSockets.validationValue = -2;
      }
      else if (SceneManager.GetActiveScene().name == "Octopus")
      {
        octopusScript.randomIndex = Random.Range(0, 21);
        yield return new WaitUntil(() => octopusScript.isMatch);
        octopusScript.isMatch = false;
        webSockets.validationValue = -2;
      }
      else if (SceneManager.GetActiveScene().name == "Monkey")
      {
        monkeyScript.randomIndex = Random.Range(0, 11);
        yield return new WaitUntil(() => monkeyScript.isCaught);
        monkeyScript.isCaught = false;
        webSockets.validationValue = -2;      
        }
      else if (SceneManager.GetActiveScene().name == "Owl")
      {
        owlScript.randomIndex = Random.Range(0, 28);
        yield return new WaitUntil(() => owlScript.isMatch);
        owlScript.isMatch = false;
        webSockets.validationValue = -2;      
      }
      else if (SceneManager.GetActiveScene().name == "Fish")
      {
        //fishScript.randomIndex = Random.Range(0, 13);
        //yield return new WaitUntil(() => fishScript.isCaught);
        //fishScript.isCaught = false;
        webSockets.validationValue = -2;      
      }
      yield return StartCoroutine(PreparedGameResult());
    }
  }

  IEnumerator PrepareGameStructure()
  {
    yield return new WaitUntil(() => structReqDone);
    Debug.Log("Waiting for structure...");
    Debug.Log("Structure request completed! Actions: " + contentList.Count);
    
    Debug.Log("Waiting for word repository...");
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

  IEnumerator PreparedGameResult()
  {
    Debug.Log("REP " + repetition);

    if(repetition == false)
    {
      if(webSockets.statusValue > 0)
      {
        Debug.Log("ERRORSTATUS" + webSockets.statusValue);
        errorStatus = 0;
      }
      else
      {
        errorStatus = 1;
      }
      yield return StartCoroutine(webRequests.PostGameResult(errorStatus.ToString(), "0", currentActionID.ToString(),  gameExecutionID.ToString(), startTime, endTime, currentWord));     
      Debug.Log("LOG POST GAME RESULT");
      Debug.Log("STATUS: " + errorStatus.ToString() + " ACTIONID: " +  currentActionID.ToString() + " GAMEEXECUTIONID: " +  gameExecutionID.ToString() + " WORD: " + currentWord);
    }
  }


  void RecordSound(int timer)
  {
    userRecording = GetComponent<AudioSource>();
    userRecording.clip = Microphone.Start("", true, timer, 48000);
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
    webSockets.StopClient(payload);
  }
}

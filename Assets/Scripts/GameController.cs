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

  [SerializeField] private ParticleSystem confetti;
  public GameObject finalMenu;
    
  [SerializeField] private AudioSource userRecording;
  //[SerializeField] private TextMeshProUGUI wordToSay;
  
  public GameObject startMenuUI;

  public HomeScript homeScript;
  public ChameleonScript chameleonScript;
  public FrogScript frogScript;
  public TravelScript travelScript;
  public MonkeyScript monkeyScript;
  public OwlScript owlScript;
  public OctopusScript octopusScript;
  public FishScript fishScript;

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

  public AudioSource aud;
  public List<AudioClip> clips; 
  public AudioSource home1;
  public AudioSource home2;
  public AudioSource introChapVoice;
  public AudioSource finalChapVoice;
  public AudioSource askToRepeatF;
  public AudioSource travelTrip1;
  public AudioSource travelTrip2;
  public AudioSource travelTrip3;
  public AudioSource travelFinal;

  // Start is called before the first frame update
  void Start()
  {
    if(SceneManager.GetActiveScene().name == "Travel")
    {
      finalMenu.SetActive(false);
    }
    
    aud = GetComponent<AudioSource>();

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
      PlayerPrefs.SetInt("ChapterPlayed", 0);  
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

  IEnumerator GameLoop()
  {
    if((SceneManager.GetActiveScene().name == "Home"))
    {
      yield return StartCoroutine(PreparedGameExecutionID());
      yield return StartCoroutine(HomeIntro());
    }

    yield return StartCoroutine(ChapIntro());
    yield return StartCoroutine(PrepareSequence());
    yield return new WaitUntil(() => sequenceToPlayList.Count > 0);

    for(int i = 0; i < sequenceToPlayList.Count; i++)
    {
      currentActionID = sequenceToPlayList[i].id;
      currentWordID = sequenceToPlayList[i].word;
      PlayerPrefs.SetInt("SEQUENCEID", sequenceToPlayList[i].sequenceid);
      startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
      FindWordNameByWordId(currentWordID);
      string payload = "{\"therapist\": " + therapistID + ", \"game\": \"" + PLAYGAMEID + "\", \"status\": " + 0 + ", \"order\": " + 0 + ", \"level\": \"" + sequenceToPlayList[i].level + "\", \"sequence\": \"" + sequenceToPlayList[i].sequence + "\", \"action\": \"" + sequenceToPlayList[i].id + "\", \"percent\": " + 0 + ", \"time\": " + 0 + "}";        
      webSockets.PrepareMessage("game", payload); 
      Debug.Log("DIZ -> " + currentWord);
      
      yield return StartCoroutine(PlaySentences(currentWord));
      yield return StartCoroutine(PlayAudioClip(currentWord));
      
      timer = sequenceToPlayList[i].time;
      RecordSound(timer);
      yield return StartCoroutine(WaitForValidation());
    }
    yield return StartCoroutine(ChapFinal());
    Debug.Log("ACABOU O SEQUENCIA");

    if((SceneManager.GetActiveScene().name == "Home"))
    {
      SceneManager.LoadScene("Travel"); 
    }

    else if((SceneManager.GetActiveScene().name == "Frog"))
    {
      if(errorDetected == true)
      {
        yield return new WaitUntil(() => frogScript.chapterFinished);
        SceneManager.LoadScene("Monkey"); 
      }
      else
      {
        yield return new WaitUntil(() => frogScript.chapterFinished);
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

    else if((SceneManager.GetActiveScene().name == "Fish"))
    {
      if(errorDetected == true)
      {
        SceneManager.LoadScene("Octopus"); 
      }
      else
      {
        SceneManager.LoadScene("Travel"); 
      }
    }
  }

   IEnumerator BonusGameLoop()
  {
    yield return StartCoroutine(ChapIntro());
    gameExecutionID = PlayerPrefs.GetInt("GAMEEXECUTIONID");
    sequenceID = PlayerPrefs.GetInt("SEQUENCEID");
    yield return StartCoroutine(webRequests.GetChapterErrors(gameExecutionID.ToString(), sequenceID.ToString()));
    yield return new WaitUntil(() => webRequests.chapterErrorListDone);
    
    for(int i = 0; i < webRequests.chapterErrorList.Count; i ++)
    { 
      activeChapter = "Fonema /" + webRequests.chapterErrorList[i].phoneme + "/";
      Debug.Log("FONEMA: " + activeChapter);
      
      yield return StartCoroutine(PrepareSequence());
      yield return new WaitUntil(() => sequenceToPlayList.Count > 0);
      Debug.Log("NUMERO DE PALAVRAS " + sequenceToPlayList.Count);
      for(int j = 0; j < sequenceToPlayList.Count; j++)
      {      
        currentActionID = sequenceToPlayList[j].id;
        currentWordID = sequenceToPlayList[j].word;
        startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        FindWordNameByWordId(currentWordID);
        string payload = "{\"therapist\": " + therapistID + ", \"game\": \"" + PLAYGAMEID + "\", \"status\": " + 0 + ", \"order\": " + 0 + ", \"level\": \"" + sequenceToPlayList[j].level + "\", \"sequence\": \"" + sequenceToPlayList[j].sequence + "\", \"action\": \"" + sequenceToPlayList[j].id + "\", \"percent\": " + 0 + ", \"time\": " + 0 + "}";        
        webSockets.PrepareMessage("game", payload); 
        Debug.Log("DIZ -> " + currentWord); 
        yield return StartCoroutine(PlayAudioClip(currentWord));

        //wordToSay.text = currentWord;
        timer = sequenceToPlayList[j].time;
        RecordSound(timer);
        yield return StartCoroutine(WaitForValidation());
      }   
    }
   yield return StartCoroutine(ChapFinal());
    SceneManager.LoadScene("Travel");      
  }

  public void FindWordNameByWordId(int wordID)
  {
    for(int i = 0; i < dataList.Count; i ++)
    {
      if(dataList[i].id == wordID)
      {
        currentWord = dataList[i].name;
      }
    }
  }

  IEnumerator HomeIntro()
  {
    yield return new WaitUntil(() => PlayerPrefs.GetInt("GAMESTARTED") == 1);
    home1.Play();  
    yield return new WaitForSeconds(7.1f);
  }

  IEnumerator ChapIntro()
  {
    introChapVoice.Play();
    if(SceneManager.GetActiveScene().name == "Frog")
    {
      yield return new WaitForSeconds(15.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Monkey")
    {
      yield return new WaitForSeconds(9.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Chameleon")
    {
      yield return new WaitForSeconds(14.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Owl")
    {
      yield return new WaitForSeconds(14.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Fish")
    {
      yield return new WaitForSeconds(9.0f);    
    }
    else if (SceneManager.GetActiveScene().name == "Octopus")
    {
      yield return new WaitForSeconds(10.0f);
    }
  }

  IEnumerator ChapFinal()
  {
    finalChapVoice.Play();
    if(SceneManager.GetActiveScene().name == "Frog")
    {
     yield return new WaitForSeconds(4.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Monkey")
    {
      yield return new WaitForSeconds(5.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Chameleon")
    {
      yield return new WaitForSeconds(6.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Owl")
    {
      yield return new WaitForSeconds(5.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Fish")
    {
      yield return new WaitForSeconds(5.0f);
    }
    else if (SceneManager.GetActiveScene().name == "Octopus")
    {
      yield return new WaitForSeconds(5.0f);
    }
  }

  IEnumerator PlaySentences(string currentWord)
  {
    if(currentWord == "O sapato da menina tem bolas amarelas.")
    {
      webSockets.PlaySentencesRequest(therapistID);
      //webSockets.playSentences = 1 playSentences     webSockets.playSentences = -1 dont playSentences
      yield return new WaitUntil(() => webSockets.getPlaySentencesDone);
      if(webSockets.playSentences == 1)
      {
        home2.Play();
        yield return new WaitForSeconds(4.8f);
      }
      else if(webSockets.playSentences == -1)
      {
        SceneManager.LoadScene("Travel");
      } 
    }
  }

   IEnumerator PlayAudioClip(string clipToPlay)
  {
    foreach (AudioClip clip in clips)
    {
      if(clip.name == clipToPlay)
      {
        aud.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
      }
    }
  }

  IEnumerator PrepareNextLevel()
  {    
    yield return new WaitUntil(() => selectionDone);
    //yield return new WaitUntil(() => prepareLevelsDone);
    //yield return new WaitUntil(() => travelScript.animationDone);
    if(PlayerPrefs.GetInt("ChapterPlayed") == 0)
    {
      PlayerPrefs.SetInt("ChapterPlayed", 1);
      travelTrip1.Play();
      yield return new WaitForSeconds(4.0f);
      SceneManager.LoadScene(PlayerPrefs.GetString("ChapterOne"));
    }
    else if(PlayerPrefs.GetInt("ChapterPlayed") == 1)
    {
      PlayerPrefs.SetInt("ChapterPlayed", 2);
      if(PlayerPrefs.GetInt("ChaptersToQuitGame") == 1)
      {
        confetti.Play();
        travelFinal.Play();
        yield return new WaitForSeconds(5.0f);
        finalMenu.SetActive(true);
      }
      else
      {
       travelTrip2.Play();
       yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(PlayerPrefs.GetString("ChapterTwo")); 
      }
    }
    else if(PlayerPrefs.GetInt("ChapterPlayed") == 2)
    {
      if(PlayerPrefs.GetInt("ChaptersToQuitGame") == 2)
      {
        confetti.Play();
        travelFinal.Play();
        yield return new WaitForSeconds(5.0f);
        finalMenu.SetActive(true);
      }
      else
      {
        PlayerPrefs.SetInt("ChapterPlayed", 3);
        travelTrip3.Play();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(PlayerPrefs.GetString("ChapterThree")); 
      }
    }
    else if(PlayerPrefs.GetInt("ChapterPlayed") == 3)
    {
       if(PlayerPrefs.GetInt("ChaptersToQuitGame") == 3)
      {
        confetti.Play();
        travelFinal.Play();
        yield return new WaitForSeconds(5.0f);
        finalMenu.SetActive(true);
      }
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
      PlayerPrefs.SetInt("ChaptersToQuitGame", 1);
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
        PlayerPrefs.SetString("ChapterOne", "Fish");
        
      }
    }
    else if(webSockets.levelsList.Count == 2)
    {
      PlayerPrefs.SetInt("ChaptersToQuitGame", 2);
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
          PlayerPrefs.SetString("ChapterTwo", "Fish");
        }
      }
      else if(webSockets.levelsList[0].Equals("2"))
      {
        PlayerPrefs.SetString("ChapterOne", "Chameleon");
        PlayerPrefs.SetString("ChapterTwo", "Fish");
      }
    }
    else if(webSockets.levelsList.Count == 3)
    {
      PlayerPrefs.SetInt("ChaptersToQuitGame", 3);
      PlayerPrefs.SetInt("NumberOfChaptersToPlay", 3);
      PlayerPrefs.SetString("ChapterOne", "Frog");
      PlayerPrefs.SetString("ChapterTwo", "Chameleon");
      PlayerPrefs.SetString("ChapterThree", "Fish");
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
       
      if(repetition == true)
      {
        yield return StartCoroutine(webRequests.PostRepSample(currentWord, currentActionID.ToString(), gameExecutionID.ToString(), currentWordID.ToString(), repSampleID.ToString()));   
      }
      else
      {
        yield return StartCoroutine(webRequests.PostSample(currentWord, currentActionID.ToString(), gameExecutionID.ToString(), currentWordID.ToString()));
        gameSampleID = PlayerPrefs.GetInt("GAMESAMPLEID");
        //Debug.Log("SAMPLE ID " + gameSampleID);
        yield return StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));
      }

      webSockets.ActionClassificationRequest(therapistID, currentWordID, gameSampleID);
    }
    //ESPERAR ATE QUE A VALIDACAO SEJA FEITA
    yield return new WaitUntil(() => webSockets.validationDone);
    yield return new WaitUntil(() => webSockets.validationValue > -2);

    if(webSockets.validationValue == -1)
    {
      askToRepeatF.Play();
      yield return new WaitForSeconds(2.0f);
      startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
      Debug.Log("DIZ -> " + currentWord); 
      
      yield return StartCoroutine(PlayAudioClip(currentWord));
      
      RecordSound(timer);
      webSockets.validationValue = -2;
      repetition = true;
      repSampleID = gameSampleID;
      //Debug.Log("REP SAMPLE ID" + repSampleID);
      yield return StartCoroutine(WaitForValidation());
    }

    else if(webSockets.validationValue >= 0)
    {
      if(repetition == true)
      {     
        repetition = false;
      }

      if(webSockets.validationValue > 0)
      {
        errorDetected = true;
      }

      if (SceneManager.GetActiveScene().name == "Home")
      {
        homeScript.doAnimation = true;
        if(homeScript.wordsDone)
        {
          yield return new WaitUntil(() => homeScript.animationDone);
          homeScript.animationDone = false;
        }
        homeScript.animationDone = false;
        webSockets.validationValue = -2;      
      }
      else if (SceneManager.GetActiveScene().name == "Frog")
      {
        frogScript.canShow = true;
        frogScript.bugNumber ++;
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
        octopusScript.canShow = true;
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
        owlScript.canShow = true;
        yield return new WaitUntil(() => owlScript.isMatch);
        owlScript.isMatch = false;
        webSockets.validationValue = -2;      
      }
      else if (SceneManager.GetActiveScene().name == "Fish")
      {
        fishScript.canShowFood = true;
        yield return new WaitUntil(() => fishScript.isCaught);
        fishScript.isCaught = false;
        fishScript.canShowFood = false;
        fishScript.foodNumber ++;
        webSockets.validationValue = -2;      
      }
      yield return StartCoroutine(PreparedGameResult());
    }
  }

  IEnumerator PrepareGameStructure()
  {
    yield return new WaitUntil(() => structReqDone);
    //Debug.Log("Waiting for structure...");
    //Debug.Log("Structure request completed! Actions: " + contentList.Count);
    
    //Debug.Log("Waiting for word repository...");
    yield return new WaitUntil(() => respositoryReqDone);
    //Debug.Log("Repository request completed! Words: " + dataList.Count);
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
    //Debug.Log("REP " + repetition);

    if(repetition == false)
    {
      if(webSockets.statusValue > 0)
      {
        //Debug.Log("ERRORSTATUS" + webSockets.statusValue);
        errorStatus = 0;
      }
      else
      {
        errorStatus = 1;
      }
      yield return StartCoroutine(webRequests.PostGameResult(errorStatus.ToString(), "0", currentActionID.ToString(),  gameExecutionID.ToString(), startTime, endTime, currentWord));     
      //Debug.Log("LOG POST GAME RESULT");
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

  public void OnApplicationQuit()
  {
    Debug.Log("Stop WS client and logut");
    string payload = "{\"therapist\": " + therapistID + "}";
    webSockets.PrepareMessage("status", payload);
    webSockets.StopClient(payload);
    Application.Quit();       

  }
}

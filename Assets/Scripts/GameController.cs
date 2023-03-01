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
  public bool alreadyRequestLevels = false;

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
      StartCoroutine(GameLoop());    
    }

    else if(SceneManager.GetActiveScene().name == "Travel")
    {
      if(alreadyRequestLevels == false)
      {
        StartCoroutine(PrepareLevels());
        alreadyRequestLevels = true;
      }
      //else
      //{
      StartCoroutine(PrepareNextLevel()); 
      //}
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
    //NEXT SCENE
    SceneManager.LoadScene("Travel"); 
  }

  IEnumerator PrepareNextLevel()
  {
    yield return new WaitUntil(() => webSockets.getLevelsDone);
    yield return new WaitUntil(() => travelScript.patientInteractionDone);

    if(webSockets.levelsList[0].Equals("DONE"))
    {
      if(webSockets.levelsList[1].Equals("DONE"))
      {
        if(webSockets.levelsList[2].Equals("DONE"))
        {
          Debug.Log("TERMINOU O JOGO");
        }
        else
        { 
          if(webSockets.levelsList[2].Equals("1"))
          {
            webSockets.levelsList[2] = "DONE";
            PlayerPrefs.SetString("NEXTSCENE", "Frog");
            SceneManager.LoadScene("Frog");
          }
          else if(webSockets.levelsList[2].Equals("2"))
          { 
            webSockets.levelsList[2] = "DONE";
            PlayerPrefs.SetString("NEXTSCENE", "Chameleon");
            SceneManager.LoadScene("Chameleon");
          }
          else if(webSockets.levelsList[2].Equals("3"))
          {
            webSockets.levelsList[2] = "DONE";
            PlayerPrefs.SetString("NEXTSCENE", "Octopus");
            SceneManager.LoadScene("Octopus");
          }  
        }
      }
      else
      {
        if(webSockets.levelsList[1].Equals("1"))
        {
          webSockets.levelsList[1] = "DONE";
          PlayerPrefs.SetString("NEXTSCENE", "Frog");
          SceneManager.LoadScene("Frog");
        }
        else if(webSockets.levelsList[1].Equals("2"))
        {
          webSockets.levelsList[1] = "DONE";
          PlayerPrefs.SetString("NEXTSCENE", "Chameleon");
          SceneManager.LoadScene("Chameleon");
        }
        else if(webSockets.levelsList[1].Equals("3"))
        {
          webSockets.levelsList[1] = "DONE";
          PlayerPrefs.SetString("NEXTSCENE", "Octopus");
          SceneManager.LoadScene("Octopus");
        }      
      }            
    } 
    else
    {
      if(webSockets.levelsList[0].Equals("1"))
      {
        webSockets.levelsList[0] = "DONE";
        PlayerPrefs.SetString("NEXTSCENE", "Frog");
        SceneManager.LoadScene("Frog");
      }
      else if(webSockets.levelsList[0].Equals("2"))
      {
        webSockets.levelsList[0] = "DONE";
        PlayerPrefs.SetString("NEXTSCENE", "Chameleon");
        SceneManager.LoadScene("Chameleon");
      }
      else if(webSockets.levelsList[0].Equals("3"))
      {
        webSockets.levelsList[0] = "DONE";
        PlayerPrefs.SetString("NEXTSCENE", "Octopus");
        SceneManager.LoadScene("Octopus");
      }  
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
    yield return new WaitUntil(() => webSockets.socketIsReady);
    webSockets.LevelsToPlayRequest(therapistID);
    yield return new WaitUntil(() => webSockets.getLevelsDone);
  }

  IEnumerator WaitForValidation()
  {
    yield return new WaitUntil(() => webSockets.socketIsReady);
    

    if (SceneManager.GetActiveScene().name == "Home")
    {
      webSockets.ActionClassificationGeralRequest(therapistID, currentWordID, gameSampleID);
    }
    else
    {
      webSockets.ActionClassificationRequest(therapistID, currentWordID, gameSampleID);
      Debug.Log("ACTION ID " + currentActionID);
    }

    yield return new WaitUntil(() => webSockets.validationDone);
    yield return new WaitUntil(() => webSockets.validationValue > -2);

    endTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");         
    SavWav.Save(currentWord + ".wav", userRecording.clip);

    gameExecutionID = PlayerPrefs.GetInt("GAMEEXECUTIONID");
    //yield return StartCoroutine(webRequests.PostSample(currentWord, contentList[currentActionID].id.ToString(), gameExecutionDone.ToString(), contentList[currentActionID].word.ToString()));
    yield return StartCoroutine(webRequests.PostSample(currentWord, currentActionID.ToString(), gameExecutionID.ToString(), currentWordID.ToString()));
    
    Debug.Log("LOG POST SAMPLE");
    Debug.Log("WORD: " + currentWord + " ACTIONID: " +  currentActionID.ToString() + " GAMEEXECUTIONID: " +  gameExecutionID.ToString() + " WORDID: " + currentWordID.ToString());
   
    gameSampleID = PlayerPrefs.GetInt("GAMESAMPLEID");
    yield return StartCoroutine(webRequests.PostGameRequest(gameSampleID.ToString()));
    Debug.Log("GAMESAMPLEID: " + gameSampleID.ToString());

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
    //yield return StartCoroutine(webRequests.PostGameResult("1", "0", [currentActionID].id.ToString(), gameExecutionDone.ToString(), startTime, endTime, currentWord));     
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

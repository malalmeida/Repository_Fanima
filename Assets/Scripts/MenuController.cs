using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
    const int PLAYGAMEID = 29;
    int patientID;
    public GameObject startMenuUI;
    public GameObject connectButton;
    public GameObject playButton;

    private string startTime;
    private string endTime;
    public AudioSource song;
    public GameStructureRequest gameStructureRequest;
    public AudioSource buttonConfirm;

   
    void Awake()
    {
        if (!DataManager.instance.structReqDone)
        {
            StartCoroutine(gameStructureRequest.GetStructureRequest(PLAYGAMEID));
            StartCoroutine(gameStructureRequest.GetRepository());
            PlayerPrefs.SetInt("GAMESTARTED", 0);

        }

    }

    void Update()
    {
        if(gameStructureRequest.gameController.newGameExecutuionIDRequest)
        {
            gameStructureRequest.gameController.newGameExecutuionIDRequest = false;
            if(!DataManager.instance.gameExecutionDone)
                StartCoroutine(gameStructureRequest.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), patientID.ToString()));
            PlayerPrefs.SetInt("GAMESTARTED", 1);

        }
    }

    void Start()
    {
        //patientID = Int32.Parse(PlayerPrefs.GetString("PLAYERID"));
        patientID = DataManager.instance.patientID;
        if(SceneManager.GetActiveScene().name == "Geral")
        {
            playButton.SetActive(false);
        }
        StartCoroutine(WaitForTherapistReady());

    }

    public void  StartGame()
    {
        buttonConfirm.Play();               
        Debug.Log("RESTORE DONE");   

        if(gameStructureRequest.gameController.requestGameExecutionID)
        {
            Debug.Log("REQUEST DONE");        
            gameStructureRequest.gameController.requestGameExecutionID = false;
            if (!DataManager.instance.gameExecutionDone)
            {
                StartCoroutine(gameStructureRequest.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), patientID.ToString()));
            }
        }
        startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        Time.timeScale = 1f;                
        startMenuUI.SetActive(false);
        PlayerPrefs.SetInt("GAMESTARTED", 1);
        song.Stop();
    }

    IEnumerator WaitForTherapistReady()
    {
        if(gameStructureRequest.gameController.therapistReady)
        {
            playButton.SetActive(true);
            yield return new WaitUntil(() => gameStructureRequest.gameController.responseToRestoreDone);
            if(gameStructureRequest.gameController.webSockets.continueGame == false)
            {
                yield return new WaitUntil(() => gameStructureRequest.gameController.requestGameExecutionID);
            }
            Debug.Log("REQUEST DONE");        
            StartGame();
        }
        else
        {
            while (!gameStructureRequest.gameController.therapistReady)
            {
                gameStructureRequest.gameController.requestTherapistStatus = true;
                yield return new WaitForSeconds(2.0f);
            }
            //esperar que o terapeuta esteja ready e msotra o botão jogar
            playButton.SetActive(true);
            Debug.Log("responseToRestoreDone " + gameStructureRequest.gameController.responseToRestoreDone);
            yield return new WaitUntil(() => gameStructureRequest.gameController.responseToRestoreDone);
            if(gameStructureRequest.gameController.webSockets.continueGame == false)
            {
                yield return new WaitUntil(() => gameStructureRequest.gameController.requestGameExecutionID);
            }
            StartGame();
        }
    }
}

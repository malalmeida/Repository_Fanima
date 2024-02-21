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
        StartCoroutine(gameStructureRequest.GetStructureRequest(PLAYGAMEID));
        StartCoroutine(gameStructureRequest.GetRepository());
        PlayerPrefs.SetInt("GAMESTARTED", 0);
    }

    void Start()
    {
        patientID = Int32.Parse(PlayerPrefs.GetString("PLAYERID"));
        if(SceneManager.GetActiveScene().name == "Geral")
        {
            playButton.SetActive(false);
        }

        StartCoroutine(WaitForTherapistReady());
    }

    public void StartGame()
    {
        buttonConfirm.Play();
        if(gameStructureRequest.gameController.responseToRestoreDone)
        {                
            if(gameStructureRequest.gameController.requestGameExecutionID)
            {
                gameStructureRequest.gameController.requestGameExecutionID = false;
                StartCoroutine(gameStructureRequest.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), patientID.ToString()));
            }
            startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            Time.timeScale = 1f;                
            startMenuUI.SetActive(false);
            PlayerPrefs.SetInt("GAMESTARTED", 1);
            song.Stop();
        }
    }

    IEnumerator WaitForTherapistReady()
    {
        if(gameStructureRequest.gameController.therapistReady)
        {
            playButton.SetActive(true);
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
        }

        //yield return new WaitUntil(() => gameStructureRequest.gameController.therapistReady);
        //playButton.SetActive(true);
    }
/*
    public void StartGame()
    {   
        if(gameStructureRequest.gameController.therapistReady)
        { 
            if(gameStructureRequest.gameController.responseToRestoreDone)
            {
                gameStructureRequest.gameController.requestTherapistStatus = false;
                
                if(gameStructureRequest.gameController.requestGameExecutionID)
                {
                    gameStructureRequest.gameController.requestGameExecutionID = false;
                    StartCoroutine(gameStructureRequest.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), patientID.ToString()));
                }
                startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
                Time.timeScale = 1f;
                startMenuUI.SetActive(false);
                PlayerPrefs.SetInt("GAMESTARTED", 1);
                song.Stop();
            }
        }
        else
        {
            gameStructureRequest.gameController.requestTherapistStatus = true;
        }
    }
*/

/*
    public void SessionCheck()
    {   
        if(gameStructureRequest.gameController.therapistReady == false)
        { 
            gameStructureRequest.gameController.requestTherapistStatus = true;
            //connectButton.SetActive(false);
            //playButton.SetActive(true);
        }
        else
        {
            connectButton.SetActive(false);
            playButton.SetActive(true);
        }
    }

    public void StartGame()
    {   
        if(gameStructureRequest.gameController.responseToRestoreDone)
        {
            if(gameStructureRequest.gameController.requestGameExecutionID)
            {
                gameStructureRequest.gameController.requestGameExecutionID = false;
                StartCoroutine(gameStructureRequest.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), patientID.ToString()));
            }
        
            startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            Time.timeScale = 1f;
            startMenuUI.SetActive(false);
            PlayerPrefs.SetInt("GAMESTARTED", 1);
            song.Stop();
        }
    }
*/
}

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
    public GameObject continueButtonUI;
    private string startTime;
    private string endTime;
    public AudioSource song;

    public GameStructureRequest gameStructureRequest;
   
    void Awake()
    {
        StartCoroutine(gameStructureRequest.GetStructureRequest(PLAYGAMEID));
        StartCoroutine(gameStructureRequest.GetRepository());
        PlayerPrefs.SetInt("GAMESTARTED", 0);

        //verificar se uma sessão foi interrompeida 
        /*if(jogo interrompido a meio)
        {
            continueButtonUI.SetActive(true);
        }
        else
        {
            continueButtonUI.SetActive(false);
        }
        */
    }

    void Start()
    {
        patientID = Int32.Parse(PlayerPrefs.GetString("PLAYERID"));
    }

    public void StartGame()
    {
        if(gameStructureRequest.gameController.therapistReady)
        {
            gameStructureRequest.gameController.requestTherapistStatus = false;
            StartCoroutine(gameStructureRequest.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), patientID.ToString()));
            startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            Time.timeScale = 1f;
            startMenuUI.SetActive(false);
            PlayerPrefs.SetInt("GAMESTARTED", 1);
            song.Stop();
        }
        else
        {
            gameStructureRequest.gameController.requestTherapistStatus = true;
        }
    }
    //para continuar a sessao 
    // ver onde a sessão parou e mudar para a scene onde ficou
    public void ContinueGame()
    {
        if(gameStructureRequest.gameController.therapistReady)
        {
            CheckLastChapterPlayed();
            gameStructureRequest.gameController.requestTherapistStatus = false;
            StartCoroutine(gameStructureRequest.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), patientID.ToString()));
            startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            Time.timeScale = 1f;
            startMenuUI.SetActive(false);
            PlayerPrefs.SetInt("GAMESTARTED", 1);
            song.Stop();
        }
        else
        {
            gameStructureRequest.gameController.requestTherapistStatus = true;
        }
    }

    public void CheckLastChapterPlayed()
    {

    }
}

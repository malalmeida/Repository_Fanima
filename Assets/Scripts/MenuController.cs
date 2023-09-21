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
    private string startTime;
    private string endTime;
    public AudioSource song;

    public GameStructureRequest gameStructureRequest;
   
    void Awake()
    {
        StartCoroutine(gameStructureRequest.GetStructureRequest(PLAYGAMEID));
        StartCoroutine(gameStructureRequest.GetRepository());
        PlayerPrefs.SetInt("GAMESTARTED", 0);

    }

    void Start()
    {
        //patientID = Int32.Parse(PlayerPrefs.GetString("PLAYERID", "52"));
        patientID = Int32.Parse(PlayerPrefs.GetString("PLAYERID"));
        //PlayerPrefs.SetInt("PATIENTID", patientID);
        //pauseMenuUI.SetActive(false);
        //StartCoroutine(gameStructureRequest.GetTherapist(patientID.ToString()));
    }

    public void StartGame()
    {
        //StartCoroutine(gameStructureRequest.gameController.RequestTherapistStatus());

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
}

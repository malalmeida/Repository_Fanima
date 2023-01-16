using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
    const int PLAYGAMEID = 29;
    int USERID;
    //public WebRequests webRequests;
    public GameObject startMenuUI;
    public GameObject pauseMenuUI;
    private bool isOnline = false;
    private string startTime;
    private string endTime;

    public GameStructureRequest gameStructureRequest;
   
    void Awake()
    {
       StartCoroutine(gameStructureRequest.GetStructureRequest(PLAYGAMEID));
       StartCoroutine(gameStructureRequest.GetRepository());
    }

    void Start()
    {
        USERID = Int32.Parse(PlayerPrefs.GetString("PLAYERID", "52"));
        pauseMenuUI.SetActive(false);

    }

    public void StartGame()
    {
        StartCoroutine(gameStructureRequest.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), USERID.ToString()));

        startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        Time.timeScale = 1f;
        isOnline = true;
        startMenuUI.SetActive(false); 
    
        //SceneManager.LoadScene("Frog");

    }
}

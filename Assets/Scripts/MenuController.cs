using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
    const int PLAYGAMEID = 29;
    const int REPOSITORYID = 2;
    int USERID;
    public WebRequests webRequests;
    public GameObject startMenuUI;
    public GameObject pauseMenuUI;
    private bool isOnline = false;
    private string startTime;
    private string endTime;
   
    void Awake()
    {
        StartCoroutine(webRequests.GetStructureRequest(PLAYGAMEID));
        StartCoroutine(webRequests.GetRepository(REPOSITORYID));
    }

    void Start()
    {
        USERID = Int32.Parse(PlayerPrefs.GetString("PLAYERID", "52"));
        pauseMenuUI.SetActive(false);

    }

    public void StartGame()
    {
        StartCoroutine(webRequests.PostGameExecutionRequest(System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), PLAYGAMEID.ToString(), USERID.ToString()));

        startTime = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        Time.timeScale = 1f;
        isOnline = true;
        startMenuUI.SetActive(false); 
    
        //SceneManager.LoadScene("Frog");

    }
}

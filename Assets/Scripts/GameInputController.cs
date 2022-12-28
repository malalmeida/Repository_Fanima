using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameInputController : MonoBehaviour
{
    public bool structReqDone = false;
    public bool respositoryReqDone = false;
    public int gameexecutionid = -1;
    public List<actionClass> contentList;
    public List<dataSource> dataList;
    public List<string> repositoryOfWords; 
    const int PLAYGAMEID = 29;

     void Awake()
    {
        //StartCoroutine(webRequests.GetStructureRequest(PLAYGAMEID)); 
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DATA E HORA: " + System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + " UTC");
        List<string> repositoryOfWords = new List<string>();
       if(SceneManager.GetActiveScene().name == "Home")
            {
                StartCoroutine(InitiateGame());
                StartCoroutine(PreparedToStart());   
            } 
    }

    IEnumerator InitiateGame()
    {
        Debug.Log("Waiting for structure request...");
        yield return new WaitUntil(() => structReqDone);
        Debug.Log("Structure request completed! Actions -> " + contentList.Count);
    
        Debug.Log("Waiting for word repository request...");
        yield return new WaitUntil(() => respositoryReqDone);
        Debug.Log("Repository request completed! Words -> " + dataList.Count);

        //prep repository of strings
        for (int i = 0; i < contentList.Count; i++)
        {
            for (int j = 0; j < dataList.Count; j++)
            {
                if(contentList[i].word == dataList[j].id)
                {
                    repositoryOfWords.Add(dataList[j].name);
                    PlayerPrefs.SetString("Frog" + i , dataList[j].name);
                }
            }
        }
        
        foreach (string w in repositoryOfWords)
        {  
            Debug.Log("Words added to repository! Words -> " + w);
        }
        Debug.Log("Repository " + repositoryOfWords.Count);
        
    }

    IEnumerator PreparedToStart()
    {
        Debug.Log("Waiting for execution ID...");
        yield return new WaitUntil(() => gameexecutionid > 0);
        Debug.Log("Game Execution request completed! ID -> " + gameexecutionid);
    }
  
}

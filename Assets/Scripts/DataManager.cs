using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public bool helpAllowed = true;
    public bool musicAllowed = true;
    //public GameObject backgroundMusic;
    public Sprite maleGuide;
    public bool maleChoosen;
    //public bool femaleChoosen;
    //public Sprite femaleGuide;

    //public string gameExecutionID;
    public string token;
    public int therapistID;
    public int patientID;
    public string playerID; //not used?

    //gameStructure
    public List<actionClass> contentList;
    public bool structReqDone = false;
    public List<actionClass> contentChapter0List;
    public List<actionClass> contentChapter1List;
    public List<actionClass> contentChapter2List;
    public List<actionClass> contentChapter3List;
    public bool contentIsSplit = false;

    //gameRepository
    public List<dataSource> dataList;
    public bool respositoryReqDone = false;

    //PostGameExecutionRequest
    public int gameExecutionID;
    public bool gameExecutionDone = false;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            
        }
    }

    void Update()
    {
        if(structReqDone && !contentIsSplit)
        {
            splitDataByScene();
        }
    }

    void splitDataByScene()
    {
        for (int i = 0; i < contentList.Count; i++)
        {
            if (contentList[i].level == "Geral")
            {
                contentChapter0List.Add(contentList[i]);
            }
            else if (contentList[i].level == "Oclusivas")
            {
                contentChapter1List.Add(contentList[i]);
            }
            else if (contentList[i].level == "Fricativas")
            {
                contentChapter2List.Add(contentList[i]);
            }
            else if (contentList[i].level == "Vibrantes e Laterais")
            {
                contentChapter3List.Add(contentList[i]);
            }
        }

        contentIsSplit = true;
    }
}

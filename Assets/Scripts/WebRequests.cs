using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.Windows;

public class WebRequests : MonoBehaviour
{
    readonly string baseURL = "http://193.137.46.11/api/";

    public GameInputController gameScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator PostLoginRequest(string user, string pass)
    {
        var url = baseURL + "login";

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("username", user));
        parameters.Add(new MultipartFormDataSection("password", pass));

        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR POST SAMPLE: " + www.error + " END");
        }
        else {
            Debug.Log("ANSWER POST SAMPLE: " + www.downloadHandler.text + " END");

            PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(www.downloadHandler.text);

            if(playerInfo.content[0] == "wrong data")
            {
                Debug.Log("Wrong login info!");
            }
            else
            {
                PlayerPrefs.SetString("PLAYERID", playerInfo.content[0]);
                PlayerPrefs.SetString("PLAYERNAME", playerInfo.content[1]);
                PlayerPrefs.SetString("TOKEN", playerInfo.content[3]);

                SceneManager.LoadScene("Home");
            }     
        }

    }

    public IEnumerator PostLogoutRequest()
    {
        var url = baseURL + "logout";

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");

        PlayerPrefs.DeleteKey("TOKEN");
        PlayerPrefs.DeleteKey("PLAYERID");
        PlayerPrefs.DeleteKey("PLAYERNAME");

        PlayerPrefs.Save();

        UnityWebRequest www = UnityWebRequest.Post(url, "");

        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR POST SAMPLE: " + www.error + " END");
            SceneManager.LoadScene("Login");
        }
        else {
            Debug.Log("ANSWER POST SAMPLE: " + www.downloadHandler.text + " END");
            SceneManager.LoadScene("Login");
        }

    }

    public IEnumerator GetStructureRequest(int gameID)
    {
        var url = baseURL + "game/" + gameID + "/structure";

        Debug.Log("GET STRUCTURE CALLED -> " + url);

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR GET STRUCTURE: " + www.error + " END");
        }
        else {
            Debug.Log("ANSWER GET STRUCTURE: " + www.downloadHandler.text + " END");
            jsonDataLoader jsonData = JsonUtility.FromJson<jsonDataLoader>(www.downloadHandler.text);
            
            if(SceneManager.GetActiveScene().name == "Home")
            {
                gameScript.contentList = jsonData.content;
                gameScript.structReqDone = true;
            }            
        }
    }   

     public IEnumerator GetRepository(int repositoryID)
    {
        var url = baseURL + "datasource/" + repositoryID;

        Debug.Log("GET REPOSITORY CALLED -> " + url);

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR GET REPOSITORY: " + www.error + " END");
        }
        else {
            Debug.Log("ANSWER GET REPOSITORY: " + www.downloadHandler.text + " END");
            jsonDataRepository jsonDataRepository = JsonUtility.FromJson<jsonDataRepository>(www.downloadHandler.text);
            
            if(SceneManager.GetActiveScene().name == "Home")
            {
                gameScript.dataList = jsonDataRepository.content;
                gameScript.respositoryReqDone = true;
            }            
        }
    }

     public IEnumerator PostGameExecutionRequest(string executedDate, string gameID, string userID)
    {
        var url = baseURL + "gameexecution";

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("executed", executedDate));
        parameters.Add(new MultipartFormDataSection("gameid", gameID));
        parameters.Add(new MultipartFormDataSection("userid", userID));

        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR GAME EXECUTION:" + www.error + " END");
        }
        else {
            Debug.Log("ANSWER GAME EXECUTION: " + www.downloadHandler.text + " END");
            
            if(SceneManager.GetActiveScene().name == "Home")
            {
                gameScript.gameexecutionid = int.Parse(www.downloadHandler.text);
            }
        }
    } 









}

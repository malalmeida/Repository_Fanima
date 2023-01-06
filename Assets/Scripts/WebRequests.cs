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

    public GameController gameScript;

    // Start is called before the first frame update
    void Start()
    {
        
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

        //Debug.Log("GET REPOSITORY CALLED -> " + url);

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

    public IEnumerator PostSampleRequest(byte[] byteArray, string actionID, string gameExeID)
    {
        var url = baseURL + "gamesample";
        string time = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

        //Debug.Log("GAME SAMPLE SENT TIME: " + time);

    
        //Debug.Log(byteArray);
        string base64String = System.Convert.ToBase64String(byteArray);
        //Debug.Log(base64String);

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("data", "{\"time\":\""+ time +"\", \"base64\":\""+ base64String +"\"}"));
        parameters.Add(new MultipartFormDataSection("gameactionid", actionID));
        parameters.Add(new MultipartFormDataSection("gameexecutionid", gameExeID));

        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR POST SAMPLE: " + www.error + " END");
        }
        else {
            Debug.Log("ANSWER POST SAMPLE: " + www.downloadHandler.text + " END");
        }

    }

    public IEnumerator PostGameResultRequest(string status, string actionID, string gameExeID, string startTime, string endTime)
    {
        var url = baseURL + "gameresult";
        Debug.Log("POST GAME RESULT Start: " + startTime + " End: " + endTime);

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("status", status));
        parameters.Add(new MultipartFormDataSection("gameactionid", actionID));
        parameters.Add(new MultipartFormDataSection("gameexecutionid", gameExeID));
        parameters.Add(new MultipartFormDataSection("start", startTime));
        parameters.Add(new MultipartFormDataSection("end", endTime));

        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR POST GAME RESULT:" + www.error + " END");
        }
        else {
            
            Debug.Log("ANSWER POST GAME RESULT: " + www.downloadHandler.text + " END");
        }
    }

    public IEnumerator PostGameRequest(string sampleID)
    {
        var url = baseURL + "gamerequest";

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("sampleID", sampleID));
    

        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR POST GAME REQUEST:" + www.error + " END");
        }
        else {
            
            Debug.Log("ANSWER POST GAME REQUEST: " + www.downloadHandler.text + " END");
        }

    }
/*
    public IEnumerator PutGameSampleRequest(string sampleID, string label)
    {
        var url = baseURL + "gamesample";

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("sampleID", sampleID));
        parameters.Add(new MultipartFormDataSection("label", label));

        UnityWebRequest www = UnityWebRequest.Put(url, parameters);

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR PUT GAME SAMPLE REQUEST:" + www.error + " END");
        }
        else {
            
            Debug.Log("ANSWER PUT GAME SAMPLE REQUEST: " + www.downloadHandler.text + " END");
        }

    }
*/

}

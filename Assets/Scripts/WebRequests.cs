using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Text;
using System;

public class WebRequests : MonoBehaviour
{
    readonly string baseURL = "http://193.137.46.11/api/";

    public List<errorClass> chapterErrorList;
    public bool chapterErrorListDone = false;
    public bool badgesListDone = false;
    public List<badgesClass> badgesList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Envio sample do audio 
    //public IEnumerator PostSample(byte[] byteArray, string actionID, string gameExeID)
    public IEnumerator PostSample(string fileName, string actionID, string gameExeID, string wordID)
    {
        var url = baseURL + "gamesample";
        string time = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");

        string path = PlayerPrefs.GetString("FILEPATH");

        byte[] audiobyte = System.IO.File.ReadAllBytes(path);

        string base64String = System.Convert.ToBase64String(audiobyte);

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("data", "{\"id\":\""+ wordID +"\", \"time\":\""+ time +"\", \"base64\":\""+ base64String +"\"}"));
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

            //gameScript.gameSampleID = int.Parse(www.downloadHandler.text);
            Debug.Log("WebREQUEST POST SAMPLE RESPONSE " + int.Parse(www.downloadHandler.text));
            PlayerPrefs.SetInt("GAMESAMPLEID", int.Parse(www.downloadHandler.text));
        }

    }

    public IEnumerator PostRepSample(string fileName, string actionID, string gameExeID, string wordID, string sampleID, string repeatValue)
    {
        var url = baseURL + "gamesample";
        string time = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");

        string path = PlayerPrefs.GetString("FILEPATH");

        byte[] audiobyte = System.IO.File.ReadAllBytes(path);

        string base64String = System.Convert.ToBase64String(audiobyte);

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("data", "{\"id\":\""+ wordID +"\", \"time\":\""+ time +"\", \"base64\":\""+ base64String +"\", \"repeat\":\""+ repeatValue +"\"}"));
        parameters.Add(new MultipartFormDataSection("gameactionid", actionID));
        parameters.Add(new MultipartFormDataSection("gameexecutionid", gameExeID));
        parameters.Add(new MultipartFormDataSection("sampleid", sampleID));


        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR POST SAMPLE: " + www.error + " END");
        }
        else {
            Debug.Log("ANSWER POST SAMPLE: " + www.downloadHandler.text + " END");

            //gameScript.gameSampleID = int.Parse(www.downloadHandler.text);
            //PlayerPrefs.SetInt("GAMESAMPLEID", int.Parse(www.downloadHandler.text));
        }

    }

    //Envio do pedido da classificação da PLAY
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

    //Envio do resultado 
    //status = 0 
    //score = 0
    public IEnumerator PostGameResult(string status, string score, string actionID, string gameExeID, string startTime, string endTime, string fileName)
    {
        string path = PlayerPrefs.GetString("FILEPATH");
        System.IO.File.Delete(path);

        var url = baseURL + "gameresult";
        //Debug.Log("POST GAME RESULT Start: " + startTime + " End: " + endTime);

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("status", status));
        parameters.Add(new MultipartFormDataSection("score", score));
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
            PlayerPrefs.SetInt("GAMERESULTID", int.Parse(www.downloadHandler.text));

        }
    }

     public IEnumerator GetChapterErrors(string gameExeID, string sequenceID)
    {
        var url = baseURL + "gameexecution/" + gameExeID + "/sequence/" + sequenceID + "/error/phoneme" ;
        Debug.Log("GAMEEXEID " + gameExeID + " SEQUENCEID " + sequenceID);
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) 
        {
            Debug.Log("ERROR GET CHAPTER ERROS: " + www.error + " END");
        }
        else 
        {
            Debug.Log("ANSWER GET CHAPTER ERROS: " + www.downloadHandler.text + " END");
            jsonDataError jsonDataError = JsonUtility.FromJson<jsonDataError>(www.downloadHandler.text);
            
            chapterErrorList = jsonDataError.content;
            chapterErrorListDone = true;
        }
    }  

    public IEnumerator PostStopGameExecutionRequest(string gameExecutionID, string levels, string levelID, string sequenceID, string actionID, string startTime, string endTime)
    {
        var url = baseURL + "gameexecution";

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("gameexid", gameExecutionID));
       // parameters.Add(new MultipartFormDataSection("status", "{" + levels + "\", \"levelid\":\" " + levelID + "\", \"sequenceid\":\"" + sequenceID + "\", \"actionid\":\"" + actionID + "\", \"duration", "{\"start\":\"" + startTime + "\", \"end\":\"" + endTime + "\"}" "\"}"));


        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR GAME EXECUTION:" + www.error + " END");
        }
        else {
            Debug.Log("ANSWER GAME EXECUTION: " + www.downloadHandler.text + " END");
        }
    } 

    public IEnumerator PostBadge(string patientID, string gameID, string gameExecutionID, string badgeID)
    {
        var url = baseURL + "userbadge";

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("userid", patientID));
        parameters.Add(new MultipartFormDataSection("gameid", gameID));
        parameters.Add(new MultipartFormDataSection("execid", gameExecutionID));
        parameters.Add(new MultipartFormDataSection("badgeid", badgeID));        

        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR GAME EXECUTION:" + www.error + " END");
        }
        else {
            Debug.Log("ANSWER GAME EXECUTION: " + www.downloadHandler.text + " END");
        }
    }

    public IEnumerator GetSessionBagdes(string patientID, string gameID, string gameExecutionID)
    {
        var url = baseURL + "patient/" + patientID + "/game/" + gameID + "/exec/" + gameExecutionID + "/badge" ;
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) 
        {
            Debug.Log("ERROR GET BADGES: " + www.error + " END");
        }
        else 
        {
            Debug.Log("ANSWER GET BADGES: " + www.downloadHandler.text + " END");
            jsonDataBadges jsonDataBadges = JsonUtility.FromJson<jsonDataBadges>(www.downloadHandler.text);
            
            badgesList = jsonDataBadges.content;
            badgesListDone = true;
        }
    }
}

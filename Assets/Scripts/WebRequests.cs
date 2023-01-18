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

    //public GameController gameScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }
/*    
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
            
            //if(SceneManager.GetActiveScene().name == "Home")
            //{
                gameScript.contentList = jsonData.content;
                gameScript.structReqDone = true;
            //}            
        }
    }   

     public IEnumerator GetRepository()
    {
        var url = baseURL + "datasource/speech";

        //Debug.Log("GET REPOSITORY CALLED -> " + url);

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR GET REPOSITORY: " + www.error + " END");
        }
        else {
            Debug.Log("ANSWER GET REPOSITORY: " + www.downloadHandler.text + " END");
            jsonDataRepository jsonDataRepository = JsonUtility.FromJson<jsonDataRepository>(www.downloadHandler.text);
            
            //if(SceneManager.GetActiveScene().name == "Home")
            //{
                gameScript.dataList = jsonDataRepository.content;
                gameScript.respositoryReqDone = true;
            //}            
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
                gameScript.gameExecutionID = int.Parse(www.downloadHandler.text);
            }
        }
        
    } 
*/
    //Envio sample do audio 
    //public IEnumerator PostSample(byte[] byteArray, string actionID, string gameExeID)
    public IEnumerator PostSample(string fileName, string actionID, string gameExeID, string wordID)
    {
        var url = baseURL + "gamesample";
        string time = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");

        //Debug.Log("GAME SAMPLE SENT TIME: " + time);

        //string path = "C:/Users/gvgia/AppData/LocalLow/DefaultCompany/Fanima/" + fileName + ".wav";
        //MAC path
        string path = "/Users/inesantunes/Library/Application Support/DefaultCompany/Fanima/" + fileName + ".wav";

        byte[] audiobyte = File.ReadAllBytes(path);

        string base64String = System.Convert.ToBase64String(audiobyte);
    
        //Debug.Log(byteArray);
        //string base64String = System.Convert.ToBase64String(byteArray);
        //Debug.Log(base64String);

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
            PlayerPrefs.SetInt("GAMESAMPLEID", int.Parse(www.downloadHandler.text));
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
    public IEnumerator PostGameResult(string status, string score, string actionID, string gameExeID, string startTime, string endTime)
    {
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
        }
    }

}

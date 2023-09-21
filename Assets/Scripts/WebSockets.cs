using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


using System.Web;

public class WebSockets : MonoBehaviour{

    private WebSocket ws;
    public string wsURL;
    public delegate void WebSocketOnOpenHandler(EventArgs e);
    public event WebSocketOnOpenHandler onOpen;
    public delegate void WebSocketOnCloseHandler(CloseEventArgs e);
    public event WebSocketOnCloseHandler onClose;
    public delegate void WebSocketOnErrorHandler(ErrorEventArgs e);
    public event WebSocketOnErrorHandler onError;
    public delegate void WebSocketOnMessageHandler(MessageEventArgs e);
    public event WebSocketOnMessageHandler onMessage;
    public delegate void WebSocketOnProcessHandler();
    public event WebSocketOnProcessHandler onProcess;
    public int patientID = -1;
    public int gameID;
    public string appName;
    public int therapistID = -1;
    public int sampleID = -1;
    public int executionID = -1;
    public bool socketIsReady = false;
    public bool operationDone = false;

    public jsonDataValidation jsonDataValidation;
    public bool validationDone = false;
    public jsonDataLevels jsonDataLevels;
    public bool getLevelsDone = false;
    public int validationValue;
    public List<string> levelsList; 
    public int statusValue = -3;
    public jsonDataSentences jsonDataSentences;
    public int playSentences = 2;
    public bool getPlaySentencesDone = false;
    public jsonDataAware jsonDataAware;
    public int awareValue = -1;
    public bool getAwareValue = false;
    public void SetupClient(string url, int patientID, int gameId, string appName)
    {
        this.wsURL = url;
        this.patientID = patientID;
        this.gameID = gameId;
        this.appName = appName;
    }

    public void StartClient()
    {
        ws = new WebSocket(wsURL, null);
        jsonDataValidation jsonDataValidation = new jsonDataValidation();
        //jsonDataLevels jsonDataLevels = new jsonDataLevels();

        ws.OnOpen += (sender, e) => {
            //Debug.Log("ws open");
            ws.Send("{\"id\":\"" + patientID + "\",\"msg\":\"app\",\"value\":\"" + appName + "\"}");
            socketIsReady = true;
        }; 
        ws.OnClose += (sender, e) => {
            Debug.Log("ws close"); //, e.Reason);
            //ws.Send("{\"id\":\"666\",\"msg\":\"close\",\"value\":\"" + e.Reason + "\"}");
        };
        ws.OnError += (sender, e) => { 
            //Debug.Log("ws error", e.Message.toString()); 
        };
        ws.OnMessage += (sender, e) => {
            var msg = e.Data;

            if (msg == "{\"msg\":\"ping\"}")
            {
                ws.Send("{\"msg\":\"pong\"}");
            }
            else if (msg == "{\"msg\":\"status\"}")
            {
                string status = "{\"therapist\":" + therapistID + ",\"game\":\"" + gameID + "\"}";
                Debug.Log("STATUS " + status);
                PrepareMessage("status", status);
            }
            
            else if(msg.Contains("action"))
            {
                Debug.Log("ACTION " + msg);
                jsonDataValidation = JsonUtility.FromJson<jsonDataValidation>(msg);
                if(jsonDataValidation.value.Contains(":"))
                {
                    //aleatory nunmbem just needed to be bigget then -3 and not -2 or -1
                    validationValue = 9999;
                    statusValue = 1;
                    validationDone = true;
                }
                else
                {
                    validationValue = int.Parse(jsonDataValidation.value);
                    statusValue = int.Parse(jsonDataValidation.value);
                    validationDone = true;
                }
                
            }
            else if(msg.Contains("levels"))
            {
                Debug.Log("LEVELS " + msg);
                jsonDataLevels = JsonUtility.FromJson<jsonDataLevels>(msg);
                levelsList = jsonDataLevels.value;
                getLevelsDone = true;    
            }
            else if(msg.Contains("nextlevel"))
            {
                Debug.Log("PLAYSENTENCES " + msg);
                jsonDataSentences = JsonUtility.FromJson<jsonDataSentences>(msg);
                playSentences = int.Parse(jsonDataSentences.value);
                getPlaySentencesDone = true;
            }
            else if(msg.Contains("aware"))
            {
                Debug.Log("AWARE " + msg);
                jsonDataAware = JsonUtility.FromJson<jsonDataAware>(msg);
                awareValue = int.Parse(jsonDataAware.value);
                getAwareValue = true;
            }
            else
            {
                Debug.Log("MSG " + msg);
            }
        };

        ws.ConnectAsync();
    }

    public void StopClient(string payload)
    {
        PrepareMessage("close", payload);
        ws.CloseAsync();
    }
    public bool IsClientAvailable()
    {
        return ws.IsAlive;
    }
    
    public void PrepareMessage(string msg, string value)
    {
        try
        {
            Debug.Log("PepareMessage");
            ws.Send("{\"id\":" + patientID + ",\"msg\":\"" + msg + "\",\"value\":" + value + "}");
        }
        catch (Exception ex) 
        {
            Debug.Log("PepareMessage FAIL" + ex.ToString());
            Debug.Log("PATIENT id " + patientID);
            Debug.Log("MSG " + msg);
            Debug.Log("VALUE " + value);
        }


    }

    public void LevelsToPlayRequest(int therapistID)
    {
        string request = "{\"therapist\":\"" + therapistID + "\",\"levels\":\"" + gameID + "\"}";
        PrepareMessage("request", request);
    }

     public void HelpRequest(int therapistID)
    {
        string request = "{\"therapist\":\"" + therapistID + "\",\"cancel\":\"" + 1 + "\"}";
        PrepareMessage("request", request);
    }

    public void ActionClassificationRequest(int therapistID, int wordID, int sampleID)
    {
        string request = "{\"therapist\":" + therapistID + ",\"word\":" + wordID + ",\"sample\":" + sampleID + ",\"errors\":" + 1 + "}";
        PrepareMessage("request", request);
    }

    //public void ActionClassificationGeralRequest(int therapistID, int wordID, int sampleID)
    //{
        //string request = "{\"therapist\":" + therapistID + ",\"word\":" + wordID + ",\"sample\":" + sampleID +  ",\"errors\":" + 0 + "}";
        //PrepareMessage("request", request);
    //}

    public void PlaySentencesRequest(int therapistID)
    {
        string request = "{\"therapist\":\"" + therapistID + "\",\"sentences\":\"" + 1 + "\"}";
        PrepareMessage("request", request);
    }

    public void VerifyTherapistActivity(int therapistID)
    {
        string request = "{\"therapist\":\"" + therapistID + "\",\"aware\":\"" + 1 + "\"}";
        PrepareMessage("request", request);
    }

    public void ActionClassificationGeralRequest(int therapistID, int wordID, int sampleID)
    {
        string request = "{\"therapist\":" + therapistID + ",\"word\":" + wordID + ",\"sample\":" + sampleID +  ",\"multiple\":" + 1 + "}";
        PrepareMessage("request", request);
    }
}
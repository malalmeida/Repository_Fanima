using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

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
    public int userID;
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
    
    public void SetupClient(string url, int userId, int gameId, string appName)
    {
        this.wsURL = url;
        this.userID = userId;
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
            ws.Send("{\"id\":\"" + userID + "\",\"msg\":\"app\",\"value\":\"" + appName + "\"}");
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
                //string status = "{\"game\":\"" + gameID + "\"}";                
                //PrepareMessage("status", status);
                string status = "{\"therapist\":" + therapistID + ",\"game\":\"" + gameID + "\"}";
                Debug.Log("STATUS " + status);
                PrepareMessage("status", status);
            }
            
            else if(msg.Contains("action"))
            {
                Debug.Log("ACTION " + msg);
                jsonDataValidation = JsonUtility.FromJson<jsonDataValidation>(msg);
                validationValue = int.Parse(jsonDataValidation.value);
                validationDone = true;
            }
            else if(msg.Contains("levels"))
            {
                Debug.Log("LEVELS " + msg);
                jsonDataLevels = JsonUtility.FromJson<jsonDataLevels>(msg);
                levelsList = jsonDataLevels.value;
                getLevelsDone = true;         
            }
            else
            {
                Debug.Log("MSG " + msg);
            }
        };

        ws.ConnectAsync();
    }

    public void StopClient()
    {
        PrepareMessage("close", "1");
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
            ws.Send("{\"id\":" + userID + ",\"msg\":\"" + msg + "\",\"value\":" + value + "}");
        }
        catch (Exception) { }
    }

    public void LevelsToPlayRequest(int therapistID)
    {
        string request = "{\"therapist\":\"" + therapistID + "\",\"levels\":\"" + gameID + "\"}";
        PrepareMessage("request", request);
    }

    public void ActionClassificationRequest(int therapistID, int wordID, int sampleID)
    {
        string request = "{\"therapist\":" + therapistID + ",\"word\":" + wordID + ",\"sample\":" + sampleID + ",\"errors\":" + 1 + "}";
        PrepareMessage("request", request);
    }

    public void ActionClassificationGeralRequest(int therapistID, int wordID, int sampleID)
    {
        string request = "{\"therapist\":" + therapistID + ",\"word\":" + wordID + ",\"sample\":" + sampleID +  ",\"errors\":" + 0 + "}";
        PrepareMessage("request", request);
    }

}
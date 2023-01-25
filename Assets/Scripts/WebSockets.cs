using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

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
   
    void Start()
    {

    }
    
    void Update()
    {
       
    }  
    
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
            if (msg == "{\"msg\":\"status\"}")
            {
                string status = "{\"game\":\"" + gameID + "\"}";
                PrepareMessage("status", status);
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

    public void GetLevelsToPlay(int therapistID, int gameExecutionID)
    {
        string request = "{\"therapist\":" + therapistID + ",\"levels\":" + gameExecutionID + "}";
        PrepareMessage("request", request);
    }

    public void GetActionEvaluation(int therapistID, int sampleID)
    {
        string request = "{\"therapist\":" + therapistID + ",\"sample\":" + sampleID + "}";
        PrepareMessage("request", request);
    }
}
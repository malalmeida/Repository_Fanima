using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class WebSockets : MonoBehaviour{

    private WebSocket ws;
    readonly string wsURL = "ws://193.137.46.11/";
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
    private int userID;
    private int gameID;
    private int therapistID;
    private string appName;
    private bool socketIsReady = false;
    private bool operationDone = false;

    void Start()
    {
        therapistID = 51;
        userID = 52;
        gameID = 29;
        appName = "Fanima";
       
        StartClient();
         
    }
    
    void Update()
    {
        if (ws == null)
        {
            Debug.Log("WS is null");
            //_ws.ConnectAsync();
        }
        if (socketIsReady)
        {
            if (!operationDone)
            {
                string levelsRequest = "{\"therapist\":\"" + therapistID + "\",\"levels\":\"" + gameID + "\"}";
                Debug.Log("Send WS levels request: " + levelsRequest);
                PrepareMessage("request", levelsRequest);

                string classificationRequest = "{\"therapist\":\"" + therapistID + "\",\"sample\":\"" + gameID + "\"}";
                Debug.Log("Send WS classification request: " + classificationRequest);
                PrepareMessage("request", classificationRequest);

                operationDone = true;
            }
        }   
    }  

    public void StartClient()
    {
        ws = new WebSocket(wsURL, null);

        ws.OnOpen += (sender, e) => {
            Debug.Log("ws open");
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
                string status = "{\"game\":" + gameID + "}";
                //string request = "{\"therapist\":" + 51 + ",\"levels\":" + 51 + "}";

                //ws.Send("{\"id\":\"" + userID + "\",\"msg\":\"status\",\"value\":" + status + "}");
                PrepareMessage("status",status);

                //PrepareMessage("request",request);


                //ws.Send("{\"id\":\"" + userID + "\",\"msg\":\"request\",\"value\":" + request + "}");
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
            string teste = "{\"id\":" + 52 + ",\"msg\":\"" + msg + "\",\"value\":" + value + "}";
            Debug.Log("TESTEANTES" + teste);
            ws.Send(teste);
            //ws.Send("{\"id\":\"" + 52 + "\",\"msg\":\"request\",\"value\":" + value + "}");

            
            Debug.Log("TESTEDEPOIS" + teste);

        }
        catch (Exception) { }
    }

    #region Game Messages
    public  void GetLevelsToPlay(int therapistID, int gameExecutionID)
    {
        string request = "{\"therapist\":" + 51 + ",\"levels\":" + 0 + "}";
        PrepareMessage("request", request);
    }

    //public void LevelSelection(int therapistID, int gameExecutionID) {
       //PrepareMessage("request", "{\"therapist\":" + therapistID + ",\"levels\":" + gameExecutionID + "}");

       //string request = "{\"therapist\":" + 51 + ",\"levels\":" + 0 + "}";
       //PrepareMessage("request", request);
       //return;
    //}

    //public void ActionClassification(int therapistID, int sampleID) {
    public void GetActionEvaluation(int therapistID, int sampleID)
    {
        string request = "{\"therapist\":" + 51 + ",\"sample\":" + sampleID + "}";
        PrepareMessage("request", request);
       //"{\"therapist\":" + therapistID + ",\"level\":" + sampleID + "}");
    }

    #endregion
}
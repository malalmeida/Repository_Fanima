using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using System;

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
    private int userId;
    private int gameId;
    private int therapistID;
    private string appName;


    void Start()
    {
        therapistID = 51;
        userId = 52;
        gameId = 29;
        appName = "Fanima";
       
        StartClient();
         
    }
    
    void Update()
    {
            
    }  

    public void StartClient()
    {
        ws = new WebSocket(wsURL, null);

        ws.OnOpen += (sender, e) => {
            //Debug.Log("ws open");
            ws.Send("{\"id\":\"" + userId + "\",\"msg\":\"app\",\"value\":\"" + appName + "\"}");

        }; 
        ws.OnClose += (sender, e) => {
            //Debug.Log("ws close", e.Reason);
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
                string status = "{\"game\":\"" + gameId + "\"}";
                ws.Send("{\"id\":\"" + userId + "\",\"msg\":\"status\",\"value\":" + status + "}");
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
            ws.Send("{\"id\":\"" + userId + "\",\"msg\":\"" + msg + "\",\"value\":\"" + value + "\"}");
        }
        catch (Exception) { }
    }

    #region Game Callbacks
    //
    public void StartCallback(int game, string status, int order, int level, int sequence, int action, float percentage, int time) {
        PrepareMessage("status", "{\"game\": game, \"status\": status, \"order\": order, \"level\": level, \"sequence\": sequence, \"action\": action, \"percent\": percentage, \"time\": time}");
        return;
    }

    public void NextStartCallback(int game, string status, int order, int level, int sequence, int action, float percentage, int time) {
        //let r = { 'status':data.status, 'score':1, 'gameactionid':ga.id,'gameexecutionid':ge.id, 'start':d1,'end':d2}
        //gapi.sendResult(r)
        return; 
    }

    public void NextEndCallback(int game, string status, int order, int level, int sequence, int action, float percentage, int time) {
        PrepareMessage("status", "{\"game\": game, \"status\": status, \"order\": order, \"level\": level, \"sequence\": sequence, \"action\": action, \"percent\": percentage, \"time\": time}");
        return;
    }

    public void NextLevelCallback(int game, string status, int order, int level, int sequence, int action, float percentage, int time) {
        PrepareMessage("status", "{\"game\": game, \"status\": status, \"order\": order, \"level\": level, \"sequence\": sequence, \"action\": action, \"percent\": percentage, \"time\": time}");
        return;
    }

    public void NextSequenceCallback(int game, string status, int order, int level, int sequence, int action, float percentage, int time) {
        PrepareMessage("status", "{\"game\": game, \"status\": status, \"order\": order, \"level\": level, \"sequence\": sequence, \"action\": action, \"percent\": percentage, \"time\": time}");
        return;
    }

    public void EndCallback(int game, string status, int order, int level, int sequence, int action, float percentage, int time) {
        PrepareMessage("status", "{\"game\": game, \"status\": status, \"order\": order, \"level\": level, \"sequence\": sequence, \"action\": action, \"percent\": percentage, \"time\": time}");
        return;
    }

    public void GameCycleCallback(int game, string status, int order, int level, int sequence, int action, float percentage, int time) {
        PrepareMessage("status", "{\"game\": game, \"status\": status, \"order\": order, \"level\": level, \"sequence\": sequence, \"action\": action, \"percent\": percentage, \"time\": time}");
        return; 
    }

    public void LevelsSelection(int therapistID, int gameExecutionID) {
       PrepareMessage("status", "{\"therapistID\": therapist, \"level\": gameExecutionID}");
       return;
    }

    public void ActionClassification(int therapistID, int sampleID) {
       PrepareMessage("status", "{\"therapistID\": therapist, \"sample\": sampleID}");
       return;
    }

    #endregion
}
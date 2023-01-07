using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using System;

public class WebSockets : MonoBehaviour{

    WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://193.137.46.11/");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);

            if(e.Data == "{\"msg\":\"ping\"}")
            {
                ws.Send("{\"id\":52,\"msg\":\"pong\",\"value\":\"fanima\"}");
            }
        };
         
    }
    
    void Update()
    {
        if(ws == null)
        {
            return;
        }      
    }  
}
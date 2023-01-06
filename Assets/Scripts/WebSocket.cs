//using WebSocketSharp;
using UnityEngine;

public class WebSocket : MonoBehaviour{

    //WebSocket ws;
    readonly string baseURL = "http://193.137.46.11/api/";

    void Start()
    {
       /* ws = new WebSocket(baseURL);
        ws.OnMenssage += (sender, e) =>
        {
            Debug.Log("Menssage received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
        ws.Connect();
        */
    }

    void Update()
    {
       /* if(ws == null)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hello");
        }
        */
   }
}
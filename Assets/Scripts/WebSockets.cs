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
    public int repeatValue = 2;
    public bool stop = false;
    public bool restoreDone = false;
    public jsonDataRestore jsonDataRestore;
    public int restoreLevelId = -1;
    public int restoreGameExecutionID = -1;
    public bool continueGame = false;
    public bool lvl1Selected = false;
    public bool lvl2Selected = false;
    public bool lvl3Selected = false;
    public List<int> chapterOneWordIDList;
    public List<int> chapterTwoWordIDList;
    public List<int> chapterThreeWordIDList;
    public bool playAllChapter1 = false;
    public bool playAllChapter2 = false;
    public bool playAllChapter3 = false;
    public List<string> actionsChapter1List;
    public List<string> actionsChapter2List;
    public List<string> actionsChapter3List;
    public List<string> actionsChapterEx1List;
    public List<string> actionsChapterEx2List;
    public List<string> actionsChapterEx3List;
    public bool endGame = false;
    public jsonDataEndGame jsonDataEndGame;
    public int actionIdStop = -1;

    //Mayra
    public jsonDataRequestAutoHelp jsonDataRequestAutoHelp;
    public bool activateAutoHelp = true;
    //the 3 variables below are not used yet.
    public jsonDataSkipBonusChapter jsonDataSkipBonusChapter;
    public int skipBonusChapter; //give an int value to this variable
    public bool getSkipBonusChapterDone = false;

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
            if (msg != "{\"msg\":\"ping\"}")
            {
                Debug.Log("RECEBEU MSG " + msg);
            }

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
             else if(msg.Contains("restore"))
            {
                Debug.Log("RESTORE " + msg);
                jsonDataRestore = JsonUtility.FromJson<jsonDataRestore>(msg);
                Debug.Log("RESTORE value " + jsonDataRestore.value.gameexecutionid);
                if(jsonDataRestore.value.gameexecutionid > 0)
                {
                    continueGame = true;
                    restoreGameExecutionID = jsonDataRestore.value.gameexecutionid;
                    levelsList = jsonDataRestore.value.levels;
                    restoreLevelId = jsonDataRestore.value.levelid;
                    actionsChapter1List = jsonDataRestore.value.actions1;
                    actionsChapter2List = jsonDataRestore.value.actions2;
                    actionsChapter3List = jsonDataRestore.value.actions3;
                    actionsChapterEx1List = jsonDataRestore.value.extra1;
                    actionsChapterEx2List = jsonDataRestore.value.extra2;
                    actionsChapterEx3List = jsonDataRestore.value.extra3;
                    actionIdStop = jsonDataRestore.value.actionid;
                    for (int i = 0; i < levelsList.Count; i++)
                    {
                        if(levelsList[i].Contains("1"))
                        { 
                            //if(restoreLevelId != 1)
                            //{
                            Debug.Log("LVL 1 SELECIONADO!");
                            lvl1Selected = true;
                            //} 
                        }
                        else if(levelsList[i].Contains("2"))
                        {
                            //if(restoreLevelId != 2)
                            //{
                            Debug.Log("LVL 2 SELECIONADO!");
                            lvl2Selected = true;
                            //}
                        }
                        else if(levelsList[i].Contains("3"))
                        {
                            //if(restoreLevelId != 3)
                            //{
                            Debug.Log("LVL 3 SELECIONADO!");
                            lvl3Selected = true;
                            //}
                        }
                    }
                }
                restoreDone = true;
            }
            else if(msg.Contains("levels"))
            {
                Debug.Log("LEVELS " + msg);
                jsonDataLevels = JsonUtility.FromJson<jsonDataLevels>(msg);
                actionsChapter1List = jsonDataLevels.value.actions1;
                actionsChapter2List = jsonDataLevels.value.actions2;
                actionsChapter3List = jsonDataLevels.value.actions3;
                levelsList = jsonDataLevels.value.levels;
                for (int i = 0; i < levelsList.Count; i++)
                {
                    if(levelsList[i].Contains("1"))
                    {
                        Debug.Log("LVL 1 SELECIONADO!");
                        lvl1Selected = true;
                    }
                    else if(levelsList[i].Contains("2"))
                    {
                        Debug.Log("LVL 2 SELECIONADO!");
                        lvl2Selected = true;
                    }
                    else if(levelsList[i].Contains("3"))
                    {
                        Debug.Log("LVL 3 SELECIONADO!");
                        lvl3Selected = true;
                    }
                }
                getLevelsDone = true;    
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
            else if(msg.Contains("stop"))
            {
                Debug.Log("STOP " + msg);
                stop = true;
            }
            else if(msg.Contains("end"))
            {
                Debug.Log("END " + msg);
                endGame = true;
            }
            else if (msg.Contains("request")) //Mayra: this is for hiding and showing the auto help in the game
            {
                Debug.Log("REQUEST " + msg);
                jsonDataRequestAutoHelp = JsonUtility.FromJson<jsonDataRequestAutoHelp>(msg);
                int autoHelpData = jsonDataRequestAutoHelp.value.data;

                if (autoHelpData == 0) // hide and deactivate auto help
                {
                    Debug.Log("HIDE AUTO HELP, DATA " + autoHelpData);
                    activateAutoHelp = false;
                }
                else // show and activate auto help
                {
                    Debug.Log("SHOW AUTO HELP, DATA " + autoHelpData);
                    activateAutoHelp = true;
                }
            }
            /* else if (msg.Contains("skip"))
             {
                 Debug.Log("Skip Bonus Chapter " + msg);
                 jsonDataSkipBonusChapter = JsonUtility.FromJson<jsonDataSkipBonusChapter>(msg);
                 skipBonusChapter = int.Parse(jsonDataSkipBonusChapter.value);
                 getSkipBonusChapterDone = true;
             }*/
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
            Debug.Log("PrepareMessage: MGS: " + msg + " VALUE: " + value);
            ws.Send("{\"id\":" + patientID + ",\"msg\":\"" + msg + "\",\"value\":" + value + "}");
        }
        catch (Exception ex) 
        {
            Debug.Log("PrepareMessage FAIL" + ex.ToString());
            Debug.Log("PATIENT id " + patientID);
            Debug.Log("MSG " + msg);
            Debug.Log("VALUE " + value);
        }
    }

    public void RestoreRequest(int therapistID)
    {
        string request = "{\"therapist\":\"" + therapistID + "\",\"patient\":\"" + patientID + "\",\"game\":\"" + gameID + "\",\"restore\":\"" + 1 + "\"}";
        //Debug.Log("TID: " + therapistID + " PID: " + patientID + " GID: " + gameID);
        PrepareMessage("request", request);
    }

    public void RequestPlayAgain(int therapistID)
    {
        string request = "{\"therapist\":\"" + therapistID + "\",\"patient\":\"" + patientID + "\",\"levels\":\"" + gameID + "\",\"more\":\"" + gameID + "\"}";
        PrepareMessage("request", request);
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
        string request = "{\"therapist\":" + therapistID + ",\"word\":" + wordID + ",\"sample\":" + sampleID + ",\"errors\":" + 1 + ",\"multiple\":" + 1 + "}";
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

   /* public void PlayBonusChapterRequest(int therapistID)
    {
       // string request = "{\"therapist\":\"" + therapistID + "\",\"skipBonus\":\"" + 1 + "\"}";
       // PrepareMessage("request", request);
    }*/

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
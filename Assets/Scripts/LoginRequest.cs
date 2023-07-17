using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.Windows;

public class LoginRequest : MonoBehaviour
{
   readonly string baseURL = "http://193.137.46.11/api/";
   int therapistID;
   int patientID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator PostLoginRequest(string user, string pass)
    {
        var url = baseURL + "login";

        List<IMultipartFormSection> parameters = new List<IMultipartFormSection>();
        parameters.Add(new MultipartFormDataSection("username", user));
        parameters.Add(new MultipartFormDataSection("password", pass));

        UnityWebRequest www = UnityWebRequest.Post(url, parameters);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR POST SAMPLE: " + www.error + " END");
        }
        else {
            //Debug.Log("ANSWER POST SAMPLE: " + www.downloadHandler.text + " END");
            Debug.Log("LOGIN DONE! ANSWER: " + www.downloadHandler.text + " END");

            PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(www.downloadHandler.text);

            if(playerInfo.content[0] == "wrong data")
            {
                Debug.Log("Wrong login info!");
            }
            else
            {
                PlayerPrefs.SetString("PLAYERID", playerInfo.content[0]);
                PlayerPrefs.SetString("PLAYERNAME", playerInfo.content[1]);
                PlayerPrefs.SetString("TOKEN", playerInfo.content[3]);

                patientID = Int32.Parse(PlayerPrefs.GetString("PLAYERID"));
                PlayerPrefs.SetInt("PATIENTID", patientID);
                therapistID = Int32.Parse(playerInfo.content[4]);
                PlayerPrefs.SetInt("THERAPISTID", therapistID);

                SceneManager.LoadScene("Octopus");
            }     
        }

    }

    public IEnumerator PostLogoutRequest()
    {
        var url = baseURL + "logout";

        string token = PlayerPrefs.GetString("TOKEN", "ERROR");

        PlayerPrefs.DeleteKey("TOKEN");
        PlayerPrefs.DeleteKey("PLAYERID");
        PlayerPrefs.DeleteKey("PLAYERNAME");

        PlayerPrefs.Save();

        UnityWebRequest www = UnityWebRequest.Post(url, "");

        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ERROR POST SAMPLE: " + www.error + " END");
            SceneManager.LoadScene("Login");
        }
        else {
            Debug.Log("ANSWER POST SAMPLE: " + www.downloadHandler.text + " END");
            SceneManager.LoadScene("Login");
        }

    }
}

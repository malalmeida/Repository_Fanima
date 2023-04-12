using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    public LoginRequest loginRequest;
    //public TMP_InputField userInput;
    //public TMP_InputField passInput;
  

    public void ConfirmLogin()
    {
        //string user = userInput.text;
        //string pass = passInput.text;

        //const string user = "speech1@play.pt";
        //const string pass = "123";
        const string user = "cresce1@play.pt";
        const string pass = "123";

        StartCoroutine(loginRequest.PostLoginRequest(user, pass));  
    }

    public void Quit()
    {
        Application.Quit();       
    }

   
}

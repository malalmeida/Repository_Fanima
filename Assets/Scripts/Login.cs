using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    public WebRequests request;
    //public TMP_InputField userInput;
    //public TMP_InputField passInput;

    public void ConfirmLogin()
    {
        //string user = userInput.text;
        //string pass = passInput.text;

        const string user = "paciente da ines";
        const string pass = "123";

        StartCoroutine(request.PostLoginRequest(user, pass));  
    }

    public void Quit()
    {
        Application.Quit();       
    }

   
}

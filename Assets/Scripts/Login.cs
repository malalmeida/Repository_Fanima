using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Login : MonoBehaviour
{
    public LoginRequest loginRequest;
    public TMP_InputField userInput;
    public TMP_InputField passInput;
    public AudioSource buttonConfirm;
    public AudioSource buttonEnd;

    public void ConfirmLogin()
    {
        string user = userInput.text;
        string pass = passInput.text;

        buttonConfirm.Play();
        StartCoroutine(loginRequest.PostLoginRequest(user, pass));  
    }

  

    public void Quit()
    {
        buttonEnd.Play();
        Application.Quit();       
    }
}

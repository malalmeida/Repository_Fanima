using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public LoginRequest loginRequest;
    public TMP_InputField userInput;
    public TMP_InputField passInput;
    public AudioSource buttonConfirm;
    public AudioSource buttonEnd;
    public GameObject characterSelection;

    public void Start()
    {
        characterSelection.SetActive(false);
       
    }

    public void ConfirmLogin()
    {
        string user = userInput.text;
        string pass = passInput.text;

        buttonConfirm.Play();

        characterSelection.SetActive(true);

        //wait for characterGuide to be choosen
        StartCoroutine(ChooseCharacter(user, pass));
        
        
        /* relocate it to ChooseCharacter(), otherwise the new Coroutine would start before
         * character selection
         */
        //StartCoroutine(loginRequest.PostLoginRequest(user, pass));
    }

    public IEnumerator ChooseCharacter(string user, string pass)
    {
        yield return new WaitUntil(() => DataManager.instance.characterChoosen);
        //SceneManager.LoadScene("Chameleon"); used to test if sprite changes

        StartCoroutine(loginRequest.PostLoginRequest(user, pass));
    }


    public void Quit()
    {
        buttonEnd.Play();
        Application.Quit();       
    }
}

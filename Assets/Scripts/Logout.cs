using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logout : MonoBehaviour
{
    public LoginRequest request;

     public void EndSession()
    {
        StartCoroutine(request.PostLogoutRequest());  
    }
}

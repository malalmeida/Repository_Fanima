using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logout : MonoBehaviour
{
    public WebRequests request;

     public void EndSession()
    {
        StartCoroutine(request.PostLogoutRequest());  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public bool helpAllowed = true;
    public bool musicAllowed = true;
    //public GameObject backgroundMusic;
    public Sprite maleGuide;
    public bool characterChoosen;
    //public bool femaleChoosen;
    //public Sprite femaleGuide;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            
        }

    }

    
}

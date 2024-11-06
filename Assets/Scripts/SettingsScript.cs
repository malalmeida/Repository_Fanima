using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public GameObject settingsMenu;
    public AudioSource song;
    public GameObject musicStop;
    public GameObject musicStart;

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false) ;
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void StopBackgroundMusic()
    {
        musicStop.SetActive(false);
        musicStart.SetActive(true);
        song.Stop();
        DataManager.instance.musicAllowed = false;
    }

    public void StartBackgroundMusic()
    {
        musicStop.SetActive(true);
        musicStart.SetActive(false);
        song.Play();
        DataManager.instance.musicAllowed = true;
    }
}

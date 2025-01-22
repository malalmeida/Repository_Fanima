using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    [Header("Character guides")]
    public Image character;
    //public Image femaleGuide;

    [Header("Options of character guides")]
    public List<Sprite> characterOptions = new List<Sprite>();
   // public List<Sprite> femaleGuideOptions = new List<Sprite>();

    public int currentCharacter = 0;

    public Login login;
    
    public void Start()
    {
        DataManager.instance.characterGuide = character.sprite;
        DataManager.instance.guideChoosen = false;
       // DataManager.instance.femaleChoosen = false;

    }

    public void NextCharacter()
    {
        currentCharacter++;

        if(currentCharacter >= characterOptions.Count) {
            currentCharacter = 0;
        }
        character.sprite = characterOptions[currentCharacter];
    }

    public void PreviousCharacter()
    {
        currentCharacter--;

        if (currentCharacter < 0)
        {
            currentCharacter = characterOptions.Count - 1;
        }
        character.sprite = characterOptions[currentCharacter];
    }

    public void AcceptCharacter()
    {
        DataManager.instance.characterGuide = character.sprite;
        login.characterSelection.SetActive(false);
        //SceneManager.LoadScene("1");
       
        DataManager.instance.guideChoosen = true;
        //DataManager.instance.femaleChoosen = true;


        //TODO add code to choose femaleGuide
        //TODO add PRACTISESCRIPT code in GameController, in the Start method
    }
}

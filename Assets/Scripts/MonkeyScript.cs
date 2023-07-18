using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyScript : MonoBehaviour
{
    public int monkeyNumber = -1;
    public int randomIndex = -1;
    public GameObject monkey1;
    public GameObject monkey2;
    public GameObject monkey3;
    public GameObject monkey4;
    public GameObject monkey5;
    public GameObject monkey6;
    public GameObject monkey7;
    public GameObject monkey8;
    public GameObject monkey9;
    public List<GameObject> monkeyList;
    public bool canShow = false;
    public bool isCaught = false;
    public GameObject currentObj;

    public bool canShowImage = false;
    public string currentWord = "XXXX";
    public int repNumber = -1;
    public bool nextAction = false;

    public bool newPhonemeGroup = false;
    public bool newWord = false;
    public GameObject star1GO;
    public GameObject star2GO;
    public GameObject star3GO;

    public SpriteRenderer rend;
    public SpriteRenderer rendStar1;
    public SpriteRenderer rendStar2;
    public SpriteRenderer rendStar3;

    public AudioSource monkeySound;

    // Start is called before the first frame update
    void Start()
    {
        monkeyList = new List<GameObject>();
        monkeyList.Add(monkey1);
        monkeyList.Add(monkey2);
        monkeyList.Add(monkey3);
        monkeyList.Add(monkey4);
        monkeyList.Add(monkey5);
        monkeyList.Add(monkey6);
        monkeyList.Add(monkey7);
        monkeyList.Add(monkey8);
        monkeyList.Add(monkey9);

        monkey1.SetActive(false);
        monkey2.SetActive(false);
        monkey3.SetActive(false);
        monkey4.SetActive(false);
        monkey5.SetActive(false);
        monkey6.SetActive(false);
        monkey7.SetActive(false);
        monkey8.SetActive(false);
        monkey9.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(randomIndex > -1)
        {
            ShowMonkey();
        }

        if(nextAction)
        {
            ShowStart();
        }

        if(newWord)
        {
            ResartStars();
            HidePreviousImage();  
        }

        if(canShowImage)
        {
           ShowObj();
        }

         if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                     if(hit.collider.CompareTag("Monkey1"))
                    {
                        monkey1.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();                    
                    } 
                    if(hit.collider.CompareTag("Monkey2"))
                    {
                        monkey2.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();                    
                    } 
                    if(hit.collider.CompareTag("Monkey3"))
                    {
                        monkey3.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();                    
                    } 
                    if(hit.collider.CompareTag("Monkey4"))
                    {
                        monkey4.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey5"))
                    {
                        monkey5.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey6"))
                    {
                        monkey6.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey7"))
                    {
                        monkey7.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey8"))
                    {
                        monkey8.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey9"))
                    {
                        monkey9.SetActive(false);
                        isCaught = true;
                        monkeySound.Play();
                    } 
                }
            }
        }
    }

    public void ShowObj()
    {
        if(currentWord != "XXXX")
        {
            string gameObjName = currentWord + "Obj";       
            Debug.Log("OBJ " + gameObjName);

            currentObj = GameObject.Find(gameObjName);

            rend = currentObj.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 10;

            canShowImage = false;
            isCaught = false;
        }
    }

    public void HidePreviousImage()
    {
        if(currentObj != null)
        {
            currentObj.SetActive(false);
        }
        
        nextAction = false;
    } 

    public void ShowStart()
    {   
        if(repNumber == 0)
        {
            rendStar1.sortingOrder = 10;
        }
        else if(repNumber == 1)
        {
            rendStar2.sortingOrder = 10;
        }
        else if(repNumber == 2)
        {
            rendStar3.sortingOrder = 10;
        }
        nextAction = false;
    }

    public void ResartStars()
    {
        star1GO = GameObject.Find("star1");
        rendStar1 = star1GO.GetComponent<SpriteRenderer>();

        star2GO = GameObject.Find("star2");
        rendStar2 = star2GO.GetComponent<SpriteRenderer>();

        star3GO = GameObject.Find("star3");
        rendStar3 = star3GO.GetComponent<SpriteRenderer>();

        rendStar1.sortingOrder = -5;
        rendStar2.sortingOrder = -5;
        rendStar3.sortingOrder = -5;
        newWord = false;
    }

    public void ShowMonkey()
    {
        monkeyList[randomIndex].SetActive(true);
        randomIndex = -1;
        monkeySound.Play();
        
    }

}

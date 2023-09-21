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

    public SpriteRenderer rend;
    public SpriteRenderer rend1;
    public SpriteRenderer rend2;
    public SpriteRenderer rend3;

    public GameObject currentObj1;
    public GameObject currentObj2;
    public GameObject currentObj3;

    public bool hide1 = true;
    public bool hide2 = true;
    public bool hide3 = true;

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
            NextObj();
        }

        if(newWord)
        {
            //ResartStars();
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
                        //monkeySound.Play();                    
                    } 
                    if(hit.collider.CompareTag("Monkey2"))
                    {
                        monkey2.SetActive(false);
                        isCaught = true;
                        //monkeySound.Play();                    
                    } 
                    if(hit.collider.CompareTag("Monkey3"))
                    {
                        monkey3.SetActive(false);
                        isCaught = true;
                        //monkeySound.Play();                    
                    } 
                    if(hit.collider.CompareTag("Monkey4"))
                    {
                        monkey4.SetActive(false);
                        isCaught = true;
                        //monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey5"))
                    {
                        monkey5.SetActive(false);
                        isCaught = true;
                        //monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey6"))
                    {
                        monkey6.SetActive(false);
                        isCaught = true;
                        //monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey7"))
                    {
                        monkey7.SetActive(false);
                        isCaught = true;
                        //monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey8"))
                    {
                        monkey8.SetActive(false);
                        isCaught = true;
                        //monkeySound.Play();
                    } 
                    if(hit.collider.CompareTag("Monkey9"))
                    {
                        monkey9.SetActive(false);
                        isCaught = true;
                        //monkeySound.Play();
                    } 
                }
            }
        }
    }

    public void ShowObj()
    {
         if(currentWord != "XXXX")
        {
            string gameObjName1 = currentWord + "Obj1";  
            string gameObjName2 = currentWord + "Obj2";       
            string gameObjName3 = currentWord + "Obj3";       

            //Debug.Log("OBJ " + gameObjName);
            Debug.Log("OBJ " + gameObjName1);
            Debug.Log("OBJ " + gameObjName2);
            Debug.Log("OBJ " + gameObjName3);

            //currentObj = GameObject.Find(gameObjName);

            //rend = currentObj.GetComponent<SpriteRenderer>();
            //rend.sortingOrder = 10;

            currentObj1 = GameObject.Find(gameObjName1);
            currentObj2 = GameObject.Find(gameObjName2);
            currentObj3 = GameObject.Find(gameObjName3);

            rend1 = currentObj1.GetComponent<SpriteRenderer>();
            rend2 = currentObj2.GetComponent<SpriteRenderer>();
            rend3 = currentObj3.GetComponent<SpriteRenderer>();
            
            //colorBlack = rend3.color;
            
            //rend1.color = colored; 
                       
            rend1.sortingOrder = 10;
            rend2.sortingOrder = 10;
            rend3.sortingOrder = 10;
            
            if(repNumber == 0)
            {
                hide1 = false;
            }

            canShowImage = false;
            isCaught = false;

            newWord = false;
        }
    }

    public void HidePreviousImage()
    {
        if(currentObj1 != null && currentObj2 != null && currentObj3 != null)
        {
            currentObj1.SetActive(false);
            currentObj2.SetActive(false);
            currentObj3.SetActive(false);
            //currentObj.SetActive(false);
        }
        nextAction = false;
    } 

    public void NextObj()
    {   
        if(repNumber == 0)
        {
            Debug.Log("repNumber " + repNumber);
            //rend1.color = colorBlack; 
            //rend2.color = colored;
            hide1 = true;            
            hide2 = false;  
        }
        else if(repNumber == 1)
        {
            Debug.Log("repNumber " + repNumber);
            //rend2.color = colorBlack;
            //rend3.color = colored;
            hide2 = true;
            hide3 = false;
        }
        else if(repNumber == 2)
        {
            hide3 = true;
            Debug.Log("repNumber " + repNumber);
            HidePreviousImage();
        }
        nextAction = false;
    }
/**
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
**/
    public void ShowMonkey()
    {
        monkeyList[randomIndex].SetActive(true);
        randomIndex = -1;
        monkeySound.Play();
        
    }

}

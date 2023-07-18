using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChameleonScript : MonoBehaviour
{
    public int removedChameleons = 0;
    public int randomIndex = -1;
    public GameObject chameleon1;
    public GameObject chameleon2;
    public GameObject chameleon3;
    public GameObject chameleon4;
    public GameObject chameleon5;
    public GameObject chameleon6;
    public GameObject chameleon7;
    public GameObject chameleon8;
    public GameObject chameleon9;
    public GameObject chameleon10;
    public GameObject chameleon11;
    public GameObject chameleon12;
    public GameObject chameleon13;
    public GameObject chameleon14;
    public List<GameObject> chameleonList;
    public bool canShow = false;
    public bool isCaught = false;
    public GameObject currentObj;

    public bool hideObj = false;
    public bool canShowImage = false;
    public string currentWord = "";
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

    public GameObject currentChameleon;

    public AudioSource validationSound;
    public AudioSource chameleonSound;

    // Start is called before the first frame update
    void Start()
    {  
        chameleonList = new List<GameObject>();
        chameleonList.Add(chameleon1);
        chameleonList.Add(chameleon2);
        chameleonList.Add(chameleon3);
        chameleonList.Add(chameleon4);
        chameleonList.Add(chameleon5);
        chameleonList.Add(chameleon6);
        chameleonList.Add(chameleon7);
        chameleonList.Add(chameleon8);
        chameleonList.Add(chameleon9);
        chameleonList.Add(chameleon10);
        chameleonList.Add(chameleon11);
        chameleonList.Add(chameleon12);
        chameleonList.Add(chameleon13);
        chameleonList.Add(chameleon14);
        chameleon1.SetActive(false);
        chameleon2.SetActive(false);
        chameleon3.SetActive(false);
        chameleon4.SetActive(false);
        chameleon5.SetActive(false);
        chameleon6.SetActive(false);
        chameleon7.SetActive(false);
        chameleon8.SetActive(false);
        chameleon9.SetActive(false);
        chameleon10.SetActive(false);
        chameleon11.SetActive(false);
        chameleon12.SetActive(false);
        chameleon13.SetActive(false);
        chameleon14.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(randomIndex > -1)
        {
            ShowChameleon();
        }

        if(nextAction)
        {
            ShowStart();
            //HidePreviousImage();
        }

        if(newWord)
        {
            ResartStars();
            HidePreviousImage();
        }

        if(isCaught)
        {
            ShowObj();
        }

        //if(hideObj)
        //{
            //HidePreviousImage();
        //}
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                     if(hit.collider.CompareTag("Chameleon1"))
                    {
                        chameleon1.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon2"))
                    {
                        chameleon2.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon3"))
                    {
                        chameleon3.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon4"))
                    {
                        chameleon4.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon5"))
                    {
                        chameleon5.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon6"))
                    {
                        chameleon6.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon7"))
                    {
                        chameleon7.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon8"))
                    {
                        chameleon8.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon9"))
                    {
                        chameleon9.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon10"))
                    {
                        chameleon10.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon11"))
                    {
                        chameleon11.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon12"))
                    {
                        chameleon12.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon13"))
                    {
                        chameleon13.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    }
                    if(hit.collider.CompareTag("Chameleon14"))
                    {
                        chameleon14.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        chameleonSound.Play();
                    }      
                }
            }
        }
    }

    public void ShowObj()
    {
        string gameObjName = currentWord + "Obj";       
        Debug.Log("OBJ " + gameObjName);

        currentObj = GameObject.Find(gameObjName);

        rend = currentObj.GetComponent<SpriteRenderer>();
        rend.sortingOrder = 10;

        canShowImage = false;
        isCaught = false;
    }

    public void HidePreviousImage()
    {
        if(currentObj != null)
        {
            //rend = currentObj.GetComponent<SpriteRenderer>();
            //rend.sortingOrder = -5;
            currentObj.SetActive(false);
        }
        
        nextAction = false;
        //hideObj = false;
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
        //StartCoroutine(PlayStarSound());
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

    public void ShowChameleon()
    {
        Debug.Log("CAMELEON NUMBER " + randomIndex);
        chameleonList[randomIndex].SetActive(true);
        randomIndex = -1;
    }
}


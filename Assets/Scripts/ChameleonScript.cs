using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public string currentWord = "XXXX";
    public int repNumber = -1;
    public bool nextAction = false;

    public bool newPhonemeGroup = false;
    public bool newWord = false;
    
    public GameObject currentObj1;
    public GameObject currentObj2;
    public GameObject currentObj3;

    public SpriteRenderer rend;
    public SpriteRenderer rend1;
    public SpriteRenderer rend2;
    public SpriteRenderer rend3;

    public bool hide1 = true;
    public bool hide2 = true;
    public bool hide3 = true;

    public GameObject currentChameleon;

    public AudioSource validationSound;
    public AudioSource chameleonSound;

    public Image barImage;
    //public int totalphonemesToPlay = -1;
    public float incrementAmount = 0.0f;

    public GameObject rewardBoard;
    public SpriteRenderer rendRewardBoard;

    public bool showReward = false;
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

        barImage.fillAmount = 0.0f;
        Debug.Log("totalphonemesToPlay: " + incrementAmount);
        
        /**
        if(totalphonemesToPlay >0)
        {
            if(totalphonemesToPlay == 1)
            {
                incrementAmount = 0.2f;
                Debug.Log("incrementAmount: " + incrementAmount);
            }
            else if(totalphonemesToPlay == 2)
            {
                incrementAmount = 0.1f;
                Debug.Log("incrementAmount: " + incrementAmount);
            }
            else if(totalphonemesToPlay == 3)
            {
                incrementAmount = 0.07f;
                Debug.Log("incrementAmount: " + incrementAmount);
            }
            else if(totalphonemesToPlay == 4)
            {
                incrementAmount = 0.05f;
                Debug.Log("incrementAmount: " + incrementAmount);
            }
            else if(totalphonemesToPlay == 5)
            {
                incrementAmount = 0.04f;
                Debug.Log("incrementAmount: " + incrementAmount);
            }
            else if(totalphonemesToPlay == 6)
            {
                incrementAmount = 0.033f;
                Debug.Log("incrementAmount: " + incrementAmount);
            } 
        }else
        {   
            Debug.Log("AINDA NÂO FOI DEFINIDO!");
        }
        **/
    }

    // Update is called once per frame
    void Update()
    {
        if(showReward)
        {
            ShowRewardBoard();
        }

        if(randomIndex > -1)
        {
            ShowChameleon();
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
                     if(hit.collider.CompareTag("Chameleon1"))
                    {
                        chameleon1.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon2"))
                    {
                        chameleon2.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon3"))
                    {
                        chameleon3.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon4"))
                    {
                        chameleon4.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();

                    } 
                    if(hit.collider.CompareTag("Chameleon5"))
                    {
                        chameleon5.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon6"))
                    {
                        chameleon6.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon7"))
                    {
                        chameleon7.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon8"))
                    {
                        chameleon8.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon9"))
                    {
                        chameleon9.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon10"))
                    {
                        chameleon10.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon11"))
                    {
                        chameleon11.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon12"))
                    {
                        chameleon12.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    } 
                    if(hit.collider.CompareTag("Chameleon13"))
                    {
                        chameleon13.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
                    }
                    if(hit.collider.CompareTag("Chameleon14"))
                    {
                        chameleon14.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                        barImage.fillAmount += incrementAmount;
                        //chameleonSound.Play();
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

/**
    public void ResartStars()
    {
        star1GO = GameObject.Find("star1");
        rendStar1 = star1GO.GetComponent<SpriteRenderer>();

        star2GO = GameObject.Find("star2");
        rendStar2 = star2GO.GetComponent<SpriteRenderer>();

        star3GO = GameObject.Find("star3");
        rendStar3 = star3GO.GetComponent<SpriteRenderer>();

        //rendStar1.sortingOrder = -5;
        //rendStar2.sortingOrder = -5;
        //rendStar3.sortingOrder = -5;
        newWord = false;
    }
**/
    public void ShowChameleon()
    {
        chameleonList[randomIndex].SetActive(true);
        randomIndex = -1;
        chameleonSound.Play();
    }

    public void ShowRewardBoard()
    {
        rendRewardBoard = rewardBoard.GetComponent<SpriteRenderer>();
        rendRewardBoard.sortingOrder = 20;
    }
}


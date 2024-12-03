using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class OwlScript : MonoBehaviour
{
    public SpriteRenderer rend;
    public GameObject currentObj; 
    public string currentWord = "";
    public bool canShowImage = false;

    public bool nextAction = false;
    public bool isMatch = false;

    public int randomIndex = -1;

    public bool pop = false;
    public bool startValidation = false;

    public AudioSource popSound;
    //public AudioSource validationSound;

    public string gameObjName;

    public Image barImage;
    public float incrementAmount = 0.08f;

    public GameObject rewardBoard;
    public SpriteRenderer rendRewardBoard;

    public bool showReward = false;

    // Start is called before the first frame update
    void Start()
    {
        barImage.fillAmount = 0.0f;
    }

    void Update()
    {
        if(showReward)
        {
            ShowRewardBoard();
        }
        
        if(canShowImage)
        {
            ShowObj();
        }

        if(nextAction)
        {
            HidePreviousImage();
        }
         
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null && !pop)
                {           
                    if(hit.collider.CompareTag("Chameleon1"))
                    {   
                        if(gameObjName == "focaObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon2"))
                    {   
                        if(gameObjName == "garfoObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon3"))
                    {   
                        if(gameObjName == "velaObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon4"))
                    {   
                        if(gameObjName == "livrosObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon5"))
                    {   
                        if(gameObjName == "maçãObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon6"))
                    {   
                        if(gameObjName == "zeroObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon7"))
                    {   
                        if(gameObjName == "casaObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon8"))
                    {   
                        if(gameObjName == "chaveObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon9"))
                    {   
                        if(gameObjName == "ganchoObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon10"))
                    {   
                        if(gameObjName == "queijoObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon11"))
                    {   
                        if(gameObjName == "cestoObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon12"))
                    {   
                        if(gameObjName == "janelaObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon13"))
                    {   
                        if(gameObjName == "cisneObj")
                        {
                            popSound.Play();
                            pop = true;
                            barImage.fillAmount += incrementAmount;
                        }   
                    }
                }
            }
        }
    }

    public void ShowObj()
    {
        //pop = false; //CHECK IF SHOULD UNCOMMENT
        gameObjName = currentWord + "Obj";       
        Debug.Log("OBJ " + gameObjName);

        currentObj = GameObject.Find(gameObjName);
        
        rend = currentObj.GetComponent<SpriteRenderer>();
        StartCoroutine(WaitForAmimation());
        rend.sortingOrder = 10;

        canShowImage = false;
        

    }

    IEnumerator WaitForAmimation()
    {
        yield return new WaitForSeconds(1.5f);
    }

    public void HidePreviousImage()
    {
        pop = false;
        //rend = currentObj.GetComponent<SpriteRenderer>();
        //rend.sortingOrder = -1;
        currentObj.SetActive(false);
        //validationSound.Play();
        nextAction = false;
    }

    public void ShowRewardBoard()
    {
        rendRewardBoard = rewardBoard.GetComponent<SpriteRenderer>();
        rendRewardBoard.sortingOrder = 20;
    }
}


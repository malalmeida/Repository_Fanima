using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
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
                if(hit.collider != null)
                {           
                    if(hit.collider.CompareTag("Chameleon1"))
                    {   
                        if(gameObjName == "focaObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon2"))
                    {   
                        if(gameObjName == "garfoObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon3"))
                    {   
                        if(gameObjName == "velaObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon4"))
                    {   
                        if(gameObjName == "livrosObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon5"))
                    {   
                        if(gameObjName == "maçãObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon6"))
                    {   
                        if(gameObjName == "zeroObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon7"))
                    {   
                        if(gameObjName == "casaObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon8"))
                    {   
                        if(gameObjName == "chaveObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon9"))
                    {   
                        if(gameObjName == "ganchoObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon10"))
                    {   
                        if(gameObjName == "queijoObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon11"))
                    {   
                        if(gameObjName == "cestoObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon12"))
                    {   
                        if(gameObjName == "janelaObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                    if(hit.collider.CompareTag("Chameleon13"))
                    {   
                        if(gameObjName == "cisneObj")
                        {
                            popSound.Play();
                            pop = true;
                        }   
                    }
                }
            }
        }
    }

    public void ShowObj()
    {
        pop = false;
        gameObjName = currentWord + "Obj";       
        Debug.Log("OBJ " + gameObjName);

        currentObj = GameObject.Find(gameObjName);
        
        rend = currentObj.GetComponent<SpriteRenderer>();
        rend.sortingOrder = 10;

        canShowImage = false;
        

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
}


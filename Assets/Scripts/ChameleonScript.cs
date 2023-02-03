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
    public List<GameObject> chameleonList;
    public bool canShow = false;
    public bool isCaught = false;

    public GameObject currentChameleon;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(randomIndex > -1)
        {
            WaitToShowChameleon();
        }
        

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    //Color newColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                    //hit.collider.GetComponent<SpriteRenderer>().material.color = newColor;
                    if(hit.collider.CompareTag("Chameleon1"))
                    {
                        chameleon1.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon2"))
                    {
                        chameleon2.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon3"))
                    {
                        chameleon3.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
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
                    } 
                    if(hit.collider.CompareTag("Chameleon6"))
                    {
                        chameleon6.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon7"))
                    {
                        chameleon7.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon8"))
                    {
                        chameleon8.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon9"))
                    {
                        chameleon9.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon10"))
                    {
                        chameleon10.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon11"))
                    {
                        chameleon11.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon12"))
                    {
                        chameleon12.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Chameleon13"))
                    {
                        chameleon13.SetActive(false);
                        removedChameleons ++;
                        isCaught = true;
                    }   
                }
            }
        }

        if(removedChameleons == 13)
        {   
            //SceneManager.LoadScene("Monkey");
        }
    }

    public void  WaitToShowChameleon()
    {
        Debug.Log("CAMELEON NUMBER " + randomIndex);
        chameleonList[randomIndex].SetActive(true);
        //currentChameleon = chameleonList[randomIndex];
        randomIndex = -1;
    }
}

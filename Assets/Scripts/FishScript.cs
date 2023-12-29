using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishScript : MonoBehaviour
{
   
    public int removedFishFoods = 0;
    /**
    public int foodNumber = 0;
    public GameObject food1;
    public GameObject food2;
    public GameObject food3;
    public GameObject food4;
    public GameObject food5;
    public GameObject food6;
    public GameObject food7;
    public GameObject food8;
    public GameObject food9;
    public GameObject food10;
    public GameObject food11;
    public GameObject food12;
    public GameObject food13;
    public List<GameObject> foodList;
    **/
    public bool isCaught = false;
    public bool canShowFood = false;
    public GameObject currentFood;

    public GameObject currentObject;
    public SpriteRenderer rend;

    public bool canShowImage = false;
    public string currentWord = "";
    public bool canShake = false;

    public AudioSource fishEating;
    public MoveObject fish;

    public Image barImage;
    public float incrementAmount = 0.083f;

    void Start()
    {
        barImage.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
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
                    if(hit.collider.CompareTag("Food1"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }
                    else if(hit.collider.CompareTag("Food2"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }
                    else if(hit.collider.CompareTag("Food3"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }
                    else if(hit.collider.CompareTag("Food4"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }               
                    else if(hit.collider.CompareTag("Food5"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    } 
                    else if(hit.collider.CompareTag("Food6"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }
                    else if(hit.collider.CompareTag("Food7"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    } 
                    else if(hit.collider.CompareTag("Food8"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }               
                    else if(hit.collider.CompareTag("Food9"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }
                    else if(hit.collider.CompareTag("Food10"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }
                    else if(hit.collider.CompareTag("Food11"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }
                    else if(hit.collider.CompareTag("Food12"))
                    {
                        barImage.fillAmount += incrementAmount;
                        MoveFish();
                    }    
                }
            }
        }
    }

    public void ShowObj()
    {

        string gameObjName = currentWord + "Obj";       
        Debug.Log("OBJ " + gameObjName);

        currentObject = GameObject.Find(gameObjName);

        rend = currentObject.GetComponent<SpriteRenderer>();
        rend.sortingOrder = 2;

        currentObject.SetActive(true);
        canShowImage = false;
    }

    IEnumerator WaitForAnimationDone()
    {
        fish.starAnimation = true;
        yield return new WaitUntil(() => fish.starAnimation);
        fish.animationDone = false;
        canShake = false;
        yield return new WaitUntil(() => fish.animationDone);
        HideObj();
    }

    public void HideObj()
    {
        fishEating.Play();
        rend = currentObject.GetComponent<SpriteRenderer>();
        rend.sortingOrder = -1;
        removedFishFoods ++;
        isCaught = true;
    }

    public void MoveFish()
    {
        if(canShake)
        {
            StartCoroutine(WaitForAnimationDone());
        }
    }
  }


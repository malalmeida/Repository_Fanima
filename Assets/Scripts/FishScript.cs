using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        /**
        foodList = new List<GameObject>();
        foodList.Add(food1);
        foodList.Add(food2);
        foodList.Add(food3);
        foodList.Add(food4);
        foodList.Add(food5);
        foodList.Add(food6);
        foodList.Add(food7);
        foodList.Add(food8);
        foodList.Add(food9);
        foodList.Add(food10);
        foodList.Add(food11);
        foodList.Add(food12);
        foodList.Add(food13);
        food1.SetActive(false);
        food2.SetActive(false);
        food3.SetActive(false);
        food4.SetActive(false);
        food5.SetActive(false);
        food6.SetActive(false);
        food7.SetActive(false);
        food8.SetActive(false);
        food9.SetActive(false);
        food10.SetActive(false);
        food11.SetActive(false);
        food12.SetActive(false);
        food13.SetActive(false);
        **/
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
                        IsShaking();
                    }
                    else if(hit.collider.CompareTag("Food2"))
                    {
                        IsShaking();
                    }
                    else if(hit.collider.CompareTag("Food3"))
                    {
                        IsShaking();
                    }
                    else if(hit.collider.CompareTag("Food4"))
                    {
                       IsShaking();
                    }               
                    else if(hit.collider.CompareTag("Food5"))
                    {
                        IsShaking();
                    } 
                    else if(hit.collider.CompareTag("Food6"))
                    {
                        IsShaking();
                    }
                    else if(hit.collider.CompareTag("Food7"))
                    {
                         IsShaking();
                    } 
                    else if(hit.collider.CompareTag("Food8"))
                    {
                         IsShaking();
                    }               
                    else if(hit.collider.CompareTag("Food9"))
                    {
                         IsShaking();
                    }
                    else if(hit.collider.CompareTag("Food10"))
                    {
                         IsShaking();
                    }
                    else if(hit.collider.CompareTag("Food11"))
                    {
                         IsShaking();
                    }
                    else if(hit.collider.CompareTag("Food12"))
                    {
                         IsShaking();
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
        yield return new WaitUntil(() => fish.starAnimation);
        fish.animationDone = false;
        //Debug.Log("STARTANIMATIONDONE " + fish.starAnimation);
        //Debug.Log("ANIMATIONDONE " + fish.animationDone);

        yield return new WaitUntil(() => fish.animationDone);
        HideObj();
        removedFishFoods ++;
        isCaught = true;
    }

    public void HideObj()
    {
        fishEating.Play();
        rend = currentObject.GetComponent<SpriteRenderer>();
        rend.sortingOrder = -1;
    }

    public void IsShaking()
    {
        if(canShake)
        {
            fish.starAnimation = true;
            StartCoroutine(WaitForAnimationDone());
        }
    }
}

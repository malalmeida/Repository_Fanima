using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public int removedFishFoods = 0;
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
    public bool isCaught = false;
    public bool canShowFood = false;
    public GameObject currentFood;

    public MoveObject fish;

    // Start is called before the first frame update
    void Start()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canShowFood)
        {
            ShowFood();
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
                        fish.starAnimation = true;
                        currentFood = food1;
                        StartCoroutine(WaitForAnimationDone());
                       // food1.SetActive(false);
                        //removedFishFoods ++;
                        //isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Food2"))
                    {
                        fish.starAnimation = true;
                        currentFood = food2;
                        StartCoroutine(WaitForAnimationDone());
                        
                    } 
                    if(hit.collider.CompareTag("Food3"))
                    {
                        fish.starAnimation = true;
                        currentFood = food3;
                        StartCoroutine(WaitForAnimationDone());
                        
                    } 
                    if(hit.collider.CompareTag("Food4"))
                    {
                        fish.starAnimation = true;
                        currentFood = food4;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                    if(hit.collider.CompareTag("Food5"))
                    {
                        fish.starAnimation = true;
                        currentFood = food5;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                    if(hit.collider.CompareTag("Food6"))
                    {
                        fish.starAnimation = true;
                        currentFood = food6;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                    if(hit.collider.CompareTag("Food7"))
                    {
                        fish.starAnimation = true;
                        currentFood = food7;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                    if(hit.collider.CompareTag("Food8"))
                    {
                        fish.starAnimation = true;
                        currentFood = food8;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                    if(hit.collider.CompareTag("Food9"))
                    {
                        fish.starAnimation = true;
                        currentFood = food9;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                    if(hit.collider.CompareTag("Food10"))
                    {
                        fish.starAnimation = true;
                        currentFood = food10;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                    if(hit.collider.CompareTag("Food11"))
                    {
                        fish.starAnimation = true;
                        currentFood = food11;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                    if(hit.collider.CompareTag("Food12"))
                    {
                        fish.starAnimation = true;
                        currentFood = food12;
                        StartCoroutine(WaitForAnimationDone());
                    }   
                    if(hit.collider.CompareTag("Food13"))
                    {
                        fish.starAnimation = true;
                        currentFood = food13;
                        StartCoroutine(WaitForAnimationDone());
                    } 
                }
            }
        }
    }

    void ShowFood()
    {
        Debug.Log("FOOD NUMBER " + foodNumber);
        foodList[foodNumber].SetActive(true);
    }

    IEnumerator WaitForAnimationDone()
    {
        fish.animationDone = false;
        Debug.Log("STARTANIMATIONDONE " + fish.starAnimation);
        Debug.Log("ANIMATIONDONE " + fish.animationDone);

        yield return new WaitUntil(() => fish.animationDone);
        currentFood.SetActive(false);
        removedFishFoods ++;
        isCaught = true;
    }
}

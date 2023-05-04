using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public int removedFishFoods = 0;
    public int foodNumber = 1;
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
    public bool canShow = false;
    public bool isCaught = false;
    public bool canShowFood = false;

    public GameObject currentFood;
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
            WaitToShowFood();
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
                    if(hit.collider.CompareTag("FishFood1"))
                    {
                        food1.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood2"))
                    {
                        food2.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood3"))
                    {
                        food3.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood4"))
                    {
                        food4.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood5"))
                    {
                        food5.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood6"))
                    {
                        food6.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood7"))
                    {
                        food7.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood8"))
                    {
                        food8.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood9"))
                    {
                        food9.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood10"))
                    {
                        food10.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood11"))
                    {
                        food11.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("FishFood12"))
                    {
                        food12.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    }   
                    if(hit.collider.CompareTag("FishFood13"))
                    {
                        food13.SetActive(false);
                        removedFishFoods ++;
                        isCaught = true;
                    } 
                }
            }
        }
    }

    public void WaitToShowFood()
    {
        Debug.Log("FOOD NUMBER " + foodNumber);
        foodList[foodNumber].SetActive(true);
    }
}

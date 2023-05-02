using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public int removedFoods = 0;
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
        if(isCaught == true)
        {
            WaitToShowFood();
            isCaught == false;
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
                    if(hit.collider.CompareTag("Monkey1"))
                    {
                        monkey1.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey2"))
                    {
                        monkey2.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey3"))
                    {
                        monkey3.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey4"))
                    {
                        monkey4.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey5"))
                    {
                        monkey5.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey6"))
                    {
                        monkey6.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey7"))
                    {
                        monkey7.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey8"))
                    {
                        monkey8.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey9"))
                    {
                        monkey9.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey10"))
                    {
                        monkey10.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey11"))
                    {
                        monkey11.SetActive(false);
                        removedMonkeys ++;
                        isCaught = true;
                    } 
                    if(hit.collider.CompareTag("Monkey12"))
                    {
                        monkey12.SetActive(false);
                        removedMonkeys ++;
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

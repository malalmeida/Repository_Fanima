using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyScript : MonoBehaviour
{
    public int removedMonkeys = 0;
    public int randomIndex = -1;
    public GameObject monkey1;
    public GameObject monkey2;
    public GameObject monkey3;
    public GameObject monkey4;
    public GameObject monkey5;
    public GameObject monkey6;
    public GameObject monkey7;
    public GameObject monkey8;
    public GameObject monkey9;
    public GameObject monkey10;
    public GameObject monkey11;
    public GameObject monkey12;
    public List<GameObject> monkeyList;
    public bool canShow = false;
    public bool isCaught = false;

    public GameObject currentMonkey;
    // Start is called before the first frame update
    void Start()
    {
        monkeyList = new List<GameObject>();
        monkeyList.Add(monkey1);
        monkeyList.Add(monkey2);
        monkeyList.Add(monkey3);
        monkeyList.Add(monkey4);
        monkeyList.Add(monkey5);
        monkeyList.Add(monkey6);
        monkeyList.Add(monkey7);
        monkeyList.Add(monkey8);
        monkeyList.Add(monkey9);
        monkeyList.Add(monkey10);
        monkeyList.Add(monkey11);
        monkeyList.Add(monkey12);
        monkey1.SetActive(false);
        monkey2.SetActive(false);
        monkey3.SetActive(false);
        monkey4.SetActive(false);
        monkey5.SetActive(false);
        monkey6.SetActive(false);
        monkey7.SetActive(false);
        monkey8.SetActive(false);
        monkey9.SetActive(false);
        monkey10.SetActive(false);
        monkey11.SetActive(false);
        monkey12.SetActive(false);
  
    }

    // Update is called once per frame
    void Update()
    {
        if(randomIndex > -1)
        {
            WaitToShowMonkey();
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

    public void  WaitToShowMonkey()
    {
        Debug.Log("MONKEY NUMBER " + randomIndex);
        monkeyList[randomIndex].SetActive(true);
        randomIndex = -1;
    }
}

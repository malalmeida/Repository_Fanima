using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class OwlScript : MonoBehaviour
{
    public GameObject square;
    public GameObject triangle;
    public GameObject circle;
    
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public GameObject obj7;
    public GameObject obj8;
    public GameObject obj9;
    public GameObject obj10;
    public GameObject obj11;
    public GameObject obj12;
    public GameObject obj13;
    public GameObject obj14;
    public GameObject obj15;
    public GameObject obj16;
    public GameObject obj17;
    public GameObject obj18;
    public GameObject obj19;
    public GameObject obj20;
    public GameObject obj21;
    public GameObject obj22;
    public GameObject obj23;
    public GameObject obj24;
    public GameObject obj25;
    public GameObject obj26;
    public GameObject obj27;
    public GameObject obj28;
    public GameObject obj29;

    public List<GameObject> objList;
    public bool canShow = false;
    public bool isMatch = false;

    public int randomIndex = -1;


    public GameObject currentObj;

    // Start is called before the first frame update
    void Start()
    {
        square.SetActive(true);
        triangle.SetActive(true);
        circle.SetActive(true);
        
        objList = new List<GameObject>();
        objList.Add(obj1);
        objList.Add(obj2);
        objList.Add(obj3);
        objList.Add(obj4);
        objList.Add(obj5);
        objList.Add(obj6);
        objList.Add(obj7);
        objList.Add(obj8);
        objList.Add(obj9);
        objList.Add(obj10);
        objList.Add(obj11);
        objList.Add(obj12);
        objList.Add(obj13);
        objList.Add(obj14);
        objList.Add(obj15);
        objList.Add(obj16);
        objList.Add(obj17);
        objList.Add(obj18);
        objList.Add(obj19);
        objList.Add(obj20);
        objList.Add(obj21);
        objList.Add(obj22);
        objList.Add(obj23);
        objList.Add(obj24);
        objList.Add(obj25);
        objList.Add(obj26);
        objList.Add(obj27);
        objList.Add(obj28);
        objList.Add(obj29);

        obj1.SetActive(false);
        obj2.SetActive(false);
        obj3.SetActive(false);
        obj4.SetActive(false);
        obj5.SetActive(false);
        obj6.SetActive(false);
        obj7.SetActive(false);
        obj8.SetActive(false);
        obj9.SetActive(false);
        obj10.SetActive(false);
        obj11.SetActive(false);
        obj12.SetActive(false);
        obj13.SetActive(false);
        obj14.SetActive(false);
        obj15.SetActive(false);
        obj16.SetActive(false);
        obj17.SetActive(false);
        obj18.SetActive(false);
        obj19.SetActive(false);
        obj20.SetActive(false);
        obj21.SetActive(false);
        obj22.SetActive(false);
        obj23.SetActive(false);
        obj24.SetActive(false);
        obj25.SetActive(false);
        obj26.SetActive(false);
        obj27.SetActive(false);
        obj28.SetActive(false);
        obj29.SetActive(false);
    }

    void Update()
    {
        if(randomIndex > -1)
        {
            WaitToShowObj();
        }
        
    }

    public void  WaitToShowObj()
    {
        Debug.Log("OBJ NUMBER " + randomIndex);
        objList[randomIndex].SetActive(true);
        randomIndex = -1;
    }

}

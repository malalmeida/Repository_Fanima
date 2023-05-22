using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusScript : MonoBehaviour
{
    public GameObject red;
    public GameObject yellow;
    public GameObject green;
    
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

    public List<GameObject> objList;
    public bool canShow = false;
    public bool isMatch = false;

    public int randomIndex = -1;

    private Transform dragging = null;
    private Vector3 offset;
    [SerializeField] private LayerMask movableLayers;
    public Vector3 startPosition;
    public GameObject currentObj;

    // Start is called before the first frame update
    void Start()
    {
        red.SetActive(true);
        yellow.SetActive(true);
        green.SetActive(true);
        
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
    }

    void Update()
    {
        //if(randomIndex > -1)
        if(canShow)
        {
            WaitToShowObj();
        }
         
        if (Input.GetMouseButtonDown(0)) 
        {
            // Cast our own ray.
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.PositiveInfinity, movableLayers);
            if (hit) 
            {
                // If we hit, record the transform of the object we hit.
                dragging = hit.transform;
                // And record the offset.
                offset = dragging.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            // Stop dragging.
            dragging = null;
        }

        if (dragging != null) 
        {
            // Move object, taking into account original offset.
            dragging.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    public void WaitToShowObj()
    {
        canShow = false;
        Debug.Log("OBJ NUMBER " + randomIndex);
        Debug.Log("objs " +  objList.Count);
        objList[randomIndex].SetActive(true);
        currentObj = objList[randomIndex];
        startPosition = new Vector3(currentObj.transform.position.x, currentObj.transform.position.y, currentObj.transform.position.z);
    }
}

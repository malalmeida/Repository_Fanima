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
  
    public SpriteRenderer rend;
    public GameObject currentObj; 
    public string currentWord = "";
    public bool canShowImage = false;
    public int repNumber = -1;

    public bool nextAction = false;
    public bool isMatch = false;

    public int randomIndex = -1;

    private Transform dragging = null;
    private Vector3 offset;
    [SerializeField] private LayerMask movableLayers;
    public Vector3 startPosition;



    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        square.SetActive(true);
        triangle.SetActive(true);
        circle.SetActive(true);
    }

    void Update()
    {
        if(canShowImage)
        {
            WaitToShowObj();
        }
        if(nextAction)
        {
            HidePreviousImage();
        }
        if(repNumber == 3)
        {
            MoveImage();
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
        string gameObjName = currentWord + "Obj" + repNumber;       
        Debug.Log("OBJ " + gameObjName);

        currentObj = GameObject.Find(gameObjName);
        currentObj.transform.position = new Vector3(0, 1.4f, 0);

        rend = currentObj.GetComponent<SpriteRenderer>();
        rend.sortingOrder = 10;

        canShowImage = false;
    }

    public void HidePreviousImage()
    {
        rend = currentObj.GetComponent<SpriteRenderer>();
        rend.sortingOrder = -1;
        nextAction = false;
    }

    public void MoveImage()
    {
        HidePreviousImage();
        WaitToShowObj();
    }
}


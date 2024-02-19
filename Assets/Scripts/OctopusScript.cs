using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OctopusScript : MonoBehaviour
{
    public GameObject red;
    public GameObject yellow;
    public GameObject green;

    public bool nextAction = false;
    public bool isMatch = false;
    public SpriteRenderer rend;
    public GameObject currentObj; 
    public string currentWord = "";
    public bool canShowImage = false;
    public int repNumber = -1;

    public int randomIndex = -1;

    private Transform dragging = null;
    private Vector3 offset;
    [SerializeField] private LayerMask movableLayers;
    public Vector3 startPosition;

    public AudioSource validationSound;

    public GameObject rewardBoard;
    public SpriteRenderer rendRewardBoard;

    public float incrementAmount = 0.0f;
    public Image barImage;

    public bool showReward = false;
    // Start is called before the first frame update
    void Start()
    {
        red.SetActive(true);
        yellow.SetActive(true);
        green.SetActive(true);

        barImage.fillAmount = 0.0f;

    }

    void Update()
    {
        if(showReward)
        {
            ShowRewardBoard();
        }
        
        if(canShowImage)
        {
            ShowObj();

        }
        if(nextAction)
        {
            HidePreviousImage();
            //validationSound.Play();
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

    public void MoveImage()
    {
        HidePreviousImage();
        ShowObj();
        repNumber = -1;
    }

   public void ShowObj()
    {
        string gameObjName = currentWord + "Obj" + repNumber;       
        Debug.Log("OBJ " + gameObjName);

        currentObj = GameObject.Find(gameObjName);
        if(gameObjName.Contains("Rurru"))
        {
            currentObj.transform.position = new Vector3(0f, -0.6f, 0);
        }
        else
        {
            currentObj.transform.position = new Vector3(0f, 1.0f, 0);
        }

        rend = currentObj.GetComponent<SpriteRenderer>();
        //rend.sortingOrder = 10;
        canShowImage = false;
    }

    public void HidePreviousImage()
    {
        currentObj.SetActive(false);
        //rend = currentObj.GetComponent<SpriteRenderer>();
        //rend.sortingOrder = -1;
        nextAction = false;
    }   

    public void ShowRewardBoard()
    {
        rendRewardBoard = rewardBoard.GetComponent<SpriteRenderer>();
        rendRewardBoard.sortingOrder = 20;
    } 
}

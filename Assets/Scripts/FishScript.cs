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

    public GameObject rewardBoard;
    public SpriteRenderer rendRewardBoard;

    public bool showReward = false;

    public int foodPosition = -1;

    void Start()
    {
        barImage.fillAmount = 0.0f;
    }

    // Update is called once per frame
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
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }
                    else if(hit.collider.CompareTag("Food2"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }
                    else if(hit.collider.CompareTag("Food3"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }
                    else if(hit.collider.CompareTag("Food4"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }               
                    else if(hit.collider.CompareTag("Food5"))
                    {
                       MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    } 
                    else if(hit.collider.CompareTag("Food6"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }
                    else if(hit.collider.CompareTag("Food7"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    } 
                    else if(hit.collider.CompareTag("Food8"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }               
                    else if(hit.collider.CompareTag("Food9"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }
                    else if(hit.collider.CompareTag("Food10"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }
                    else if(hit.collider.CompareTag("Food11"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }
                    else if(hit.collider.CompareTag("Food12"))
                    {
                        MoveFish();
                        //barImage.fillAmount += incrementAmount;
                    }    
                }
            }
        }
    }

    public void ShowObj()
    {
        //string gameObjName = currentWord + "Obj";
        string gameObjName = currentWord + "Obj" + foodPosition.ToString();       
        Debug.Log("OBJ " + gameObjName);

        currentObject = GameObject.Find(gameObjName);

        if(foodPosition == 0)
        { 
        currentObject.transform.position = new Vector3(5.0f, -1.0f, 0);
        }
        else if(foodPosition == 1)
        {
        currentObject.transform.position = new Vector3(10.0f, 1.0f, 0);
        }
        else if(foodPosition == 2)
        {
        currentObject.transform.position = new Vector3(15.0f, 3.0f, 0);
        }
        else if(foodPosition == 3)
        {
        currentObject.transform.position = new Vector3(20.0f, 5.0f, 0);
        }
        else if(foodPosition == 4)
        {
        currentObject.transform.position = new Vector3(26.0f, 6.0f, 0);
        }
        else if(foodPosition == 5)
        {
        currentObject.transform.position = new Vector3(32.0f, 5.0f, 0);
        }
        else if(foodPosition == 6)
        {
        currentObject.transform.position = new Vector3(37.0f, 3.0f, 0);
        }
        else if(foodPosition == 7)
        {
        currentObject.transform.position = new Vector3(42.0f, 1.0f, 0);
        }
        else if(foodPosition == 8)
        {
        currentObject.transform.position = new Vector3(47.0f, 0.0f, 0);
        }
        else if(foodPosition == 9)
        {
        currentObject.transform.position = new Vector3(52.0f, 2.0f, 0);
        }
        else if(foodPosition == 10)
        {
        currentObject.transform.position = new Vector3(57.0f, 4.0f, 0);
        }
        else if(foodPosition == 11)
        {
        currentObject.transform.position = new Vector3(62.0f, 3.0f, 0);
        }

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
        barImage.fillAmount += incrementAmount;
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

    public void ShowRewardBoard()
    {
        rendRewardBoard = rewardBoard.GetComponent<SpriteRenderer>();
        rendRewardBoard.sortingOrder = 20;
    }

  }


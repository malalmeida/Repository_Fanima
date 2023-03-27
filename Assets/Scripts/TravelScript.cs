using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TravelScript : MonoBehaviour
{
    public Animator animator;
    private bool finish;
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject cloud4;
    public int cloudsRemoved = 0;
    public bool patientInteractionDone = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(cloudsRemoved == 4)
        {
            animator.SetBool("finish", true);
            //PlayerPrefs.SetInt("ChapterNumber", 0);
            patientInteractionDone = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    Color newColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                    //hit.collider.GetComponent<Rigidbody>().enabled = false;
                    hit.collider.GetComponent<SpriteRenderer>().material.color = newColor;
                    cloudsRemoved ++;

                }
            }
        }

/*
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    Color newColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                    hit.collider.GetComponent<SpriteRenderer>().material.color = newColor;
                }
            }
        }
*/
    }
}

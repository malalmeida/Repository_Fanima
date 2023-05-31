using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSquareScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public OwlScript owlScript;


    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Square")
        {
            owlScript.isMatch = true;
            owlScript.currentObj.SetActive(false);
        }
    }
}

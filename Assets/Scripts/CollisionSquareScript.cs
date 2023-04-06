using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSquareScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TOU!");
        if (collision.gameObject.tag == "Square")
        {
            Debug.Log("ACERTOU!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriangleScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TOU!");
        if (collision.gameObject.tag == "Triangle")
        {
            Debug.Log("ACERTOU!");
        }
    }
}

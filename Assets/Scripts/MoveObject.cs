using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] float objectSpeed;
    int nextPositionIndex;
    Transform nextPosition;
    public bool starAnimation = false;
    public bool animationDone = false;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = positions[0];
    }

    // Update is called once per frame
    void Update()
    {   
        if(starAnimation)
        {
            if(animationDone == false)
            {
                MoveGameObject();
            } 
        }
    }
    
    public void MoveGameObject()
    {
        if(transform.position == nextPosition.position)
        {   
            animationDone = true;
            nextPositionIndex ++;
            if(nextPositionIndex >= positions.Length)
            {
                nextPositionIndex = 0;
            }
            nextPosition = positions[nextPositionIndex];
        }
        else
        {
            //animationDone = false;
            transform.position = Vector3.MoveTowards(transform.position, nextPosition.position, objectSpeed * Time.deltaTime);
        }
    }
}

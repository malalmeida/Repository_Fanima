using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] float ObjectSpeed;
    Transform NextPosition;
    int NextPositionIndex;

    // Start is called before the first frame update
    void Start()
    {
        NextPosition = Positions[0]; 
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject(); 
    }

    void MoveObject()
    {
        if(transform.position == NextPosition.position)
        {
          NextPositionIndex ++;
          if (NextPositionIndex >= Positions.Length)
          {
            NextPositionIndex = 0;
          }
          NextPosition = Positions[NextPositionIndex];
        }
        else
        {
          transform.position = Vector3.MoveTowards(transform.position, NextPosition.position, ObjectSpeed = Time.deltaTime);
        }
    }
}

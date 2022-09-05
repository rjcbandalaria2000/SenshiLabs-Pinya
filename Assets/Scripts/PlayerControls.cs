using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class PlayerControls : MonoBehaviour
{
    public int moveSpeed = 5;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = this.transform.position;
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = this.transform.position.z;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed*Time.deltaTime);
    }
}

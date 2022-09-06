using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingControl : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        Debug.Log("X coordinate: " + mousePosition.normalized.x);

    }

}

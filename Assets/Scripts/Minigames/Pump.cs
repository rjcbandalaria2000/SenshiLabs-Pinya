using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pump : MonoBehaviour
{
    private Camera mainCamera;
    public HingeJoint2D hingeJoint;
    Vector3 mouseWorldPosition;
    public GameObject parent;
    public float rotation;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //hingeJoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, rotation++);
        }
        if (Input.GetMouseButton(1))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, rotation--);
        }
    }

    private void OnMouseDown()
    {
      
    }
}

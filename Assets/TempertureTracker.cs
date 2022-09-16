using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TempertureTracker : MonoBehaviour
{
    public GameObject Parent;

    private TemperatureControl parentTempControl; 
    // Start is called before the first frame update
    void Start()
    {
        if(Parent == null)
        {
            Parent = this.transform.parent.gameObject;
        }
        parentTempControl = Parent.GetComponent<TemperatureControl>();
        Assert.IsNotNull(Parent, "Parent of Temp Tracker is null or is not set");
        Assert.IsNotNull(parentTempControl, "Temp control of parent is null or is not set");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Temperature collidedTemp = collision.gameObject.GetComponent<Temperature>();
        if (collidedTemp)
        {
            parentTempControl.ChosenTemp = collidedTemp.gameObject;
            Debug.Log("Collided temp");
        }
    }
}

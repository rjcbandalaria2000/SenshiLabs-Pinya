using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public GameObject   Parent;
  
    public void Start()
    {
        if(Parent == null)
        {
            Parent = this.gameObject.transform.parent.gameObject;
        }
    }

    public GameObject GetParent()
    {
        return Parent;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public GameObject   Parent;
    public GameObject   UI;
  
    public void Start()
    {
        if (Parent == null)
        {
            Parent = this.gameObject.transform.parent.gameObject;
        }
    }

    public GameObject GetParent()
    {
        if(Parent == null) { return null; }
        return Parent;
    }
}

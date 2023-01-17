using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    public GameObject   Parent;
    public GameObject   UI;
    public GameObject   effects;
    public Sprite       iconSprite;
  
    public void Start()
    {
        if (Parent == null)
        {
            Parent = this.gameObject.transform.parent.gameObject;
        }
        if (effects)
        {
            effects.SetActive(false);
        }
    }

    public GameObject GetParent()
    {
        if(Parent == null) { return null; }
        return Parent;
    }

    
}

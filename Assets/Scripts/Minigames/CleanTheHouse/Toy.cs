using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    public bool IsHolding;
    public bool IsHoveredOver;
    public bool IsPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        IsPickedUp = false;
    }

    private void OnMouseDown()
    {
        IsPickedUp = true;
    }

    private void OnMouseOver()
    {
        if (!IsHoveredOver)
        {
            IsHoveredOver = true;
        }
    }

    private void OnMouseExit()
    {
        if (IsHoveredOver)
        {
            IsHoveredOver = false;
        }
    }

    private void OnMouseDrag()
    {
        IsHolding = true;
    }

    private void OnMouseUp()
    {
        IsHolding = false;
    }

}

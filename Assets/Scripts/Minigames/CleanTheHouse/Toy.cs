using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [Header("States")]
    public bool         isHolding;
    public bool         isHoveredOver;
    public bool         isPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        isPickedUp = false;
    }

    private void OnMouseDown()
    {
        isPickedUp = true;
    }

    private void OnMouseOver()
    {
        if (!isHoveredOver)
        {
            isHoveredOver = true;
        }
    }

    private void OnMouseExit()
    {
        if (isHoveredOver)
        {
            isHoveredOver = false;
        }
    }

    private void OnMouseDrag()
    {
        isHolding = true;
    }

    private void OnMouseUp()
    {
        isHolding = false;
    }

}

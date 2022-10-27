using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public bool IsHolding;
    public bool IsHoveredOver;
    public bool IsPickedUp;

    SFXManager sFX;
    public AudioClip pickedSFX;
    // Start is called before the first frame update
    void Start()
    {
        sFX = GetComponent<SFXManager>();
        IsPickedUp = false;
    }

    private void OnMouseDown()
    {
        IsPickedUp = true;
        sFX.PlaySFX(pickedSFX);
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

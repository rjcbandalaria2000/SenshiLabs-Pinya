using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayItemCounter : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public int quantity;
    bool isDuplicated;


    private void Start()
    {
        quantity = 1;
        counter.text = quantity + "x";
    }

    public void addPoint(int point)
    {
       
        quantity += point;
        counter.text = quantity + "x";
        Debug.Log("Add Quantity");
    }
    public void decreasePoint()
    {
        quantity--;
        counter.text = quantity + "x";
    }

    public void resetQuantity()
    {
        quantity = 1;
        counter.text = quantity + "x";
    }
}

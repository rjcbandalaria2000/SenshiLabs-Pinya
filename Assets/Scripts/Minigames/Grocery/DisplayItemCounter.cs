using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayItemCounter : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public int quantity;
    public bool isDuplicated;


    private void Start()
    {
     
        counter.text = "x " + quantity;
    }

    public void addPoint(int value)
    {
       
        this.quantity += value;
        Debug.Log("Quantity: " + quantity);

        this.counter.text = "x " + quantity;
        Debug.Log("Add Quantity");
    }
    public void decreasePoint()
    {
        this.quantity--;
        Debug.Log("Quantity: " + quantity);

        this.counter.text = "x " + quantity;
        Debug.Log("Minus Quantity");
    }

    public void resetQuantity()
    {
        quantity = 1;
        counter.text = quantity + "x";
    }
}

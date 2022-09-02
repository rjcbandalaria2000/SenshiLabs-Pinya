using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    

    public void Awake()
    {
        SingletonManager.Register(this);
    }

   
}

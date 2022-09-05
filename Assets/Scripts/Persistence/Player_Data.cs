using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    public float storedMotivationData;
    public float storedPinyaData;

    public void Awake()
    {
        SingletonManager.Register(this);
    }

   
}

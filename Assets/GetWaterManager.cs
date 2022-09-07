using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWaterManager : MonoBehaviour
{
    public int RequiredNumSwipes = 3;
    public int NumOfSwipes = 0;

    private void Awake()
    {
        GetWaterManager getWaterManager = SingletonManager.Get<GetWaterManager>();
        if (getWaterManager != null) {

            SingletonManager.Remove<GetWaterManager>();
            SingletonManager.Register(this);
        }
        else
        {
            SingletonManager.Register(this);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}

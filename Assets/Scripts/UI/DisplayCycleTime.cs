using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCycleTime : MonoBehaviour
{
    public TextMeshProUGUI cycleTimer_UI;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SingletonManager.Get<DayCycle>() != null)
        {
            cycleTimer_UI.text = SingletonManager.Get<DayCycle>().time.ToString();
        }
        else 
        { 
            Debug.Log("DayCycle doesnt exist"); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTimerDisplay()
    {
        if (SingletonManager.Get<DayCycle>() != null)
        {
            cycleTimer_UI.text = SingletonManager.Get<DayCycle>().time.ToString();
        }
        else
        {
            Debug.Log("DayCycle doesnt exist");
        }
    }
}

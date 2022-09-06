using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("DayCycle Timer")]
    public float timer;
    public float endTime;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    public void Start()
    {
        StartCoroutine(dayStart());
    }
    public IEnumerator dayStart()
    {
        if(SingletonManager.Get<DayCycle>() != null)
        {
            SingletonManager.Get<DayCycle>().time = timer;
            SingletonManager.Get<DayCycle>().endTime = endTime;
        }
        else
        {
            Debug.Log("DayCycle doesnt exist");
        }
        if(SingletonManager.Get<UIManager>() != null)
        {
            SingletonManager.Get<UIManager>().activateTimerUI(); 
            //SingletonManager.Get<UIManager>().motivationMeter.SetActive(true); 
            //SingletonManager.Get<UIManager>().piñyaMeter.SetActive(true);
        }
        else
        {
            Debug.Log("UI Manager doesnt exist");
        }

        yield return new WaitForSeconds(3f);
    }

    public IEnumerator dayEnd()
    {
        if (SingletonManager.Get<UIManager>() != null)
        {
            SingletonManager.Get<UIManager>().deactivateTimerUI();
            SingletonManager.Get<UIManager>().activateDayEnd_UI();

        }
        else
        {
            Debug.Log("UI Manager doesnt exist");
        }

        yield return new WaitForSeconds(3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Time UI")]
    public GameObject timerDisplay;

    [Header("Meters UI")]
    public GameObject motivationMeter;
    public GameObject piñyaMeter;

    [Header("Display UI")] //Mostly text base UI
    public GameObject dayEnd_UI;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    public void activateTimerUI()
    {
        if (timerDisplay != null)
        {
            timerDisplay.SetActive(true);
        }
        else { Debug.Log("timerDisplay null"); }
    }

    public void deactivateTimerUI()
    {
        if (timerDisplay != null)
        {
            timerDisplay.SetActive(false);
        }
        else { Debug.Log("timerDisplay null"); }
    }

    public void activateDayEnd_UI()
    {
        dayEnd_UI.SetActive(true);
    }

    public void deactivateDayEnd_UI()
    {
        dayEnd_UI.SetActive(false);
    }
}

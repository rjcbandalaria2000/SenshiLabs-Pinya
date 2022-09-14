using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Time UI")]
    public GameObject timerDisplay;

    [Header("Minigame Time UI")]
    public GameObject miniGameTimerDisplay;

    [Header("Meters UI")]
    public GameObject motivationMeter;
    public GameObject pi�yaMeter;

    [Header("Display UI")] //Mostly text base UI
    public GameObject dayEnd_UI;

    [Header("Loading UI")]
    public GameObject Loading_UI;

    [Header("Ask Mom UI")]
    public Button askMomButton;

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

    public void activateMiniGameTimer_UI()
    {
        miniGameTimerDisplay.SetActive(true);
    }

    public void deactivateMiniGameTimer_UI()
    {
        miniGameTimerDisplay.SetActive(false);
    }

    public void activateLoading_UI()
    {

        Loading_UI.SetActive(true);
    }

    public void deactivateLoading_UI()
    {
        Loading_UI.SetActive(false);
    }

    public void buttonUninteractable()
    {
        if(askMomButton != null)
        {
            askMomButton.interactable = false;
        }
    }
    public void buttonInteractable()
    {
        if (askMomButton != null)
        {
            askMomButton.interactable = true;
        }
    }
}

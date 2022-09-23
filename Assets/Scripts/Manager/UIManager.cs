using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    //------------------------------------------------MAINGAME------------------------------------------------------------------------------------
    [Header("Time UI")]
    public GameObject timerDisplay;

    [Header("Meters UI")]
    public GameObject motivationMeter;
    public GameObject pinyaMeter;

    [Header("Display UI")] //Mostly text base UI
    public GameObject dayEnd_UI;
    public GameObject curtainsUI;
    public GameObject gameUI;

    //------------------------------------------------MINIGAME------------------------------------------------------------------------------------
  
    [Header("Minigame UI")]
    public GameObject miniGameTimerDisplay;
    public GameObject miniGameMainMenu;
    public GameObject minigameStartCountdownUI;
    public GameObject minigameResultsUI;
    public GameObject minigameGoodResult;
    public GameObject minigameBadResult;

    [Header("Loading UI")]
    public GameObject Loading_UI;

    [Header("Ask Mom UI")]
    public Button askMomButton;

    private void Start()
    {
        if(miniGameMainMenu != null)
        {
            InitializeUI();
        }
        if(curtainsUI != null)
        {
            //For the curtain transition
            Events.OnCurtainsOpened.AddListener(OnTransitionOpened);
            Events.OnCurtainStart.AddListener(OnTransitionStarted);
            Events.OnSceneChange.AddListener(OnSceneChange);
        }
       
    }

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    public void ActivateTimerUI()
    {
        if (timerDisplay != null)
        {
            timerDisplay.SetActive(true);
        }
        else { Debug.Log("timerDisplay null"); }
    }

    public void DeactivateTimerUI()
    {
        if (timerDisplay != null)
        {
            timerDisplay.SetActive(false);
        }
        else { Debug.Log("timerDisplay null"); }
    }

    public void ActivateDayEndUI()
    {
        dayEnd_UI.SetActive(true);
    }

    public void DeactivateDayEndUI()
    {
        dayEnd_UI.SetActive(false);
    }

    public void ActivateMiniGameTimerUI()
    {
        miniGameTimerDisplay.SetActive(true);
    }

    public void DeactivateMiniGameTimerUI()
    {
        if(miniGameTimerDisplay == null) { return; }
        miniGameTimerDisplay.SetActive(false);
    }

    public void ActivateLoadingUI()
    {

        Loading_UI.SetActive(true);
    }

    public void DeactivateLoadingUI()
    {
        Loading_UI.SetActive(false);
    }

    public void ButtonUninteractable()
    {
        if(askMomButton != null)
        {
            askMomButton.interactable = false;
        }
    }
    public void ButtonInteractable()
    {
        if (askMomButton != null)
        {
            askMomButton.interactable = true;
        }
    }

    public void ActivateMiniGameMainMenu()
    {
        if(miniGameMainMenu == null) { return; }
        miniGameMainMenu.SetActive(true);
    }

    public void DeactivateMiniGameMainMenu()
    {
        miniGameMainMenu.SetActive(false);
    }

    public void InitializeUI()
    {
        DeactivateMiniGameTimerUI();

        ActivateMiniGameMainMenu();

        DeactivateResultScreen();
        DeactivateGameUI();
    }

    public void PlayMiniGameUI()
    {
        DeactivateMiniGameMainMenu();

        ActivateMiniGameTimerUI();

    }

    public void ActivateGameCountdown()
    {
        minigameStartCountdownUI.SetActive(true);
    }
    public void DeactivateGameCountdown()
    {
        minigameStartCountdownUI.SetActive(false);
    }

    public void ActivateResultScreen()
    {
        if(minigameResultsUI == null) { return; }
        minigameResultsUI.SetActive(true);
        DisplayMinigameResult results = minigameResultsUI.GetComponent<DisplayMinigameResult>();
        //if(results == null) { return; }
        Assert.IsNotNull(results);
        results.DisplayPinyaMeter();
        results.DisplayMotivation();
    }

    public void DeactivateResultScreen()
    {
        if (minigameResultsUI == null){ return;}
        minigameResultsUI.SetActive(false);
    }

    public void ActivateGoodResult()
    {
        minigameGoodResult.SetActive(true);
        minigameBadResult.SetActive(false);
    }

    public void ActivateBadResult()
    {
        minigameGoodResult.SetActive(false);
        minigameBadResult.SetActive(true);
    }

    public void ActivateOpeningCurtains()
    {
        curtainsUI.SetActive(true);
    }

    public void DeactivateOpeningCurtains()
    {
        curtainsUI.SetActive(false);
    }

    public void ActivateGameUI()
    {
        if(gameUI == null) { return; }
        gameUI.SetActive(true);
    }
    public void DeactivateGameUI()
    {
        if (gameUI == null) { return; }
        gameUI.SetActive(false);
    }

    #region Transition Functions

    public void OnTransitionStarted()
    {
        DeactivateGameUI();
        DeactivateMiniGameMainMenu();
    }
    public void OnTransitionOpened()
    {
        ActivateGameUI();
        ActivateMiniGameMainMenu();
    }

    public void OnTransitionClosed()
    {
        DeactivateGameUI();
    }
    #endregion

    public void OnSceneChange()
    {
        Events.OnCurtainsOpened.RemoveListener(OnTransitionOpened);
        Events.OnCurtainStart.RemoveListener(OnTransitionStarted);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}

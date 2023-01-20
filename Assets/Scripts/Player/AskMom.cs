using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;
using UnityEngine.EventSystems;

public class AskMom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Highlighted Objects")]
    //public List<GameObject>     highLight = new();
    public List<GameObject>     minigameObjects = new();
    public float                coolDown;

    [Header("Pinya Meter")]
    public PinyaMeter           playerPinyaMeter;
    public int                  pinyaCost;
    public DisplayPinyaMeter    pinyaMeterUI;

    [Header("Effects")]
    public GameObject heartGO;
    //public GameObject twinkleGO;
    //public GameObject arrowsGO;
    public GameObject lifeBar;

    private Coroutine           askMomRoutine;
    private UIManager           uiManager;
    private TaskManager         taskManager;

    private Camera cm;

   

   // public GameObject crackedGlass;
   // public GameObject overlayGO;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = SingletonManager.Get<UIManager>();
        taskManager = SingletonManager.Get<TaskManager>();
        askMomRoutine = null;
        //DisableHighlight();
        cm = Camera.main;
    }
    
    public void OnAskMomButtonPressed()
    {
        if(playerPinyaMeter == null) { return; }
        if (pinyaMeterUI)
        {
            pinyaMeterUI.StopDamageFade();
        }
        BeginAskMom();
        

    }

    public void BeginAskMom()
    {
        Assert.IsNotNull(playerPinyaMeter);

        if (playerPinyaMeter.pinyaValue > 0)
        {
            askMomRoutine = StartCoroutine(AskMomCD());
            if (pinyaMeterUI)
            {
                pinyaMeterUI.StartDamageFade(pinyaCost);
            }
        }
        
        playerPinyaMeter.DecreasePinyaMeter(pinyaCost);

    }

    IEnumerator AskMomCD()
    {
        if (uiManager != null)
        {
            uiManager.ButtonUninteractable();
        }
        EnableHighlight();
        if (taskManager != null)
        {
            taskManager.DisplayTasks();
        }
        yield return new WaitForSeconds(coolDown);
        if (uiManager != null)
        {
            uiManager.ButtonInteractable();
        }
        DisableHighlight();
        if (taskManager != null)
        {
            taskManager.HideTasks();
        }
    }

    public void EnableHighlight()
    {
        //  cm.transform.dos(1,0.3f,10,0,false);
        EnableEffects();
        lifeBar.transform.DOShakeScale(coolDown + 1, 0.1f, 1, 0, false);
        PulsatingHeart();
    }

    public void DisableHighlight()
    {
        DisableEffects();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // When the player hovers over the ask mom button
        Debug.Log("Ask mom hover");
        if (pinyaMeterUI)
        {
            pinyaMeterUI.StartDamageFade(pinyaCost);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (pinyaMeterUI)
        {
            pinyaMeterUI.StopDamageFade();
        }
        Debug.Log("Ask mom exit");
    }
    public void PulsatingHeart()
    {
        StartCoroutine(PulsateHeart());
    }


    IEnumerator PulsateHeart()
    {
        float tempCooldown = coolDown;
        while (tempCooldown > 0)
        {
            Debug.Log("animatio");
            heartGO.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);
           // overlayGO.transform.DOScale(1.1f, 0.3f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(1f);
            heartGO.transform.DOScale(4, 0.3f).SetEase(Ease.OutBounce);
            //overlayGO.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBounce);
        //    crackedGlass.SetActive(false);
            tempCooldown--;

        }
      //  heartGO.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);

        heartGO.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);
      //  overlayGO.SetActive(false);
        Debug.Log("End Pulsate");
    }

    public void EnableEffects()
    {
        //if(minigameObjects.Count <=0) { return; }
        Assert.IsNotNull(taskManager, "Task manager is not set or is null");
        if(taskManager.requiredTasks.Count <= 0) { return; }
        foreach(MinigameObject minigame in taskManager.requiredTasks)
        {
            if (!minigame.hasCompleted)
            {
                UnitInfo unitInfo = minigame.gameObject.GetComponent<UnitInfo>();
                if (unitInfo)
                {
                    unitInfo.effects.gameObject.SetActive(true);
                    //Debug.Log("Activate Effects");
                }
            }
            
        }
    }

    public void DisableEffects()
    {
        Assert.IsNotNull(taskManager, "Task manager is not set or is null");
        if (taskManager.requiredTasks.Count <= 0) { return; }
        foreach (MinigameObject minigame in taskManager.requiredTasks)
        {
            
            UnitInfo unitInfo = minigame.gameObject.GetComponent<UnitInfo>();
            if (unitInfo)
            {
                unitInfo.effects.gameObject.SetActive(false);
                //Debug.Log("Activate Effects");
            }

        }
    }

    public void EnableSpeechBubble()
    {
        if (minigameObjects.Count  <=0) { return; }
        foreach(GameObject minigame in minigameObjects)
        {
            UnitInfo unitInfo = minigame.GetComponent<UnitInfo>();
            if (unitInfo)
            {
                if (unitInfo.UI)
                {
                    unitInfo.UI.gameObject.SetActive(true);
                }
            }
        }
    }

    public void DisableSpeechBubble()
    {
        if (minigameObjects.Count <= 0) { return; }
        foreach (GameObject minigame in minigameObjects)
        {
            UnitInfo unitInfo = minigame.GetComponent<UnitInfo>();
            if (unitInfo)
            {
                if (unitInfo.UI)
                {
                    unitInfo.UI.gameObject.SetActive(false);
                }
            }
        }
    }
}

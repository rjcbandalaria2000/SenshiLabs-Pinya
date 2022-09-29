using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class AskMom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Highlighted Objects")]
    public List<GameObject>     highLight = new();
    public float                coolDown;

    [Header("Pinya Meter")]
    public PinyaMeter           playerPinyaMeter;
    public int                  pinyaCost;
    public DisplayPinyaMeter    pinyaMeterUI;

    private Coroutine           askMomRoutine;
    private UIManager           uiManager;
    private TaskManager         taskManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = SingletonManager.Get<UIManager>();
        taskManager = SingletonManager.Get<TaskManager>();
        askMomRoutine = null;
        DisableHighlight();
    }
    
    public void OnAskMomButtonPressed()
    {
        if(playerPinyaMeter == null) { return; }
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
        if(highLight.Count > 0)
        {
            for(int i = 0; i < highLight.Count; i++)
            {
                highLight[i].SetActive(true);
            }
        }
    }

    public void DisableHighlight()
    {
        if (highLight.Count > 0)
        {
            for (int i = 0; i < highLight.Count; i++)
            {

                highLight[i].SetActive(false);

            }
        }
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
}

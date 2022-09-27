using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AskMom : MonoBehaviour
{
    [Header("Highlighted Objects")]
    public List<GameObject>     highLight = new();
    public float                coolDown;

    [Header("Pinya Meter")]
    public PinyaMeter           playerPinyaMeter;
    public int                  pinyaCost;

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
        
        if(playerPinyaMeter.PinyaValue > 0)
        {
            askMomRoutine = StartCoroutine(AskMomCD());
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
    
}

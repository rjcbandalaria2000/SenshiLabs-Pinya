using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("Values")]
    public int                      maxNumOfTasks = 1;

    [Header("Minigames")]
    public List<MinigameObject>     minigameObjects = new();
    //public List<PreRequisiteTask>   tasks = new();
    public List<MinigameObject>     requiredTasks = new();
    public List<MinigameObject>     finishedTasks = new();
  
    [Header("Task UI")]
    public GameObject               taskTextPrefab;
    public GameObject               taskListParent;

    private List<GameObject>        taskTexts = new();
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetRandomTasks();
        taskListParent.SetActive(false);
        //DisplayTasks();
    }

    public void DisplayTasks()
    {
        taskListParent.SetActive(true);
        Assert.IsNotNull(minigameObjects, "Minigameobjects are not set");
        if (taskTexts.Count > 0)
        {
            for (int i = 0; i < taskTexts.Count; i++)
            {
                Destroy(taskTexts[i]);
            }
            taskTexts.Clear();
        }
        Assert.IsNotNull(taskTextPrefab, "taskTextPrefab is not set");
        for (int i = 0; i < requiredTasks.Count; i++)
        {
            //Only spawn if minigame is not yet completed 
            if (!requiredTasks[i].hasCompleted)
            {
                GameObject spawnedText = Instantiate(taskTextPrefab, taskListParent.transform, false);
                TextMeshProUGUI text = spawnedText.GetComponent<TextMeshProUGUI>();
                if (text)
                {
                    text.text = requiredTasks[i].minigameName.ToString();
                }
                taskTexts.Add(spawnedText);
            }
        }
    }

    public void HideTasks()
    {
        if (taskTexts.Count > 0)
        {
            for (int i = 0; i < taskTexts.Count; i++)
            {
                Destroy(taskTexts[i]);
            }
            taskTexts.Clear();
        }
        taskListParent.SetActive(false);
    }

    public void SetRandomTasks() {

        //Temporarily store all tasks. Modify to avoid duplicates 
        List<MinigameObject> tempTaskList = new();

        //Only add to the tempTaskList tasks/minigames that are not yet completed
        for (int i = 0; i < minigameObjects.Count; i++) {

            if (!minigameObjects[i].hasCompleted)
            {
                tempTaskList.Add(minigameObjects[i]);
            }
        
        }
        for (int i = 0; i < maxNumOfTasks; i++) {

            int randomTaskIndex = Random.Range(0, tempTaskList.Count);
            //Check for duplicates
            requiredTasks.Add(tempTaskList[randomTaskIndex]);
            tempTaskList.RemoveAt(randomTaskIndex);
               
        }
    }

}

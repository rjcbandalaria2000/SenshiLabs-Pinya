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
    public bool                     isCurrentTasksDone = false;

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
        Initialize();
    }

    public void Initialize()
    {
        //if (SingletonManager.Get<PlayerData>())
        //{
        //    if(SingletonManager.Get<PlayerData>().requiredTasks.Count > 0)
        //    {
        //        RestoreSavedRequiredTasks();
        //    }
        //}
        //if (requiredTasks.Count <= 0)
        //{
        //    SetRandomTasks();
        //}
        if (SingletonManager.Get<PlayerData>().requiredTasks.Count > 0)
        {
            RestoreSavedRequiredTasks();
        }
        else
        {
            SetRandomTasks();
        }

        CheckIfTasksDone();
        taskListParent.SetActive(false);
        Events.OnSceneChange.AddListener(OnSceneChange);
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
        // Clear required task list 
        requiredTasks.Clear();
        for (int i = 0; i < maxNumOfTasks; i++) {

            int randomTaskIndex = Random.Range(0, tempTaskList.Count);
            //Check for duplicates
            requiredTasks.Add(tempTaskList[randomTaskIndex]);
            //Check if the task has pre requisite
            if (tempTaskList[randomTaskIndex].preRequisiteTasks.Count > 0)
            {
                //if there is a pre requisite add it to the required task list
                for(int j = 0; j < tempTaskList[randomTaskIndex].preRequisiteTasks.Count; j++)
                {
                    requiredTasks.Add(tempTaskList[randomTaskIndex].preRequisiteTasks[j]);
                    
                }
            }

            tempTaskList.RemoveAt(randomTaskIndex);
            //if the required tasks are over the max number of tasks
            
            //if the temp task list is not enough 
            if (tempTaskList.Count <= 0) { break; }
               
        }
        if (requiredTasks.Count > maxNumOfTasks)
        {
            // only remove the ones without pre requisites
            for (int i = 0; i < requiredTasks.Count - maxNumOfTasks; i++)
            {
                for (int j = 0; j < requiredTasks.Count; j++)
                {
                    if (requiredTasks[j].preRequisiteTasks.Count <= 0)
                    {
                        requiredTasks.RemoveAt(j);
                        break;
                    }
                }
            }
        }
        ActivateSetTasks();
    }

    public bool AreRequiredTasksDone()
    {
        bool allTasksDone = false;
        if (requiredTasks.Count <= 0) { return false; }
        for (int i = 0; i < requiredTasks.Count; i++)
        { 
            Debug.Log(requiredTasks[i].minigameName + " " + requiredTasks[i].hasCompleted);
            if (!requiredTasks[i].hasCompleted)
            {
                allTasksDone = false;
               
                Debug.Log("Not all tasks are done");
                break;
            }
            else
            {
                allTasksDone = true;
            }
        }
        return allTasksDone;
    }

    public void CheckIfTasksDone()
    {
        if (AreAllTasksDone())
        {
            Debug.Log("Player wins ");
            Events.OnTasksComplete.Invoke();
        }
        else if (AreRequiredTasksDone())
        {
            // Set new random tasks
            OnTasksDone();
            Debug.Log("All Tasks are done");
        }
    }

    public void OnTasksDone()
    {
        SetRandomTasks();
    }

    public void OnSceneChange()
    {
        //Save the required tasks in the PlayerData 
        if(requiredTasks.Count <= 0) { return; }
        PlayerData playerData = SingletonManager.Get<PlayerData>();
        if (playerData)
        {
            //Refresh the player data required Tasks
            playerData.requiredTasks.Clear();
            foreach (MinigameObject minigame in requiredTasks)
            {
                playerData.requiredTasks.Add(minigame.minigameName);
            }
        }
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }

    public void RestoreSavedRequiredTasks()
    {
        PlayerData playerData = SingletonManager.Get<PlayerData>();
        if (playerData == null) { return; }
        if(playerData.requiredTasks.Count <= 0) { return; }
        //Set the required tasks 
        for (int i = 0; i < playerData.requiredTasks.Count; i++)
        {
            for(int j = 0; j < minigameObjects.Count; j++)
            {
                if (playerData.requiredTasks[i] == minigameObjects[j].minigameName)
                {
                    requiredTasks.Add(minigameObjects[j]);
                    break;
                }
            }


        }
    }

    public bool AreAllTasksDone()
    {
        bool areTasksDone = false;
        if (minigameObjects.Count <= 0) { return false; }
        for (int i = 0; i < minigameObjects.Count; i++) 
        {
            if (!minigameObjects[i].hasCompleted)
            {
                areTasksDone = false;
                break;
            }
            else
            {
                areTasksDone = true;
            }
        
        
        }
        return areTasksDone;

    }

    public void ActivateSetTasks()
    {
        if(minigameObjects.Count <= 0) { return; }
        if(requiredTasks.Count <= 0) { return; }
        for(int i = 0; i < minigameObjects.Count; i++)
        {
            for(int j = 0; j < requiredTasks.Count; j++)
            {
                if (requiredTasks[j] == minigameObjects[i])
                {
                    requiredTasks[j].gameObject.SetActive(true);
                    break;
                }
                else
                {
                    minigameObjects[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

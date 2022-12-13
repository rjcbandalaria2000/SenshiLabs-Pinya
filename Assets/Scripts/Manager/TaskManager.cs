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
   // public List<MinigameObject>     finishedTasks = new();
  
    [Header("Task UI")]
    public GameObject               taskTextPrefab;
    public GameObject               taskListParent;

    private List<GameObject>        taskTexts = new();
    private List<MinigameObject> tempTaskList = new();

    

    private void Awake()
    {
        SingletonManager.Register(this);
        //Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        if (SingletonManager.Get<PlayerData>().hasSaved)
        {
            if (SingletonManager.Get<PlayerData>().requiredTasks.Count > 0)
            {
                RestoreSavedRequiredTasks();
                //ActivateSetTasks();
            }
        }
    }

    public void Initialize()
    {
        CheckIfAllTasksDone();
        
        if(!SingletonManager.Get<PlayerData>().hasSaved)
        {
            SetRandomTasks();
            //ActivateSetTasks();
            //SingletonManager.Get<DayCycle>().ChangeTimePeriod(SingletonManager.Get<DayCycle>().timeIndex);
        }

        CheckIfRequiredTasksDone();
       
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

    public void SetRandomTasks()
    {
        
        //Temporarily store all tasks. Modify to avoid duplicates 
        //List<MinigameObject> tempTaskList = new();

        //Only add to the tempTaskList tasks/minigames that are not yet completed
        for (int i = 0; i < minigameObjects.Count; i++)
        {

            if (!minigameObjects[i].hasCompleted)
            {
                tempTaskList.Add(minigameObjects[i]);
            }
          

        }
        if(tempTaskList.Count <= 0)
        {
            Debug.Log("No more incomplete minigames");
            return;
        }
        // Clear required task list 
        requiredTasks.Clear();
        for (int i = 0; i < maxNumOfTasks; i++)
        {

            int randomTaskIndex = Random.Range(0, tempTaskList.Count);
            Debug.Log("Random Selected task: " + tempTaskList[randomTaskIndex]);
            //Check for duplicates
            if (!CheckForMinigameDuplicateInList(requiredTasks, tempTaskList[randomTaskIndex]))
            {
                requiredTasks.Add(tempTaskList[randomTaskIndex]);
            }



            //Check if the task has pre requisite
            if (tempTaskList[randomTaskIndex].preRequisiteTasks.Count > 0)
            {
                //if there is a pre requisite add it to the required task list
                for (int j = 0; j < tempTaskList[randomTaskIndex].preRequisiteTasks.Count; j++)
                {
                    //Check if it is not yet completed
                    if (!tempTaskList[randomTaskIndex].preRequisiteTasks[j].hasCompleted)
                    {
                        //check if the pre requisite task is already in the required tasks
                        if (!CheckForMinigameDuplicateInList(requiredTasks, tempTaskList[randomTaskIndex].preRequisiteTasks[j]) && j <= tempTaskList[randomTaskIndex].preRequisiteTasks.Count - 1)
                        {
                            //Remove the task with pre requisite
                            requiredTasks.Remove(tempTaskList[randomTaskIndex]);
                            // only add the pre req task if its not in the required task
                            requiredTasks.Add(tempTaskList[randomTaskIndex].preRequisiteTasks[j]);
                            tempTaskList.Remove(tempTaskList[randomTaskIndex].preRequisiteTasks[j]);
                            break;
                        }
                    }
                   
                }
            }
            tempTaskList.RemoveAt(randomTaskIndex);
            //if the temp task list is not enough 
            if (tempTaskList.Count <= 0) { break; }
        }
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

    public void CheckIfRequiredTasksDone()
    {
        if (AreRequiredTasksDone())
        {
            // Set new random tasks
            OnTasksDone();
            SingletonManager.Get<DayCycle>().timeIndex++;
            SingletonManager.Get<DayCycle>().ChangeTimePeriod(SingletonManager.Get<DayCycle>().timeIndex);

            //Save the new time index
            SingletonManager.Get<PlayerData>().savedTimeIndex = SingletonManager.Get<DayCycle>().timeIndex;
            SingletonManager.Get<PlayerData>().savedTimePeriod = SingletonManager.Get<DayCycle>().timePeriod;

            Debug.Log("Required Tasks are done");

        }
        else
        {
            SingletonManager.Get<DayCycle>().ChangeTimePeriod(SingletonManager.Get<DayCycle>().timeIndex);
            Debug.Log("Required Tasks are not yet done");
        }
    }

    public void CheckIfAllTasksDone()
    {
        if (AreAllTasksDone())
        {
            Debug.Log("Player wins ");
            Events.OnTasksComplete.Invoke();
        }
        {
            //SingletonManager.Get<DayCycle>().ChangeTimePeriod(SingletonManager.Get<DayCycle>().timeIndex);
            Debug.Log("All tasks are not yet done");
        }
    }

    public void OnTasksDone()
    {
        SetRandomTasks();
        //ActivateSetTasks();
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
            Debug.Log(minigameObjects[i].minigameName + " is completed?  " + minigameObjects[i].hasCompleted);
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
        Debug.Log("Activating Required Tasks");
    }

    public bool CheckForMinigameDuplicateInList(List<MinigameObject> minigameList, MinigameObject minigameToCheck) {

        bool isDuplicate = false;
        if (minigameList.Count <= 0) { return false; } 
        if(minigameToCheck == null) { return false; }
        foreach(MinigameObject minigame in minigameList)
        {
            if(minigame == minigameToCheck)
            {
                isDuplicate = true;
                Debug.Log(minigameToCheck.minigameName + " is a duplicate");
                return isDuplicate;
                
            }
        }
        if (!isDuplicate)
        {
            Debug.Log("Is not duplicate");
        }
        return isDuplicate;
    }

    public void chechTaskStatus()
    {
        for(int i = 0;i < minigameObjects.Count;i++)
        {
            if (minigameObjects[i].hasCompleted)
            {
                minigameObjects[i].deactivateUnfinishState();
            }
            
        }
    }
}

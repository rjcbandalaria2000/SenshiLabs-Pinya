using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("Minigames")]
    public List<MinigameObject> minigameObjects;

    [Header("Task UI")]
    public GameObject taskTextPrefab;
    public GameObject taskListParent;

    private List<GameObject> taskTexts = new();
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    void Start()
    {
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
        for (int i = 0; i < minigameObjects.Count; i++)
        {
            GameObject spawnedText = Instantiate(taskTextPrefab, taskListParent.transform, false);
            TextMeshProUGUI text = spawnedText.GetComponent<TextMeshProUGUI>();
            if (text)
            {
                text.text = minigameObjects[i].minigameName.ToString();
            }
            taskTexts.Add(spawnedText);
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

}

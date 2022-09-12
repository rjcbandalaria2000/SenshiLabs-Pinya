using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HideSeekManager : MonoBehaviour
{
    public GameObject children;
    public int score;
    public List<GameObject> spawnPoints;
    private int RNG;
    public int count;

    [Header("Scene Change")]
    public string NameOfScene;
    private SceneChange sceneChange;

    private void Start()
    {
        count = 0;
        StartCoroutine(spawn());
    }
    

    IEnumerator spawn()
    {
        for(int i = 0; i < spawnPoints.Count; i++)
        {
            List<GameObject> list = new List<GameObject>();
            list = spawnPoints;

            int randomPoint = Random.Range(0, list.Count);
            GameObject child = Instantiate(children, list[i].transform.position, Quaternion.identity);
            count += 1;
            list.RemoveAt(randomPoint);
           
        }

        yield return null;

    }

    public void checkChildren()
    {
        if(score >= spawnPoints.Count)
        {
            CheckIfFinished();
            Debug.Log("Success");
        }
    }

    public void CheckIfFinished()
    {
        if (count <= 0)
        {
            Debug.Log("Minigame complete");
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfScene);
        }
        else
        {
            Debug.Log("Minigame Fail");
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfScene);
        }
    }

}

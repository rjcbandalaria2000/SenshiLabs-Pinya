using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HideSeekManager : MinigameManager
{
    public GameObject children;
    public int score;
    public List<GameObject> spawnPoints;
    private int RNG;
    public int count;
    Coroutine spawnRoutine;

    private void Start()
    {
        count = 0;
        sceneChange = this.GetComponent<SceneChange>() ;
        spawnRoutine = StartCoroutine(spawn());
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

    public override void CheckIfFinished()
    {
        if (score >= spawnPoints.Count)
        {
            Debug.Log("Minigame complete");
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfNextScene);
        }
       
    }

    public override void OnMinigameLose()
    {
        Debug.Log("Minigame lose");
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }

}

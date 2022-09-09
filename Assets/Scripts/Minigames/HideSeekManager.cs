using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSeekManager : MonoBehaviour
{
    public GameObject children;
    public int score;
    public List<GameObject> spawnPoints;
    private int RNG;

    [Header("Scene Change")]
    public string NameOfScene;

    private void Start()
    {

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

            list.RemoveAt(randomPoint);
           
        }

        yield return null;

    }

    public void checkChildren()
    {
        if(score >= spawnPoints.Count)
        {
            Debug.Log("Success");
        }
    }
  
}

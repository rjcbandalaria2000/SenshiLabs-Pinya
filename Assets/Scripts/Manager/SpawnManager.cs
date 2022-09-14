using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    [Header("Setup Values")]
    public List<GameObject>     ObjectToSpawn = new();
    public List<int>            NumToSpawn = new(); //must be equal to the number of objects to spawn
    public float                SpawnTime = 1f;
    //public List<WaveSpawnScriptableObject> WaveSpawnScripts = new(); // can be utilized later 
    [Header("Spawnpoints")]
    public List<GameObject>     SpawnPoints = new();
    [Header("Box Spawnpoint")]
    public GameObject BoxSpawnPoint;

    [Header("Spawned Objects")]
    public List<GameObject> SpawnedObjects = new();

    private Coroutine timedSpawnRoutine;
    private Coroutine timedBoxSpawnRoutine;
    private Coroutine timedUnlimitedSpawnBoxRoutine;


    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void SpawnRandomNoRepeat()
    {
        if (ObjectToSpawn.Count > 0)
        {
            if(NumToSpawn.Count > 0)
            {
                // Do not repeat the same spawn point
                //Store in separate temp variable
                List<GameObject> tempSpawnPoint = new List<GameObject>();
                tempSpawnPoint = SpawnPoints;
                
                for(int i = 0; i < ObjectToSpawn.Count; i++) //loop how many objects are needed to spawn
                {
                    for (int j = 0; j < NumToSpawn[i]; j++) //how many objects will spawn
                    {
                        int randomSpawnPoint = Random.Range(0, tempSpawnPoint.Count);
                        GameObject spawnedObject = Instantiate(ObjectToSpawn[i], tempSpawnPoint[randomSpawnPoint].transform.position, Quaternion.identity);

                        //Remove already selected spawnpoint from tempList 
                        tempSpawnPoint.RemoveAt(randomSpawnPoint);
                    }
                }
                
                
            }
        }
    }

    public void SpawnAtBoxLocation()
    {
        if(ObjectToSpawn.Count <= 0) { return; }
        if(NumToSpawn.Count <= 0) { return; }
        for (int i = 0; i < ObjectToSpawn.Count; i++) //loop how many objects are needed to spawn
        {
            for (int j = 0; j < NumToSpawn[i]; j++) //how many objects will spawn
            {
                GameObject spawnedObject = Instantiate(ObjectToSpawn[i], GetRandomBoxPosition(), Quaternion.identity);
            }
        }
    }

    public void StartTimedSpawn()
    {
        timedSpawnRoutine = StartCoroutine(TimedSpawn());
    }

    public void StartUnlimitedTimedSpawnBoxSpawn()
    {
        timedUnlimitedSpawnBoxRoutine = StartCoroutine(TimedUnlimitedBoxSpawn());
    }

    public void StopTimedUnlimitedSpawnBox()
    {
        StopCoroutine(timedUnlimitedSpawnBoxRoutine);
    }

    IEnumerator TimedSpawn()
    {
        yield return null; 
    }

    IEnumerator TimedSpawnBoxSpawn()
    {
        if (ObjectToSpawn.Count <= 0) { yield return null; }
        if (NumToSpawn.Count <= 0) { yield return null; }
        for (int i = 0; i < ObjectToSpawn.Count; i++) //loop how many objects are needed to spawn
        {
            for (int j = 0; j < NumToSpawn[i]; j++) //how many objects will spawn
            {
                GameObject spawnedObject = Instantiate(ObjectToSpawn[i], GetRandomBoxPosition(), Quaternion.identity);
                yield return new WaitForSeconds(SpawnTime);
            }
        }
        //yield return null;
    }

    IEnumerator TimedUnlimitedBoxSpawn()
    {
        if (ObjectToSpawn.Count <= 0) { yield return null; }
        if (NumToSpawn.Count <= 0) { yield return null; }
        while (true)
        {
            for (int i = 0; i < ObjectToSpawn.Count; i++) //loop how many objects are needed to spawn
            {

                GameObject spawnedObject = Instantiate(ObjectToSpawn[i], GetRandomBoxPosition(), Quaternion.identity);
                yield return new WaitForSeconds(SpawnTime);

            }
            yield return null;
        }
        //yield return null;
    }

    private Vector2 GetRandomBoxPosition()
    {   
        Vector2 cubeCenter = BoxSpawnPoint.transform.position; 
        Vector2 cubeSize = new();

        BoxCollider2D boxCollider = BoxSpawnPoint.GetComponent<BoxCollider2D>();

        cubeSize.x = BoxSpawnPoint.transform.localScale.x * boxCollider.size.x;
        cubeSize.y = BoxSpawnPoint.transform.localScale.y * boxCollider.size.y;

        Vector2 randomPosition = new Vector2((Random.Range(-cubeSize.x / 2, cubeSize.x / 2)), 
            (Random.Range(-cubeSize.y / 2, cubeSize.y / 2)));

        return cubeCenter + randomPosition; 
    }

    public void SpawnInStaticPositions()
    {
        if(ObjectToSpawn.Count <= 0) { return; }
        if(NumToSpawn.Count <= 0) { return; }
        for (int i = 0; i < ObjectToSpawn.Count; i++) 
        { 
            for(int j = 0; j < NumToSpawn[i]; j++)
            {
                GameObject spawnedObject = Instantiate(ObjectToSpawn[i],
                    SpawnPoints[j].transform.position, Quaternion.identity);
                SpawnedObjects.Add(spawnedObject);
            }
        
        }
    }
}

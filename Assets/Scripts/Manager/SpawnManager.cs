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
    [Header("Set Spawnpoints")]
    public List<GameObject>     SpawnPoints = new();
    [Header("Random Spawnpoints")]
    public List<GameObject>     RandomSpawnLocation = new();
    [Header("Box Spawnpoint")]
    public GameObject BoxSpawnPoint;

    private Coroutine timedSpawnRoutine;
    private Coroutine timedBoxSpawnRoutine;


    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //NumToSpawn[0] = SingletonManager.Get<CleanTheHouseManager>().NumberOfTrash;
        //NumToSpawn[1] = SingletonManager.Get<CleanTheHouseManager>().NumberOfDust;
        //SpawnNoRepeat();
    }

    public void SpawnNoRepeat()
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

    public void StartTimedSpawnBoxSpawn() 
    {
        timedBoxSpawnRoutine = StartCoroutine(TimedSpawnBoxSpawn());
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
        yield return null;
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
}

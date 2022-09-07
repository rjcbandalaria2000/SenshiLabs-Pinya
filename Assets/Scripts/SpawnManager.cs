using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints = new();
    public List<GameObject> ObjectToSpawn = new();
    public List<int> NumToSpawn = new(); //must be equal to the number of objects to spawn
    //public List<WaveSpawnScriptableObject> WaveSpawnScripts = new(); // can be utilized later 

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        NumToSpawn[0] = SingletonManager.Get<CleanTheHouseManager>().NumberOfTrash;
        NumToSpawn[1] = SingletonManager.Get<CleanTheHouseManager>().NumberOfDust;
        Spawn();
    }

    public void Spawn()
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
  
}

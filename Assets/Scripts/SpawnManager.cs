using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints = new();
    public List<GameObject> ObjectToSpawn = new();
    public List<int> NumToSpawn = new();
    public List<WaveSpawnScriptableObject> WaveSpawnScripts = new();

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
                //foreach(GameObject obj in ObjectToSpawn) //to spawn all gameobjects in the list
                //{
                //    foreach (int spawnCount in NumToSpawn) //different spawn count 
                //    {
                //        for (int i = 0; i < spawnCount; i++) //the amount of objects to spawn
                //        {
                //            int randomSpawnPoint = Random.Range(0, tempSpawnPoint.Count);
                //            GameObject spawnedObject = Instantiate(obj, tempSpawnPoint[randomSpawnPoint].transform.position, Quaternion.identity);

                //            //Remove already selected spawnpoint from tempList 
                //            tempSpawnPoint.RemoveAt(randomSpawnPoint);
                //        }
                       
                //    }
                    

                //}
                for(int i = 0; i < ObjectToSpawn.Count; i++)
                {
                    for (int j = 0; j < NumToSpawn[i]; j++)
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

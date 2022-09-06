using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints = new();
    public GameObject ObjectToSpawn;
    public int NumToSpawn = 1;

    // Start is called before the first frame update
    void Start()
    {
        NumToSpawn = SingletonManager.Get<CleanTheHouseManager>().NumberOfTrash;
        Spawn();
    }

    public void Spawn()
    {
        if (ObjectToSpawn)
        {
            if(NumToSpawn > 0)
            {
                // Do not repeat the same spawn point
                //Store in separate temp variable
                List<GameObject> tempSpawnPoint = new List<GameObject>();
                tempSpawnPoint = SpawnPoints;

                for(int i = 0; i < NumToSpawn; i++)
                {
                    int randomSpawnPoint = Random.Range(0, tempSpawnPoint.Count);
                    GameObject spawnedObject = Instantiate(ObjectToSpawn, tempSpawnPoint[randomSpawnPoint].transform.position, Quaternion.identity); 

                    //Remove already selected spawnpoint from tempList 
                    tempSpawnPoint.RemoveAt(randomSpawnPoint);

                }
                
            }
        }
    }
  
}

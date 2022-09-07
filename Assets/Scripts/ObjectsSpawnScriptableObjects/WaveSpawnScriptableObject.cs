using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSpawn", menuName = "ScriptableObjects/WaveSpawn")]
public class WaveSpawnScriptableObject : ScriptableObject
{
    public GameObject ObjectToSpawn;
    public int SpawnCount; 

}

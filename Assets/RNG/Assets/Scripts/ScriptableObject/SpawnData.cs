using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnData : ScriptableObject
{
    public GameObject[] itemTospawn;
    public int minSpawn;
    public int maxSpawn;
}

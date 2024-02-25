using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnData spawnData;
    }
    public GridController grid;
    public RandomSpawner[] spawnData;
    void Start()
    {
        // grid = GetComponent<GridController>();

    }
    public void InitalliseObjectSpawning()
    {
        foreach (RandomSpawner rs in spawnData)
        {
            SpawnObjects(rs);
        }
    }
    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnData.minSpawn, data.spawnData.maxSpawn + 1);
        GameController.instance.numEnemy += randomIteration;
        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject go = Instantiate(data.spawnData.itemTospawn[Random.Range(0, 3)], grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPos);
            Debug.Log("Spawned");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private List<GameObject> enemyPrefabs; 
    [SerializeField] private List<Transform> spawnPoints;   
    [SerializeField] private float spawnInterval = 1f;    

    void Start()
    {
       
    }

    public IEnumerator SpawnEnemies(int totalEnemies, int enemiesPerSpawn)
    {
        int spawned = 0;
        while (spawned < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn && spawned < totalEnemies; i++)
            {
                Transform spawnPoint = spawnPoints[0];
                GameObject enemyGO = EnemyFactory.CreateEnemy(GetRandomEnemyType(), spawnPoint.position);
                if (enemyGO == null) continue;
                spawned++;
                Debug.Log($"Enemy spawned at {spawnPoint.position}");
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private EnemyType GetRandomEnemyType()
    {
        float rand = Random.value;
        if (rand < 0.5f) return EnemyType.Weak;
        if (rand < 0.8f) return EnemyType.Medium;
        return EnemyType.Strong;
    }
}
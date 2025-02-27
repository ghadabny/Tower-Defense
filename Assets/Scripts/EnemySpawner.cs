using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // Assigné dans l'Inspector
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnInterval = 1f;

    private void Awake()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0)
        {
            Debug.LogError("Aucun prefab d'ennemi n'est assigné dans EnemySpawner !");
            return;
        }

        // Associer chaque prefab à un type d'ennemi
        Dictionary<EnemyType, GameObject> prefabsDict = new Dictionary<EnemyType, GameObject>
        {
            { EnemyType.Basic, enemyPrefabs[0] },
            { EnemyType.Fast, enemyPrefabs.Count > 1 ? enemyPrefabs[1] : enemyPrefabs[0] },
            { EnemyType.Tank, enemyPrefabs.Count > 2 ? enemyPrefabs[2] : enemyPrefabs[0] }
        };

        EnemyFactory.Initialize(prefabsDict); // Initialisation de la factory
    }

    public IEnumerator SpawnEnemies(int totalEnemies, int enemiesPerSpawn)
    {
        int spawned = 0;
        while (spawned < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn && spawned < totalEnemies; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                GameObject enemyGO = EnemyFactory.CreateEnemy(EnemyType.Basic, spawnPoint.position);
                if (enemyGO == null) continue;
                spawned++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

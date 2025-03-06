using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private List<GameObject> enemyPrefabs; // Liste des prefabs d'ennemis
    [SerializeField] private List<Transform> spawnPoints;   // Liste des points de spawn
    [SerializeField] private float spawnInterval = 1f;     // Intervalle de spawn

    void Start()
    {
        // D�marre la coroutine pour g�n�rer les ennemis
        //StartCoroutine(SpawnEnemies(10, 2)); // G�n�re 10 ennemis, 2 par vague
    }

    // Coroutine pour g�n�rer les ennemis
    public IEnumerator SpawnEnemies(int totalEnemies, int enemiesPerSpawn)
    {
        int spawned = 0;
        while (spawned < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn && spawned < totalEnemies; i++)
            {
                // Utilise le premier (et seul) point de spawn
                Transform spawnPoint = spawnPoints[0];
                // G�n�re un ennemi � ce point de spawn
                GameObject enemyGO = EnemyFactory.CreateEnemy(GetRandomEnemyType(), spawnPoint.position);
                if (enemyGO == null) continue;
                spawned++;
                Debug.Log($"Enemy spawned at {spawnPoint.position}");
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // M�thode pour choisir un type d'ennemi al�atoire
    private EnemyType GetRandomEnemyType()
    {
        float rand = Random.value;
        if (rand < 0.5f) return EnemyType.Weak;
        if (rand < 0.8f) return EnemyType.Medium;
        return EnemyType.Strong;
    }
}
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Basic,
    Fast,
    Tank
}

public static class EnemyFactory
{
    private static Dictionary<EnemyType, GameObject> enemyPrefabs = new Dictionary<EnemyType, GameObject>();

    public static void Initialize(Dictionary<EnemyType, GameObject> prefabs)
    {
        enemyPrefabs = prefabs;
    }

    public static GameObject CreateEnemy(EnemyType type, Vector3 position)
    {
        if (!enemyPrefabs.ContainsKey(type))
        {
            Debug.LogError("Enemy prefab not found for type: " + type);
            return null;
        }
        return Object.Instantiate(enemyPrefabs[type], position, Quaternion.identity);
    }
}

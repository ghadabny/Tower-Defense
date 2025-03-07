using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Weak,    
    Medium,  
    Strong   
}

public static class EnemyFactory
{
    
    public static GameObject CreateEnemy(EnemyType type, Vector3 position)
    {
        
        GameObject enemyPrefab = LoadEnemyPrefab(type);

        if (enemyPrefab == null)
        {
            Debug.LogError($"Prefab not found for enemy type: {type}");
            return null;
        }

        GameObject enemyInstance = Object.Instantiate(enemyPrefab, position, Quaternion.identity);

        return enemyInstance;
    }


    private static GameObject LoadEnemyPrefab(EnemyType type)
    {
        string prefabPath = $"Prefabs/{type}Enemy";

        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        if (prefab == null)
        {
            Debug.LogError($"Prefab not found at path: {prefabPath}");
        }

        return prefab;
    }
}
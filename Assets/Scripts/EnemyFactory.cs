using UnityEngine;

public enum EnemyType
{
    Basic
    // Future types: Fast, Heavy, etc.
}

public static class EnemyFactory
{
    /// <summary>
    /// Creates and returns an enemy GameObject of the given type at the specified position.
    /// </summary>
    public static GameObject CreateEnemy(EnemyType type, Vector3 position)
    {
        GameObject prefab = null;
        switch (type)
        {
            case EnemyType.Basic:
            default:
                // Assumes a prefab exists at Resources/Prefabs/BasicEnemy
                prefab = Resources.Load<GameObject>("Prefabs/BasicEnemy");
                break;
        }
        if (prefab != null)
        {
            GameObject enemy = Object.Instantiate(prefab, position, Quaternion.identity);
            enemy.tag = "Enemy"; // Ensure enemy is tagged if needed.
            return enemy;
        }
        else
        {
            Debug.LogError("Enemy prefab not found for type: " + type);
            return null;
        }
    }
}

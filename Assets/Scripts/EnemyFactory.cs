using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Weak,    // Ennemi faible 🟢
    Medium,  // Ennemi moyen 🟡
    Strong   // Ennemi fort 🔴
}

public static class EnemyFactory
{
    /// <summary>
    /// Crée un ennemi en fonction du type spécifié.
    /// </summary>
    /// <param name="type">Le type d'ennemi à créer.</param>
    /// <param name="position">La position où l'ennemi doit être instancié.</param>
    /// <returns>L'instance de l'ennemi créé, ou null si le type n'est pas supporté.</returns>
    public static GameObject CreateEnemy(EnemyType type, Vector3 position)
    {
        // Charger le prefab correspondant au type d'ennemi
        GameObject enemyPrefab = LoadEnemyPrefab(type);

        // Si le prefab n'est pas trouvé, retourner null
        if (enemyPrefab == null)
        {
            Debug.LogError($"Prefab not found for enemy type: {type}");
            return null;
        }

        // Instancier l'ennemi à la position donnée
        GameObject enemyInstance = Object.Instantiate(enemyPrefab, position, Quaternion.identity);

        // Retourner l'instance de l'ennemi
        return enemyInstance;
    }

    /// <summary>
    /// Charge le prefab correspondant au type d'ennemi.
    /// </summary>
    /// <param name="type">Le type d'ennemi.</param>
    /// <returns>Le prefab de l'ennemi, ou null si non trouvé.</returns>
    private static GameObject LoadEnemyPrefab(EnemyType type)
    {
        // Chemin des prefabs dans le dossier Resources
        string prefabPath = $"Prefabs/{type}Enemy";

        // Charger le prefab depuis les ressources
        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        // Si le prefab n'est pas trouvé, log une erreur
        if (prefab == null)
        {
            Debug.LogError($"Prefab not found at path: {prefabPath}");
        }

        return prefab;
    }
}
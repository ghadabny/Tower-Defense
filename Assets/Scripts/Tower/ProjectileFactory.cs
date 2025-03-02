using UnityEngine;
using System.Collections.Generic;

public enum ProjectileType
{
    Arrow,
    Fireball,
    Rock
}

public class ProjectileFactory : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject fireballPrefab;
    public GameObject rockPrefab;

    private Dictionary<ProjectileType, GameObject> prefabDictionary;

    private void Awake()
    {
        prefabDictionary = new Dictionary<ProjectileType, GameObject>();

        if (arrowPrefab != null)
            prefabDictionary[ProjectileType.Arrow] = arrowPrefab;
        else
            Debug.LogError("ProjectileFactory: Arrow prefab is missing!");

        if (fireballPrefab != null)
            prefabDictionary[ProjectileType.Fireball] = fireballPrefab;
        else
            Debug.LogError("ProjectileFactory: Fireball prefab is missing!");

        if (rockPrefab != null)
            prefabDictionary[ProjectileType.Rock] = rockPrefab;
        else
            Debug.LogError("ProjectileFactory: Rock prefab is missing!");
    }

    public GameObject GetProjectilePrefab(ProjectileType type)
    {
        if (prefabDictionary.ContainsKey(type))
        {
            return prefabDictionary[type];
        }
        else
        {
            Debug.LogError($"ProjectileFactory: No prefab found for {type}!");
            return null;
        }
    }
}

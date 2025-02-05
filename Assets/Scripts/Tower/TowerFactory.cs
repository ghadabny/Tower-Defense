using UnityEngine;
using System.Collections.Generic;

public enum TowerType
{
    CastleTower,
    KnightPost,
    ArcherTower
}

public class TowerFactory : MonoBehaviour
{
    // Assign these prefab references in the Inspector.
    public GameObject castleTowerPrefab;
    public GameObject knightPostPrefab;
    public GameObject archerTowerPrefab;

    private Dictionary<TowerType, GameObject> prefabDictionary;

    private void Awake()
    {
        // Build a dictionary of tower prefabs from the Inspector.
        prefabDictionary = new Dictionary<TowerType, GameObject>();
        if (castleTowerPrefab != null)
            prefabDictionary.Add(TowerType.CastleTower, castleTowerPrefab);
        else
            Debug.LogWarning("CastleTower prefab not assigned in TowerFactory!");

        if (knightPostPrefab != null)
            prefabDictionary.Add(TowerType.KnightPost, knightPostPrefab);
        else
            Debug.LogWarning("KnightPost prefab not assigned in TowerFactory!");

        if (archerTowerPrefab != null)
            prefabDictionary.Add(TowerType.ArcherTower, archerTowerPrefab);
        else
            Debug.LogWarning("ArcherTower prefab not assigned in TowerFactory!");
    }

    /// <summary>
    /// Creates and returns an instantiated tower of the given type at the specified position.
    /// The prefabs are assigned via the Inspector, so no file paths are used.
    /// </summary>
    public GameObject CreateTower(TowerType type, Vector3 position)
    {
        if (prefabDictionary != null && prefabDictionary.ContainsKey(type))
        {
            GameObject prefab = prefabDictionary[type];
            // Instantiate the prefab so that you get a scene instance.
            GameObject towerInstance = Instantiate(prefab, position, Quaternion.identity);
            towerInstance.tag = "Tower";  // Tag the tower for detection by the grid.
            Debug.Log("Instantiated tower: " + towerInstance.name + " at " + position);
            return towerInstance;
        }
        else
        {
            Debug.LogError("Tower prefab not found for type: " + type);
            return null;
        }
    }
}

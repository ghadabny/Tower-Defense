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
    public GameObject castleTowerPrefab;
    public GameObject knightPostPrefab;
    public GameObject archerTowerPrefab;
    public ProjectileFactory projectileFactory; 

    private Dictionary<TowerType, GameObject> prefabDictionary;

    private void Awake()
    {
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

    public GameObject CreateTower(TowerType type, Vector3 position)
    {
        if (prefabDictionary.TryGetValue(type, out GameObject prefab))
        {
            GameObject towerInstance = Instantiate(prefab, position, Quaternion.identity);
            towerInstance.tag = "Tower";

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

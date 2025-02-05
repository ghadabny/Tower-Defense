using UnityEngine;


public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;  // Tower prefab reference
    [SerializeField]
    private Sprite dragSprite;
    [SerializeField]
    private int towerPrice;
    // Add this field to indicate the type of tower this button creates.
    [SerializeField]
    private TowerType towerType;

    public int TowerPrice => towerPrice;
    public Sprite DragSprite => dragSprite;
    public GameObject TowerPrefab => towerPrefab;
    public TowerType TowerType => towerType;
}

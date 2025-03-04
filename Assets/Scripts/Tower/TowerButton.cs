using UnityEngine;


public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab; 
    [SerializeField]
    private Sprite dragSprite;
    [SerializeField]
    private int towerPrice;
    [SerializeField]
    private TowerType towerType;

    public int TowerPrice => towerPrice;
    public Sprite DragSprite => dragSprite;
    public GameObject TowerPrefab => towerPrefab;
    public TowerType TowerType => towerType;
}

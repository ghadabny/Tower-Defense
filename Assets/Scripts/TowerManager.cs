using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager>
{
    public TowerButton towerBtnPressed { get; set; }
    public List<GameObject> TowerList = new List<GameObject>();
    public List<Collider2D> BuildList = new List<Collider2D>();

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // If the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("BuildSite"))
            {
                // Mark the build site as used.
                hit.collider.tag = "BuildSiteFull";
                RegisterBuildSite(hit.collider);
                PlaceTower(hit);
            }
        }
        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    // Helper method to snap a raw position to the center of a grid cell (assumes 1x1 units).
    private Vector3 GetSnappedPosition(Vector3 rawPosition)
    {
        float snappedX = Mathf.Floor(rawPosition.x) + 0.5f;
        float snappedY = Mathf.Floor(rawPosition.y) + 0.5f;
        return new Vector3(snappedX, snappedY, rawPosition.z);
    }

    private void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            Vector3 position = GetSnappedPosition(hit.transform.position);

            // Get the TowerFactory from the scene.
            TowerFactory factory = FindObjectOfType<TowerFactory>();
            if (factory != null)
            {
                // Use the TowerButton's TowerType property.
                GameObject newTower = factory.CreateTower(towerBtnPressed.TowerType, position);

                if (newTower != null)
                {
                    TowerList.Add(newTower);
                    EconomyManager.Instance.SubtractMoney(towerBtnPressed.TowerPrice);
                    GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.BuildTower);

                    Grid grid = FindObjectOfType<Grid>();
                    if (grid != null)
                    {
                        grid.RecalculateGrid();
                    }
                    ObstacleEvents.NotifyObstaclesUpdated();
                }
            }
            disableDragSprite();
        }
    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }

    public void RenameTagsBuildSites()
    {
        foreach (Collider2D buildTag in BuildList)
        {
            buildTag.tag = "BuildSite";
        }
        BuildList.Clear();
    }

    public void DestroyAllTowers()
    {
        foreach (GameObject tower in new List<GameObject>(TowerList))
        {
            if (tower != null && tower.scene.IsValid())
            {
                Destroy(tower);
            }
        }
        TowerList.Clear();
    }

    public void selectedTower(TowerButton towerBtn)
    {
        if (towerBtn.TowerPrice <= EconomyManager.Instance.TotalMoney)
        {
            towerBtnPressed = towerBtn;
            enableDragSprite(towerBtn.DragSprite);
        }
    }

    public void buyTower(int price)
    {
        EconomyManager.Instance.SubtractMoney(price);
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.BuildTower);
    }

    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void enableDragSprite(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void disableDragSprite()
    {
        spriteRenderer.enabled = false;
        towerBtnPressed = null;
    }
}

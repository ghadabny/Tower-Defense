/*using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager>
{

    public TowerButton towerBtnPressed { get; set; }
    public List<GameObject> TowerList = new List<GameObject>();
    public List<Collider2D> BuildList = new List<Collider2D>();

    private SpriteRenderer spriteRenderer;
    private Collider2D buildTile;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
    }

    void Update()
    {
        //If the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider.tag == "BuildSite")
            {
                // hit.collider.tag = "BuildSiteFull";
                buildTile = hit.collider;
                buildTile.tag = "BuildSiteFull";
                RegisterBuildSite(buildTile);
                placeTower(hit);
            }
        }
        if (spriteRenderer.enabled)
        {
            followMouse();
        }
    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        // site.collider.tag = "BuildSiteFull";
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

    public void RegisterTower(GameObject tower)
    {
        TowerList.Add(tower);
    }

    public void DestroyAllTowers()
    {
        foreach (GameObject tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void placeTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            RegisterTower(newTower);
            buyTower(towerBtnPressed.TowerPrice);
            disableDragSprite();
        }
    }

    public void selectedTower(TowerButton towerBtn)
    {
        if (towerBtn.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            towerBtnPressed = towerBtn;
            enableDragSprite(towerBtn.DragSprite);
        }
    }

    public void buyTower(int price)
    {
        GameManager.Instance.subtractMoney(price);
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.BuildTower);
    }

    private void followMouse()
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
*/
/*
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;

public class TowerManager : Singleton<TowerManager>
{

    public TowerButton towerBtnPressed { get; set; }
    public List<GameObject> TowerList = new List<GameObject>();
    public List<Collider2D> BuildList = new List<Collider2D>();

    private SpriteRenderer spriteRenderer;
    private Collider2D buildTile;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
    }

    void Update()
    {
        //If the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider.tag == "BuildSite")
            {
                // hit.collider.tag = "BuildSiteFull";
                buildTile = hit.collider;
                buildTile.tag = "BuildSiteFull";
                RegisterBuildSite(buildTile);
                placeTower(hit);
            }
        }
        if (spriteRenderer.enabled)
        {
            followMouse();
        }
    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        // site.collider.tag = "BuildSiteFull";
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

    public void RegisterTower(GameObject tower)
    {
        TowerList.Add(tower);
    }

    public void DestroyAllTowers()
    {
        foreach (GameObject tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }
    private void placeTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;

            hit.collider.tag = "BuildSiteFull";
            NotifyEnemiesToRecalculatePaths();
            RegisterTower(newTower);
            buyTower(towerBtnPressed.TowerPrice);
            disableDragSprite();
        }
    }
    

    //added
    /*private void placeTower(RaycastHit2D hit)
    {
        GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
        newTower.transform.position = hit.transform.position;
        TowerList.Add(newTower);

        FindObjectOfType<Grid>().UpdateNodeWalkability(newTower.transform.position, false);
        NotifyEnemiesToRecalculatePaths();
    }*/


/* private void NotifyEnemiesToRecalculatePaths()
 {
     foreach (Enemy enemy in FindObjectsOfType<Enemy>())
     {
         enemy.RecalculatePath();
     }
 }*/
//added

/*private void NotifyEnemiesToRecalculatePaths()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.CalculatePath();
        }
    }

    public void selectedTower(TowerButton towerBtn)
    {
        if (towerBtn.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            towerBtnPressed = towerBtn;
            enableDragSprite(towerBtn.DragSprite);
        }
    }

    public void buyTower(int price)
    {
        GameManager.Instance.subtractMoney(price);
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.BuildTower);
    }

    private void followMouse()
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
*/


/*

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager>
{
    public TowerButton towerBtnPressed { get; set; }
    public List<GameObject> TowerList = new List<GameObject>();
    public List<Collider2D> BuildList = new List<Collider2D>();

    private SpriteRenderer spriteRenderer;
    private Collider2D buildTile;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
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
                buildTile = hit.collider;
                buildTile.tag = "BuildSiteFull";
                RegisterBuildSite(buildTile);
                placeTower(hit);
            }
        }
        if (spriteRenderer.enabled)
        {
            followMouse();
        }
    }

    // Helper method to snap a raw position to the center of a grid cell.
    private Vector3 GetSnappedPosition(Vector3 rawPosition)
    {
        // Assumes each tile is 1 unit wide and high.
        float snappedX = Mathf.Floor(rawPosition.x) + 0.5f;
        float snappedY = Mathf.Floor(rawPosition.y) + 0.5f;
        return new Vector3(snappedX, snappedY, rawPosition.z);
    }

    // NEW: Updates all grid nodes that are overlapped by the tower's collider.
    private void UpdateGridForTower(GameObject tower)
    {
        Grid grid = FindObjectOfType<Grid>();
        if (grid == null)
        {
            Debug.LogError("Grid not found!");
            return;
        }

        // Get the tower's BoxCollider2D component.
        BoxCollider2D towerCollider = tower.GetComponent<BoxCollider2D>();
        if (towerCollider == null)
        {
            Debug.LogError("Tower does not have a BoxCollider2D!");
            return;
        }

        // Get the world-space bounds of the tower's collider.
        Bounds bounds = towerCollider.bounds;

        // Iterate through all nodes in the grid.
        for (int x = 0; x < grid.grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.grid.GetLength(1); y++)
            {
                Node node = grid.grid[x, y];
                // If the node's center is within the bounds, mark it as unwalkable.
                if (bounds.Contains(node.worldPosition))
                {
                    node.walkable = false;
                }
            }
        }
    }

    private void placeTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            // Snap the tower's position to the grid center.
            newTower.transform.position = GetSnappedPosition(hit.transform.position);

            // Update all grid nodes overlapped by the tower's collider.
            UpdateGridForTower(newTower);

            hit.collider.tag = "BuildSiteFull";
            RegisterTower(newTower);
            buyTower(towerBtnPressed.TowerPrice);

            // Notify enemies to recalculate their paths.
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.RecalculatePath();
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

    public void RegisterTower(GameObject tower)
    {
        TowerList.Add(tower);
    }

    public void DestroyAllTowers()
    {
        foreach (GameObject tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void selectedTower(TowerButton towerBtn)
    {
        if (towerBtn.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            towerBtnPressed = towerBtn;
            enableDragSprite(towerBtn.DragSprite);
        }
    }

    public void buyTower(int price)
    {
        GameManager.Instance.subtractMoney(price);
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.BuildTower);
    }

    private void followMouse()
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
*/


using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager>
{
    public TowerButton towerBtnPressed { get; set; }
    public List<GameObject> TowerList = new List<GameObject>();
    public List<Collider2D> BuildList = new List<Collider2D>();

    private SpriteRenderer spriteRenderer;
    private Collider2D buildTile;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
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
                buildTile = hit.collider;
                buildTile.tag = "BuildSiteFull";
                RegisterBuildSite(buildTile);
                placeTower(hit);
            }
        }
        if (spriteRenderer.enabled)
        {
            followMouse();
        }
    }

    // Helper method to snap a raw position to the center of a grid cell.
    private Vector3 GetSnappedPosition(Vector3 rawPosition)
    {
        float snappedX = Mathf.Floor(rawPosition.x) + 0.5f;
        float snappedY = Mathf.Floor(rawPosition.y) + 0.5f;
        return new Vector3(snappedX, snappedY, rawPosition.z);
    }

    private void placeTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            // Snap tower's position to grid center.
            newTower.transform.position = GetSnappedPosition(hit.transform.position);
            // Tag the new tower so that the grid can find it.
            newTower.tag = "Tower";
            RegisterTower(newTower);
            buyTower(towerBtnPressed.TowerPrice);

            // Recalculate the grid (it scans for all objects tagged "Tower").
            Grid grid = FindObjectOfType<Grid>();
            if (grid != null)
            {
                grid.RecalculateGrid();
            }

            // Notify all enemies to recalculate their paths.
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.RecalculatePath();
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

    public void RegisterTower(GameObject tower)
    {
        TowerList.Add(tower);
    }

    public void DestroyAllTowers()
    {
        foreach (GameObject tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void selectedTower(TowerButton towerBtn)
    {
        if (towerBtn.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            towerBtnPressed = towerBtn;
            enableDragSprite(towerBtn.DragSprite);
        }
    }

    public void buyTower(int price)
    {
        GameManager.Instance.subtractMoney(price);
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.BuildTower);
    }

    private void followMouse()
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




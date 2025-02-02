using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    // Define grid world size (in world units) and node size.
    public Vector2 gridWorldSize = new Vector2(20, 11);
    public float nodeRadius = 0.5f; // Node diameter will be 1 unit.
    public Node[,] grid;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    // CreateGrid builds the grid and marks nodes as walkable unless a tower overlaps.
    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        // Compute the bottom‐left corner (assume the Grid GameObject is centered)
        Vector3 worldBottomLeft = transform.position - new Vector3(gridWorldSize.x / 2, gridWorldSize.y / 2, 0);

        // Find all obstacles (towers) by tag.
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Tower");

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Compute the center of this node.
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                                                      + Vector3.up * (y * nodeDiameter + nodeRadius);

                // Assume the node is walkable unless an obstacle covers its center.
                bool walkable = true;
                foreach (GameObject obs in obstacles)
                {
                    Collider2D col = obs.GetComponent<Collider2D>();
                    if (col != null && col.bounds.Contains(worldPoint))
                    {
                        walkable = false;
                        break;
                    }
                }

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    // Call this method to rebuild the grid when obstacles (towers) change.
    public void RecalculateGrid()
    {
        CreateGrid();
    }

    public Node GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }

    void OnDrawGizmos()
    {
        // Draw the grid outline.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                // White for walkable, red for unwalkable.
                Gizmos.color = node.walkable ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}

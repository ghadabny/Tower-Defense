using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int gCost, hCost;
    public Node parent;

    public int fCost { get { return gCost + hCost; } }

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        // Force z = 0 so that nodes lie in the 2D plane.
        this.worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0);
        this.gridX = gridX;
        this.gridY = gridY;
    }
}

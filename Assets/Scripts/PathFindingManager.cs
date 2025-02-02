using UnityEngine;
using System.Collections.Generic;

public class PathfindingManager : MonoBehaviour
{
    public Grid grid;
    private Pathfinding pathfinding;

    void Awake()
    {
        if (grid == null)
        {
            grid = FindObjectOfType<Grid>();
        }
        pathfinding = new Pathfinding(grid);
    }

    public List<Node> FindPath(Vector3 start, Vector3 target)
    {
        return pathfinding.FindPath(start, target);
    }
}

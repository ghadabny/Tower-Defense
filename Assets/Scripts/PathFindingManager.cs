using UnityEngine;
using System.Collections.Generic;

public class PathfindingManager : MonoBehaviour
{
    public Grid grid;
    private IPathfinder pathfinder;

    void Awake()
    {
        if (grid == null)
        {
            grid = FindObjectOfType<Grid>();
        }
        pathfinder = new Pathfinding(grid); 
    }

    public List<Node> FindPath(Vector3 start, Vector3 target)
    {
        return pathfinder.FindPath(start, target);
    }
}

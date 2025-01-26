/*using UnityEngine;
using System.Collections.Generic;

public class PathfindingManager : MonoBehaviour
{
    public Grid grid;
    private Pathfinding pathfinding;

    void Awake()
    {
        if (grid == null)
        {
            grid = GetComponent<Grid>();
        }

        if (grid == null)
        {
            Debug.LogError("Grid not found on PathfindingManager!");
        }

        pathfinding = new Pathfinding(grid);
    }

    public List<Node> FindPath(Vector3 start, Vector3 target)
    {
        return pathfinding.FindPath(start, target);
    }
}
*/

using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public Grid grid;
    private Pathfinding pathfinding;

    void Awake()
    {
        pathfinding = new Pathfinding(grid);
    }

    public List<Node> FindPath(Vector3 start, Vector3 target)
    {
        return pathfinding.FindPath(start, target);
    }
}

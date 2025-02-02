using System.Collections.Generic;
using UnityEngine;

public interface IPathfinder
{
    List<Node> FindPath(Vector3 start, Vector3 target);
}

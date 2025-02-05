using System;

public static class ObstacleEvents
{
    public static event Action OnObstaclesUpdated;

    public static void NotifyObstaclesUpdated()
    {
        OnObstaclesUpdated?.Invoke();
    }
}

using UnityEngine;

public class CastleTower : Tower
{
    public int Cost { get; private set; } = 10;

    protected override void Start()
    {
        // Set defaults for CastleTower.
        timeBetweenAttacks = 2f;
        attackRadius = 4f;

        // The projectilePrefab (rock) must be assigned in the Inspector.
        if (projectilePrefab == null)
        {
            Debug.LogError("CastleTower: ProjectilePrefab (rock) is not assigned in the Inspector!");
        }
        base.Start();
    }
}

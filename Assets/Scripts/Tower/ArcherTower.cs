using UnityEngine;

public class ArcherTower : Tower
{
    public int Cost { get; private set; } = 5;

    protected override void Start()
    {
        // Set defaults for ArcherTower.
        timeBetweenAttacks = 1f;
        attackRadius = 2f;

        // The projectilePrefab (rock) must be assigned in the Inspector.
        if (projectilePrefab == null)
        {
            Debug.LogError("ArcherTower: ProjectilePrefab (rock) is not assigned in the Inspector!");
        }
        base.Start();
    }
}

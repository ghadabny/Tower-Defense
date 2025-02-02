using UnityEngine;

public class KnightPost : Tower
{
    public int Cost { get; private set; } = 15;

    protected override void Start()
    {
        // Set defaults for KnightPost.
        timeBetweenAttacks = 2f;
        attackRadius = 4f;

        // The projectilePrefab (fireball) must be assigned in the Inspector.
        if (projectilePrefab == null)
        {
            Debug.LogError("KnightPost: ProjectilePrefab (fireball) is not assigned in the Inspector!");
        }
        base.Start();
    }
}

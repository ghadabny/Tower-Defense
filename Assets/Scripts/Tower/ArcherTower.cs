using UnityEngine;

public class ArcherTower : Tower
{
    public int Cost { get; private set; } = 5;

    protected override void Start()
    {
        timeBetweenAttacks = 0.5f;
        attackRadius = 4f;
        projectileType = ProjectileType.Arrow;
        base.Start();
    }
}

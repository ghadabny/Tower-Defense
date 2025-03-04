using UnityEngine;

public class CastleTower : Tower
{
    public int Cost { get; private set; } = 10;

    protected override void Start()
    {
        timeBetweenAttacks = 2f;
        attackRadius = 50f;
        projectileType = ProjectileType.Rock;
        base.Start();
    }
}

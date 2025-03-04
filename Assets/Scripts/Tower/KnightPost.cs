using UnityEngine;

public class KnightPost : Tower
{
    public int Cost { get; private set; } = 15;

    protected override void Start()
    {
        timeBetweenAttacks = 1.5f;
        attackRadius = 5f;
        projectileType = ProjectileType.Fireball;
        base.Start();
    }
}

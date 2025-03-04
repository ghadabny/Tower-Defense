using UnityEngine;

public class RockProjectile : Projectile
{
    public override ProjectileType ProjectileType => ProjectileType.Rock;

    protected override void Awake()
    {
        base.Awake();
        speed = 5f;
        attackStrength = 15;
        PlayProjectileSound();
    }
}

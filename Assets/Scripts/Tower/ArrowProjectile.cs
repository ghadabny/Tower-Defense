using UnityEngine;

public class ArrowProjectile : Projectile
{
    public override ProjectileType ProjectileType => ProjectileType.Arrow;

    protected override void Awake()
    {
        base.Awake();
        speed = 15f;
        attackStrength = 10;
        PlayProjectileSound();
    }
}

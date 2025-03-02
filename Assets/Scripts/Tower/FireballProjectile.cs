using UnityEngine;

public class FireballProjectile : Projectile
{
    public override ProjectileType ProjectileType => ProjectileType.Fireball;

    protected override void Awake()
    {
        base.Awake();
        speed = 8f;
        attackStrength = 20;
        PlayProjectileSound();
    }
}

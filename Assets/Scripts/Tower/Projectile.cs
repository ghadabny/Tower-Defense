using UnityEngine;

public enum proType
{
    Rock,
    Arrow,
    Fireball
};

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int attackStrength;
    [SerializeField]
    private proType projectileType;

    public int AttackStrength => attackStrength;
    public proType ProjectileType => projectileType;
}

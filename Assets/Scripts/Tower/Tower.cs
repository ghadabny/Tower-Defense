using UnityEngine;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected ProjectileType projectileType;

    private ProjectileFactory projectileFactory; 

    protected Enemy targetEnemy = null;
    protected float attackCounter;

    protected virtual void Start()
    {
        projectileFactory = FindObjectOfType<ProjectileFactory>(); 

        if (projectileFactory == null)
        {
            Debug.LogError("Tower: No ProjectileFactory found in the scene!");
        }

        ObstacleEvents.NotifyObstaclesUpdated();
    }

    protected virtual void Update()
    {
        attackCounter -= Time.deltaTime;

        if (targetEnemy == null || targetEnemy.IsDead)
        {
            targetEnemy = GetNearestEnemyInRange();
        }

        if (targetEnemy != null && attackCounter <= 0f)
        {
            Attack();
            attackCounter = timeBetweenAttacks;
        }
    }

    protected virtual void Attack()
    {
        if (targetEnemy == null)
        {
            return; 
        }

        if (projectileFactory == null)
        {
            Debug.LogError($"Tower {gameObject.name} has no ProjectileFactory assigned!");
            return;
        }

        
        GameObject projectilePrefab = projectileFactory.GetProjectilePrefab(projectileType);
        if (projectilePrefab == null)
        {
            Debug.LogError($"Tower {gameObject.name}: No prefab found for {projectileType}");
            return;
        }

      
        GameObject projInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projScript = projInstance.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.SetTarget(targetEnemy); 
        }
    }

    protected virtual Enemy GetNearestEnemyInRange()
    {
        Enemy nearest = null;
        float minDist = float.MaxValue;

        foreach (Enemy enemy in EnemyManager.Instance.EnemyList)
        {
            if (enemy != null && !enemy.IsDead)
            {
                float dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist < minDist && dist <= attackRadius)
                {
                    minDist = dist;
                    nearest = enemy;
                }
            }
        }
        return nearest;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    protected float timeBetweenAttacks;
    [SerializeField]
    protected float attackRadius;
    [SerializeField]
    protected Projectile projectilePrefab;  // Must be assigned in the Inspector for each concrete tower

    protected AudioSource audioSource;
    protected bool isAttacking = false;
    protected Enemy targetEnemy = null;
    protected float attackCounter;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // When the tower is placed, notify the system that obstacles changed.
        ObstacleEvents.NotifyObstaclesUpdated();
    }

    protected virtual void Update()
    {
        attackCounter -= Time.deltaTime;

        if (targetEnemy == null || targetEnemy.IsDead)
        {
            targetEnemy = GetNearestEnemyInRange();
        }

        if (targetEnemy != null)
        {
            if (attackCounter <= 0f)
            {
                isAttacking = true;
                attackCounter = timeBetweenAttacks;
            }
            else
            {
                isAttacking = false;
            }

            if (Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRadius)
            {
                targetEnemy = null;
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (isAttacking)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (targetEnemy == null)
            return;

        // Instantiate the projectile (the concrete prefab is assigned in the Inspector).
        Projectile proj = Instantiate(projectilePrefab);
        proj.transform.position = transform.position;
        PlayProjectileSound(proj.ProjectileType);
        StartCoroutine(MoveProjectile(proj));
    }

    protected virtual IEnumerator MoveProjectile(Projectile proj)
    {
        while (proj != null && targetEnemy != null &&
               Vector2.Distance(transform.position, targetEnemy.transform.position) > 0.2f)
        {
            Vector3 direction = targetEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            proj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            proj.transform.position = Vector2.MoveTowards(proj.transform.position, targetEnemy.transform.position, 5f * Time.deltaTime);
            yield return null;
        }
        if (proj != null)
        {
            Destroy(proj.gameObject);
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

    protected virtual void PlayProjectileSound(proType type)
    {
        if (audioSource == null)
            return;
        switch (type)
        {
            case proType.Arrow:
                audioSource.PlayOneShot(SoundManager.Instance.Arrow);
                break;
            case proType.Fireball:
                audioSource.PlayOneShot(SoundManager.Instance.Fireball);
                break;
            case proType.Rock:
                audioSource.PlayOneShot(SoundManager.Instance.Rock);
                break;
        }
    }
}

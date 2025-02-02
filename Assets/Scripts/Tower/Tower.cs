/*using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tower : MonoBehaviour
{

    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private Projectile projectile;
    private bool isAttack = false;
    private Enemy targetEnemy = null;
    private float attackCounter;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Update()
    {
        attackCounter -= Time.deltaTime;
        if (targetEnemy == null || targetEnemy.IsDead)
        {
            Enemy nearestEnemy = GetNearestEnemyInRange();
            if (nearestEnemy != null && Vector2.Distance(transform.position, nearestEnemy.transform.position) <= attackRadius)
            {
                targetEnemy = nearestEnemy;
            }
        }
        else
        {
            if (attackCounter <= 0f)
            {
                isAttack = true;
                // Reset attack counter
                attackCounter = timeBetweenAttacks;
            }
            else
            {
                isAttack = false;
            }
            if (Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRadius)
            {
                targetEnemy = null;
            }
        }
    }

    void FixedUpdate()
    {
        if (isAttack)
        {
            Attack();
        }
    }

    public void Attack()
    {
        isAttack = false;
        Projectile newProjectile = Instantiate(projectile) as Projectile;
        newProjectile.transform.localPosition = transform.localPosition;
        if (newProjectile.ProjectileType == proType.arrow)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Arrow);
        }
        else if (newProjectile.ProjectileType == proType.fireball)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Fireball);
        }
        else if (newProjectile.ProjectileType == proType.rock)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Rock);
        }
        if (targetEnemy == null)
        {
            Destroy(newProjectile);
        }
        else
        {
            StartCoroutine(MoveProjectile(newProjectile));
        }
    }

    IEnumerator MoveProjectile(Projectile projectile)
    {
        while (getTargetDistance(targetEnemy) > 0.20f && projectile != null && targetEnemy != null)
        {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }

        if (projectile != null || targetEnemy == null)
        {
            Destroy(projectile);
        }
    }

    private float getTargetDistance(Enemy thisEnemy)
    {
        if (thisEnemy == null)
        {
            thisEnemy = GetNearestEnemyInRange();
            if (thisEnemy == null)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius && !enemy.IsDead)
            {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach (Enemy enemy in GetEnemiesInRange())
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance)
            {
                smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}*/

/*using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private Projectile projectile;

    private bool isAttack = false;
    private Enemy targetEnemy = null;
    private float attackCounter;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Update()
    {
        attackCounter -= Time.deltaTime;

        // Check if the current target is null or dead
        if (targetEnemy == null || targetEnemy.IsDead)
        {
            targetEnemy = GetNearestEnemyInRange();
        }

        if (targetEnemy != null)
        {
            // If attack cooldown is over, start an attack
            if (attackCounter <= 0f)
            {
                isAttack = true;
                attackCounter = timeBetweenAttacks; // Reset attack counter
            }
            else
            {
                isAttack = false;
            }

            // If the target moves out of range, reset the target
            if (Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRadius)
            {
                targetEnemy = null;
            }
        }
    }

    void FixedUpdate()
    {
        if (isAttack)
        {
            Attack();
        }
    }

    public void Attack()
    {
        isAttack = false;

        if (targetEnemy == null)
        {
            return; // Avoid creating a projectile if there is no target
        }

        Projectile newProjectile = Instantiate(projectile);
        newProjectile.transform.localPosition = transform.localPosition;

        // Play sound based on projectile type
        if (newProjectile.ProjectileType == proType.arrow)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Arrow);
        }
        else if (newProjectile.ProjectileType == proType.fireball)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Fireball);
        }
        else if (newProjectile.ProjectileType == proType.rock)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Rock);
        }

        StartCoroutine(MoveProjectile(newProjectile));
    }

    IEnumerator MoveProjectile(Projectile projectile)
    {
        while (projectile != null && targetEnemy != null && GetTargetDistance(targetEnemy) > 0.20f)
        {
            Vector3 direction = targetEnemy.transform.localPosition - transform.localPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);

            yield return null;
        }

        // Destroy the projectile if it reaches the target or if the target is destroyed
        if (projectile != null)
        {
            Destroy(projectile.gameObject);
        }
    }

    private float GetTargetDistance(Enemy thisEnemy)
    {
        if (thisEnemy == null || thisEnemy.IsDead)
        {
            return float.PositiveInfinity; // Return a high value if the enemy is null or dead
        }
        return Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition);
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();

        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            if (enemy != null && !enemy.IsDead && Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange;
    }


    private Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;

        foreach (Enemy enemy in GetEnemiesInRange())
        {
            if (enemy != null && !enemy.IsDead)
            {
                float distance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
*/

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private Projectile projectile;

    private bool isAttack = false;
    private Enemy targetEnemy = null;
    private float attackCounter;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Update grid to mark this tower's position as unwalkable
        Grid grid = FindObjectOfType<Grid>();
        if (grid != null)
        {
            //grid.UpdateNodeWalkability(transform.position, false);
            NotifyEnemiesToRecalculatePaths();
        }
    }

    void Update()
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
                isAttack = true;
                attackCounter = timeBetweenAttacks;
            }
            else
            {
                isAttack = false;
            }

            if (Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRadius)
            {
                targetEnemy = null;
            }
        }
    }


    void FixedUpdate()
    {
        if (isAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (targetEnemy == null) return;

        Projectile newProjectile = Instantiate(projectile);
        newProjectile.transform.localPosition = transform.localPosition;

        if (newProjectile.ProjectileType == proType.arrow)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Arrow);
        }
        else if (newProjectile.ProjectileType == proType.fireball)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Fireball);
        }
        else if (newProjectile.ProjectileType == proType.rock)
        {
            audioSource.PlayOneShot(SoundManager.Instance.Rock);
        }

        StartCoroutine(MoveProjectile(newProjectile));
    }


    /*IEnumerator MoveProjectile(Projectile projectile)
    {
        while (projectile != null && targetEnemy != null && GetTargetDistance(targetEnemy) > 0.20f)
        {
            Vector3 direction = targetEnemy.transform.localPosition - transform.localPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);

            yield return null;
        }

        if (projectile != null)
        {
            Destroy(projectile.gameObject);
        }
    }*/

    IEnumerator MoveProjectile(Projectile projectile)
    {
        while (GetTargetDistance(targetEnemy) > 0.20f && projectile != null && targetEnemy != null)
        {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }

        if (projectile != null || targetEnemy == null)
        {
            Destroy(projectile);
        }
    }

    private float GetTargetDistance(Enemy thisEnemy)
    {
        return thisEnemy == null ? float.MaxValue : Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition);
    }

    private Enemy GetNearestEnemyInRange()
    {
        float smallestDistance = float.MaxValue;
        Enemy nearestEnemy = null;

        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            if (!enemy.IsDead && Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
            {
                float distance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }

    private void NotifyEnemiesToRecalculatePaths()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.RecalculatePath();
        }
    }
}

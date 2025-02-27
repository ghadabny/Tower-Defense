using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int healthPoints;
    [SerializeField] private int rewardAmt;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 1f;

    private List<Node> path;
    private int pathIndex = 0;
    private PathfindingManager pathfindingManager;
    private Collider2D enemyCollider;
    private bool isDead = false;

    public bool IsDead => isDead;
    public int RewardAmount => rewardAmt;

    void Start()
    {
        enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        EnemyManager.Instance.RegisterEnemy(this);
        pathfindingManager = FindObjectOfType<PathfindingManager>();
        CalculatePath();
        ObstacleEvents.OnObstaclesUpdated += RecalculatePath;
    }

    void Update()
    {
        if (!isDead && path != null && pathIndex < path.Count)
        {
            FollowPath();
        }
    }

    private void FollowPath()
    {
        if (path == null || pathIndex >= path.Count)
        {
            HandleEscape();
            return;
        }

        Vector3 targetPos = path[pathIndex].worldPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            pathIndex++;
        }
        if (pathIndex >= path.Count)
        {
            HandleEscape();
        }
    }

    public void CalculatePath()
    {
        GameObject[] finishPoints = GameObject.FindGameObjectsWithTag("Finish");
        if (finishPoints.Length == 0)
        {
            Debug.LogError("No finish points found!");
            return;
        }
        GameObject closest = finishPoints[0];
        float minDist = Vector3.Distance(transform.position, closest.transform.position);
        foreach (GameObject fp in finishPoints)
        {
            float d = Vector3.Distance(transform.position, fp.transform.position);
            if (d < minDist)
            {
                minDist = d;
                closest = fp;
            }
        }
        path = pathfindingManager.FindPath(transform.position, closest.transform.position);
        pathIndex = 0;
    }

    public void RecalculatePath()
    {
        if (!isDead)
        {
            CalculatePath();
            pathIndex = 0;
        }
    }

    private void HandleEscape()
    {
        EnemyManager.Instance.EnemyEscaped(this);
    }

    public void enemyHit(int damage)
    {
        if (healthPoints > damage)
        {
            healthPoints -= damage;
            anim.Play("Hurt");
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        anim.SetTrigger("didDie");
        enemyCollider.enabled = false;
        EnemyManager.Instance.EnemyKilled(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            HandleEscape();
        }
        else if (other.CompareTag("Projectile"))
        {
            Projectile proj = other.GetComponent<Projectile>();
            if (proj != null)
            {
                enemyHit(proj.AttackStrength);
                Destroy(other.gameObject);
            }
        }
    }

    void OnDestroy()
    {
        ObstacleEvents.OnObstaclesUpdated -= RecalculatePath;
    }
}

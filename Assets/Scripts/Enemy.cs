using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int healthPoints;
    [SerializeField] private int rewardAmt;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 1f;
    private Transform enemy;
    private Collider2D enemyCollider;

    private bool isDead = false;
    private List<Node> path;
    private int pathIndex = 0;

    //added
    private PathfindingManager pathfindingManager;

    public bool IsDead => isDead;

    void Start()
    {
        enemy = GetComponent<Transform> ();
        anim = GetComponent<Animator>();
        GameManager.Instance.RegisterEnemy(this);
        pathfindingManager = FindObjectOfType<PathfindingManager>();
        CalculatePath();
        enemyCollider = GetComponent<Collider2D>();

    }

    void Update()
    {
        if (!isDead && path != null && pathIndex < path.Count)
        {
            FollowPath();
        }
    }


    /*void Update()
    {
        if (!isDead && path != null && pathIndex < path.Count)
        {
            Vector3 targetPosition = path[pathIndex].worldPosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                pathIndex++;
            }
        }
    }*/

    private void FollowPath()
    {
        if (path == null || pathIndex >= path.Count)
        {
            Debug.LogWarning("Path is null or completed. Enemy cannot move.");
            HandleEscape();
            return;
        }

        Vector3 targetPosition = path[pathIndex].worldPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the enemy reached the current path node
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            pathIndex++;
        }

        // Check if the enemy has reached the endpoint
        if (pathIndex >= path.Count && Vector3.Distance(transform.position, exitPoint.position) < 0.1f)
        {
            HandleEscape();
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            HandleEscape();
        }
    }*/


    public void CalculatePath()
    {
        PathfindingManager pathfindingManager = FindObjectOfType<PathfindingManager>();
        if (pathfindingManager == null)
        {
            Debug.LogError("PathfindingManager not found!");
            return;
        }

        GameObject[] endPoints = GameObject.FindGameObjectsWithTag("Finish");
        if (endPoints.Length == 0)
        {
            Debug.LogError("No endpoints found with the tag 'Finish'!");
            return;
        }

        GameObject closestEndPoint = null;
        float shortestDistance = float.MaxValue;

        foreach (GameObject endPoint in endPoints)
        {
            float distance = Vector3.Distance(transform.position, endPoint.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEndPoint = endPoint;
            }
        }

        if (closestEndPoint != null)
        {
            path = pathfindingManager.FindPath(transform.position, closestEndPoint.transform.position);
            if (path == null || path.Count == 0)
            {
                Debug.LogError("No valid path found for the enemy!");
            }
            else
            {
                Debug.Log("Path calculated successfully.");
            }
        }

        pathIndex = 0;
    }


    /*private void CalculatePath()
    {
        PathfindingManager pathfindingManager = FindObjectOfType<PathfindingManager>();
        if (pathfindingManager == null)
        {
            Debug.LogError("PathfindingManager not found!");
            return;
        }

        path = pathfindingManager.FindPath(transform.position, exitPoint.position);
        if (path == null || path.Count == 0)
        {
            Debug.LogWarning("No valid path found for the enemy!");
        }

        pathIndex = 0;
    }
    */
    private void HandleEscape()
    {
        GameManager.Instance.TotalEscaped += 1;
        GameManager.Instance.RoundEscaped += 1;
        GameManager.Instance.UnRegister(this);
        Destroy(gameObject);
    }

    public void enemyHit(int hitPoints)
    {
        if (healthPoints - hitPoints > 0)
        {
            anim.Play("Hurt");
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
            healthPoints -= hitPoints;
        }
        else
        {
            die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "Finish")
        {
            GameManager.Instance.TotalEscaped += 1;
            GameManager.Instance.RoundEscaped += 1;
            GameManager.Instance.UnRegister(this);
            GameManager.Instance.isWaveOver();
        }
        else if (other.tag == "Projectile")
        {
            Projectile newP = other.gameObject.GetComponent<Projectile>();
            enemyHit(newP.AttackStrength);
            Destroy(other.gameObject);
        }
    }

    private void die()
    {
        isDead = true;
        anim.SetTrigger("didDie");
        GameManager.Instance.TotalKilled += 1;
        enemyCollider.enabled = false;
        GameManager.Instance.addMoney(rewardAmt);
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Die);
        Destroy(gameObject);
        GameManager.Instance.isWaveOver();
    }


    public void RecalculatePath()
    {
        PathfindingManager pathfindingManager = FindObjectOfType<PathfindingManager>();
        if (pathfindingManager != null)
        {
            path = pathfindingManager.FindPath(transform.position, exitPoint.position);
            pathIndex = 0;
        }
    }
}

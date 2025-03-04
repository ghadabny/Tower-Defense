using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected int attackStrength;
    [SerializeField]
    protected float speed;

    protected Enemy target;
    protected AudioSource audioSource;

    public int AttackStrength => attackStrength;
    public abstract ProjectileType ProjectileType { get; }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetTarget(Enemy enemy)
    {
        target = enemy;
    }

    private void Update()
    {
        if (target == null || target.IsDead)
        {
            Destroy(gameObject);
            return;
        }
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.NotifyHit(this); // ✅ Projectile simply tells enemy "I hit you"
            }
            Destroy(gameObject); // ✅ Projectile disappears after hit
        }
    }


    protected virtual void PlayProjectileSound()
    {
        if (audioSource == null) return;

        switch (ProjectileType)
        {
            case ProjectileType.Arrow:
                audioSource.PlayOneShot(SoundManager.Instance.Arrow);
                break;
            case ProjectileType.Fireball:
                audioSource.PlayOneShot(SoundManager.Instance.Fireball);
                break;
            case ProjectileType.Rock:
                audioSource.PlayOneShot(SoundManager.Instance.Rock);
                break;
        }
    }
}

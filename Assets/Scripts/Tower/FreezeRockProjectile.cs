using UnityEngine;

public class FreezeRockProjectile : RockProjectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                FreezeEffect freeze = enemy.gameObject.AddComponent<FreezeEffect>();
                freeze.ApplyFreeze(4f);

              
                enemy.NotifyHit(this);
            }
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class BombFireballProjectile : FireballProjectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                BombEffect bomb = enemy.gameObject.GetComponent<BombEffect>();
                if (bomb == null)
                {
                    // Premier coup
                    bomb = enemy.gameObject.AddComponent<BombEffect>();
                    bomb.RecordHit();
                    enemy.NotifyHit(this);
                }
                else
                {
                    // Deuxième coup
                    enemy.NotifyHit(this);
                    enemy.NotifyHit(this);
                }
            }
            Destroy(gameObject);
        }
    }
}

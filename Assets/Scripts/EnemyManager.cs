using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    // Liste privée pour stocker les ennemis actifs.
    private List<Enemy> enemyList = new List<Enemy>();

    // Propriété en lecture seule pour y accéder depuis d'autres classes.
    public List<Enemy> EnemyList => enemyList;

    // Méthode pour enregistrer un ennemi.
    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy != null && !enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
            Debug.Log("Enemy registered: " + enemy.name);
        }
    }

    // Méthode pour désenregistrer un ennemi.
    public void UnregisterEnemy(Enemy enemy)
    {
        if (enemy != null && enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
            Debug.Log("Enemy unregistered: " + enemy.name);
        }
    }

    // Méthode pour détruire tous les ennemis (par exemple lors d'un reset de vague).
    public void DestroyAllEnemies()
    {
        // Pour éviter les problèmes lors de l'itération, on copie la liste.
        foreach (Enemy enemy in new List<Enemy>(enemyList))
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        enemyList.Clear();
    }
}

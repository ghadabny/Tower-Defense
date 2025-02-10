using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    // Liste priv�e pour stocker les ennemis actifs.
    private List<Enemy> enemyList = new List<Enemy>();

    // Propri�t� en lecture seule pour y acc�der depuis d'autres classes.
    public List<Enemy> EnemyList => enemyList;

    // M�thode pour enregistrer un ennemi.
    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy != null && !enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
            Debug.Log("Enemy registered: " + enemy.name);
        }
    }

    // M�thode pour d�senregistrer un ennemi.
    public void UnregisterEnemy(Enemy enemy)
    {
        if (enemy != null && enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
            Debug.Log("Enemy unregistered: " + enemy.name);
        }
    }

    // M�thode pour d�truire tous les ennemis (par exemple lors d'un reset de vague).
    public void DestroyAllEnemies()
    {
        // Pour �viter les probl�mes lors de l'it�ration, on copie la liste.
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

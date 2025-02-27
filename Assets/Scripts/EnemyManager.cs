using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> EnemyList => enemyList;
    public int EnemyCount => enemyList.Count;


    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy != null && !enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
            Debug.Log("Enemy registered: " + enemy.name);
        }
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (enemy != null && enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
            Debug.Log("Enemy unregistered: " + enemy.name);

            // Vérifie automatiquement si la vague est terminée
            CheckWaveStatus();
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (var enemy in enemyList)
        {
            if (enemy != null)
                Destroy(enemy.gameObject);
        }
        enemyList.Clear();
    }

    public void CheckWaveStatus()
    {
        if (enemyList.Count == 0)
        {
            GameManager.Instance.setCurrentGameState();
            UIManager.Instance.ShowGameStatus(GameManager.Instance.CurrentState, GameManager.Instance.AudioSource);
        }
    }

}

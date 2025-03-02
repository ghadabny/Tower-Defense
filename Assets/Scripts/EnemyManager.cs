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

    public void EnemyEscaped(Enemy enemy)
    {
        GameManager.Instance.TotalEscaped++;
        GameManager.Instance.RoundEscaped++;
        UnregisterEnemy(enemy);
        Destroy(enemy.gameObject);
        CheckWaveStatus();
    }

    public void EnemyKilled(Enemy enemy)
    {
        GameManager.Instance.TotalKilled++;
        EconomyManager.Instance.AddMoney(enemy.RewardAmount);
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Die);
        UnregisterEnemy(enemy);
        Destroy(enemy.gameObject);
        CheckWaveStatus();
    }



}

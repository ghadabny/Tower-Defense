using UnityEngine;
using System.Collections;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private int totalWaves = 10;
    private int waveNumber = 0;
    private int totalEnemies;
    private int enemiesPerSpawn;
    public int TotalWaves => totalWaves;

    private EnemySpawner enemySpawner;

    public int WaveNumber => waveNumber;
    public int TotalEnemies => totalEnemies;

    protected override void Awake()
    {
        base.Awake();
        enemySpawner = FindObjectOfType<EnemySpawner>(); 
    }

    private void CheckWaveEnd()
    {
        if (EnemyManager.Instance.EnemyCount == 0)
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.WaveCompleted();
            }
        }
    }

    public void StartNextWave()
    {
        if (waveNumber >= totalWaves)
        {
            GameManager.Instance.setCurrentGameState();
            return;
        }

        waveNumber++;
        totalEnemies = 3 + waveNumber;
        enemiesPerSpawn = Mathf.Max(1, waveNumber / 2);

        UIManager.Instance.UpdateWaveDisplay(waveNumber);
        UIManager.Instance.HideGameStatus();

        StartCoroutine(SpawnWaveAndCheckEnd());
    }

    private IEnumerator SpawnWaveAndCheckEnd()
    {
        yield return StartCoroutine(enemySpawner.SpawnEnemies(totalEnemies, enemiesPerSpawn));

        while (EnemyManager.Instance.EnemyCount > 0)
        {
            yield return null; 
        }

        CheckWaveEnd();
    }

    public void ResetWaves()
    {
        waveNumber = 0;
        totalEnemies = 3;
        enemiesPerSpawn = 1;
    }
}

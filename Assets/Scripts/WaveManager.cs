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

        StartCoroutine(enemySpawner.SpawnEnemies(totalEnemies, enemiesPerSpawn));
    }


    public void ResetWaves()
    {
        waveNumber = 0;
        totalEnemies = 3;
        enemiesPerSpawn = 1;
    }
}

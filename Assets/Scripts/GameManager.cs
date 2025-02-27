using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus
{
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int totalWaves = 10;
    [SerializeField] private int totalEnemies = 3;
    [SerializeField] private int enemiesPerSpawn;
    [SerializeField] private int waveNumber = 0;
    [SerializeField] private EnemySpawner enemySpawner;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int enemiesToSpawn = 0;
    private gameStatus currentState = gameStatus.play;
    private AudioSource audioSource;

    public gameStatus CurrentState => currentState;
    public int WaveNumber { get => waveNumber; set => waveNumber = value; }

    public int TotalEscaped
    {
        get => totalEscaped;
        set
        {
            totalEscaped = value;
            UIManager.Instance.UpdateEscapedDisplay(totalEscaped);
        }
    }

    public int RoundEscaped { get => roundEscaped; set => roundEscaped = value; }
    public int TotalKilled { get => totalKilled; set => totalKilled = value; }
    public AudioSource AudioSource => audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UIManager.Instance.ShowGameStatus(currentState, audioSource);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.disableDragSprite();
            TowerManager.Instance.towerBtnPressed = null;
        }
    }

    public void setCurrentGameState()
    {
        if (TotalEscaped >= 10)
            currentState = gameStatus.gameover;
        else if (waveNumber == 0 && (totalKilled + roundEscaped) == 0)
            currentState = gameStatus.play;
        else if (waveNumber >= totalWaves)
            currentState = gameStatus.win;
        else
            currentState = gameStatus.next;
    }

    public void nextWavePressed()
    {
        switch (currentState)
        {
            case gameStatus.next:
                waveNumber++;
                totalEnemies += waveNumber;
                break;
            default:
                totalEnemies = 3;
                TotalEscaped = 0;
                waveNumber = 0;
                enemiesToSpawn = 0;
                EconomyManager.Instance.TotalMoney = 10;
                TowerManager.Instance.DestroyAllTowers();
                TowerManager.Instance.RenameTagsBuildSites();
                UIManager.Instance.UpdateEscapedDisplay(TotalEscaped);
                audioSource.PlayOneShot(SoundManager.Instance.NewGame);
                break;
        }

        roundEscaped = 0;
        UIManager.Instance.UpdateWaveDisplay(waveNumber);

        UIManager.Instance.HideGameStatus();

        StartCoroutine(enemySpawner.SpawnEnemies(totalEnemies, enemiesPerSpawn));
    }
}


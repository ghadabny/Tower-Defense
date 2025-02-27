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
    const float waitingTime = 1f;

    [SerializeField] private int totalWaves = 10;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int totalEnemies = 3;
    [SerializeField] private int enemiesPerSpawn;
    [SerializeField] private Text totalMoneyLabel;
    [SerializeField] private Image GameStatusImage;
    [SerializeField] private Text nextWaveBtnLabel;
    [SerializeField] private Text escapedLabel;
    [SerializeField] private Text waveLabel;
    [SerializeField] private Text GameStatusLabel;
    [SerializeField] private int waveNumber = 0;
    [SerializeField] private EnemySpawner enemySpawner;

    private int totalMoney = 10;
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
            escapedLabel.text = "Escaped " + totalEscaped + "/10";
        }
    }

    public int RoundEscaped { get => roundEscaped; set => roundEscaped = value; }
    public int TotalKilled { get => totalKilled; set => totalKilled = value; }

    public int TotalMoney
    {
        get => totalMoney;
        set
        {
            totalMoney = value;
            totalMoneyLabel.text = totalMoney.ToString();
        }
    }

    public AudioSource AudioSource => audioSource;

    void Start()
    {
        GameStatusImage.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        showMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.disableDragSprite();
            TowerManager.Instance.towerBtnPressed = null;
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyManager.Instance.RegisterEnemy(enemy);
    }

    public void UnRegister(Enemy enemy)
    {
        EnemyManager.Instance.UnregisterEnemy(enemy);
        isWaveOver();
    }

    public void addMoney(int amount) => TotalMoney += amount;
    public void subtractMoney(int amount) => TotalMoney -= amount;

    public void isWaveOver()
    {
        if ((roundEscaped + totalKilled) >= totalEnemies)
        {
            enemiesToSpawn = waveNumber;
            setCurrentGameState();
            showMenu();
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
                TotalMoney = 10;
                TowerManager.Instance.DestroyAllTowers();
                TowerManager.Instance.RenameTagsBuildSites();
                totalMoneyLabel.text = TotalMoney.ToString();
                escapedLabel.text = "Escaped " + TotalEscaped + "/10";
                audioSource.PlayOneShot(SoundManager.Instance.NewGame);
                break;
        }

        if (EnemyManager.Instance.EnemyList.Count > 0)
            DestroyAllEnemies();

        totalKilled = 0;
        roundEscaped = 0;
        waveLabel.text = "Wave " + (waveNumber + 1);
        StartCoroutine(enemySpawner.SpawnEnemies(totalEnemies, enemiesPerSpawn));
        GameStatusImage.gameObject.SetActive(false);
    }

    public void DestroyAllEnemies()
    {
        EnemyManager.Instance.DestroyAllEnemies();
    }

    public void showMenu()
    {
        switch (currentState)
        {
            case gameStatus.gameover:
                GameStatusLabel.text = "Gameover";
                audioSource.PlayOneShot(SoundManager.Instance.Gameover);
                nextWaveBtnLabel.text = "Play again";
                break;
            case gameStatus.next:
                nextWaveBtnLabel.text = "Next Wave";
                GameStatusLabel.text = "Wave " + (waveNumber + 2) + " next.";
                break;
            case gameStatus.play:
                nextWaveBtnLabel.text = "Play";
                break;
            case gameStatus.win:
                nextWaveBtnLabel.text = "Play";
                GameStatusLabel.text = "You Won!";
                break;
        }
        GameStatusImage.gameObject.SetActive(true);
    }
}

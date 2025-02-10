using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum gameStatus
{
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager>
{
    const float waitingTime = 1f;

    [SerializeField] private int totalWaves = 10;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int totalEnemies = 3;
    [SerializeField] private int enemiesPerSpawn;
    [SerializeField] private Text totalMoneyLabel;
    [SerializeField] private Image GameStatusImage;
    [SerializeField] private Text nextWaveBtnLabel;
    [SerializeField] private Text escapedLabel;
    [SerializeField] private Text waveLabel;
    [SerializeField] private Text GameStatusLabel;
    [SerializeField] private int waveNumber = 0;

    private int totalMoney = 10;
    private int totalEscaped = 0;      // Escapes cumulées
    private int roundEscaped = 0;        // Escapes pour la vague en cours
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
            if (escapedLabel != null)
                escapedLabel.text = "Escaped " + totalEscaped + "/10";
        }
    }

    // Permet aux scripts ennemis d'incrémenter le compteur
    public int RoundEscaped
    {
        get => roundEscaped;
        set => roundEscaped = value;
    }

    public int TotalKilled
    {
        get => totalKilled;
        set { totalKilled = value; }
    }

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

    // Ces méthodes ne font plus gérer la liste d'ennemis localement,
    // elles délèguent directement à EnemyManager.
    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            EnemyManager.Instance.RegisterEnemy(enemy);
        }
    }

    public void UnRegister(Enemy enemy)
    {
        if (enemy != null)
        {
            EnemyManager.Instance.UnregisterEnemy(enemy);
        }
        isWaveOver();
    }

    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void subtractMoney(int amount)
    {
        TotalMoney -= amount;
    }

    // Vérifie si la vague est terminée (c'est-à-dire si le nombre d'ennemis tués et d'évasions atteint totalEnemies).
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
        {
            currentState = gameStatus.gameover;
        }
        else if (waveNumber == 0 && (totalKilled + roundEscaped) == 0)
        {
            currentState = gameStatus.play;
        }
        else if (waveNumber >= totalWaves)
        {
            currentState = gameStatus.win;
        }
        else
        {
            currentState = gameStatus.next;
        }
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

        // On vérifie maintenant la liste d'ennemis via EnemyManager
        if (EnemyManager.Instance.EnemyList.Count > 0)
            DestroyAllEnemies();

        totalKilled = 0;
        roundEscaped = 0; // Réinitialisation pour la nouvelle vague.
        waveLabel.text = "Wave " + (waveNumber + 1);
        StartCoroutine(SpawnEnemies());
        GameStatusImage.gameObject.SetActive(false);
    }

    IEnumerator SpawnEnemies()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("Aucun prefab d'ennemi n'est assigné. Assignez-le dans l'Inspector.");
            yield break;
        }

        // On utilise désormais EnemyManager.Instance.EnemyList.Count pour comparer
        if (enemiesPerSpawn > 0 && EnemyManager.Instance.EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyManager.Instance.EnemyList.Count < totalEnemies)
                {
                    GameObject enemyGO = Instantiate(enemyPrefabs[0]);
                    GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    enemyGO.transform.position = spawnPoint.transform.position;
                    Enemy enemyComp = enemyGO.GetComponent<Enemy>();
                    if (enemyComp != null)
                    {
                        // L'enregistrement de l'ennemi est normalement effectué dans Enemy.Start().
                        // Si nécessaire, vous pouvez aussi appeler EnemyManager.Instance.RegisterEnemy(enemyComp);
                    }
                    else
                    {
                        Debug.LogError("L'ennemi instancié ne possède pas le composant Enemy !");
                    }
                }
            }
            yield return new WaitForSeconds(waitingTime);
            StartCoroutine(SpawnEnemies());
        }
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

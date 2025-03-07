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
    [SerializeField] private int waveNumber = 0;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
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
        currentState = (TotalEscaped >= 10) ? gameStatus.gameover
                     : (WaveManager.Instance.WaveNumber >= WaveManager.Instance.TotalWaves) ? gameStatus.win
                     : gameStatus.next;

        UIManager.Instance.ShowGameStatus(currentState, audioSource);
    }

    public void nextWavePressed()
    {
        if (currentState == gameStatus.gameover || currentState == gameStatus.win)
        {
            ResetGame();
            return;
        }

        WaveManager.Instance.StartNextWave();
    }

    private void ResetGame()
    {
        WaveManager.Instance.ResetWaves();
        TotalEscaped = 0;
        TotalKilled = 0;
        roundEscaped = 0;
        currentState = gameStatus.play; 

        EconomyManager.Instance.TotalMoney = 10;

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        EnemyManager.Instance.DestroyAllEnemies();

        TowerManager.Instance.DestroyAllTowers();
        TowerManager.Instance.RenameTagsBuildSites();

        UIManager.Instance.UpdateEscapedDisplay(TotalEscaped);
        UIManager.Instance.UpdateWaveDisplay(WaveManager.Instance.WaveNumber);
        UIManager.Instance.HideGameStatus();
        UIManager.Instance.ShowGameStatus(gameStatus.play, audioSource);

        audioSource.PlayOneShot(SoundManager.Instance.NewGame);

    }

}


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text totalMoneyLabel;
    [SerializeField] private Image gameStatusImage;
    [SerializeField] private Text nextWaveBtnLabel;
    [SerializeField] private Text escapedLabel;
    [SerializeField] private Text waveLabel;
    [SerializeField] private Text gameStatusLabel;
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu(bool show)
    {
        pauseMenu.SetActive(show);
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1; // Met le jeu en pause
    }

    public void TogglePauseMenu()
    {
        bool isActive = pauseMenu.activeSelf;
        TogglePauseMenu(!isActive);
    }


    protected override void Awake()
    {
        base.Awake(); 
    }

    public void HideGameStatus()
    {
        gameStatusImage.gameObject.SetActive(false);
    }


    public void UpdateMoneyDisplay(int totalMoney)
    {
        totalMoneyLabel.text = totalMoney.ToString();
    }

    public void UpdateEscapedDisplay(int totalEscaped)
    {
        escapedLabel.text = "Escaped " + totalEscaped + "/10";
    }

    public void UpdateWaveDisplay(int waveNumber)
    {
        waveLabel.text = "Wave " + waveNumber;
    }

    public void Continuer()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RelancerPartie()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitterJeu()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    public void ShowGameStatus(gameStatus status, AudioSource audioSource)
    {
        switch (status)
        {
            case gameStatus.gameover:
                gameStatusLabel.text = "Gameover";
                audioSource.PlayOneShot(SoundManager.Instance.Gameover);
                nextWaveBtnLabel.text = "Play again";
                break;
            case gameStatus.next:
                nextWaveBtnLabel.text = "Next Wave";
                gameStatusLabel.text = "Wave " + (GameManager.Instance.WaveNumber + 2) + " next.";
                break;
            case gameStatus.play:
                nextWaveBtnLabel.text = "Play";
                break;
            case gameStatus.win:
                nextWaveBtnLabel.text = "Play";
                gameStatusLabel.text = "You Won!";
                break;
        }
        gameStatusImage.gameObject.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;      
    [SerializeField] private Text bestScoreText;

    private int currentWave = 0;

    void Start()
    {
        UpdateUI();
    }


    public void WaveCompleted()
    {
        currentWave++;
        UpdateUI();
        SaveBestScore();
    }

    private void UpdateUI()
    {
        scoreText.text = "Waves : " + currentWave;

        int best = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best score : " + best;
    }

    private void SaveBestScore()
    {
        int best = PlayerPrefs.GetInt("BestScore", 0);

        if (currentWave > best)
        {
            PlayerPrefs.SetInt("BestScore", currentWave);
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    public void ResetScore()
    {
        currentWave = 0;
        UpdateUI();
    }

}

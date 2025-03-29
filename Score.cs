using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    private int score = 0;
    private int level = 1;

    void Start()
    {
        UpdateScore();
        UpdateLevel();
    }

    public void IncreaseScore()
    {
        score += 100;
        UpdateScore();
        if (score >= 100)
        {
            IncreaseLevel();
        }
    }

    public void IncreaseLevel()
    {
        if (level <= 9)
        level += 1;
        FindFirstObjectByType<Move>().SetNewFallTime();
        UpdateLevel();
    }

    private void UpdateScore()
    {
        scoreText.text = "SCORE: " + score;
    }

    private void UpdateLevel()
    {
        levelText.text = "LEVEL: " + level;
    }
}
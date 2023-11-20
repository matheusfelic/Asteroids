using UnityEngine;
using UnityEngine.UI;

public class UIManager : IUIManager
{
    private Text scoreText;
    private Text livesText;

    public UIManager(Text scoreText, Text livesText)
    {
        this.scoreText = scoreText;
        this.livesText = livesText;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetLives(int lives)
    {
        livesText.text = lives.ToString();
    }
}

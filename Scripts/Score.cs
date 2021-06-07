using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Highscores highscores;

    private Text scoreText;
    public int currentScore;
    public int highScore;

    [SerializeField] int perfectScore = 1000;
    [SerializeField] int greatScore = 500;
    [SerializeField] float heldScorePerSecond = 1000;

    private void Awake()
    {
        highscores = FindObjectOfType<Highscores>();

        scoreText = GetComponent<Text>();
        currentScore = 0;
        highScore = 0;
    }

    public void UpdateScore(Color accuracy)
    {
        if (accuracy == Color.yellow)
        {
            currentScore += perfectScore;
            scoreText.text = currentScore.ToString();
        }
        if (accuracy == Color.magenta)
        {
            currentScore += greatScore;
            scoreText.text = currentScore.ToString();
        }
    }

    public void HeldScore()
    {
        float heldScoreTime = heldScorePerSecond * Time.deltaTime;
        currentScore += (int)heldScoreTime;
        scoreText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }

    private void Update()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;

            highscores.UpdateHighScore(highScore);
        }
    }
}

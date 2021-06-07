using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Highscores : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI bestStreakText;

    public void UpdateHighScore(int Highscore)
    {
        highScoreText.text = "Highscore: " + Highscore.ToString();
    }

    public void UpdateBestStreak(int bestStreak)
    {
        bestStreakText.text = "Best Streak: " + bestStreak.ToString();
    }
}

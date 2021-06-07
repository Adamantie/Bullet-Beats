using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Streak : MonoBehaviour
{
    private Highscores highscores;

    private Text streakText;
    private int currentStreak;
    public int bestStreak;

    private void Awake()
    {
        highscores = FindObjectOfType<Highscores>();

        streakText = GetComponent<Text>();
        currentStreak = 0;
        bestStreak = 0;
    }

    public void IncreaseStreak()
    {
        currentStreak += 1;
        streakText.text = "x" + currentStreak.ToString();
    }

    public void ResetStreak()
    {
        currentStreak = 0;
        streakText.text = "x" + currentStreak.ToString();
    }

    private void Update()
    {
        if (currentStreak > bestStreak)
        {
            bestStreak = currentStreak;

            highscores.UpdateBestStreak(bestStreak);
        }
    }
}

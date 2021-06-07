using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongStart : MonoBehaviour
{
    private Score score;
    private Streak streak;

    private void Awake()
    {
        score = FindObjectOfType<Score>();
        streak = FindObjectOfType<Streak>();
    }

    private void Update()
    {
        if (gameObject.transform.position.z <= 16)
        {
            score.ResetScore();
            streak.ResetStreak();
        }
    }
}

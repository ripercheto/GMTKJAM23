using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI Score;
    private static TextMeshProUGUI ScoreInstance;
    private static int CurrentScore = 0;

    void Awake()
    {
        ScoreInstance = Score;
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            CurrentScore = 0;
        }
        Score.SetText(CurrentScore.ToString("000"));
    }

    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (CurrentScore > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", CurrentScore);
            }
        }
    }

    public static void onScoreChanged(int amount)
    {
        CurrentScore += amount;

        ScoreInstance.SetText(CurrentScore.ToString("000"));
    }
}
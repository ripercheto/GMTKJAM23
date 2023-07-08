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
if(SceneManager.GetActiveScene().name == "SampleScene")
{
    CurrentScore = 0;
}
Score.SetText(CurrentScore.ToString("000"));
}

public static void onScoreChanged(int amount)
{
CurrentScore += amount;
ScoreInstance.SetText(CurrentScore.ToString("000"));
}
}
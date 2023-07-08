using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
public TextMeshProUGUI Score;
private static TextMeshProUGUI ScoreInstance;
private static int CurrentScore;

void Awake()
{
ScoreInstance = Score;
CurrentScore = 0;
Score.SetText("000");
}

public static void onScoreChanged(int amount)
{
CurrentScore += amount;
ScoreInstance.SetText(CurrentScore.ToString("000"));
}
}
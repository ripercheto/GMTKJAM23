using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoreCounter : MonoBehaviour
{
    
    public TextMeshProUGUI score;
    void Awake()
    {
        score.SetText(PlayerPrefs.GetInt("HighScore").ToString("000"));
    }
}
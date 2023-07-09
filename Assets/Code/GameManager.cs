using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float delayAfterCharacterDies = 1;
    private bool gameOver;

    private void Start()
    {
        MainCharacters.onMainDeath += OnMainDeath;
    }

    private void OnMainDeath()
    {
        if (gameOver)
        {
            return;
        }

        gameOver = true;
        StartCoroutine(WaitForSeconds());

        IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(delayAfterCharacterDies);
            SceneManager.LoadScene("Game Over");
        }
    }
}
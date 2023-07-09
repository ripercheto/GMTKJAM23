using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public AudioSource music;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        music.Pause();
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        music.UnPause();
    }

    void Update()
    {
        if ((Input.GetButtonDown("Cancel")) && (PauseMenu.activeSelf == false))
        {
            Pause();
        }
    else if ((Input.GetButtonDown("Cancel")) && (PauseMenu.activeSelf == true))
        {
            Continue();
        }
    }

}

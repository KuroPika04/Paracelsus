using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pause_menu;
    public SettingsMenu settings_menu;
    public Animator dialogue;
    public static bool game_paused = false;

    private void Start()
    {
        ResumeGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (game_paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        pause_menu.SetActive(false);
        settings_menu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        game_paused = false;
    }
    public void PauseGame()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
        game_paused = true;
    }
    public void SettingsMenu()
    {
        AudioManager.instance.PlaySFX("ClickSound");
        settings_menu.gameObject.SetActive(true);
        //settings_menu.InitialSettings();
        pause_menu.SetActive(false);
        Time.timeScale = 0f;
        game_paused = true;
    }
    public void MainMenu()
    {
        pausetransition.ComefromPause = true;
        AudioManager.instance.PlaySFX("ClickSound");
        AudioManager.instance.music_source.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}

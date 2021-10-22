using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _higscoreTable;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _gameGuide;

    private int _idMainMenuScene = 0;
    private int _idGameScene = 1;
    private bool _gameInPause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameInPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _gameInPause = false;
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _gameInPause = true;
    }

    public void StartGame()
    {
        Resume();
        SceneManager.LoadScene(_idGameScene);
    }

    public void LoadMainMenu()
    {       
        SceneManager.LoadScene(_idMainMenuScene);
    }

    public void Settings()
    {
        _settings.SetActive(true);
    }

    public void HighScore()
    {
        _higscoreTable.SetActive(true);
    }

    public void GameGuide()
    {
        _gameGuide.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        if(_higscoreTable != null)
        _higscoreTable.SetActive(false);

        if (_settings != null)
        _settings.SetActive(false);

        if (_gameGuide != null)
        _gameGuide.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _higscoreTable;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _gameGuide;
    [SerializeField] private AudioMixer _audioMixer;

    private int _idMainMenuScene = 0;
    private int _idGameScene = 1;

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

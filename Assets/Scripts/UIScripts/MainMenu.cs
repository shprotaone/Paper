using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoCache
{
    [SerializeField] private GameObject _higscoreTable;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _escTextObj;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _gameGuide;
    [SerializeField] private GameObject _confirmExit;
    [SerializeField] private GameStats _gameStats;

    private int _idMainMenuScene = 0;
    private int _idGameScene = 1;

    public override void OnTick()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameStats.PlayerIsDeath)
        {
            if(_pauseMenu != null)
            {
                if (_gameStats.GameInPause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }           
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        _escTextObj.SetActive(true);
        BackButton();
        Time.timeScale = 1f;
        _gameStats.GameInPause = false;
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        _escTextObj.SetActive(false);
        Time.timeScale = 0f;
        _gameStats.GameInPause = true;     
    }

    public void StartGame()
    {
        Resume();
        SceneManager.LoadScene(_idGameScene);
    }

    public void LoadMainMenu()
    {
        Resume();    
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

        if (_confirmExit != null)
            _confirmExit.SetActive(false);
    }

    public void ConfirmExitWindow()
    {
        _confirmExit.SetActive(true);
    }
    public void Guide()
    {
        _gameGuide.SetActive(true);
    }
}

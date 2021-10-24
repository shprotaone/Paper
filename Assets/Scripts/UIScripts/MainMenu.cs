using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _higscoreTable;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _gameGuide;
    [SerializeField] private GameObject _confirmExit;
    [SerializeField] private GameManager _gameManager;

    private int _idMainMenuScene = 0;
    private int _idGameScene = 1;
    private bool _gameInPause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_pauseMenu != null)
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
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        BackButton();
        Time.timeScale = 1f;
        _gameInPause = false;
        _gameManager.GameInPause = _gameInPause;
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _gameInPause = true;
        _gameManager.GameInPause = _gameInPause;     
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

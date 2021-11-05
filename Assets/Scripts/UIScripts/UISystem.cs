using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISystem : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Camera _cam;

    [SerializeField] private Text _timerText;
    [SerializeField] private Text _scoreText;

    private void Update()
    {
        if (!_gameStats.PlayerIsDeath)
        {
            DrawTime();
            DrawScore();
        }
    }

    private void DrawTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(_gameStats.InGameTime);
        _timerText.text = time.ToString(@"mm\:ss");
    }

    private void DrawScore()
    {
        _scoreText.text = _gameStats.Score.ToString();
    } 
}

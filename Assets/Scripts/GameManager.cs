using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Text _timerText;
    [SerializeField] private Text _scoreText;

    private float _timeStart;
    private float _score;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        _timeStart = _timeStart + Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(_timeStart);
        _timerText.text = time.ToString(@"mm\:ss");
    }

    public void AddScore(float points)
    {
        _score += points;
        _scoreText.text = _score.ToString();    //NullReference check
    }
}

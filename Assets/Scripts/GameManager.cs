using System;
using UnityEngine;

public class GameManager : MonoCache
{
    [SerializeField] private GameStats _gameStats;

    private AudioSource _mainMusic;
    private float _round = 30;

    private void Awake()
    {
        _gameStats.Restart();
        _mainMusic = GetComponent<AudioSource>();
    }

    public override void OnTick()
    {
        if (!_gameStats.PlayerIsDeath)
        {
            Timer();
            Level();
            StartMusic();
        }
    }

    private void Timer()
    {
        _gameStats.InGameTime = _gameStats.InGameTime + Time.deltaTime;
    }

    /// <summary>
    /// Повышения уровня сложности в зависимости от времени
    /// </summary>
    private void Level()
    {
        if (_gameStats.InGameTime > _round)
        {
            _round += 20;
            _gameStats.SpawnTimeDecrease();
            print("Decrease");
        }
    }

    public void StartMusic()
    {
        if (_gameStats.FirstBlood && !_mainMusic.isPlaying)
        {
            _mainMusic.Play();          
        }        
    }
}

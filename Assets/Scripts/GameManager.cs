using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;

    private AudioSource _mainMusic;
    private float _inGameTime;
    private float _round = 30;

    #region Properties
    public float InGameTime { get { return _inGameTime; } }
    public float Score { get { return _gameStats.Score; } }
    public string NameForRecord { get; set; }

    #endregion

    private void Awake()
    {
        _gameStats.Restart();
        _mainMusic = GetComponent<AudioSource>();
    }

    private void Update()
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
        _inGameTime = _inGameTime + Time.deltaTime;
    }

    /// <summary>
    /// Повышения уровня сложности в зависимости от времени
    /// </summary>
    private void Level()
    {
        if (_inGameTime > _round)
        {
            _round += 20;
            _gameStats.SpawnTime -= 0.1f;
        }
    }

    public void StartMusic()
    {
        if (_gameStats.firstBlood && !_mainMusic.isPlaying)
        {
            _mainMusic.Play();          
        }        
    }
}

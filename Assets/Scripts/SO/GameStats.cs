using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateGameStats", order = 53)]
public class GameStats : ScriptableObject
{
    public event Action<bool> OnDeath;

    [SerializeField] private float _spawnTime = 2;
    [SerializeField] private float _score = 0;
    [SerializeField] private bool _playerIsDeath = false;
    
    private float _inGameTime;
    private bool _gameInPause = false;
    private bool _firstBlood;

    public float InGameTime { get { return _inGameTime; } set { _inGameTime = value; } }
    public bool GameInPause { get { return _gameInPause; } set { _gameInPause = value; } }
    public float SpawnTime { get { return _spawnTime; } }
    public bool PlayerIsDeath { get { return _playerIsDeath; } }
    public float Score { get { return _score; } }
    public bool FirstBlood { get { return _firstBlood; } set { _firstBlood = value; } }

    public void Restart()
    {       
        _spawnTime = 2;
        _score = 0;
        _playerIsDeath = false;
        _gameInPause = false;
        _firstBlood = false;
        InGameTime = 0;
    }

    public void AddScore(float score)
    {
        this._score += score;
    }

    public void SpawnTimeDecrease()
    {
        _spawnTime -= 0.1f;
    }

    public void PlayerDeathInv(bool state)
    {
        if (OnDeath != null)
        {            
            OnDeath.Invoke(state);
            _playerIsDeath = true;
        }
    }
}

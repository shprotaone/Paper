using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;   

    private DropList _dropItems;
    private KillCounter _killCounter;
    private EnemyFactory _enemyFactory;
    private float _inGameTime;
    private float _score;
    private float _spawnTime = 2f;
    private float _round = 30;
    private bool _firstBlood = false;

    #region Properties
    public float InGameTime { get { return _inGameTime; } }
    public float Score { get { return _score; } }
    public float SpawnTime { get { return _spawnTime; } }
    public string NameForRecord { get; set; }
    public bool PlayerIsDeath { get; set; }
    public bool GameInPause { get; set; }

    #endregion

    private void Awake()
    {
        _dropItems = GetComponent<DropList>();
        _killCounter = GetComponent<KillCounter>();
        _enemyFactory = new EnemyFactory();        

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (!PlayerIsDeath)
        {
            Timer();
            Level();
        }
    }

    private void Timer()
    {
        _inGameTime = _inGameTime + Time.deltaTime;
    }

    public void AddScore(float points,string name)
    {
        if (!PlayerIsDeath)
        {
            if (!_firstBlood)
            {
                StartMusic();
            }
            
            _score += points;
            _killCounter.AddCount(name);           
        }           
    }

    public void DropItem(Vector3 pos, string name)
    {
        if (name != _enemyFactory.LightEnemyID && name != _enemyFactory.MidEnemyID)
        {
            GameObject drop = _dropItems.Drop();
            if (drop != null)
            {
                Instantiate(drop, pos, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// Повышения уровня сложности в зависимости от времени
    /// </summary>
    private void Level()
    {
        if (_inGameTime > _round)
        {
            _round += 20;
            _spawnTime -= 0.1f;
        }
    }

    private void StartMusic()
    {        
        this.GetComponent<AudioSource>().Play();
        _firstBlood = true;
    }
}

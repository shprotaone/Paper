using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyList _fillEnemy;
    [SerializeField] private GameObject[] _spawnPoint; 
    [SerializeField] private Transform _enemyContain;   
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private PlayerController _player;
    [SerializeField] private float _spawnLimit = 20;

    [Range(0, 1)]
    [SerializeField] private float _spawnChanceMid = 0.4f;
    [Range(0, 1)]
    [SerializeField] private float _spawnChanceFat = 0.7f;
    [Range(0, 1)]
    [SerializeField] private float _spawnChanceBoss = 0.9f;

    private EnemyFactory _factory;    
    private string[] _enemies;

    void Start()
    {
        _factory = new EnemyFactory();
        _factory.Init(_fillEnemy);

        InitEnemies();
        StartCoroutine(SpawnEnemy());
    }

    private void InitEnemies()
    {
        _enemies = new string[4];

        _enemies[0] = _factory.LightEnemyID;
        _enemies[1] = _factory.MidEnemyID;
        _enemies[2] = _factory.FatEnemyID;
        _enemies[3] = _factory.BossID;
    }

    IEnumerator SpawnEnemy()
    {
        while (!_gameStats.playerIsDeath)
        {
            yield return new WaitForSeconds(_gameStats.spawnTime);
            if(_spawnLimit > _enemyContain.transform.childCount)
            {
                CreateEnemy(EnemyVariable(), RandomPosition());
            }
        }
        EndGame();
    }

    private void CreateEnemy(string name, int pos)
    {
        EnemyModel enemyModel = _factory.CreateEnemy(name);              //создаем врага
        var obj = enemyModel.Description.Prefab;                         //определяем префаб

        GameObject enemy = Instantiate(obj, _spawnPoint[pos].transform.position, Quaternion.identity);   //определяем позицию для спавна
        EnemyController stats = enemy.GetComponent<EnemyController>();   //Инициализируем характеристики   

        stats.Fill(enemyModel);                                          //заполняем характеристики   

        enemy.transform.SetParent(_enemyContain);                        //Присваиваем родителя
    }

    /// <summary>
    /// Адрес спавна
    /// </summary>
    /// <returns></returns>
    private int RandomPosition()
    {
        int result = Random.Range(0, _spawnPoint.Length);
        if (CheckDistanceToPlayer(_spawnPoint[result].transform.position))
        {
            return result;
        }
        else
        {
            return RandomPosition();
        }        
    }

    /// <summary>
    /// Выбираем врага для спавна
    /// </summary>
    /// <returns></returns>
    private string EnemyVariable()
    {
        int enemyVar;

        float _spawnChance = Random.value;

        if (_spawnChance > _spawnChanceBoss)
        {
            enemyVar = 3;
        }
        else if (_spawnChance > _spawnChanceFat )
        {
            enemyVar = 2;
        }
        else if (_spawnChance > _spawnChanceMid)
        {
            enemyVar = 1;
        }
        else
        {
           enemyVar = 0;
        }

        string result = _enemies[enemyVar];

        return result;
    }

    /// <summary>
    /// Удаление всех врогов при конце игры
    /// </summary>
    private void EndGame()
    {
        for (int i = 0; i < _enemyContain.childCount; i++)
        {
            Destroy(_enemyContain.GetChild(i).gameObject);
        }
    }

    private bool CheckDistanceToPlayer(Vector3 spawner)
    {
        float distance = Vector3.Distance(_player.transform.position, spawner);
       
        if (distance > 15)
        {
            return true;
        }
        else return false;
    }

}

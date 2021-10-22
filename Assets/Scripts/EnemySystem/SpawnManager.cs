using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyList _fillEnemy;
    [SerializeField] private GameObject[] _spawnPoint; 
    [SerializeField] private Transform _enemyContain;
    [SerializeField] private float _spawnLimit = 20;

    private EnemyFactory _factory;
    private GameManager _gameManager;
    private string[] _enemies;

    void Start()
    {
        _factory = new EnemyFactory();
        _factory.Init(_fillEnemy);

        InitEnemies();
        _gameManager = GetComponent<GameManager>();

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
        while (!_gameManager.PlayerIsDeath)
        {
            yield return new WaitForSeconds(_gameManager.SpawnTime);
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
        EnemyController stats = obj.GetComponent<EnemyController>();     //Инициализируем характеристики   

        stats.Fill(enemyModel);                                          //заполняем характеристики

        GameObject enemy = Instantiate(obj, _spawnPoint[pos].transform.position,Quaternion.identity);   //определяем позицию для спавна
        enemy.transform.SetParent(_enemyContain);                        //Присваиваем родителя
    }

    /// <summary>
    /// Адрес спавна
    /// </summary>
    /// <returns></returns>
    private int RandomPosition()
    {
        int result = Random.Range(0, _spawnPoint.Length);
        return result;
    }

    /// <summary>
    /// Выбираем врага для спавна
    /// </summary>
    /// <returns></returns>
    private string EnemyVariable()
    {
        int enemyVar;

        float _spawnChance = Random.value;

        if (_spawnChance > 0.9)
        {
            enemyVar = 3;
        }
        else if (_spawnChance > 0.7 )
        {
            enemyVar = 2;
        }
        else if (_spawnChance > 0.4)
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

}

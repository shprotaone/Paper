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

    void Start()
    {
        _factory = new EnemyFactory();
        _factory.Init(_fillEnemy);

        _gameManager = GetComponent<GameManager>();

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_gameManager.SpawnTime);
            if(_spawnLimit > _enemyContain.transform.childCount)
            {
                CreateEnemy(EnemyVariable(), RandomPosition());
            }
        }        
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
    /// Вариант врага
    /// </summary>
    /// <returns></returns>
    private string EnemyVariable()
    {
        string[] enemies = new string[3];

        enemies[0] = _factory.FatEnemyID;
        enemies[1] = _factory.LightEnemyID;
        enemies[2] = _factory.MidEnemyID;

        string result = enemies[Random.Range(0, enemies.Length)];
        return result;
    }
}

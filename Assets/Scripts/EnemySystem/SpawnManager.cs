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

    void Start()
    {
        _factory = new EnemyFactory();
        _factory.Init(_fillEnemy);

        //CreateEnemy(EnemyVariable(), RandomPosition());
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (_spawnLimit > this.transform.childCount)
        {
            yield return new WaitForSeconds(2f);
            CreateEnemy(EnemyVariable(), RandomPosition());
        }
        yield return null;
    }

    private void CreateEnemy(string name, int pos)
    {
        EnemyModel enemy = _factory.CreateEnemy(name);
        GameObject obj = enemy.Description.Prefab;          //решить проблему с родительским объектом
        EnemyController stats = obj.GetComponent<EnemyController>();
        stats.Fill(enemy);
        Instantiate(obj, _spawnPoint[pos].transform);
    }

    private int RandomPosition()
    {
        int result = Random.Range(0, _spawnPoint.Length);
        return result;
    }

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

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _smallTaskText;
    [SerializeField] private TMP_Text _taskText;
    [SerializeField] private TMP_Text _ALotOfTasks;
    [SerializeField] private TMP_Text _annualReport;

    public Dictionary<EnemyTypes,int> _kills;
    private EnemyFactory _enemyFactory;

    private void Start()
    {
        _enemyFactory = new EnemyFactory();
        _kills = new Dictionary<EnemyTypes, int>();

        _kills.Add(_enemyFactory.LightEnemyID, 0);
        _kills.Add(_enemyFactory.MidEnemyID, 0);
        _kills.Add(_enemyFactory.FatEnemyID, 0);
        _kills.Add(_enemyFactory.BossID, 0);
    }

    public void AddCount(EnemyTypes type)
    {
        _kills[type]++;
        UpdateResult();
    }

    private void UpdateResult()
    {
        _smallTaskText.text = _kills[_enemyFactory.LightEnemyID].ToString();
        _taskText.text = _kills[_enemyFactory.MidEnemyID].ToString();
        _ALotOfTasks.text = _kills[_enemyFactory.FatEnemyID].ToString();
        _annualReport.text = _kills[_enemyFactory.BossID].ToString();
    }

}

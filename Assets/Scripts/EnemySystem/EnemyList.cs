using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateEnemyList", order = 51)]
public class EnemyList : ScriptableObject
{
    [SerializeField] private EnemyDescription _lightEnemy;
    [SerializeField] private EnemyDescription _midEnemy;
    [SerializeField] private EnemyDescription _fatEnemy;

    public EnemyDescription listLightEnemy => _lightEnemy;
    public EnemyDescription listMidEnemy => _midEnemy;
    public EnemyDescription listFatEnemy => _fatEnemy;
}

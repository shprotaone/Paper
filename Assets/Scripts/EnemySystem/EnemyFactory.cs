using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private const string lightEnemyID = "light";
    private const string midEnemyID = "mid";
    private const string fatEnemyID = "Fat";

    public string LightEnemyID { get { return lightEnemyID; } }
    public string MidEnemyID { get { return midEnemyID; } }
    public string FatEnemyID { get { return fatEnemyID; } }

    private Dictionary<string, EnemyModel> enemyFactory;

    public void Init(EnemyList FillEnemy)
    {
        enemyFactory = new Dictionary<string, EnemyModel>();
        enemyFactory.Add(lightEnemyID, new EnemyModel(FillEnemy.listLightEnemy));
        enemyFactory.Add(midEnemyID, new EnemyModel(FillEnemy.listMidEnemy));
        enemyFactory.Add(fatEnemyID, new EnemyModel(FillEnemy.listFatEnemy));
    }

    public EnemyModel CreateEnemy(string nameMob)
    {
        return enemyFactory[nameMob];
    }
}

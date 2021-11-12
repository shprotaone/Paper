using System.Collections.Generic;

public class EnemyFactory
{
    public EnemyTypes LightEnemyID { get { return EnemyTypes.LIGHT_ENEMY; } }
    public EnemyTypes MidEnemyID { get { return EnemyTypes.MID_ENEMY; } }
    public EnemyTypes FatEnemyID { get { return EnemyTypes.BIG_ENEMY; } }
    public EnemyTypes BossID { get { return EnemyTypes.BOSS; } }

    private Dictionary<EnemyTypes, EnemyModel> enemyFactory;

    public void Init(EnemyList fillEnemy)
    {
        enemyFactory = new Dictionary<EnemyTypes, EnemyModel>();
        enemyFactory.Add(LightEnemyID, new EnemyModel(fillEnemy.listLightEnemy));
        enemyFactory.Add(MidEnemyID, new EnemyModel(fillEnemy.listMidEnemy));
        enemyFactory.Add(FatEnemyID, new EnemyModel(fillEnemy.listFatEnemy));
        enemyFactory.Add(BossID, new EnemyModel(fillEnemy.listBossEnemy));
    }

    public EnemyModel CreateEnemy(EnemyTypes type)
    {
        return enemyFactory[type];
    }
}

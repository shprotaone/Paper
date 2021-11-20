using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _dropObject;
    // 0-Health, 1-ammo
    [Range(0, 1)]
    [SerializeField] private float _chanceDropHealth = 0.9f;
    [Range(0,1)]
    [SerializeField] private float _chanceDropAmmo = 0.6f;

    private EnemyFactory _enemyFactory;

    private void Start()
    {
        _enemyFactory = new EnemyFactory();
    }

    public void DropItem(Vector3 pos, EnemyTypes type)
    {
        if (type != _enemyFactory.LightEnemyID && type != _enemyFactory.MidEnemyID)
        {
            GameObject drop = Drop();
            if (drop != null)
            {
                Instantiate(drop, pos, Quaternion.identity);
            }
        }
    }

    private GameObject Drop()
    {        
        float chance = Random.value;

        if (chance > _chanceDropHealth)
        {
            return _dropObject[0];
        }
        else if(chance > _chanceDropAmmo)
        {            
            return _dropObject[1];
        }
        else
        {
            return null;
        }
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropList : MonoBehaviour
{
    [SerializeField] private GameObject[] _dropObject;
    [Range(0, 1)]
    [SerializeField] private float _chanceDropHealth = 0.9f;
    [Range(0,1)]
    [SerializeField] private float _chanceDropAmmo = 0.6f;

    // 0-Health, 1-ammo

    public GameObject Drop()
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

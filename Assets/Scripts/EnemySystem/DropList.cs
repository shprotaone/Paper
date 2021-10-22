using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropList : MonoBehaviour
{
    [SerializeField] private GameObject[] _dropObject; 
    // 0-Health, 1-ammo

    public GameObject Drop()
    {        
        float chance = Random.value;

        if (chance > 0.95)
        {
            Debug.Log(_dropObject[0]);
            return _dropObject[0];
        }
        else if(chance > 0.8)
        {
            Debug.Log(_dropObject[1]);
            return _dropObject[1];
        }
        else
        {
            return null;
        }

    }
}

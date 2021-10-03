using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropList : MonoBehaviour
{
    [SerializeField] private GameObject[] _dropObject;  

    public GameObject Drop()
    {        
        float chance = Random.value;

        if (chance > 0.9)
        {
            return _dropObject[0];
        }
        else if(chance > 0.8)
        {
            return _dropObject[1];
        }
        else
        {
            return null;
        }
    }
}

using System;
using UnityEngine;

[Serializable]
public class EnemyDescription
{
    public EnemyTypes Type;
    public float MaxHealth;
    public float Speed;
    public float Points;
    public GameObject Prefab;
}

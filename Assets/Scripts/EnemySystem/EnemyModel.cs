using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Model
/// </summary>
public class EnemyModel
{
    private EnemyDescription _description;
    private GameObject _prefab;
    [SerializeField] private EnemyTypes _currentType;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _currentPoints;
    [SerializeField] private float _currentSpeed;

    public EnemyDescription Description => _description;    

    public EnemyModel(EnemyDescription description)    //конструктор
    {
        _description = description;
        _prefab = _description.Prefab;
        _currentType = _description.Type;
        _currentHealth = _description.MaxHealth;
        _currentPoints = _description.Points;
        _currentSpeed = _description.Speed;
    }
}

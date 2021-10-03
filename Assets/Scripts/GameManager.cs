using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float _inGameTime;
    private float _score;
    private float _spawnTime = 2f;
    private float _round = 30;
    private DropList _dropItems;

    public float InGameTime { get { return _inGameTime; } }
    public float Score { get { return _score; } }
    public float SpawnTime { get { return _spawnTime; } }

    private void Awake()
    {
        _dropItems = GetComponent<DropList>();

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        Timer();
        Level();
    }

    private void Timer()
    {
        _inGameTime = _inGameTime + Time.deltaTime;
    }

    public void AddScore(float points)
    {
        _score += points;
    }

    public void DropItem(Vector3 pos)
    {
        GameObject drop = _dropItems.Drop();
        if (drop != null)
        {
            Instantiate(drop, pos, Quaternion.identity);
        }
    }

    /// <summary>
    /// ��������� ������ ��������� � ����������� �� �������
    /// </summary>
    private void Level()
    {
        if (_inGameTime > _round)
        {
            _round += 20;
            _spawnTime -= 0.1f;            
        }
    }
}

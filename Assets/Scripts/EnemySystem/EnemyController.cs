using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private string _currName;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _points;

    private NavMeshAgent _agent;
    private GameObject _player;    
    private int _damage = 1;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent.speed = _speed;
    }

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }

    /// <summary>
    /// Заполнение характеристик из model
    /// </summary>
    /// <param name="model"></param>
    public void Fill(EnemyModel model)      
    {
        _currName = model.Description.Name;
        _health = model.Description.MaxHealth;
        _speed = model.Description.Speed;
        _points = model.Description.Points;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<PlayerController>().ApplyDamage(_damage);
            DeathEnemy();            
        }
        else if (other.CompareTag("projectile"))
        {
            _health -= 1;
            if (_health == 0) DeathEnemy();
        }
    }

    private void DeathEnemy()
    {
        GameManager.instance.AddScore(_points);
        GameManager.instance.DropItem(this.transform.position);
        Destroy(this.gameObject);        
    }
}

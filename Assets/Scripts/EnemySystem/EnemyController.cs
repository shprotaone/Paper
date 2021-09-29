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

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");       
    }

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }

    public void Fill(EnemyModel model)      //придумать как инкапсулировать
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
            Destroy(this.gameObject);
            
        }
        else if (other.CompareTag("projectile"))
        {
            _health -= 1;
            if (_health == 0) Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.AddScore(_points);
    }
}

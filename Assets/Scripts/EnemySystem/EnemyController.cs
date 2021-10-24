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
    [SerializeField] private GameObject _flamesContainer;
    [SerializeField] private ParticleSystem _explosion;

    private NavMeshAgent _agent;
    private Animator _animator;
    private GameObject _player;    
    private int _damage = 1;
    private ParticleSystem[] _flames;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();

        _agent.speed = _speed;

        if(_flamesContainer != null)
        {
          _flames = _flamesContainer.GetComponentsInChildren<ParticleSystem>();
        }       
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
            ShowDamage((int)_health);
            if (_health == 0) DeathEnemy();
        }
    }

    private void DeathEnemy()
    {
        GameManager.instance.AddScore(_points,_currName);
        GameManager.instance.DropItem(this.transform.position,_currName);
        
        StartCoroutine(DeathAction());
    }

    private void ShowDamage(int health)
    {   if(_flamesContainer != null)     
        _flames[health].Play();
    }

    private IEnumerator DeathAction()
    {
        _explosion.Play();

        if (_flamesContainer != null)
        {
            foreach (var flames in _flames)
            {
                flames.Stop();
            }
        }

        _agent.isStopped = true;
        _animator.gameObject.SetActive(false);

        this.gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(_explosion.main.duration);

        Destroy(this.gameObject);

        yield return null;
    }
}

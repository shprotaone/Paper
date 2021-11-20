using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoCache
{
    [SerializeField] private GameObject _flamesContainer;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameStats _gameStats;

    private EnemyTypes _currType;
    private float _health;
    private float _speed;
    private float _points;
    private int _damage = 1;

    private NavMeshAgent _agent;
    private Animator _animator;
    private KillCounter _killCount;
    private DropSystem _dropList;
    private GameObject _player;        
    private ParticleSystem[] _flames;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _killCount = GetComponentInParent<KillCounter>();
        _dropList = GetComponentInParent<DropSystem>();

        _agent.speed = _speed;

        if(_flamesContainer != null)
        {
          _flames = _flamesContainer.GetComponentsInChildren<ParticleSystem>();
        }       
    }

    public override void OnTick()
    {
        _agent.SetDestination(_player.transform.position);
    }

    /// <summary>
    /// Заполнение характеристик из model
    /// </summary>
    /// <param name="model"></param>
    public void Fill(EnemyModel model)      
    {
        _currType = model.Description.Type;
        _health = model.Description.MaxHealth;
        _speed = model.Description.Speed;
        _points = model.Description.Points;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            other.GetComponent<HealthSystem>().ApplyDamage(_damage);
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
        _killCount.AddCount(_currType);
        _gameStats.AddScore(_points);
        _gameStats.FirstBlood = true;
        _dropList.DropItem(this.transform.position,_currType);
        
        StartCoroutine(DeathAction());
    }

    private void ShowDamage(int health)
    {   
        if(_flamesContainer != null)     
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

        yield break;
    }
}

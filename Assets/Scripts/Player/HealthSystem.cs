using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action<int, bool> OnHealthChanged;

    [SerializeField] private AudioSource _damageSound;
    [SerializeField] private GameStats _gameStats;

    private int _health = 3;

    public int Health { get { return _health; } }

    private void Death()
    {
        if (Health <= 0)
        {           
            _damageSound.pitch = 0.5f;
            _damageSound.Play();
            _gameStats.PlayerDeathInv(true);
        }            
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _damageSound.Play();

        if (OnHealthChanged != null)
        {
           Death();
            OnHealthChanged.Invoke(_health, false);
        }
    }

    public bool Healing(int heal)
    {
        if (_health < 3)
        {
            _health += heal;
            if (OnHealthChanged != null)
            {
                OnHealthChanged.Invoke(_health, true);
            }
            return true;
        }
        else return false;
    }
}

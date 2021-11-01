using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action<int, bool> OnHealthChanged;

    [SerializeField] private AudioSource _damageSound;

    private int _health = 3;
    private bool _playerIsDeath;

    public int Health { get { return _health; } }
    public bool PlayerIsDeath { get { return _playerIsDeath; } }

    private void Death()
    {
        if (Health <= 0)
        {
            _damageSound.pitch = 0.5f;
            _damageSound.Play();
            _playerIsDeath = true;
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

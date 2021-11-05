using System;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public event Action<float> OnAmmoChanged;

    [SerializeField] private float _maxAmmo = 50;

    public float CurrentAmmo { get; private set; }
    public float MaxAmmo { get; private set; }
    public float CapacityAmmo { get; private set; }

    private void Awake()
    {
        CurrentAmmo = _maxAmmo;
        MaxAmmo = _maxAmmo;
        CapacityAmmo = 100;
        UpdateAmmoCapacity();
    }

    public float DecreaseAmmo()
    {
        CurrentAmmo -= 1;
        return CurrentAmmo;
    }

    public float IncreaseAmmo(float ammo)
    {
        CapacityAmmo += ammo;
        UpdateAmmoCapacity();
        return CapacityAmmo;
    }

    public bool Reloading()
    {                   
        if (CapacityAmmo > 0 && _maxAmmo != CurrentAmmo)
        {
            float addAmmo = _maxAmmo - CurrentAmmo;

            if (CapacityAmmo < addAmmo)
            {
                addAmmo = CapacityAmmo;
            }

            CapacityAmmo -= addAmmo;
            CurrentAmmo += addAmmo;

            UpdateAmmoCapacity();

            return true;
        }
        else
        {           
           return false;
        }        
    }

    private void UpdateAmmoCapacity()
    {
        if (OnAmmoChanged != null)
        {            
            OnAmmoChanged.Invoke(CapacityAmmo);
        }
    }
}

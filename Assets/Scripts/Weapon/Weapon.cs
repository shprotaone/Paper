using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    /// <summary>
    /// ДОДЕЛАТЬ СТРЕЛЬБУ ЧЕРЕЗ ВЕКТОРА
    /// </summary>
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _maxAmmo = 50;
    [SerializeField] private float _fireRate = 15f;
    [SerializeField] private Bullet _bulletPrefabT;
    [SerializeField] private Transform _bulletContain;
    [SerializeField] private PlayerController _playerController;

    private BulletPool<Bullet> pool;
    private Rigidbody _rigidbody;
    
    private float _nextTimeToFire = 0;

    public float CurrentAmmo { get; private set; }
    public float MaxAmmo { get; private set; }
    public float CapacityAmmo { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        CurrentAmmo = _maxAmmo;
        MaxAmmo = _maxAmmo;
        CapacityAmmo = 100;

        this.pool = new BulletPool<Bullet>(_bulletPrefabT, _maxAmmo, _bulletContain);
    }

    private void Update()
    {
        Shooting();
        Reloading();
        print(_playerController.ShootDirection);
    }

    private void Shooting()
    {
        if(Input.GetMouseButton(0) && Time.time >= _nextTimeToFire && CurrentAmmo > 0)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
            
            CurrentAmmo -= 1;
        }
    }

    private void Shoot()
    {
        var bullet = pool.GetBullet();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = Quaternion.identity;

        Vector3 shootDir = (_playerController.ShootDirection - _firePoint.position).normalized;
        bullet.transform.GetComponent<Bullet>().Setup(shootDir,_bulletSpeed);

        Debug.DrawRay(_firePoint.position, shootDir, Color.red, 1000);
    }

    private void Reloading()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CapacityAmmo != 0)
            {
                float addAmmo = _maxAmmo - CurrentAmmo;
                CurrentAmmo += addAmmo;
                CapacityAmmo -= addAmmo;

                print(CurrentAmmo);
            }
            else
            {
                print("You not have a Ammo");
            }
        }        
    }

    public void ReleasingWeapon()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.forward/2, ForceMode.Impulse);
    }


}

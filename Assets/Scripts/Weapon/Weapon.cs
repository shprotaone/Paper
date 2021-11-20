using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private AmmoSystem _ammoSystem;
    
    [SerializeField] private float _fireRate = 15f;
    [SerializeField] private Bullet _bulletPrefabT;
    [SerializeField] private Transform _bulletContain;
    [SerializeField] private PlayerController _playerController;

    private BulletPool<Bullet> pool;
    private ParticleSystem _shootLight;
    private Rigidbody _rigidbody;
    private AudioSource[] _sounds;
    //[0] - Reload,[1] - Shoot
    
    private float _nextTimeToFire = 0;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _sounds = GetComponents<AudioSource>();
        _shootLight = GetComponentInChildren<ParticleSystem>();

        this.pool = new BulletPool<Bullet>(_bulletPrefabT, _ammoSystem.MaxAmmo, _bulletContain);
    }

    public void Shooting()
    {
        if(Time.time >= _nextTimeToFire && _ammoSystem.CurrentAmmo > 0)
        {            
            _nextTimeToFire = Time.time + 1f / _fireRate;            
           Shoot();
            _ammoSystem.DecreaseAmmo();
        }     
    }

    private void Shoot()
    {
        var bullet = pool.GetBullet();

        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(_firePoint.position - _playerController.ShootDirection);

        Vector3 shootDir = (_playerController.ShootDirection - _firePoint.position).normalized;
        bullet.transform.GetComponent<Bullet>().Construct(shootDir, _bulletSpeed);

        _shootLight.Play();
        _sounds[1].Play();
    }

    public void ReloadWeapon()
    {
        if (_ammoSystem.Reloading())
        {
            _sounds[0].Play();
        }
    }

    public void ReleasingWeapon()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.up);
    }
}

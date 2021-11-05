using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;

    private AudioSource _hitSound;
    private Vector3 _shootDirection;
    private float _bulletSpeed;

    private void OnEnable()
    {
        StartCoroutine(LifeRoutine());
        _hitSound = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Update()
    {
        this.transform.position += _shootDirection * _bulletSpeed * Time.deltaTime;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Enemy"))
        {
            StartCoroutine(Deactivate(false));
        }
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSecondsRealtime(lifetime);

        StartCoroutine(Deactivate(true));
    }

    private IEnumerator Deactivate(bool lifetimeDestroy)
    {
        MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer>();
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();

        particle.Stop();
        render.enabled = false;
        _bulletSpeed = 0;

        if(!lifetimeDestroy)
        _hitSound.Play();
        
        yield return new WaitForSeconds(0.7f);

        render.enabled = true;
        gameObject.SetActive(false);

        yield break;
    }

    public void Setup(Vector3 shootDir, float bulletSpeed)
    {
        this._shootDirection = shootDir;
        this._bulletSpeed = bulletSpeed;
    }

}

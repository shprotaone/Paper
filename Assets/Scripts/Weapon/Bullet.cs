using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;

    private Vector3 _shootDirection;
    private float _bulletSpeed;

    private void OnEnable()
    {
        StartCoroutine(LifeRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Setup(Vector3 shootDir,float bulletSpeed)
    {
        this._shootDirection = shootDir;
        this._bulletSpeed = bulletSpeed;
    }

    private void Update()
    {
        transform.position += _shootDirection * _bulletSpeed * Time.deltaTime;
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSecondsRealtime(lifetime);

        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


}

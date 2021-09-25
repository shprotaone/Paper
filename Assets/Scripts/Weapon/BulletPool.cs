using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool<Bullet> where Bullet : MonoBehaviour
{
    public Bullet prefab { get; }
    public bool AutoExpand { get; set; }
    public Transform Container { get; }

    private List<Bullet> pool;

    public BulletPool(Bullet prefab,float count)
    {
        this.prefab = prefab;
        this.CreatePool(count);
    }

    public BulletPool(Bullet prefab,float count,Transform container)
    {
        this.prefab = prefab;
        this.Container = container;
        this.CreatePool(count);
    }
    /// <summary>
    /// Создание пула
    /// </summary>
    /// <param name="count"></param>
    private void CreatePool(float count)
    {
        this.pool = new List<Bullet>();

        for (int i = 0; i < count; i++)
        {
            this.CreateBullet();
        }
    }

    private Bullet CreateBullet(bool isActiveByDefault = false)
    {
        var createdBullet = Object.Instantiate(this.prefab, this.Container);
        createdBullet.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdBullet);
        return createdBullet;
    }   

    public bool HasFreeElement(out Bullet bullet)
    {
        foreach (var currentBullet in pool)
        {
            if (!currentBullet.gameObject.activeInHierarchy)
            {
                bullet = currentBullet;
                currentBullet.gameObject.SetActive(true);
                return true;
            }
        }

        bullet = null;
        return false;
    }

    public Bullet GetBullet()
    {
        if(this.HasFreeElement(out var bullet))
        {
            return bullet;
        }

        if (this.AutoExpand)
        {
            return this.CreateBullet(true);
        }

        throw new System.Exception("No free element");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxItem : MonoBehaviour
{
    [SerializeField] private float _ammo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<Weapon>().AddAmmo(_ammo);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxItem : Item
{
    [SerializeField] private float _ammo;
    [SerializeField] private AudioSource _pickSound;

    private void OnEnable()
    {
        _pickSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Action(other));            
        }
    }

    public override IEnumerator Action(Collider collider)
    {
        _pickSound.Play();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        
        collider.GetComponentInChildren<Weapon>().AddAmmo(_ammo);
        gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(_pickSound.clip.length);
       
        Destroy(this.gameObject);

        yield break;
    }
}

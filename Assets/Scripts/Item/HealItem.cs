using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private AudioSource _healSound;
    private int _heal = 1;

    private void OnEnable()
    {
        _healSound = GetComponent<AudioSource>();
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
        bool canHeal = collider.GetComponent<PlayerController>().Healing(_heal);

        if (canHeal)
        {
            _healSound.Play();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);

            gameObject.GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(_healSound.clip.length);

            Destroy(this.gameObject);

            yield break;
        }        
    }
}

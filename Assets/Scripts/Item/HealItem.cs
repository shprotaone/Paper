using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    private int _heal = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            
            if (playerController.Health < 3)
            {
                playerController.Healing(_heal);
                Destroy(this.gameObject);
            }            
        }
    }
}

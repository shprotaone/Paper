using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float TakeDamage(float health)
    {
        health -= 1;
        return health;
    }

    public float Healing(float health)
    {
        health += 1;
        return health;
    }
}

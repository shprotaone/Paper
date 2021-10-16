using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyItem());
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
        yield break;
    }
}

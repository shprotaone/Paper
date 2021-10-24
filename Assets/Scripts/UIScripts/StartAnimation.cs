using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Animator>().Play("WorkAnimation");
    }
}

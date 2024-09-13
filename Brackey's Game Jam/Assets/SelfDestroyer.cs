using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float waitTime;
    void Start()
    {
        Invoke("boom", waitTime);
    }

    void boom()
    {
        Destroy(gameObject);
    }
}

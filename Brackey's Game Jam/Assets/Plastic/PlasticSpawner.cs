using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticSpawner : MonoBehaviour
{
    public GameObject plastic;
    public int plasticToSpawn;

    public float top;
    public float bottom;
    public float left;
    public float right;

    void Start()
    {
        for (int i = plasticToSpawn; i > 0; i--){
            Instantiate(plastic, new Vector3(Random.Range(left, right), Random.Range(top, bottom), 0), Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}

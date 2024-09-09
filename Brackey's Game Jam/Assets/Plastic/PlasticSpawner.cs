using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticSpawner : MonoBehaviour
{
    public GameObject plastic;
    public int plasticToSpawn;
    public int activePlastic;

    public float top;
    public float bottom;
    public float left;
    public float right;

    void Start()
    {
        spawn();
    }

    void Update()
    {
        
    }
    
    public void spawn(){
        for (int i = plasticToSpawn; i > 0; i--){
            Instantiate(plastic, new Vector3(Random.Range(left, right), Random.Range(top, bottom), 0), Quaternion.identity);
            activePlastic++;
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "plastic"){
            Destroy(collision.gameObject);
            activePlastic--;
        }
    }
}

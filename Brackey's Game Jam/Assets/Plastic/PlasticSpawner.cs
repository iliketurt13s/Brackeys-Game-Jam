using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticSpawner : MonoBehaviour
{
    public GameObject plastic;
    public int plasticToSpawn;
    public int activePlastic;
    [HideInInspector] public int totalPlastic;

    public float top;
    public float bottom;
    public float left;
    public float right;
    
    public GameObject[] spawnableObstacles;
    int obstaclesToSpawn;
    List<GameObject> obstacles = new List<GameObject>();

    void Start()
    {
        spawn();
    }
    
    public void spawn(){
        foreach (GameObject activeObstacle in obstacles){
            Destroy(activeObstacle);
        }
        obstacles.Clear();
        for (int i = plasticToSpawn; i > 0; i--){
            Instantiate(plastic, new Vector3(Random.Range(left, right), Random.Range(top, bottom), 0), Quaternion.identity);
            activePlastic++;
        }
        StormManager sm = gameObject.GetComponent<StormManager>();
        obstaclesToSpawn = sm.levelNumber + 1;
        if (sm.levelNumber > 4){obstaclesToSpawn = 4;}
        if (sm.levelNumber > 8){obstaclesToSpawn = 5;}
        if (obstaclesToSpawn > plasticToSpawn){plasticToSpawn = obstaclesToSpawn;}
        for (int i = obstaclesToSpawn; i > 0; i--){
            GameObject newOb = Instantiate(spawnableObstacles[Random.Range(0, spawnableObstacles.Length)], new Vector3(Random.Range(left, right), Random.Range(top, bottom), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            obstacles.Add(newOb);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "plastic"){
            //Destroy(collision.gameObject);
            Plastic p = collision.GetComponent<Plastic>();
            p.collect();
            activePlastic--;
            totalPlastic++;
        }
    }
}

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

    public AudioSource plasticCollectSound;

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
            float clampedX = Random.Range(left, right);
            if (clampedX < 1 && clampedX > -1){clampedX = 1;}
            float clampedY = Random.Range(top, bottom);
            if (clampedY < 1 && clampedY > -1){clampedY = 1;}
            Instantiate(plastic, new Vector3(clampedX, clampedY, 0), Quaternion.identity);
            activePlastic++;
        }
        StormManager sm = gameObject.GetComponent<StormManager>();
        obstaclesToSpawn = sm.levelNumber + 1;
        if (obstaclesToSpawn > 6){obstaclesToSpawn = 6;}
        if (obstaclesToSpawn > 3){plasticToSpawn = 4;}
        for (int i = obstaclesToSpawn; i > 0; i--){
            float clampedX = Random.Range(left, right);
            if (clampedX < 1 && clampedX > -1){clampedX = 1;}
            float clampedY = Random.Range(top, bottom);
            if (clampedY < 1 && clampedY > -1){clampedY = 1;}
            GameObject newOb = Instantiate(spawnableObstacles[Random.Range(0, spawnableObstacles.Length)], new Vector3(clampedX, clampedY, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            obstacles.Add(newOb);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "plastic"){
            plasticCollectSound.Play();
            //Destroy(collision.gameObject);
            Plastic p = collision.GetComponent<Plastic>();
            p.collect();
            activePlastic--;
            totalPlastic++;
        }
    }
}

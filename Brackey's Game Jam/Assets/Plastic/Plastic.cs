using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    public Sprite[] plasticSprites;
    SpriteRenderer sr;

    bool inWater;
    public float waterStrength;

    StormManager sm;

    public GameObject poof;

    void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = plasticSprites[Random.Range(0, plasticSprites.Length)];
        sm = GameObject.FindGameObjectWithTag("logic").GetComponent<StormManager>();
    }
    void FixedUpdate()
    {
        if (inWater){
            transform.localPosition = new Vector3(transform.localPosition.x + waterStrength, transform.localPosition.y, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "waves"){
            Invoke("die", .1f);
            inWater = true;
            transform.SetParent(collision.transform, true);
            sm.gameOver();
        }
    }

    public void collect(){
        Instantiate(poof, transform.position, Quaternion.Euler(0, 0, 180));
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
        Invoke("die", 1f);
    }
    void die(){
        Destroy(gameObject);
    }
}

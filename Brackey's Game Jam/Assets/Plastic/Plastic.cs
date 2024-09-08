using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    public Sprite[] plasticSprites;
    SpriteRenderer sr;

    bool inWater;
    public float waterStrength;

    void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = plasticSprites[Random.Range(0, plasticSprites.Length)];
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
            inWater = true;
            transform.SetParent(collision.transform, true);
        }
    }
}

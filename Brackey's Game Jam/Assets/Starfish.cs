using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfish : MonoBehaviour
{
    public float speed;

    float rotationTarget;
    public float rotationSpeed;

    public float rotationChangeInterval;

    void Start()
    {
        changeRotation();
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed);

        float currentRot = transform.localEulerAngles.z;
        float newRot = Mathf.LerpAngle(currentRot, rotationTarget, rotationSpeed);
        transform.localEulerAngles = new Vector3(0, 0, newRot);
    }

    void changeRotation(){
        rotationTarget = Random.Range(0, 360);
        Invoke("changeRotation", rotationChangeInterval);
    }
}

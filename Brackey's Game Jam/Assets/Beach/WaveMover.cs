using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    bool wavesGoingOut = false;
    public float waveExtremes;
    public float speed;
    public float waitTime;
    public float offset;

    void Start()
    {
        StartCoroutine("movingWaves");
    }

    void FixedUpdate()
    {
        if (wavesGoingOut){
            float x = Mathf.Lerp(transform.localPosition.x, waveExtremes + offset, speed);
            transform.localPosition = new Vector3(x, 0, 0);
        } else {
            float x = Mathf.Lerp(transform.localPosition.x, -waveExtremes + offset, speed);
            transform.localPosition = new Vector3(x, 0, 0);
        }

    }
    IEnumerator movingWaves(){
        wavesGoingOut = false;

        yield return new WaitForSeconds(waitTime);

        wavesGoingOut = true;

        yield return new WaitForSeconds(waitTime);

        StartCoroutine("movingWaves");
    }
}

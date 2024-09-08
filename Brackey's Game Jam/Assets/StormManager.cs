using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StormManager : MonoBehaviour
{
    WaveMover waveMover;
    public float timeUntilSurge;
    int levelNumber;
    public TMP_Text surgeCountdownText;
    bool surging = false;

    void Start()
    {
        timeUntilSurge = 5;
        waveMover = GameObject.FindGameObjectWithTag("waves").GetComponent<WaveMover>();
    }
    void Update()
    {
        if (!surging){
            timeUntilSurge -= Time.deltaTime;
            if (timeUntilSurge < 0){
                surging = true;
                surgeCountdownText.color = Color.red;
                surgeCountdownText.text = "Water levels rising";
            } else {
                surgeCountdownText.text = "Time Until Storm Surge: " + timeUntilSurge;
            }
        }
    }

    void FixedUpdate()
    {
        if (surging){
            waveMover.offset -= 0.0035f;
            waveMover.waitTime = 2;
            waveMover.speed = 0.0175f;
        }
    }
}

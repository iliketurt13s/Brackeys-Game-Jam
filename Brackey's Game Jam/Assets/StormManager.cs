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

    bool gameRunning = false;
    bool gameWon = false;
    float stopSurgeAfter = 10f;
    float stopIn = 0f;

    public BoxCollider2D waveCollider1;
    public BoxCollider2D waveCollider2;
    bool gameRestarted = false;

    PlasticSpawner ps;
    public TurtleAnimation ta;
    public Transform turtleTransform;

    public GameObject shopUI;
    public RectTransform textTransform;

    void Start()
    {
        ps = gameObject.GetComponent<PlasticSpawner>(); 
        waveMover = GameObject.FindGameObjectWithTag("waves").GetComponent<WaveMover>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)){
            gameRunning = true;
            timeUntilSurge = 45;
        }
        if (gameRunning){
            if (ps.activePlastic == 0){
                timeUntilSurge = 0f;
                gameWon = true;
                surging = true;
                gameRunning = false;
                stopIn = stopSurgeAfter;
                surgeCountdownText.color = Color.green;
                surgeCountdownText.text = "all food collected";
            }
            timeUntilSurge -= Time.deltaTime;
            if (timeUntilSurge < 0){
                if (!gameWon){
                    surging = true;
                    gameRunning = false;
                    stopIn = stopSurgeAfter;
                    surgeCountdownText.color = Color.red;
                    surgeCountdownText.text = "Water levels rising";
                }
            } else {
                surgeCountdownText.color = Color.white;
                if (timeUntilSurge > 30 && levelNumber == 0){
                    textTransform.sizeDelta = new Vector2 (2000, 100);
                    if (timeUntilSurge > 40){
                        surgeCountdownText.text = "Push the food to the top of the screen";
                    } else {
                        surgeCountdownText.text = "Get the food off the beach so it doesn't get swept away";
                    }
                } else {
                    textTransform.sizeDelta = new Vector2 (960, 100);
                    surgeCountdownText.text = "Time Until Storm Surge: " + timeUntilSurge;
                }
            }
        } else {
            if (stopIn > 0){stopIn -= Time.deltaTime;}
        }
        if (gameRestarted){
            surgeCountdownText.color = Color.white;
            surgeCountdownText.text = "Reseting...";
        }
    }

    void FixedUpdate()
    {
        if (surging){
            if (stopIn >= 0){
                waveMover.offset -= 0.0035f;
                waveMover.waitTime = 2;
                waveMover.speed = 0.0175f;
            } else if (!gameRestarted){
                gameRestarted = true;
                levelNumber++;
                waveMover.speed = 0;
                waveCollider1.enabled = false;
                waveCollider2.enabled = false;

                ta.canMove = false;
                turtleTransform.position = new Vector3(0, 0, 0);

                shopUI.SetActive(true);
            }
        }
    }
    public void ContinueGame(){
        shopUI.SetActive(false);
        StartCoroutine("restartGame");
    }

    IEnumerator restartGame(){
        ps.spawn();
        waveMover.offset = -1.65f;
        waveMover.speed = 0.075f;
        waveMover.waitTime = 3f;
        
        yield return new WaitForSeconds(2f);

        ta.canMove = true;
        gameRunning = true;
        timeUntilSurge = 30 - levelNumber;
        surging = false;
        gameWon = false;
        gameRestarted = false;
        waveCollider1.enabled = false;
        waveCollider2.enabled = false;
        waveMover.waitTime = 3;
        waveMover.speed = 0.0075f;
    }
}

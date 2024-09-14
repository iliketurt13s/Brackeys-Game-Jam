using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StormManager : MonoBehaviour
{
    public bool gameInitialized = false;
    WaveMover waveMover;
    public float timeUntilSurge;
    [HideInInspector] public int levelNumber;
    public TMP_Text surgeCountdownText;
    public SpriteRenderer[] lines;
    [HideInInspector] public bool surging = false;

    bool gameRunning = false;
    bool gameWon = false;
    bool gameLost = false;
    //float stopSurgeAfter = 10f;
    //float stopIn = 0f;
    bool stopSurge;

    public BoxCollider2D waveCollider1;
    public BoxCollider2D waveCollider2;
    bool gameRestarted = false;

    PlasticSpawner ps;
    public TurtleAnimation ta;
    public Transform turtleTransform;

    public GameObject shopUI;
    public RectTransform textTransform;

    public TMP_Text waveHeader;
    public TMP_Text waveButtonText;

    public Transform waveTransform;

    public Transform cam;
    public float shakeSpeed;
    public float shakeMagnitude;
    bool started = false;
    //public AudioSource stormSound;

    void Start()
    {
        ps = gameObject.GetComponent<PlasticSpawner>(); 
        waveMover = GameObject.FindGameObjectWithTag("waves").GetComponent<WaveMover>();
        Invoke("activateWaves", 2f);
    }
    void Update()
    {
        if (gameInitialized){
        if (Input.GetKeyDown(KeyCode.W) && levelNumber == 0 && !gameRunning){
            gameRunning = true;
            //print("yeah");
            timeUntilSurge = 45;
        }
        if (Input.GetKeyDown(KeyCode.S) && levelNumber == 0 && !gameRunning){
            gameRunning = true;
            //print("yeah");
            timeUntilSurge = 45;
        }
        
        if (ps.activePlastic == 0){
            timeUntilSurge = 0f;
            gameWon = true;
            surging = true;
            gameRunning = false;
            //stopIn = stopSurgeAfter;
            //surgeCountdownText.color = Color.green;
            //foreach(SpriteRenderer sr in lines){sr.color = Color.green;}
            surgeCountdownText.text = "all food collected";
        }
        if (gameRunning){
            timeUntilSurge -= Time.deltaTime;
            if (timeUntilSurge < 0){
                surging = true;
                gameRunning = false;
                //stopIn = stopSurgeAfter;
                if (!gameWon && !gameLost){
                    //surgeCountdownText.color = Color.red;
                    //foreach(SpriteRenderer sr in lines){sr.color = Color.red;}
                    surgeCountdownText.text = "Water levels rising";
                }
            } else {
                ta.canMove = true;
                //foreach(SpriteRenderer sr in lines){sr.color = Color.white;}
                //surgeCountdownText.color = Color.white;
                if (timeUntilSurge > 30 && levelNumber == 0){
                    textTransform.sizeDelta = new Vector2 (2000, 100);
                    if (timeUntilSurge > 40){
                        surgeCountdownText.text = "Push the trash to the top of the screen";
                    } else {
                        surgeCountdownText.text = "Get it off the beach so it doesn't get swept away";
                    }
                } else {
                    textTransform.sizeDelta = new Vector2 (960, 100);
                    surgeCountdownText.text = "Time Until Storm Surge: " + timeUntilSurge;
                }
            }
        } else {
            if (waveTransform.localPosition.x <= -3.2){
                stopSurge = true;
            }
            //if (stopIn > 0){stopIn -= Time.deltaTime;}
        }
        if (gameRestarted){
            //foreach(SpriteRenderer sr in lines){sr.color = Color.white;}
            //surgeCountdownText.color = Color.white;
            surgeCountdownText.text = "Reseting...";
        }
        }
    }

    void FixedUpdate()
    {
        if (surging){
            if (!started){StartCoroutine("shake"); started = true;}
            if (!stopSurge){
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
            if (gameWon){
                waveHeader.text = "Level " + levelNumber + " Completed";
                waveButtonText.text = "Continue Game";
            }
            if (gameLost){
                waveHeader.text = "Total Food Salvaged: " + ps.totalPlastic;
                waveButtonText.text = "Restart";
            }
        }
    }
    public void ContinueGame(){
        //shopUI.SetActive(false);
        if (gameRestarted){
            if (gameWon){
                StartCoroutine("restartGame");
            } 
            if (gameLost){
                SceneManager.LoadScene(0);
            }
        }
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
        stopSurge = false;
        gameWon = false;
        gameRestarted = false;
        waveCollider1.enabled = true;
        waveCollider2.enabled = true;
        waveMover.waitTime = 3;
        waveMover.speed = 0.0075f;
    }

    public void gameOver(){
        gameLost = true;
        //surgeCountdownText.color = Color.red;
        //foreach(SpriteRenderer sr in lines){sr.color = Color.red;}
        surgeCountdownText.text = "Game Lost";
        timeUntilSurge = 0;
    }

    void activateWaves(){
        gameInitialized = true;
        waveMover.waitTime = 3;
        waveMover.speed = 0.0075f;
        waveButtonText.gameObject.SetActive(true);
        waveHeader.gameObject.SetActive(true);
        waveCollider1.enabled = true;
        waveCollider2.enabled = true;
    }

    IEnumerator shake(){
        cam.transform.position = new Vector3(Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude), -10);
        //if (!stormSound.isPlaying){stormSound.Play();}
        yield return new WaitForSeconds(shakeSpeed);
        cam.transform.position = new Vector3(0, 0, -10);
        yield return new WaitForSeconds(shakeSpeed);
        if (surging){StartCoroutine("shake");} else {started = false;}
    }
}

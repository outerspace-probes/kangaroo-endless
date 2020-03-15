using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float initGameSpeed = 100;
    public float powerupGameSpeed = 300;
    public float powerupTime = 5;
    bool gameOver = false;
    public static GameManager instance;
    public GameObject gameOverPanel;
    public Text distanceText;
    public Text koalasText;
    public Text scoreInfoText;
    public Text highScoreInfoText;
    public ParticleSystem playerPowerupParticles;
    [HideInInspector] public float gameSpeed = 100;
    int score = 0;
    int koalas = 0;
    int counterScore = 0;
    int highScore = 0;
    [HideInInspector] public bool isPowerup = false;
    [HideInInspector] public float powerupTimeLeft = 0;
    float powerupAcceleration = 150f;

    float distanceCountingSpeed = 10f;
    public float distanceTimeCounter = 100;
    public int distance = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameSpeed = initGameSpeed;
        powerupTimeLeft = powerupTime;

        var em = playerPowerupParticles.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        // distance count
        if (!gameOver)
        {
            distanceTimeCounter -= (distanceCountingSpeed * gameSpeed / 10) * Time.deltaTime;
            if (distanceTimeCounter <= 0)
            {
                distanceTimeCounter = 100;
                distance++;
                distanceText.text = distance.ToString();
            }
        }

        // score counter stop
        if(gameOver && counterScore >= score)
        {
            CancelInvoke("IncScoreCounter");
        }

        if (isPowerup)
        {
            if(playerPowerupParticles.emission.enabled == false)
            {
                var em = playerPowerupParticles.emission;
                em.enabled = true;
            }
            powerupTimeLeft -= 1 * Time.deltaTime;
            bool isPwrFinishig = powerupTimeLeft < 3;

            if (gameSpeed < powerupGameSpeed && !isPwrFinishig)
            {
                gameSpeed += powerupAcceleration * Time.deltaTime;
            }
            else if (gameSpeed > initGameSpeed && isPwrFinishig)
            {
                gameSpeed -= powerupAcceleration * Time.deltaTime;
            }
            if(powerupTimeLeft <= 2 && playerPowerupParticles.emission.enabled)
            {
                var em = playerPowerupParticles.emission;
                em.enabled = false;
            }
            if (powerupTimeLeft <= 0)
            {
                isPowerup = false;
            }
        }
        else
        {
            if (gameSpeed > initGameSpeed)
            {
                gameSpeed -= powerupAcceleration * Time.deltaTime;
            }
        }

    }

    public void GameOver()
    {
        
        ObstacleSpawner.instance.gameOver = true;
        BgObjSpawner[] bgSpawners = FindObjectsOfType<BgObjSpawner>();
        foreach (BgObjSpawner spawner in bgSpawners)
        {
            spawner.gameOver = true;
        }

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (Obstacle obj in obstacles)
        {
            obj.gameOver = true;
        }

        BgObject[] bgObjs = FindObjectsOfType<BgObject>();
        foreach (BgObject obj in bgObjs)
        {
            obj.gameOver = true;
        }

        Koala[] koalasObjs = FindObjectsOfType<Koala>();
        foreach (Koala obj in koalasObjs)
        {
            obj.gameOver = true;
        }

        PowerupStar[] pwrups = FindObjectsOfType<PowerupStar>();
        foreach (PowerupStar obj in pwrups)
        {
            obj.gameOver = true;
        }

        score = distance + koalas * 10;
        gameOver = true;

        StopScrolling();
        gameOverPanel.SetActive(true);

        InvokeRepeating("IncScoreCounter", .5f, .01f);

        if(PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        highScoreInfoText.text = highScore.ToString();

    }

    void IncScoreCounter()
    {        
        if(counterScore < score)
        {
            counterScore++;
        }
        scoreInfoText.text = counterScore.ToString();
    }

    void StopScrolling()
    {
        TextureScroll[] scrollingObjs = FindObjectsOfType<TextureScroll>();
        foreach (TextureScroll obj in scrollingObjs)
        {
            obj.scroll = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void IncrementScore()
    {
        // score++;
        
    }
    public void IncrementKoalas()
    {
        koalas++;
        koalasText.text = koalas.ToString();
    }

    public void PowerUp()
    {
        // print("manager PowerUp called");
        powerupTimeLeft = powerupTime;
        isPowerup = true;
    }
}
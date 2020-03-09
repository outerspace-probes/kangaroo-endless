using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text koalasText;
    public ParticleSystem playerPowerupParticles;
    public float gameSpeed = 100;
    public float initGameSpeed = 100;
    public float powerupGameSpeed = 300;
    public float powerupTime = 5;
    int score = 0;
    int koalas = 0;
    public bool isPowerup = false;
    public float powerupTimeLeft = 0;
    float powerupAcceleration = 150;

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

        StopScrolling();
        gameOverPanel.SetActive(true);

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

        Koala[] koalas = FindObjectsOfType<Koala>();
        foreach (Koala obj in koalas)
        {
            obj.gameOver = true;
        }

        PowerupStar[] pwrups = FindObjectsOfType<PowerupStar>();
        foreach (PowerupStar obj in pwrups)
        {
            obj.gameOver = true;
        }

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
        score++;
        scoreText.text = score.ToString();
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
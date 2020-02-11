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
    int score = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        ObstacleSpawner.instance.gameOver = true;
        StopScrolling();
        gameOverPanel.SetActive(true);
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (Obstacle obj in obstacles)
        {
            obj.gameOver = true;
        }
    }

    void StopScrolling()
    {
        TextureScroll[] scrollingObjs = FindObjectsOfType<TextureScroll>();
        foreach(TextureScroll obj in scrollingObjs)
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
}
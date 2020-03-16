using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    int highScore = 0;
    public Text highScoreText;
    [SerializeField] GameObject achievKoalasIconLock;
    [SerializeField] GameObject achievKoalasIconChecked;
    [SerializeField] GameObject achievDistanceIconLock;
    [SerializeField] GameObject achievDistanceIconChecked;

    private void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        highScoreText.text = highScore.ToString();

        if(PlayerPrefs.HasKey("AchievKoalasDone") && PlayerPrefs.GetInt("AchievKoalasDone") == 1)
        {
            achievKoalasIconChecked.SetActive(true);
        }
        else
        {
            achievKoalasIconLock.SetActive(true);
        }

        if (PlayerPrefs.HasKey("AchievDistanceDone") && PlayerPrefs.GetInt("AchievDistanceDone") == 1)
        {
            achievDistanceIconChecked.SetActive(true);
        }
        else
        {
            achievDistanceIconLock.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}

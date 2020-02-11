using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner instance;

    public GameObject[] obstacles;
    public bool gameOver = false;
    [SerializeField] float waitTimeMin = 1f;
    [SerializeField] float waitTimeMax = 2.5f;

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
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        float waitTime = 1f;
        yield return new WaitForSeconds(waitTime);

        while (!gameOver)
        {          
            SpawnObstacle();
            waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnObstacle()
    {
        int rnd = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[rnd], transform.position, Quaternion.identity);
    }

}
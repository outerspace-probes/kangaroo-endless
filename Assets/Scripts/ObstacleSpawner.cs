using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner instance;

    public GameObject[] obstacles;
    public bool gameOver = false;
    [SerializeField] float easyWaitTimeMin = 4f;
    [SerializeField] float easyWaitTimeMax = 6f;
    [SerializeField] float hardWaitTimeMin = 2.4f;
    [SerializeField] float hardWaitTimeMax = 2.6f;
    [SerializeField] float hardnessIncreaseSpeed = 100;
    [SerializeField] float waitTimeMin;
    [SerializeField] float waitTimeMax;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            waitTimeMin = easyWaitTimeMin;
            waitTimeMax = easyWaitTimeMax;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
    }

    private void Update()
    {
        if(waitTimeMin >= hardWaitTimeMin)
        {
            waitTimeMin -= (hardnessIncreaseSpeed / 10000) * Time.deltaTime;
        }
        if(waitTimeMax >= hardWaitTimeMax)
        {
            waitTimeMax -= (hardnessIncreaseSpeed / 10000) * Time.deltaTime;
        }
    }

    IEnumerator Spawn()
    {
        float waitTime = 1f;
        yield return new WaitForSeconds(waitTime);

        while (!gameOver)
        {          
            SpawnObstacle();
            float gameSpeedFactor = GameManager.instance.gameSpeed / 100;
            waitTime = Random.Range(waitTimeMin / gameSpeedFactor, waitTimeMax / gameSpeedFactor);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnObstacle()
    {
        int rnd = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[rnd], transform.position, Quaternion.identity);
    }

}
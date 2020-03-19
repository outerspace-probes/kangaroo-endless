using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner instance;

    [SerializeField] float easyWaitTimeMin = 4f;
    [SerializeField] float easyWaitTimeMax = 6f;
    [SerializeField] float hardWaitTimeMin = 2.4f;
    [SerializeField] float hardWaitTimeMax = 2.6f;
    [SerializeField] float hardnessIncreaseSpeed = 100;
    [SerializeField] int koalaChanceOneTo = 3;
    [SerializeField] public int powerupChanceOneTo = 3;
    [SerializeField] float waitTimeMin;
    [SerializeField] float waitTimeMax;

    public GameObject koalaItem;
    public GameObject powerupItem;
    public GameObject[] obstacles;
    public bool gameOver = false;


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

            int spawnPwr = Random.Range(1, koalaChanceOneTo + 1);
            // print("koala rnd " + spawnPwr.ToString());
            if (spawnPwr == 1)
            {
                yield return new WaitForSeconds(1 / gameSpeedFactor);
                if(!gameOver) {
                    SpawnKoala();
                }
                
            }

            spawnPwr = Random.Range(1, powerupChanceOneTo + 1);
            // print("pwrup rnd " + spawnPwr.ToString());
            if (spawnPwr == 1)
            {
                yield return new WaitForSeconds(Random.Range(1.5f,3) / gameSpeedFactor);
                if(!gameOver) {
                    SpawnPowerup();
                }              
            }

            waitTime = Random.Range(waitTimeMin / gameSpeedFactor, waitTimeMax / gameSpeedFactor);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnPowerup()
    {
        Instantiate(powerupItem, transform.position, Quaternion.identity);
    }

    void SpawnKoala()
    {
        Instantiate(koalaItem, transform.position, Quaternion.identity);
    }

    void SpawnObstacle()
    {
        int rnd = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[rnd], transform.position, Quaternion.identity);
    }
}
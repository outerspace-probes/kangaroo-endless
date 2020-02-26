using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObjSpawner : MonoBehaviour
{
    public static BgObjSpawner instance;

    public BgObject[] bgObjects;
    public bool gameOver = false;
    [SerializeField] float waitTimeMin = 1f;
    [SerializeField] float waitTimeMax = 2.5f;
    [SerializeField] float moveSpeed = .7f;

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
            SpawnObject();
            float gameSpeedFactor = GameManager.instance.gameSpeed / 100;
            waitTime = Random.Range(waitTimeMin / gameSpeedFactor, waitTimeMax / gameSpeedFactor);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnObject()
    {
        int rnd = Random.Range(0, bgObjects.Length);
        BgObject obj = Instantiate(bgObjects[rnd], transform.position, Quaternion.identity);
        obj.SetSpeed(moveSpeed); 
    }
}

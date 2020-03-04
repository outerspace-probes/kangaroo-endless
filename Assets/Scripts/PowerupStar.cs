using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupStar : MonoBehaviour
{
    Rigidbody2D rbody;
    public bool gameOver = false;
    public GameObject exploParticles;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            rbody.velocity = new Vector2(-1 * GameManager.instance.gameSpeed / 25, 0);
        }
        else
        {
            rbody.velocity = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject explo = Instantiate(exploParticles, transform.position, Quaternion.identity);
            Destroy(explo, 2);
            Destroy(gameObject);
        }
    }
}

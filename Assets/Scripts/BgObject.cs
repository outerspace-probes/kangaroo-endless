using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObject : MonoBehaviour
{
    Rigidbody2D rbody;
    public bool gameOver = false;
    public float moveSpeed = 1;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            rbody.velocity = new Vector2(-1 * ((GameManager.instance.gameSpeed / 25) * moveSpeed), 0);
        }
        else
        {
            rbody.velocity = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -25f)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}

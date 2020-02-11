using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody2D rbody;
    [SerializeField] private float moveSpeed = 4;
    public bool gameOver = false;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!gameOver)
        {
            rbody.velocity = new Vector2(-1 * moveSpeed, 0);
        } else
        {
            rbody.velocity = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < - 15f)
        {
            Destroy(gameObject);
        }
    }
}
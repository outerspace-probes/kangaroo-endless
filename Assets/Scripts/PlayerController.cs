using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator animator;
    bool grounded;
    bool gameOver = false;
    [SerializeField] float jumpForce = 0;
    [SerializeField] float jumpForceMin = 4;
    [SerializeField] float jumpForceMax = 12;
    [SerializeField] float jumpForceLoadingSpeed = 5;
    [SerializeField] ProgressBar jumpForceProgressBar;
    [SerializeField] GameObject exploParticles;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpForce = jumpForceMin;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && Input.GetMouseButtonUp(0))
        {
            if(grounded)
            {
                Jump();
            }
            jumpForce = jumpForceMin;
            UpdateJumpForceProgressBar();
        }
        if (!gameOver && Input.GetMouseButton(0))
        {
            if (jumpForce <= jumpForceMax) {
                jumpForce += (jumpForceLoadingSpeed / 100) * Time.deltaTime;
                UpdateJumpForceProgressBar();
            }
        }

    }

    void UpdateJumpForceProgressBar()
    {
        float barValCurr = jumpForce - jumpForceMin;
        float barValMax = jumpForceMax - jumpForceMin;
        jumpForceProgressBar.SetProgressVal(barValCurr / barValMax);
    }

    void Jump()
    {
        grounded = false;
        rbody.velocity = new Vector2(0,1 * jumpForce);
        animator.Play("PlayerJump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            grounded = true;
            animator.Play("PlayerRun");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            // Destroy(collision.gameObject);
            GameObject explo = Instantiate(exploParticles, transform.position, Quaternion.identity);
            Destroy(explo, 2);
            Destroy(gameObject);
            // animator.enabled = false;
            GameManager.instance.GameOver();
            gameOver = true;
        }
        else if(collision.gameObject.tag == "obstacle-skip")
        {
            GameManager.instance.IncrementScore();
        }
    }
}

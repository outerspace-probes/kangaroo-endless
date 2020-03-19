using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koala : MonoBehaviour
{
    Rigidbody2D rbody;
    public bool gameOver = false;
    [SerializeField] GameObject exploParticles;
    [SerializeField] AudioClip itemSnd;
    [SerializeField] [Range(0, 1)] float itemSndVol = .7f;

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
            GameManager.instance.IncrementKoalas();
            GameObject explo = Instantiate(exploParticles, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(itemSnd, Camera.main.transform.position, itemSndVol);
            Destroy(explo, 2);
            Destroy(gameObject);
        }
    }
}

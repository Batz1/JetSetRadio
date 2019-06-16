using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyScript : MonoBehaviour
{
    Rigidbody2D playerRb;
    Rigidbody2D rb;

    public float velocityLimit;

    public Vector2 goTo;
    public float pushForce;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameManager.instance.currentPlayer.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            goTo = transform.position - collision.transform.position;
            HandleHit();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerProjectile"))
        {
            gameObject.SetActive(false);
        }
    }


    private void HandleHit()
    {
        if ((playerRb.velocity.x >= velocityLimit) || (-playerRb.velocity.x <= -velocityLimit))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(goTo.normalized.x, 1) * pushForce;
        }
        else if ((playerRb.velocity.x < velocityLimit) && (-playerRb.velocity.x > -velocityLimit))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
        }
    }
}
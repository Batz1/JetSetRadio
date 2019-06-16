using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    Collider2D col;
    public string projectileTag;
    public string laserTag;
    public string eFloorTag;

    public Rigidbody2D rb;
    Vector2 goTo;

    public float pushForce;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(projectileTag) || collision.gameObject.CompareTag(laserTag) || collision.gameObject.CompareTag(eFloorTag))
        {
            GetHit(collision);
        }
    }

    public void GetHit(Collider2D collision)
    {
        Debug.Log("dead lmao");
        goTo = collision.transform.position - transform.position;
        rb.velocity = new Vector2(-goTo.normalized.x, 1) * pushForce;
        //rb.MoveRotation(30 * goTo.normalized.x);
        GameManager.instance.isPlayerAlive = false;
        col.enabled = false;
    }
}

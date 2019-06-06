using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [Header("Speed Variables")]
    public float speed;
    public float patrolSpeed;
    public float stoppingDistance;
    public float retreatDistance;

    private Transform target;
    public Vector3 newTargetPos;
    Vector2 boundries;

    [Header("Range Variables")]
    public float rangeXdist;
    public float rangeYdist;
    public Vector2 rangeX;
    public Vector2 rangeY;

    public bool inRange = false;

    bool moveRight = true;
    bool onGround = true;

    public float startTimeBtwShots;
    private float timeBtwShots;

    public GameObject projectile;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        target = GameManager.instance.currentPlayer.GetComponent<Transform>();
    }

    void Update()
    {
        GroundCheck();
        RangeCheck();

        if (inRange)
        {
            if (onGround)
            {
                MoveInCombat();
            }
            else
            {
                StopInCombat();
            }

            if (GameManager.instance.isPlayerAlive)
            {
                Shoot();
            }
        }
        else
        {
            if (onGround)
            {
                Patrol();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            boundries = new Vector2(collision.collider.bounds.min.x, collision.collider.bounds.max.x);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerProjectile"))
        {
            gameObject.SetActive(false);
        }
    }

    void MoveInCombat()
    {
        newTargetPos = target.position - transform.position;

        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            rb.velocity = new Vector2(newTargetPos.normalized.x * speed, rb.velocity.y);
        }
        else if (Vector2.Distance(transform.position, target.position) < stoppingDistance && Vector2.Distance(transform.position, target.position) > retreatDistance)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
        {
            rb.velocity = new Vector2(-newTargetPos.normalized.x * speed, rb.velocity.y);
        }
    }

    void StopInCombat()
    {
        float dir;

        if (transform.position.x > boundries.x + 0.1)
        {
            dir = -1;
            rb.velocity = new Vector2(0.25f * dir, rb.velocity.y);
        }
        else if (transform.position.x < boundries.y - 0.1)
        {
            dir = 1;
            rb.velocity = new Vector2(0.25f * dir, rb.velocity.y);
        }
    }

    void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            Vector2 v = new Vector2(transform.position.x, transform.position.y + 1);
            GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            Projectile pjScript = myProjectile.GetComponent<Projectile>();
            pjScript.parentEnemy = gameObject.transform;
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void Patrol()
    {
        if (moveRight)
        {
            rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
        }
    }

    void GroundCheck()
    {
        if(transform.position.x > boundries.x + 0.1 && transform.position.x < boundries.y - 0.1)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if(transform.position.x > boundries.y - 0.2 && moveRight)
        {
            moveRight = false;
        }
        else if(transform.position.x < boundries.x + 0.2 && !moveRight)
        {
            moveRight = true;
        }
    }

    void RangeCheck()
    {
        rangeX = new Vector2(transform.position.x - rangeXdist, transform.position.x + rangeXdist);
        rangeY = new Vector2(transform.position.y - (rangeYdist * 0.5f), transform.position.y + (rangeYdist * 1.5f));

        if(target.position.x >= rangeX.x && target.position.x <= rangeX.y && target.position.y >= rangeY.x && target.position.y <= rangeY.y)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
}
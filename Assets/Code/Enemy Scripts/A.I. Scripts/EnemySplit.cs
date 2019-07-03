using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Enemy stand on one position
 * When the player is near the enemy the enemy will start to move towards the player (move on the x axis)
 * When the enemy is close enough to the player the enemy will explodes and then split in two (instantiate)
 * If the player jumps one enemy will follow him and the other will stay in position
 * If the player is getting far the enemy rigidbody body type will turn back to static 
 */

public class EnemySplit : MonoBehaviour
{
    public float speed;
    public float range;

    [SerializeField] GameObject enemyGO;
   GameObject enemySpawn;

    private Transform target;
    public Vector3 newTargetPos;
    Vector2 boundries;

    public bool inRange = false;
    bool isCreated = false;

    Rigidbody2D myRB;

    public Animator grandmaAnimator;
    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.instance.currentPlayer.GetComponent<Transform>();
    }
     void Update()
    {
        RangeCheck();
        
        if (inRange)
        {
                MoveInCombat();
            grandmaAnimator.SetBool("Walk", true);
        }
        //Invoke("SplitEnemy",1f);
    }

    void RangeCheck()
    {
        if (Vector3.Distance(transform.position, target.position) < range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

    void MoveInCombat()
    {
        newTargetPos = target.position - transform.position;
        myRB.velocity = new Vector2(newTargetPos.normalized.x * speed, myRB.velocity.y);
    }

    public void SplitEnemy()
    {
        if (Vector3.Distance(transform.position, target.position) < range)
        {
            if (!isCreated)
            {
                grandmaAnimator.SetTrigger("Split");
                isCreated = true;
                enemySpawn = Instantiate(enemyGO, transform.position, Quaternion.identity);
                enemySpawn.transform.Translate(Vector2.up * Time.deltaTime);
            }
        }

        if (isCreated)
        {
            CancelInvoke("SplitEnemy");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
                boundries = new Vector2(collision.collider.bounds.min.x, collision.collider.bounds.max.x);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}